using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.Web.Security
{
	public class IdentityResolver
	{
		public IdentityResolver(
			UserManager<User> manager,
			UserClaimsPrincipalFactory<User, IdentityRole> factory)
		{
			this.UserManager = manager;
			this.Factory = factory;
		}

		private UserManager<User> UserManager { get; }
		private UserClaimsPrincipalFactory<User, IdentityRole> Factory { get; }

		public async Task<ClaimsIdentity> Resolve(
			string username,
			string password)
		{
			var user = await this.UserManager.FindByNameAsync(username);

			if (user != null &&
				await this.UserManager.CheckPasswordAsync(user, password))
			{
				return new ClaimsIdentity(
					new GenericIdentity(username, "Token"),
					(await this.Factory.CreateAsync(user)).Claims);
			}

			return null;
		}
	}
}
