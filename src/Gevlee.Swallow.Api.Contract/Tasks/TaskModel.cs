using System;

namespace Gevlee.Swallow.Api.Contract.Tasks
{
	public class TaskModel
	{
		public int Id
		{
			get; set;
		}

		public string Name
		{
			get; set;
		}

		public DateTime Date
		{
			get; set;
		}

		public bool IsActive
		{
			get; set;
		}

		public DateTime? ActiveSince
		{
			get; set;
		}

		public double ElapsedSeconds
		{
			get; set;
		}
	}
}
