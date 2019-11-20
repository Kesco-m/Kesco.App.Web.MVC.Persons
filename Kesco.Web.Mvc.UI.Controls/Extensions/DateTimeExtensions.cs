using System;

namespace Kesco.Web.Mvc.UI
{
	internal static class DateTimeExtensions
	{
		public static string ToJsonUTC(this DateTime dateTime)
		{
			return string.Format("Date.UTC({0},{1},{2},{3},{4},{5},{6})", (object)dateTime.Year, (object)(dateTime.Month - 1), (object)dateTime.Day, (object)dateTime.Hour, (object)dateTime.Minute, (object)dateTime.Second, (object)dateTime.Millisecond);
		}
	}
}
