using Gevlee.Swallow.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gevlee.Swallow.Web.Services
{
	public class InMemoryTasksService : ITasksService
	{
		public InMemoryTasksService()
		{

		}

		private ICollection<TaskViewModel> _tasks = new List<TaskViewModel>
		{
			new TaskViewModel
			{
				Id = 1,
				Name = "Task 1"
			},
			new TaskViewModel
			{
				Id = 2,
				Name = "Task 2"
			},
			new TaskViewModel
			{
				Id = 3,
				Name = "Task 3"
			}
		};

		public void AddTask(DateTime date, TaskViewModel model)
		{
			model.Id = _tasks.Count + 1;
			_tasks.Add(model);
		}

		public void DeleteTask(DateTime date, int id)
		{
			_tasks.Remove(_tasks.First(x => x.Id == id));
		}

		public IEnumerable<TaskViewModel> GetTasks(DateTime dateTime)
		{
			return _tasks;
		}
	}
}
