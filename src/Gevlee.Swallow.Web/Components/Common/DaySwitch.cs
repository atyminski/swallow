using Microsoft.AspNetCore.Components;
using System;

namespace Gevlee.Swallow.Web.Components.Common
{
	public partial class DaySwitch
	{
		[Parameter]
		public DateTime Current
		{
			get; set;
		} = DateTime.Now.Date;

		private void NextDay()
		{
			Current = Current.AddDays(1);
		}

		private void PreviousDay()
		{
			Current = Current.AddDays(-1);
		}
	}
}