namespace Gevlee.Swallow.Core.Persistence.Repository
{
	public interface IGenericRepository<TEntity>
	{
		bool Exists(int id);
		int Insert(TEntity entity);
		bool Delete(int id);
		bool Update(TEntity entity);
		TEntity Get(int id);
	}
}
