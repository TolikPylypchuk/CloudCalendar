using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using InterlogicProject.DAL;
using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;

namespace InterlogicProject
{
	public class Startup
	{
		private IConfigurationRoot configuration;

		public Startup(IHostingEnvironment env)
		{
			this.configuration = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json").Build();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(
				options =>
					options.UseSqlServer(
						this.configuration[
							"ConnectionStrings:DefaultConnection"]));
			services.AddTransient<IRepository<Account>, AccountRepository>();
			services.AddTransient<IRepository<Department>, DepartmentRepository>();
			services.AddTransient<IRepository<Faculty>, FacultyRepository>();
			services.AddTransient<IRepository<Group>, GroupRepository>();
			services.AddTransient<IRepository<Lecturer>, LecturerRepository>();
			services.AddTransient<IRepository<Student>, StudentRepository>();
			services.AddMvc();
		}

		public void Configure(
			IApplicationBuilder app,
			IHostingEnvironment env,
			ILoggerFactory loggerFactory)
		{
			app.UseDeveloperExceptionPage();
			app.UseStatusCodePages();
			app.UseStaticFiles();
			app.UseMvcWithDefaultRoute();
		}
	}
}
