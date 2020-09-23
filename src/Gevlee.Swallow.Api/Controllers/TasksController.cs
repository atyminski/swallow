using Gevlee.Swallow.Api.Contract.Tasks;
using Gevlee.Swallow.Api.Extensions.Mappers;
using Gevlee.Swallow.Core.Persistence.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Gevlee.Swallow.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TasksController : ControllerBase
	{
		private readonly ITaskRepository taskRepository;

		public TasksController(ITaskRepository taskRepository)
		{
			this.taskRepository = taskRepository;
		}

		[HttpGet("{id}")]
		public ActionResult<TaskModel> GetById(int id)
		{
			var task = taskRepository.Get(id);
			if (task != null)
			{
				return task.ToModel();
			}
			else
			{
				return NotFound();
			}
		}

		[HttpGet]
		public ActionResult<IEnumerable<TaskModel>> GetByQuery([FromQuery]TasksQuery query)
		{
			var tasks = taskRepository.FindByQuery(new TaskFindQueryModel
			{
				Date = query.Date
			});

			if (tasks != null)
			{
				return tasks.Select(x => x.ToModel()).ToList();
			}
			else
			{
				return new List<TaskModel>(0);
			}
		}

		[HttpPost]
		public IActionResult Create([FromBody]TaskModel task)
		{
			var entity = task.ToEntity();
			entity.Id = 0;
			var id = taskRepository.Insert(entity);
			return new CreatedResult(Url.Action(nameof(TasksController.GetById), new { id }), entity.ToModel());
		}


	}
}
