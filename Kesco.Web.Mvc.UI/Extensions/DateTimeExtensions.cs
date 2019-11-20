using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Kesco.Web.Mvc
{
	public static class DateTimeExtensions
	{
		public static DateTime FromUtcToClient(this DateTime date)
		{
			var userContext = HttpContext.Current.Session.GetUserContext();
			return date.AddMinutes(-userContext.ClientTimeZoneOffset);
		}

		public static DateTime FromLocalToClient(this DateTime date)
		{
			var userContext = HttpContext.Current.Session.GetUserContext();
			return date.ToUniversalTime().AddMinutes(-userContext.ClientTimeZoneOffset);
		}
	}
}
