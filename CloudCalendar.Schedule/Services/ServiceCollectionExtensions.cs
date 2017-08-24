using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using CloudCalendar.Schedule.Services.Options;

namespace CloudCalendar.Schedule.Services
{
	[ExcludeFromCodeCoverage]
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddSchedule(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddScoped(provider => new HttpClient());
			services.AddScoped<ICalendarService, CalendarService>();
			services.Configure<ScheduleOptions>(configuration);

			return services;
		}

		public static IServiceCollection AddSchedule(
			this IServiceCollection services,
			Action<ScheduleOptions> action)
		{
			services.AddScoped(provider => new HttpClient());
			services.AddScoped<ICalendarService, CalendarService>();
			services.Configure(action);

			return services;
		}

		public static IServiceCollection AddScheduleClient(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddScoped<IScheduleSource, ScheduleHttpClient>();
			services.Configure<ScheduleHttpClientOptions>(configuration);

			return services;
		}

		public static IServiceCollection AddScheduleClient(
			this IServiceCollection services,
			Action<ScheduleHttpClientOptions> action)
		{
			services.AddScoped<IScheduleSource, ScheduleHttpClient>();
			services.Configure(action);

			return services;
		}
	}
}
