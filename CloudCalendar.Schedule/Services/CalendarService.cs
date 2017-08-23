using System;
using System.Collections.Generic;
using System.Linq;

using CloudCalendar.Data.Repositories;
using CloudCalendar.Data.Models;
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
			IRepository<Subject> subjects)
		{
			this.Classrooms = classrooms;
			this.Groups = groups;
			this.Lecturers = lecturers;
			this.Subjects = subjects;
		}

		public IRepository<Classroom> Classrooms { get; }
		public IRepository<Group> Groups { get; }
		public IRepository<Lecturer> Lecturers { get; }
		public IRepository<Subject> Subjects { get; }

		private Dictionary<
			int,
			(IList<ClassPlace>, IList<GroupClass>, IList<LecturerClass>)> cache =
			new Dictionary<
				int,
				(IList<ClassPlace>, IList<GroupClass>, IList<LecturerClass>)>();

		public IList<CalendarClass> CreateCalendar(
			IList<ScheduleClass> schedule,
			DateTime start,
			DateTime end)
		{
			var mondayStart = start - TimeSpan.FromDays((int)start.DayOfWeek - 1);
			var fridayEnd = end + TimeSpan.FromDays(7 - (int)end.DayOfWeek);

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

							var calendarClass = new CalendarClass
							{
								DateTime = date.AddDays((int)day - 1),
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
			
			return result;
		}

		private void AddClassInfo(
			CalendarClass calendarClass,
			ScheduleClass scheduleClass)
		{
			if (!this.cache.ContainsKey(scheduleClass.Id))
			{
				(calendarClass.Places,
				 calendarClass.Groups,
				 calendarClass.Lecturers) =
					this.cache[scheduleClass.Id];
			} else
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

				this.cache.Add(
					scheduleClass.Id,
					(places, groups, lecturers));
			}
		}
	}
}
