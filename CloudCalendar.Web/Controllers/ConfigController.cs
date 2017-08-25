using Microsoft.AspNetCore.Mvc;

using CloudCalendar.Schedule.Services.Options;
using CloudCalendar.Web.Services;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CloudCalendar.Web.Controllers
{
	/// <summary>
	/// An API for the application configuration.
	/// </summary>
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class ConfigController : Controller
	{
		private Settings settings;
		private ScheduleOptions scheduleOptions;

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="ConfigController"/> class.
		/// </summary>
		/// <param name="settings">
		/// The application settings that this instance will use.
		/// </param>
		/// <param name="scheduleOptions">
		/// The schedule options that this instance will use.
		/// </param>
		public ConfigController(
			IOptionsSnapshot<Settings> settings,
			IOptionsSnapshot<ScheduleOptions> scheduleOptions)
		{
			this.settings = settings.Value;
			this.scheduleOptions = scheduleOptions.Value;
		}
		
		/// <summary>
		/// Gets the email domain of the users of this application.
		/// </summary>
		/// <returns>
		/// The email domain of the users of this application.
		/// </returns>
		[HttpGet("email-domain")]
		[Produces("text/plain")]
		[SwaggerResponse(200, Type = typeof(string))]
		public string GetEmailDomain()
			=> this.settings.EmailDomain;

		/// <summary>
		/// Gets the schedule options of this application.
		/// </summary>
		/// <returns>The schedule options of this application.</returns>
		[HttpGet("schedule")]
		[SwaggerResponse(200, Type = typeof(ScheduleOptions))]
		public ScheduleOptions GetScheduleOptions()
			=> this.scheduleOptions;
	}
}
