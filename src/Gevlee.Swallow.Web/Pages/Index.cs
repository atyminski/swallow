using Microsoft.AspNetCore.Components;
using System;

namespace Gevlee.Swallow.Web.Pages
{
	public partial class Index : ComponentBase
	{
		public DateTime Date
		{
			get; set;
		} = DateTime.Now;
	}
}
