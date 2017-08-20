using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;

using CloudCalendar.Web.Models.ViewModels;

namespace CloudCalendar.Web.Security
{
	public class TokenProviderMiddleware
	{
		public TokenProviderMiddleware(
			RequestDelegate next,
			TokenProviderOptions options)
		{
			this.Next = next;
			this.Options = options;
			
			ThrowIfInvalidOptions(this.Options);

			this.Settings = new JsonSerializerSettings
			{
				Formatting = Formatting.Indented
			};
		}

		private RequestDelegate Next { get; }
		private TokenProviderOptions Options { get; }
		private JsonSerializerSettings Settings { get; }

		public Task Invoke(HttpContext context)
		{
			if (!context.Request.Path.Equals(
				this.Options.Path, StringComparison.Ordinal))
			{
				return this.Next(context);
			}

			if (!context.Request.Method.Equals("POST"))
			{
				context.Response.StatusCode = 405;
				return context.Response.WriteAsync("405 Method Not Allowed");
			}

			if (context.Request.ContentType != "application/json")
			{
				context.Response.StatusCode = 406;
				return context.Response.WriteAsync("406 Not Acceptable");
			}
			
			return this.GenerateToken(context);
		}

		private async Task GenerateToken(HttpContext context)
		{
			string body;

			using (var reader = new StreamReader(context.Request.Body))
			{
				body = reader.ReadToEnd();
			}

			var loginModel = JsonConvert.DeserializeObject<LoginModel>(body);

			if (String.IsNullOrEmpty(loginModel.Username) ||
				String.IsNullOrEmpty(loginModel.Password))
			{
				context.Response.StatusCode = 400;
				await context.Response.WriteAsync("400 Bad Request");
				return;
			}

			var identity = await this.Options.IdentityResolver.Resolve(
					loginModel.Username, loginModel.Password);

			if (identity == null)
			{
				context.Response.StatusCode = 400;
				await context.Response.WriteAsync("400 Bad Request");
				return;
			}

			var now = DateTime.UtcNow;

			var claims = new List<Claim>
			{
				new Claim(
					JwtRegisteredClaimNames.Sub,
					loginModel.Username),
				new Claim(
					JwtRegisteredClaimNames.Jti,
					await this.Options.NonceGenerator()),
				new Claim(
					JwtRegisteredClaimNames.Iat,
					new DateTimeOffset(now).ToUniversalTime()
						.ToUnixTimeSeconds().ToString(),
					ClaimValueTypes.Integer64),
				identity.Claims.FirstOrDefault(
					claim => claim.Type == ClaimTypes.Role)
			};


			var jwt = new JwtSecurityToken(
				issuer: this.Options.Issuer,
				audience: this.Options.Audience,
				claims: claims,
				notBefore: now,
				expires: now.Add(this.Options.Expiration),
				signingCredentials: this.Options.SigningCredentials);

			var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

			var response = new
			{
				token = encodedJwt,
				expiresIn = (int)this.Options.Expiration.TotalSeconds
			};

			context.Response.ContentType = "application/json";
			await context.Response.WriteAsync(
				JsonConvert.SerializeObject(response, this.Settings));
		}
		
		private static void ThrowIfInvalidOptions(TokenProviderOptions options)
		{
			if (String.IsNullOrEmpty(options.Path))
			{
				throw new ArgumentNullException(
					nameof(TokenProviderOptions.Path));
			}

			if (String.IsNullOrEmpty(options.Issuer))
			{
				throw new ArgumentNullException(
					nameof(TokenProviderOptions.Issuer));
			}

			if (String.IsNullOrEmpty(options.Audience))
			{
				throw new ArgumentNullException(
					nameof(TokenProviderOptions.Audience));
			}

			if (options.Expiration == TimeSpan.Zero)
			{
				throw new ArgumentException(
					"The expiration must be non-zero.",
					nameof(TokenProviderOptions.Expiration));
			}

			if (options.IdentityResolver == null)
			{
				throw new ArgumentNullException(
					nameof(TokenProviderOptions.IdentityResolver));
			}

			if (options.SigningCredentials == null)
			{
				throw new ArgumentNullException(
					nameof(TokenProviderOptions.SigningCredentials));
			}

			if (options.NonceGenerator == null)
			{
				throw new ArgumentNullException(
					nameof(TokenProviderOptions.NonceGenerator));
			}
		}
	}
}
