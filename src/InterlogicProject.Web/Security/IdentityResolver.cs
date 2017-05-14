using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace InterlogicProject.Web.Security
{
	public class IdentityResolver
	{
		public Task<ClaimsIdentity> Resolve(string username, string password)
		{
			if (username == "TEST" && password == "TEST123")
			{
				return Task.FromResult(new ClaimsIdentity(
					new GenericIdentity(username, "Token"),
					new Claim[] { }));
			}

			return Task.FromResult<ClaimsIdentity>(null);
		}
	}
}
