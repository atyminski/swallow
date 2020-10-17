using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Gevlee.Swallow.Web.Components.Common
{
	public partial class DaySwitch
	{
		[Parameter]
		public DateTime Current
		{
			get; set;
		} = DateTime.Now.Date;

		[Parameter]
		public EventCallback<DateTime> CurrentChanged
		{
			get; set;
		}

		private Task NextDay()
		{
			Current = Current.AddDays(1);
			return CurrentChanged.InvokeAsync(Current);
		}

		private Task PreviousDay()
		{
			Current = Current.AddDays(-1);
			return CurrentChanged.InvokeAsync(Current);
		}
	}
}