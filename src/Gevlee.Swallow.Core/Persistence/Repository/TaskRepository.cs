using LiteDB;
using Gevlee.Swallow.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Gevlee.Swallow.Core.Persistence.Repository
{
	public class TaskRepository : ITaskRepository
	{
		private readonly ILiteDatabase db;

		private ILiteCollection<Task> Collection => db.GetCollection<Task>(LiteDbConfig.CollectionsNames.Tasks);

		public TaskRepository(ILiteDatabase db)
		{
			this.db = db;
		}

		public bool Delete(int id)
		{
			return Collection.Delete(id);
		}

		public IEnumerable<Task> FindByQuery(TaskFindQueryModel taskFindQuery)
		{
			return Collection.Find(x => x.Date.Date == taskFindQuery.Date.Date).ToList();
		}

		public int Insert(Task task)
		{
			return Collection.Insert(task);
		}

		public bool Update(Task task)
		{
			return Collection.Update(task);
		}

		public Task Get(int id)
		{
			return Collection.FindById(id);
		}
	}
}
