using System;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Tokens;

namespace InterlogicProject.Web.Security
{
	public class TokenProviderOptions
	{
		public string Path { get; set; } = "/api/token";
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(5);
		public SigningCredentials SigningCredentials { get; set; }
		public Func<string, string, Task<ClaimsIdentity>> IdentityResolver { get; set; }
	}
}
