using Gevlee.Swallow.Web.Model;
using Gevlee.Swallow.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gevlee.Swallow.Web.Components.Tasks
{
	public partial class TasksList
	{
		private ICollection<TaskViewModel> _tasks = new List<TaskViewModel>();

		private string _currentTaskName;

		[Parameter]
		public DateTime Date
		{
			get; set;
		} = DateTime.Now;

		[Inject]
		protected ITasksService TasksService { get; set; }

		public bool ReadOnly
		{
			get; set;
		}

		private void Add()
		{
			TasksService.AddTask(Date, new TaskViewModel
			{
				Name = _currentTaskName
			});
			_currentTaskName = null;
			Refresh();
		}

		private void OnKeyUp(KeyboardEventArgs args)
		{
			if(args.Code == "Enter")
			{
				Add();
			}
		}

		public void Delete(int id)
		{
			TasksService.DeleteTask(Date, id);
			Refresh();
		}

		private void Refresh()
		{
			_tasks = TasksService.GetTasks(DateTime.Now.Date).ToList();
		}

		protected override void OnInitialized()
		{
			Refresh();
		}
	}
}
