using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using CloudCalendar.Data.Models;
using CloudCalendar.Web.Services;

namespace CloudCalendar.Web.Infrastructure
{
	public class CustomUserValidator : UserValidator<User>
	{
		private Settings settings;

		public CustomUserValidator(IOptionsSnapshot<Settings> options)
		{
			this.settings = options.Value;
		}

		public override async Task<IdentityResult> ValidateAsync(
			UserManager<User> manager,
			User user)
		{
			var result = await base.ValidateAsync(manager, user);

			var errors = result.Succeeded
				? new List<IdentityError>()
				: result.Errors.ToList();

			if (!user.Email.ToLower().EndsWith($"@{this.settings.EmailDomain}"))
			{
				errors.Add(new IdentityError
				{
					Code = "EmailDomainError",
					Description =
						$"Дозволено тільки адреси @{this.settings.EmailDomain}"
				});
			}

			return errors.Count == 0
				? IdentityResult.Success
				: IdentityResult.Failed(errors.ToArray());
		}
	}
}
