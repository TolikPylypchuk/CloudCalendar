using System.Collections.Generic;
using System.Threading.Tasks;

using CloudCalendar.Schedule.Models;

namespace CloudCalendar.Schedule.Services
{
	public interface IScheduleSource
	{
		Task<IList<Class>> GetScheduleAsync(int year, int semester);
	}
}
