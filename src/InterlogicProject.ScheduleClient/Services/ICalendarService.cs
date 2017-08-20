using System;
using System.Collections.Generic;

using CalendarClass = InterlogicProject.DAL.Models.Class;
using ScheduleClass = InterlogicProject.ScheduleClient.Models.Class;

namespace InterlogicProject.ScheduleClient.Services
{
	public interface ICalendarService
	{
		IList<CalendarClass> CreateCalendar(
			IList<ScheduleClass> schedule,
			DateTime start,
			DateTime end);
	}
}
