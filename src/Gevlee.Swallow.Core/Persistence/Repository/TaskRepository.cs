using LiteDB;
using Gevlee.Swallow.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Gevlee.Swallow.Core.Persistence.Repository
{
	public class TaskRepository : GenericLiteDbRepository<Task>, ITaskRepository
	{
		public TaskRepository(ILiteDatabase db) : base(db, LiteDbConfig.CollectionsNames.Tasks)
		{
		}

		public IEnumerable<Task> FindByQuery(TaskFindQueryModel taskFindQuery)
		{
			return Collection.Find(x => x.Date.Date == taskFindQuery.Date.Date).ToList();
		}
	}
}
