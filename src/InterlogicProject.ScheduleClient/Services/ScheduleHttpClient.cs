using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

using Newtonsoft.Json;

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
			var client = new HttpClient();

			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));
			client.DefaultRequestHeaders.Add(
				"User-Agent", "Interlogic Project");

			return client.GetStreamAsync(
				this.GetScheduleUri(year, semester));
		}

		private Uri GetScheduleUri(int year, int semester)
		{
			return new Uri(
				new Uri($"{this.Options.Value.Schema}://{this.Options.Value.Host}" +
					$":{this.Options.Value.Port}"),
				String.Format(this.Options.Value.PathFormat, year, semester));
		}
	}
}
