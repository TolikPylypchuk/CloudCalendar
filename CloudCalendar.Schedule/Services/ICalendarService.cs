using System;
using System.Collections.Generic;

using CalendarClass = CloudCalendar.Data.Models.Class;
using ScheduleClass = CloudCalendar.Schedule.Models.Class;

namespace CloudCalendar.Schedule.Services
{
	public interface ICalendarService
	{
		IList<CalendarClass> CreateCalendar(
			IList<ScheduleClass> schedule,
			DateTime start,
			DateTime end);
	}
}
