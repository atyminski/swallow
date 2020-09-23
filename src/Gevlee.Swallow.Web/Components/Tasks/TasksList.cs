using Gevlee.Swallow.Web.Model;
using Gevlee.Swallow.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gevlee.Swallow.Web.Components.Tasks
{
	public partial class TasksList : ComponentBase
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

		private async Task Add()
		{
			await TasksService.AddTaskAsync(Date, new TaskViewModel
			{
				Name = _currentTaskName
			});
			_currentTaskName = null;
			await Refresh();
		}

		private async void OnKeyUp(KeyboardEventArgs args)
		{
			if(args.Code == "Enter")
			{
				await Add();
			}
		}

		public void Delete(int id)
		{
			//TasksService.DeleteTask(Date, id);
			//Refresh();
		}

		private async Task Refresh()
		{
			_tasks = (await TasksService.GetTasksAsync(DateTime.Now.Date)).ToList();
			StateHasChanged();
		}

		protected override async void OnInitialized()
		{
			await Refresh();
		}
	}
}
