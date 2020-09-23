using Gevlee.Swallow.Web.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gevlee.Swallow.Web.Services
{
	public interface ITasksService
	{
		Task AddTaskAsync(DateTime date, TaskViewModel model);
		Task<IEnumerable<TaskViewModel>> GetTasksAsync(DateTime dateTime);
	}
}