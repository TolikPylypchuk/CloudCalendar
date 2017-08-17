﻿using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InterlogicProject.ScheduleClient.Services
{
	public static class Extensions
	{
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
