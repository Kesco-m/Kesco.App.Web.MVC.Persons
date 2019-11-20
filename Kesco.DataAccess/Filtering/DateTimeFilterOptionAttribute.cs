using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.DataAccess.Filtering
{
	public class DateTimeRange
	{
		public DateTime? StartDate { get; set; }
		public DateTime? FinishDate { get; set; }
	}

	/// <summary>
	/// Определяет класс-атрибут для фильтрации 
	/// по заданному промежутку времени либо по конкретному значению.
	/// </summary>
	public class DateTimeFilterOptionAttribute : FilterOptionAttribute
	{

		public string DateTimeFormat { get; set; }

		public DateTimeFilterOptionAttribute()
			: base()
		{

		}

		public override string BuildPredicate(FilterOptionAttributeContext context)
		{
			if (context.Value is DateTimeRange)
			{
				var state = context.Value as DateTimeRange;
				if (!state.StartDate.HasValue && !state.FinishDate.HasValue)
					return null;
				if (!state.FinishDate.HasValue)
					return String.Format("(T0.{0} >= '{1}')", FieldName, state.StartDate.Value.ToString(DateTimeFormat));
				if (!state.StartDate.HasValue)
					return String.Format("(T0.{0} <= '{1}')", FieldName, state.FinishDate.Value.ToString(DateTimeFormat));
				return String.Format("(T0.{0} between '{1}' and '{2}')", FieldName, state.StartDate.Value.ToString(DateTimeFormat), state.FinishDate.Value.ToString(DateTimeFormat));				
			}

			if (context.Value is DateTime && (DateTime)context.Value != default(DateTime))
			{
				var state = (DateTime)context.Value;
				return String.Format("(T0.{0} = '{1}')", FieldName, state.ToString(DateTimeFormat));
			}
			return null;
		}

	}

}
