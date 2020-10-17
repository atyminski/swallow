using Gevlee.Swallow.Api.Contract.Tasks;
using Gevlee.Swallow.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Gevlee.Swallow.Web.Services
{
	public class ApiTaskService : ITasksService
	{
		private readonly HttpClient client;

		public ApiTaskService(HttpClient client)
		{
			this.client = client;
		}

		public async Task AddTaskAsync(DateTime date, TaskViewModel model)
		{
			var response = await client.PostAsync("tasks", JsonContent.Create(new TaskModel
			{
				Name = model.Name,
				Date = date
			}));
			response.EnsureSuccessStatusCode();
		}

		public async Task StartTaskAsync(int taskId)
		{
			var response = await client.PostAsync($"tasks/{taskId}/start", null);
			response.EnsureSuccessStatusCode();
		}

		public async Task PauseTaskAsync(int taskId)
		{
			var response = await client.PostAsync($"tasks/{taskId}/stop", null);
			response.EnsureSuccessStatusCode();
		}

		public async Task<IEnumerable<TaskViewModel>> GetTasksAsync(DateTime dateTime)
		{
			var response = await client.GetAsync($"tasks?date={dateTime:yyyy-MM-dd}");
			response.EnsureSuccessStatusCode();
			return (await response.Content.ReadFromJsonAsync<IEnumerable<TaskModel>>()).Select(x => new TaskViewModel
			{
				Id = x.Id,
				Name = x.Name,
				IsActive = x.IsActive,
				ActiveSince = x.ActiveSince,
				ElapsedTime = TimeSpan.FromSeconds(x.ElapsedSeconds),
				TotalElapsedTime = TimeSpan.FromSeconds(x.ElapsedSeconds)
			});
		}

		public async Task<TaskViewModel> GetTaskAsync(int taskId)
		{
			var response = await client.GetAsync($"tasks/{taskId}");
			response.EnsureSuccessStatusCode();
			var responseObj = await response.Content.ReadFromJsonAsync<TaskModel>();
			return new TaskViewModel
			{
				Id = responseObj.Id,
				Name = responseObj.Name,
				IsActive = responseObj.IsActive,
				ActiveSince = responseObj.ActiveSince,
				ElapsedTime = TimeSpan.FromSeconds(responseObj.ElapsedSeconds),
				TotalElapsedTime = TimeSpan.FromSeconds(responseObj.ElapsedSeconds)
			};
		}
	}
}
