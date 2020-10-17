using Gevlee.Swallow.Web.Model;
using Gevlee.Swallow.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Gevlee.Swallow.Web.Components.Tasks
{
	public partial class TasksList : ComponentBase, IDisposable
	{
		private List<TaskViewModel> _tasks = new List<TaskViewModel>();

		private string _currentTaskName;

		[Parameter]
		public DateTime Date 
		{ 
			get; set; 
		} = DateTime.Now;

		[Inject]
		protected ITasksService TasksService
		{
			get; set;
		}

		[Parameter]
		public bool ReadOnly
		{
			get; set;
		}

		private IDisposable CurrentTimerSubscription
		{
			get;
			set;
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
			if (args.Code == "Enter")
			{
				await Add();
			}
		}

		public void Delete(int id)
		{
			//TasksService.DeleteTask(Date, id);
			//Refresh();
		}

		public async Task Pause(int id)
		{
			await TasksService.PauseTaskAsync(id);
			var pausedTask = await TasksService.GetTaskAsync(id);
			_tasks[_tasks.FindIndex(x => x.Id == id)] = pausedTask;
			SetupTimer();
			StateHasChanged();
		}

		public async Task Start(int id)
		{
			var activeTaskId = _tasks.FirstOrDefault(x => x.IsActive)?.Id;
			if (activeTaskId.HasValue)
			{
				await Pause(activeTaskId.Value);
			}

			await TasksService.StartTaskAsync(id);
			var startedTask = await TasksService.GetTaskAsync(id);
			_tasks[_tasks.FindIndex(x => x.Id == id)] = startedTask;
			SetupTimer();
			StateHasChanged();
		}

		private async Task Refresh()
		{
			_tasks = (await TasksService.GetTasksAsync(Date)).ToList();
			SetupTimer();
			StateHasChanged();
		}

		//protected override async void OnInitialized()
		//{
		//	await Refresh();
		//}

		private void SetupTimer()
		{
			CurrentTimerSubscription?.Dispose();
			foreach (var task in _tasks)
			{
				if (task.IsActive)
				{
					UpdateTotalElapsedTime(task);
					CurrentTimerSubscription = Observable.Interval(TimeSpan.FromSeconds(.1)).Subscribe(_ => UpdateTotalElapsedTime(task));
				}
			}
		}

		public void UpdateTotalElapsedTime(TaskViewModel task)
		{
			task.TotalElapsedTime = task.ElapsedTime + (DateTime.UtcNow - task.ActiveSince.Value.ToUniversalTime());
			StateHasChanged();
		}

		protected override async Task OnParametersSetAsync()
		{
			await Refresh();
		}

		public void Dispose()
		{
			CurrentTimerSubscription?.Dispose();
		}
	}
}
