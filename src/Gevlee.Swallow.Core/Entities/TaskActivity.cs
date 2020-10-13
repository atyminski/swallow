using System;

namespace Gevlee.Swallow.Core.Entities
{
    public class TaskActivity
    {
		public int Id
		{
			get; set;
		}

		public Task Task
		{
			get; set;
		}

		public DateTime StartTime
		{
			get; set;
		}

		public DateTime? EndTime
		{
			get; set;
		}
	}
}
