using System;

namespace Gevlee.Swallow.Web.Model
{
	public class TaskViewModel
	{
		public int Id
		{
			get; set;
		}

		public string Name
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

		public TimeSpan ElapsedTime
		{
			get; set;
		}

		public TimeSpan TotalElapsedTime
		{
			get; set;
		}
	}
}
