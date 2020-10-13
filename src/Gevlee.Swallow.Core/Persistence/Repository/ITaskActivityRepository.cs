using Gevlee.Swallow.Core.Entities;
using System.Collections.Generic;

namespace Gevlee.Swallow.Core.Persistence.Repository
{
	public interface ITaskActivityRepository : IGenericRepository<TaskActivity>
	{
		TaskActivity FindActive();
		TaskActivity FindActive(int taskId);
		IEnumerable<TaskActivity> FindByTaskId(int taskId);
	}
}
