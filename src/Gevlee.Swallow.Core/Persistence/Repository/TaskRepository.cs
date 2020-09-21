using LiteDB;
using Gevlee.Swallow.Core.Entities;

namespace Gevlee.Swallow.Core.Persistence.Repository
{
	public class TaskRepository : ITaskRepository
	{
		private readonly ILiteDatabase db;

		public TaskRepository(ILiteDatabase db)
		{
			this.db = db;
		}

		public Task Insert(Task task)
		{
			db.GetCollection<Task>(LiteDbConfig.CollectionsNames.Tasks).Insert(task);
			return task;
		}
	}
}
