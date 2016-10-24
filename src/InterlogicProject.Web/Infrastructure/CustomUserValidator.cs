using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.Infrastructure
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

			if (!user.Email.ToLower().EndsWith("@example.com"))
			{
				errors.Add(new IdentityError
				{
					Code = "EmailDomainError",
					Description =
						"Only example.com email addresses are allowed"
				});
			}

			return errors.Count == 0
				? IdentityResult.Success
				: IdentityResult.Failed(errors.ToArray());
		}
	}
}
