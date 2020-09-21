using Gevlee.Swallow.Core.Entities;

namespace Gevlee.Swallow.Core.Persistence.Repository
{
	public interface ITaskRepository
	{
		Task Insert(Task task);
	}
}