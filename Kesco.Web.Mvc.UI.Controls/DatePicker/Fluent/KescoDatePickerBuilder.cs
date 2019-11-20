using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Kesco.Web.Mvc.UI.Fluent
{

	/// <summary>
	/// Класс, реализующий построитель для элемента управления KescoDatePicker.
	/// </summary>
	public class KescoDatePickerBuilder : ControlBuilderBase<KescoDatePicker, KescoDatePickerBuilder>
	{

		public KescoDatePickerBuilder(KescoDatePicker control) : base(control) { }


		/// <summary>
		/// Устанавливает количество месяцев, отображаемых
		/// в элементе управления
		/// </summary>
		/// <param name="monthToShow">Количество месяцев</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoDatePickerBuilder MonthsToShow(int monthToShow)
		{
			if (monthToShow > 0 && monthToShow < 13) 
				this.control.MonthsToShow = monthToShow;
			return this;
		}

		/// <summary>
		/// Устанавливает значение даты в элементе управления
		/// в элементе управления
		/// </summary>
		/// <param name="date">Значение даты</param>
		/// <returns>
		/// Построитель элемента управления
		/// </returns>
		public KescoDatePickerBuilder SetDate(DateTime? date)
		{
			this.control.Value = date;
			return this;
		}

		/// <summary>
		/// Устанавливает показывать ли дни из других месяцев
		/// в элементе управления
		/// </summary>
		/// <param name="enabled">показывать ли дни из других месяцев в элементе управления</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoDatePickerBuilder AllowToShowOtherMonths(bool enabled)
		{
			this.control.ShowOtherMonths = enabled;
			return this;
		}

		/// <summary>
		/// Устанавливает показывать ли дни из других месяцев
		/// в элементе управления
		/// </summary>
		/// <param name="enabled">показывать ли дни из других месяцев в элементе управления</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoDatePickerBuilder AllowToShowOtherMonths()
		{
			return AllowToShowOtherMonths(true);
		}

		/// <summary>
		/// Устанавливает можно ли выбирать дни из других месяцев
		/// в элементе управления
		/// </summary>
		/// <param name="enabled">можно ли выбирать дни из других месяцев в элементе управления</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoDatePickerBuilder AllowToSelectOtherMonths(bool enabled)
		{
			this.control.SelectOtherMonths = enabled;
			return this;
		}

		/// <summary>
		/// Устанавливает показывать ли дни из других месяцев
		/// в элементе управления
		/// </summary>
		/// <param name="showOtherMonths">показывать ли дни из других месяцев в элементе управления</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoDatePickerBuilder AllowToSelectOtherMonths()
		{
			return AllowToSelectOtherMonths(true);
		}

		/// <summary>
		/// Устанавливает показать ли месяцы как выпадащий список
		/// в элементе управления
		/// </summary>
		/// <param name="enabled">Показать ли месяцы как выпадащий список</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoDatePickerBuilder ShowMonthDropDown(bool enabled)
		{
			this.control.ShowMonthDropDown = enabled;
			return this;
		}

		/// <summary>
		/// Устанавливает показ месяцев как выпадащий список
		/// в элементе управления
		/// </summary>
		/// <returns>Построитель элемента управления</returns>
		public KescoDatePickerBuilder ShowMonthDropDown()
		{
			return ShowMonthDropDown(true);
		}

		/// <summary>
		/// Устанавливает показать ли года как выпадащий список
		/// в элементе управления
		/// </summary>
		/// <param name="enabled">Показать ли года как выпадащий список</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoDatePickerBuilder ShowYearDropDown(bool enabled)
		{
			this.control.ShowYearDropDown = enabled;
			return this;
		}

		/// <summary>
		/// Устанавливает показ выпадащий списка с годами
		/// в элементе управления
		/// </summary>
		/// <returns>Построитель элемента управления</returns>
		public KescoDatePickerBuilder ShowYearDropDown()
		{
			return ShowYearDropDown(true);
		}

		/// <summary>
		/// устанавливает минимальную дату, которую пользователь может выбрать
		/// в элементе управления
		/// </summary>
		/// <param name="date">Минимальная дата</param>
		/// <returns>
		/// Построитель элемента управления
		/// </returns>
		public KescoDatePickerBuilder MinDate(DateTime minDate)
		{
			this.control.MinDate = minDate;
			return this;
		}

		/// <summary>
		/// Устанавливает максимальную дату, которую пользователь может выбрать
		/// в элементе управления
		/// </summary>
		/// <param name="maxDate">Максимальная дата</param>
		/// <returns>
		/// Построитель элемента управления
		/// </returns>
		public KescoDatePickerBuilder MaxDate(DateTime maxDate)
		{
			this.control.MaxDate = maxDate;
			return this;
		}

		/// <summary>
		/// Устанавливает культуру/язык для
		/// </summary>
		/// <param name="name">Имя языка и региональных параметров.</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoDatePickerBuilder Culture(string name)
		{
			this.control.Culture = CultureInfo.GetCultureInfo(name);
			return this;
		}
	}
}
