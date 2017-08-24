using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using CloudCalendar.Schedule.Models;
using CloudCalendar.Schedule.Services.Options;

namespace CloudCalendar.Schedule.Services
{
	public class ScheduleHttpClient : IScheduleSource
	{
		public ScheduleHttpClient(
			HttpClient client,
			IOptionsSnapshot<ScheduleHttpClientOptions> options)
		{
			this.HttpClient = client;
			this.Options = options.Value;
		}

		private HttpClient HttpClient { get; }
		private ScheduleHttpClientOptions Options { get; }

		public async Task<IList<Class>> GetScheduleAsync(int year, int semester)
		{
			IList<Class> result;

			using (var stream = await this.GetStreamAsync(year, semester))
			using (var reader = new StreamReader(stream))
			using (var jsonReader = new JsonTextReader(reader))
			{
				var serializer = new JsonSerializer();
				result = serializer.Deserialize<List<Class>>(jsonReader);
			}

			return result;
		}

		private Task<Stream> GetStreamAsync(int year, int semester)
		{
			this.HttpClient.DefaultRequestHeaders.Accept.Clear();
			this.HttpClient.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));
			this.HttpClient.DefaultRequestHeaders.Add(
				"User-Agent", "Cloud Calendar");

			return this.HttpClient.GetStreamAsync(
				this.GetScheduleUri(year, semester));
		}

		private Uri GetScheduleUri(int year, int semester)
		{
			return new Uri(
				new Uri($"{this.Options.Schema}://{this.Options.Host}" +
					$":{this.Options.Port}"),
				String.Format(this.Options.PathFormat, year, semester));
		}
	}
}
