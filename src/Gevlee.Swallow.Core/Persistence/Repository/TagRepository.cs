using Gevlee.Swallow.Core.Entities;
using LiteDB;
using static Gevlee.Swallow.Core.Persistence.LiteDbConfig;

namespace Gevlee.Swallow.Core.Persistence.Repository
{
    public class TagRepository : GenericLiteDbRepository<Tag>, ITagRespository
    {
        public TagRepository(ILiteDatabase db) : base(db, CollectionsNames.Tags)
        {
        }
    }
}
