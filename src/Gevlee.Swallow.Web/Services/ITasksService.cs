using Gevlee.Swallow.Web.Model;
using System;
using System.Collections.Generic;

namespace Gevlee.Swallow.Web.Services
{
	public interface ITasksService
	{
		void AddTask(DateTime date, TaskViewModel model);
		void DeleteTask(DateTime date, int id);
		IEnumerable<TaskViewModel> GetTasks(DateTime dateTime);
	}
}