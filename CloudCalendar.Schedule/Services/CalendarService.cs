using System;
using System.Collections.Generic;

using CalendarClass = CloudCalendar.Data.Models.Class;
using ScheduleClass = CloudCalendar.Schedule.Models.Class;

namespace CloudCalendar.Schedule.Services
{
	public class CalendarService : ICalendarService
	{
		public IList<CalendarClass> CreateCalendar(
			IList<ScheduleClass> schedule,
			DateTime start,
			DateTime end)
		{
			throw new NotImplementedException();
		}
	}
}
