using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

using InterlogicProject.DAL.Models;

namespace InterlogicProject.Web.Security
{
	public class IdentityResolver
	{
		public IdentityResolver(UserManager<User> manager)
		{
			this.UserManager = manager;
		}

		private UserManager<User> UserManager { get; }

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
					new Claim[] { });
			}

			return null;
		}
	}
}
