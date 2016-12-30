using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.Web.Infrastructure
{
	public class CustomUserValidator : UserValidator<User>
	{
		public override async Task<IdentityResult> ValidateAsync(
			UserManager<User> manager,
			User user)
		{
			var result = await base.ValidateAsync(manager, user);

			var errors = result.Succeeded
				? new List<IdentityError>()
				: result.Errors.ToList();

			if (!user.Email.ToLower().EndsWith($"@{Program.EmailDomain}"))
			{
				errors.Add(new IdentityError
				{
					Code = "EmailDomainError",
					Description =
						$"Дозволено тільки адреси @{Program.EmailDomain}"
				});
			}

			return errors.Count == 0
				? IdentityResult.Success
				: IdentityResult.Failed(errors.ToArray());
		}
	}
}
