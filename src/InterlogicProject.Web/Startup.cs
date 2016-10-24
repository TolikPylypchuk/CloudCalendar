using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using InterlogicProject.DAL;
using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;
using InterlogicProject.Infrastructure;

using Swashbuckle.Swagger.Model;

namespace InterlogicProject
{
	/// <summary>
	/// This class is used to configure the application.
	/// </summary>
	public class Startup
	{
		private IConfigurationRoot configuration;

		/// <summary>
		/// Initializes a new instance of the Startup class.
		/// </summary>
		/// <param name="env">The hosting environment.</param>
		public Startup(IHostingEnvironment env)
		{
			this.configuration = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json").Build();
		}

		/// <summary>
		/// Adds and configures services used in this application.
		/// </summary>
		/// <param name="services">
		/// The collection of services used in this application.
		/// </param>
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<AppDbContext>(
				options =>
					options.UseSqlServer(
						this.configuration[
							"ConnectionStrings:DefaultConnection"]));

			services.AddIdentity<User, IdentityRole>(options => {
				options.User.RequireUniqueEmail = true;
				options.User.AllowedUserNameCharacters =
					"abcdefghijklmnopqrstuvwxyz" +
					"ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
					"1234567890" +
					"@.-!#$%&'*+-/=?^_`{|}~";

				options.Password.RequiredLength = 6;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireDigit = false;
			}).AddEntityFrameworkStores<AppDbContext>();

			services.AddTransient<IUserValidator<User>,
				CustomUserValidator>();

			services.AddTransient<IRepository<Department>,
				DepartmentRepository>();
			services.AddTransient<IRepository<Faculty>,
				FacultyRepository>();
			services.AddTransient<IRepository<Group>,
				GroupRepository>();
			services.AddTransient<IRepository<Lecturer>,
				LecturerRepository>();
			services.AddTransient<IRepository<Student>,
				StudentRepository>();

			services.AddRouting(options =>
			{
				options.LowercaseUrls = true;
			});

			services.AddMvc();

			string pathToDoc = this.configuration["Swagger:Path"];

			services.AddSwaggerGen(options =>
			{
				options.SingleApiVersion(new Info
				{
					Version = "v1",
					Title = "Interlogic Project API",
					Description = "A simple API for Interlogic Project",
					TermsOfService = "None"
				});
				options.IncludeXmlComments(pathToDoc);
				options.DescribeAllEnumsAsStrings();
			});
		}

		/// <summary>
		/// Configures this application.
		/// </summary>
		/// <param name="app">The builder of this application.</param>
		/// <param name="env">The hosting environment.</param>
		/// <param name="loggerFactory">The factory of loggers.</param>
		public void Configure(
			IApplicationBuilder app,
			IHostingEnvironment env,
			ILoggerFactory loggerFactory)
		{
			app.UseDeveloperExceptionPage();
			app.UseStatusCodePages();
			app.UseStaticFiles();
			app.UseIdentity();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});

			app.UseSwagger();
			app.UseSwaggerUi();
			
			DataInitializer.InitializeDatabaseAsync(
				app.ApplicationServices).Wait();
		}
	}
}
