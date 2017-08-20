using System;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Tokens;

namespace CloudCalendar.Web.Security
{
	public class TokenProviderOptions
	{
		public string Path { get; set; }
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public TimeSpan Expiration { get; set; }
		public SigningCredentials SigningCredentials { get; set; }
		public IdentityResolver IdentityResolver { get; set; }
		public Func<Task<string>> NonceGenerator { get; set; } =
			() => Task.FromResult(Guid.NewGuid().ToString());
	}
}
