using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using AutoMapper;

using Swashbuckle.AspNetCore.Swagger;

using CloudCalendar.Data;
using CloudCalendar.Data.Models;
using CloudCalendar.Data.Repositories;

using CloudCalendar.Schedule.Services;

using CloudCalendar.Web.Infrastructure;
using CloudCalendar.Web.Models.Dto;
using CloudCalendar.Web.Security;
using CloudCalendar.Web.Services;

namespace CloudCalendar.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			this.Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			#region Configuration
			
			var signingKey = new SymmetricSecurityKey(
				Encoding.ASCII.GetBytes(
					this.Configuration["Authentication:SecretKey"]));

			services.AddOptions();

			services.Configure<Settings>(
				this.Configuration.GetSection("Settings"));

			services.AddNodeServices(options =>
				options.InvocationTimeoutMilliseconds = 600_000);

			#endregion

			#region Schedule

			services.AddSchedule(
				this.Configuration.GetSection("Schedule"));

			services.AddScheduleClient(
				this.Configuration.GetSection("ScheduleSource"));

			#endregion

			#region EF Core, Identity and Authentication

			services.AddDbContextPool<AppDbContext>(
				options =>
					options.UseSqlServer(
						this.Configuration.GetConnectionString(
							"DefaultConnection")));

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

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme =
					JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme =
					JwtBearerDefaults.AuthenticationScheme;
				options.DefaultSignInScheme =
					JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters =
					new TokenValidationParameters
					{
						ClockSkew = TimeSpan.Zero,
						IssuerSigningKey = signingKey,
						ValidateIssuerSigningKey = true,
						ValidateIssuer = true,
						ValidIssuer = this.Configuration["Authentication:Issuer"],
						ValidateAudience = true,
						ValidAudience = this.Configuration["Authentication:Audience"],
						ValidateLifetime = false
					};
			});

			#endregion

			#region Repositories

			services.AddScoped<
				IRepository<Building>,
				BuildingRepository>();
			services.AddScoped<
				IRepository<Class>,
				ClassRepository>();
			services.AddScoped<
				IRepository<ClassPlace>,
				ClassPlaceRepository>();
			services.AddScoped<
				IRepository<Classroom>,
				ClassroomRepository>();
			services.AddScoped<
				IRepository<Comment>,
				CommentRepository>();
			services.AddScoped<
				IRepository<Department>,
				DepartmentRepository>();
			services.AddScoped<
				IRepository<Faculty>,
				FacultyRepository>();
			services.AddScoped<
				IRepository<Group>,
				GroupRepository>();
			services.AddScoped<
				IRepository<GroupClass>,
				GroupClassRepository>();
			services.AddScoped<
				IRepository<Homework>,
				HomeworkRepository>();
			services.AddScoped<
				IRepository<Lecturer>,
				LecturerRepository>();
			services.AddScoped<
				IRepository<LecturerClass>,
				LecturerClassRepository>();
			services.AddScoped<
				IRepository<Material>,
				MaterialRepository>();
			services.AddScoped<
				IRepository<UserNotification>,
				UserNotificationRepository>();
			services.AddScoped<
				IRepository<Notification>,
				NotificationRepository>();
			services.AddScoped<
				IRepository<Student>,
				StudentRepository>();
			services.AddScoped<
				IRepository<Subject>,
				SubjectRepository>();

			#endregion

			#region Singletons

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddSingleton<UserClaimsPrincipalFactory<User, IdentityRole>>();
			services.AddSingleton<IdentityResolver>();

			services.AddSingleton(
				serviceProvider => new TokenProviderOptions
				{
					Path = this.Configuration["Authentication:TokenPath"],
					Issuer = this.Configuration["Authentication:Issuer"],
					Audience = this.Configuration["Authentication:Audience"],
					Expiration = TimeSpan.FromDays(7),
					SigningCredentials = new SigningCredentials(
						signingKey, SecurityAlgorithms.HmacSha256),
					IdentityResolver = serviceProvider
						.GetService<IdentityResolver>()
				});

			#endregion

			#region MVC

			services.AddRouting(options =>
			{
				options.LowercaseUrls = true;
			});

			services.AddMvc(options =>
			{
				options.UseCommaDelimitedArrayModelBinding();
			});

			#endregion

			#region AutoMapper and Swagger

			Mapper.Initialize(config =>
			{
				config.CreateMap<Building, BuildingDto>();
				config.CreateMap<Class, ClassDto>();
				config.CreateMap<ClassPlace, ClassPlaceDto>();
				config.CreateMap<Classroom, ClassroomDto>();
				config.CreateMap<Comment, CommentDto>();
				config.CreateMap<Department, DepartmentDto>();
				config.CreateMap<Faculty, FacultyDto>();
				config.CreateMap<Group, GroupDto>();
				config.CreateMap<GroupClass, GroupClassDto>();
				config.CreateMap<Homework, HomeworkDto>();
				config.CreateMap<LecturerClass, LecturerClassDto>();
				config.CreateMap<Material, MaterialDto>();
				config.CreateMap<Subject, SubjectDto>();

				config.CreateMap<Lecturer, LecturerDto>()
					.ForMember(
						dest => dest.FirstName,
						opt => opt.MapFrom(src => src.User.FirstName))
					.ForMember(
						dest => dest.MiddleName,
						opt => opt.MapFrom(src => src.User.MiddleName))
					.ForMember(
						dest => dest.LastName,
						opt => opt.MapFrom(src => src.User.LastName))
					.ForMember(
						dest => dest.FullName,
						opt => opt.MapFrom(src => src.User.FullName))
					.ForMember(
						dest => dest.Email,
						opt => opt.MapFrom(src => src.User.Email));

				config.CreateMap<UserNotification, NotificationDto>()
					.ForMember(
						dest => dest.Text,
						opt => opt.MapFrom(src => src.Notification.Text))
					.ForMember(
						dest => dest.DateTime,
						opt => opt.MapFrom(src => src.Notification.DateTime))
					.ForMember(
						dest => dest.ClassId,
						opt => opt.MapFrom(src => src.Notification.ClassId));

				config.CreateMap<Student, StudentDto>()
					.ForMember(
						dest => dest.FirstName,
						opt => opt.MapFrom(src => src.User.FirstName))
					.ForMember(
						dest => dest.MiddleName,
						opt => opt.MapFrom(src => src.User.MiddleName))
					.ForMember(
						dest => dest.LastName,
						opt => opt.MapFrom(src => src.User.LastName))
					.ForMember(
						dest => dest.FullName,
						opt => opt.MapFrom(src => src.User.FullName))
					.ForMember(
						dest => dest.Email,
						opt => opt.MapFrom(src => src.User.Email));

				config.CreateMap<User, UserDto>()
					.ForMember(
						dest => dest.Roles,
						options => options.MapFrom(src => src.RoleNames));
			});

			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new Info
				{
					Version = "v1",
					Title = "Cloud Calendar API",
					Description = "An API for the Cloud Calendar",
					TermsOfService = "None"
				});

				options.IncludeXmlComments(this.Configuration["Swagger:Path"]);
				options.DescribeAllEnumsAsStrings();
				options.OperationFilter<
					AuthorizationHeaderParameterOperationFilter>();
			});

			#endregion
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseStatusCodePages();

				app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
				{
					HotModuleReplacement = true
				});
			} else
			{
				app.UseExceptionHandler("/home/error");
			}

			app.UseDefaultFiles();
			app.UseStaticFiles();

			app.UseAuthentication();
			app.UseMiddleware<TokenProviderMiddleware>();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");

				routes.MapSpaFallbackRoute(
					name: "spa-fallback",
					defaults: new { controller = "Home", action = "Index" });
			});
			
			app.UseSwagger(c =>
			{
				c.PreSerializeFilters.Add(
					(swagger, request) =>
					{
						swagger.Host = request.Host.Value;
						swagger.Schemes = new List<string> { "http" };
					});

			});

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
			});
		}
	}
}
