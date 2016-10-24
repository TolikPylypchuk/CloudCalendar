using System.IO;

using Microsoft.AspNetCore.Hosting;

namespace InterlogicProject
{
	/// <summary>
	/// Contains the entry point of the application.
	/// </summary>
	public class Program
	{
		/// <summary>
		/// The entry point of the application.
		/// </summary>
		/// <param name="args">
		/// Command-line arguments of this application.
		/// </param>
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
