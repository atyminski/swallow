using Gevlee.Swallow.Api.Contract.Tasks;
using Gevlee.Swallow.Server.Extensions.Mappers;
using Gevlee.Swallow.Core.Persistence.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gevlee.Swallow.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TasksController : ControllerBase
	{
		private readonly ITaskRepository taskRepository;
		private readonly ITaskActivityRepository taskActivityRepository;

		public TasksController(ITaskRepository taskRepository, ITaskActivityRepository taskActivityRepository)
		{
			this.taskRepository = taskRepository;
			this.taskActivityRepository = taskActivityRepository;
		}

		[HttpGet("{id}")]
		public ActionResult<TaskModel> GetById(int id)
		{
			var task = taskRepository.Get(id);
			if (task != null)
			{
				return task.ToModel(taskActivityRepository.FindByTaskId(id));
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
				return tasks.Select(x => x.ToModel(taskActivityRepository.FindByTaskId(x.Id))).ToList();
			}
			else
			{
				return new List<TaskModel>(0);
			}
		}

		[HttpGet("{taskId}/status")]
		public IActionResult GetActivity(int taskId)
		{
			if(!taskRepository.Exists(taskId))
			{
				return NotFound();
			}

			var activities = taskActivityRepository.FindByTaskId(taskId);
			var notEnded = activities.SingleOrDefault(x => !x.EndTime.HasValue);

			return Ok(new
			{
				Total = activities.Where(x => x.EndTime != null).Select(x => x.EndTime.Value - x.StartTime).Sum(x => x.TotalSeconds),
				IsActive = notEnded != null,
				ActiveFrom = notEnded?.StartTime
			});
		}

		[HttpPost("{taskId}/start")]
		public IActionResult StartTask(int taskId)
		{
			var task = taskRepository.Get(taskId);
			if (task == null)
			{
				return NotFound();
			}

			taskActivityRepository.Insert(new Core.Entities.TaskActivity
			{
				Task = task,
				StartTime = DateTime.UtcNow
			});

			return Ok();
		}

		[HttpPost("{taskId}/stop")]
		public IActionResult StopTask(int taskId)
		{
			if (!taskRepository.Exists(taskId))
			{
				return NotFound();
			}

			var startedActivity = taskActivityRepository.FindActive(taskId);
			startedActivity.EndTime = DateTime.UtcNow;

			taskActivityRepository.Update(startedActivity);

			return Ok();
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
