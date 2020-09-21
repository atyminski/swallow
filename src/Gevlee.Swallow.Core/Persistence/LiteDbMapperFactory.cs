using Gevlee.Swallow.Core.Entities;
using LiteDB;

namespace Gevlee.Swallow.Core.Persistence
{
	public class LiteDbConfig
	{
		public static BsonMapper Mapper
		{
			get
			{
				var mapper = new BsonMapper();
				mapper.UseLowerCaseDelimiter();
				RegisterMappings(mapper);
				return mapper;
			}
		}

		private static void RegisterMappings(BsonMapper mapper)
		{
			mapper.Entity<TaskActivity>()
				.DbRef(x => x.Task, CollectionsNames.Tasks);
		}

		public static class CollectionsNames
		{
			public static string Tasks = "tasks";
			public static string TasksActivities = "activities";

		}
	}
}
