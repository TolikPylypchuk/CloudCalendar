using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using AutoMapper;

using Swashbuckle.Swagger.Model;

using InterlogicProject.DAL;
using InterlogicProject.DAL.Models;
using InterlogicProject.DAL.Repositories;
using InterlogicProject.Infrastructure;
using InterlogicProject.Models.Dto;

namespace InterlogicProject
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			this.Configuration = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json").Build();
		}

		public IConfigurationRoot Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			Program.EmailDomain = this.Configuration["EmailDomain"];

			services.AddDbContext<AppDbContext>(
				options =>
					options.UseSqlServer(
						this.Configuration[
							"ConnectionStrings:DefaultConnection"]),
				ServiceLifetime.Scoped);

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
			}).AddEntityFrameworkStores<AppDbContext>()
			  .AddUserValidator<CustomUserValidator>()
			  .AddErrorDescriber<CustomIdentityErrorDescriber>();
			
			services.AddScoped<IRepository<Department>,
				DepartmentRepository>();
			services.AddScoped<IRepository<Faculty>,
				FacultyRepository>();
			services.AddScoped<IRepository<Group>,
				GroupRepository>();
			services.AddScoped<IRepository<Lecturer>,
				LecturerRepository>();
			services.AddScoped<IRepository<Student>,
				StudentRepository>();
			services.AddScoped<IRepository<Subject>,
				SubjectRepository>();
			services.AddScoped<IRepository<Class>,
				ClassRepository>();
			services.AddScoped<IRepository<ClassPlace>,
				ClassPlaceRepository>();
			services.AddScoped<IRepository<LecturerClass>,
				LecturerClassRepository>();

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddRouting(options =>
			{
				options.LowercaseUrls = true;
			});

			services.AddMvc();

			Mapper.Initialize(config =>
			{
				config.CreateMap<User, UserDto>();
				config.CreateMap<Student, StudentDto>();
				config.CreateMap<Lecturer, LecturerDto>();
				config.CreateMap<Group, GroupDto>();
				config.CreateMap<Department, DepartmentDto>();
				config.CreateMap<Class, ClassDto>();
				config.CreateMap<ClassPlace, ClassPlaceDto>();
				config.CreateMap<LecturerClass, LecturerClassDto>();
			});
			
			services.AddSwaggerGen(options =>
			{
				options.SingleApiVersion(new Info
				{
					Version = "v1",
					Title = "Interlogic Project API",
					Description = "A simple API for the Interlogic project",
					TermsOfService = "None"
				});
				options.IncludeXmlComments(this.Configuration["Swagger:Path"]);
				options.DescribeAllEnumsAsStrings();
			});
		}

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
