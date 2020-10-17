using Gevlee.Swallow.Web.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gevlee.Swallow.Web.Services
{
	public interface ITasksService
	{
		Task AddTaskAsync(DateTime date, TaskViewModel model);
		Task PauseTaskAsync(int taskId);
		Task StartTaskAsync(int taskId);
		Task<IEnumerable<TaskViewModel>> GetTasksAsync(DateTime dateTime);
		Task<TaskViewModel> GetTaskAsync(int taskId);
	}
}