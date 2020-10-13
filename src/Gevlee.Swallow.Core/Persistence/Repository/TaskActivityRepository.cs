using Gevlee.Swallow.Core.Entities;
using LiteDB;
using System.Collections.Generic;

namespace Gevlee.Swallow.Core.Persistence.Repository
{
	public class TaskActivityRepository : GenericLiteDbRepository<TaskActivity>, ITaskActivityRepository
	{
		public TaskActivityRepository(ILiteDatabase db) : base(db, LiteDbConfig.CollectionsNames.TasksActivities)
		{
		}

		public IEnumerable<TaskActivity> FindByTaskId(int taskId)
		{
			return Collection.Find(x => x.Task.Id == taskId);
		}

		public TaskActivity FindActive()
		{
			return Collection.FindOne(x => x.EndTime == null);
		}

		public TaskActivity FindActive(int taskId)
		{
			return Collection.FindOne(x => x.Task.Id == taskId && x.EndTime == null);
		}
	}
}
