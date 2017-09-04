using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.SwaggerGen;

using CloudCalendar.Data.Models;
using CloudCalendar.Data.Repositories;
using CloudCalendar.Schedule.Services;
using CloudCalendar.Schedule.Services.Options;

using static CloudCalendar.Web.Infrastructure.DateUtilities;

namespace CloudCalendar.Web.Controllers
{
	/// <summary>
	/// An API for schedule.
	/// </summary>
	[Authorize(Roles = "Admin")]
	[Route("api/[controller]")]
	public class ScheduleController : Controller
	{
		private IRepository<Class> classes;
		private IScheduleSource scheduleSource;
		private ICalendarService calendarService;
		private ScheduleOptions options;

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="ScheduleController" /> class.
		/// </summary>
		/// <param name="classes">
		/// The repository of classes that this instance will use.
		/// </param>
		/// <param name="scheduleSource">
		/// The schedule source that this instance will use.
		/// </param>
		/// <param name="calendarService">
		/// The calendar service that this instance will use.
		/// </param>
		/// <param name="options">
		/// The shcedule options that this instance will use.
		/// </param>
		public ScheduleController(
			IRepository<Class> classes,
			IScheduleSource scheduleSource,
			ICalendarService calendarService,
			ScheduleOptions options)
		{
			this.classes = classes;
			this.scheduleSource = scheduleSource;
			this.calendarService = calendarService;
			this.options = options;
		}

		[HttpPost("create/range/{start}/{end}")]
		[SwaggerResponse(201)]
		public async Task<IActionResult> Create(
			DateTime start,
			DateTime end)
		{
			var (year, semester) = GetCurrentYearAndSemester(this.options);

			semester++;

			if (semester == this.options.Semesters.Count)
			{
				semester = 0;
				year++;
			}

			var schedule = await this.scheduleSource.GetScheduleAsync(
				year, semester + 1);

			var (semesterStart, semesterEnd) = GetSemesterBounds(
				GetYearStart(this.options, year),
				this.options.Semesters[semester]);

			if (start < semesterStart || end >= semesterEnd)
			{
				return this.BadRequest();
			}
			
			var calendar = this.calendarService.CreateCalendar(
				schedule, start, end);
			
			this.classes.AddRange(calendar);

			return this.Created(String.Empty, null);
		}
	}
}
