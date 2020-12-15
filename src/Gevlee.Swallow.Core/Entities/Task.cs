using System;
using System.Collections.Generic;

namespace Gevlee.Swallow.Core.Entities
{
	public class Task
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

		public ICollection<Tag> Tags
		{
			get; set;
		} = new List<Tag>();
	}
}
