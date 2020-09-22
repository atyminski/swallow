using Gevlee.Swallow.Api.Contract.Tasks;
using Gevlee.Swallow.Core.Entities;
using Gevlee.Swallow.Core.Persistence.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Gevlee.Swallow.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TasksController
	{
		private readonly ITaskRepository taskRepository;

		public TasksController(ITaskRepository taskRepository)
		{
			this.taskRepository = taskRepository;
		}

		[HttpPost]
		public IActionResult Create([FromBody]TaskModel task)
		{
			taskRepository.Insert(new Task
			{
				Name = task.Name,
				Date = task.Date
			});
			return new OkResult();
		}
	}
}
