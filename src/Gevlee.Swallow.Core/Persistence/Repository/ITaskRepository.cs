using Gevlee.Swallow.Core.Entities;
using System.Collections.Generic;

namespace Gevlee.Swallow.Core.Persistence.Repository
{
	public interface ITaskRepository : IGenericRepository<Task>
	{
		IEnumerable<Task> FindByQuery(TaskFindQueryModel taskFindQuery);
	}
}