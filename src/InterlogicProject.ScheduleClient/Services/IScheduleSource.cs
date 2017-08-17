using System.Collections.Generic;
using System.Threading.Tasks;

using InterlogicProject.ScheduleClient.Models;

namespace InterlogicProject.ScheduleClient.Services
{
	public interface IScheduleSource
	{
		Task<IList<Class>> GetScheduleAsync(int year, int semester);
	}
}
