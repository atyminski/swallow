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
		public IActionResult Create([FromBody]Task task)
		{
			taskRepository.Insert(task);
			return new OkResult();
		}
	}
}
