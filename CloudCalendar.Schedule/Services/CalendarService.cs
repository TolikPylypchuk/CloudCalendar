using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Microsoft.Extensions.Options;

using CloudCalendar.Data.Repositories;
using CloudCalendar.Data.Models;
using CloudCalendar.Schedule.Services.Options;
using CloudCalendar.Schedule.Utilities;

using static CloudCalendar.Schedule.Utilities.ScheduleUtilities;

using CalendarClass = CloudCalendar.Data.Models.Class;
using ScheduleClass = CloudCalendar.Schedule.Models.Class;

namespace CloudCalendar.Schedule.Services
{
	public class CalendarService : ICalendarService
	{
		public CalendarService(
			IRepository<Classroom> classrooms,
			IRepository<Group> groups,
			IRepository<Lecturer> lecturers,
			IRepository<Subject> subjects,
			IOptionsSnapshot<ScheduleOptions> options)
		{
			this.Classrooms = classrooms;
			this.Groups = groups;
			this.Lecturers = lecturers;
			this.Subjects = subjects;
			this.Options = options.Value;
		}

		public IRepository<Classroom> Classrooms { get; }
		public IRepository<Group> Groups { get; }
		public IRepository<Lecturer> Lecturers { get; }
		public IRepository<Subject> Subjects { get; }
		public ScheduleOptions Options { get; }
		
		public IList<CalendarClass> CreateCalendar(
			IList<ScheduleClass> schedule,
			DateTime start,
			DateTime end)
		{
			var mondayStart = start - TimeSpan.FromDays((int)start.DayOfWeek - 1);
			var fridayEnd = end + TimeSpan.FromDays(5 - (int)end.DayOfWeek);

			var result = new List<CalendarClass>();

			var groupedSchedule =
				schedule.GroupBy(c => GetDayOfWeek(c.DayOfWeek))
						.OrderBy(g => g.Key)
						.ToList();

			for (var (date, numerator) = (mondayStart, true);
				 date <= fridayEnd;
				 date += TimeSpan.FromDays(7),
				 numerator = !numerator)
			{
				foreach (var group in groupedSchedule)
				{
					foreach (var scheduleClass in group)
					{
						var frequency = GetFrequency(scheduleClass.Frequency);

						if (frequency == ClassFrequency.Weekly ||
							(frequency == ClassFrequency.Numerator) == numerator)
						{
							var day = GetDayOfWeek(scheduleClass.DayOfWeek);
							var time = TimeSpan.ParseExact(
								this.Options.ClassStarts[scheduleClass.Number - 1],
								"g", // The hour:minute format specifier
								CultureInfo.InvariantCulture);

							var calendarClass = new CalendarClass
							{
								DateTime = date.AddDays((int)day - 1) + time,
								SubjectId = GetSubjectId(
									scheduleClass.Subject.Name,
									this.Subjects),
								Type = scheduleClass.Type
							};

							this.AddClassInfo(calendarClass, scheduleClass);

							result.Add(calendarClass);
						}
					}
				}
			}
			
			return result.Where(c => c.DateTime >= start)
						 .Where(c => c.DateTime <= end)
						 .ToList();
		}

		private void AddClassInfo(
			CalendarClass calendarClass,
			ScheduleClass scheduleClass)
		{
			var places = GetPlaces(
				calendarClass,
				scheduleClass.Classrooms,
				this.Classrooms);

			var groups = GetGroups(
				calendarClass,
				scheduleClass.Groups,
				this.Groups);

			var lecturers = GetLecturers(
				calendarClass,
				scheduleClass.Lecturers,
				this.Lecturers);

			calendarClass.Places = places;
			calendarClass.Groups = groups;
			calendarClass.Lecturers = lecturers;
		}
	}
}
