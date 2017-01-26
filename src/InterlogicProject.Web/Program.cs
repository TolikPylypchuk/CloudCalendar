using System.IO;

using Microsoft.AspNetCore.Hosting;

namespace InterlogicProject.Web
{
	public class Program
	{
		public static string EmailDomain { get; set; }
		public static string DefaultPassword { get; set; }

		public static void Main(string[] args)
		{
			var host = new WebHostBuilder()
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseIISIntegration()
				.UseStartup<Startup>()
				.Build();

			host.Run();
		}
	}
}
