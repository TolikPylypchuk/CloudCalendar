using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InterlogicProject.ScheduleClient.Services
{
	[ExcludeFromCodeCoverage]
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddSchedule(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddScoped(provider => new HttpClient());
			services.AddScoped<IScheduleSource, ScheduleHttpClient>();
			services.AddScoped<ICalendarService, CalendarService>();
			services.Configure<ScheduleHttpClientOptions>(configuration);

			return services;
		}

		public static IServiceCollection AddSchedule(
			this IServiceCollection services,
			Action<ScheduleHttpClientOptions> action)
		{
			services.AddScoped(provider => new HttpClient());
			services.AddScoped<IScheduleSource, ScheduleHttpClient>();
			services.AddScoped<ICalendarService, CalendarService>();
			services.Configure(action);

			return services;
		}
	}
}
