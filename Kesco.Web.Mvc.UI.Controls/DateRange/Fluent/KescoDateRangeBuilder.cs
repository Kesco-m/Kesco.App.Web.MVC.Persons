using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc.UI.Fluent
{

	/// <summary>
	/// Класс, реализующий построитель для элемента управления KescoDateRange.
	/// </summary>
	public class KescoDateRangeBuilder : ControlBuilderBase<KescoDateRange, KescoDateRangeBuilder>
	{

		public KescoDateRangeBuilder(KescoDateRange control) : base(control) { }


		/// <summary>
		/// Устанавливает показывать ли дни из других месяцев
		/// в элементе управления
		/// </summary>
		/// <param name="enabled">показывать ли дни из других месяцев в элементе управления</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoDateRangeBuilder SetDate(DateTime? date)
		{
			this.control.Value = date;
			return this;
		}

		public KescoDateRangeBuilder DatePickerFrom(Action<KescoDatePickerBuilder> datepickerAction)
		{
			datepickerAction(new KescoDatePickerBuilder(control.DatePickerFrom));
			return this;
		}

		public KescoDateRangeBuilder DatePickerTo(Action<KescoDatePickerBuilder> datepickerAction)
		{
			datepickerAction(new KescoDatePickerBuilder(control.DatePickerTo));
			return this;
		}
	}
}
