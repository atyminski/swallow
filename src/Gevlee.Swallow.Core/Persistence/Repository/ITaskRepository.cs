using Gevlee.Swallow.Core.Entities;
using System.Collections.Generic;

namespace Gevlee.Swallow.Core.Persistence.Repository
{
	public interface ITaskRepository
	{
		int Insert(Task task);
		bool Delete(int id);
		bool Update(Task task);
		IEnumerable<Task> FindByQuery(TaskFindQueryModel taskFindQuery);
		Task Get(int id);
	}
}