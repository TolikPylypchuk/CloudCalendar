using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

using InterlogicProject.ScheduleClient.Models;

namespace InterlogicProject.ScheduleClient.Services
{
	public class ScheduleHttpClient : IScheduleSource
	{
		public ScheduleHttpClient(
			IOptionsSnapshot<ScheduleHttpClientOptions> options)
		{
			this.Options = options;
		}

		private IOptionsSnapshot<ScheduleHttpClientOptions> Options { get; }

		public Task<IList<Class>> GetScheduleAsync(int year, int semester)
		{
			throw new NotImplementedException();
		}

		private Uri GetScheduleURL(int year, int semester)
		{
			return new Uri(
				new Uri($"{this.Options.Value.Schema}://{this.Options.Value.Host}" +
					$":{this.Options.Value.Port}"),
				String.Format(this.Options.Value.PathFormat, year, semester));
		}
	}
}
