using LiteDB;
using System;

namespace Gevlee.Swallow.Core.Persistence.Repository
{
	public abstract class GenericLiteDbRepository<TEntity> : IGenericRepository<TEntity>
	{
		private readonly string collectionName;

		protected ILiteCollection<TEntity> Collection => Db.GetCollection<TEntity>(collectionName);

		protected ILiteDatabase Db
		{
			get;
		}

		protected GenericLiteDbRepository(ILiteDatabase db, string collectionName)
		{
			Db = db;
			this.collectionName = collectionName;
		}

		public bool Delete(int id)
		{
			return Collection.Delete(id);
		}

		public int Insert(TEntity task)
		{
			return Collection.Insert(task);
		}

		public bool Update(TEntity task)
		{
			return Collection.Update(task);
		}

		public TEntity Get(int id)
		{
			return Collection.FindById(id);
		}

		public bool Exists(int id)
		{
			return Get(id) != null; //TODO: Optimize
		}
	}
}
