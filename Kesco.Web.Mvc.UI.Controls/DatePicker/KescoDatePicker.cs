using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.Script.Serialization;
using System.Globalization;

namespace Kesco.Web.Mvc.UI
{
	/// <summary>
	/// Реализует модель элемента управления DatePicker
	/// </summary>
	public class KescoDatePicker : ControlBase
	{
	
		/// <summary>
		/// Создаёт экземпляр элемента управления <see cref="KescoDatePicker"/>.
		/// </summary>
		/// <param name="viewContext">Представляет контекст представления <see cref="ViewContext"/> для элемента управления.</param>
		public KescoDatePicker(ViewContext viewContext)
			: base(viewContext)
		{
			Culture = CultureInfo.CurrentUICulture;
			ShowOtherMonths = true;
			SelectOtherMonths = true;
			MonthsToShow = 1;
			ShowMonthDropDown = true;
			ShowYearDropDown = true;
		}

		/// <summary>
		/// Возвращает или устанавливает дату для элемента управления
		/// </summary>
		/// <value>
		/// Текущая дата элемента управления
		/// </value>
		public DateTime? Value { get; set; }

		/// <summary>
		/// Возвращает или устанавливает минимальную дату, которую пользователь может выбрать
		/// </summary>
		/// <value>
		/// Минимальная дата элемента управления
		/// </value>
		public DateTime? MinDate { get; set; }

		/// <summary>
		/// Возвращает или устанавливает максимальную дату, которую пользователь может выбрать
		/// </summary>
		/// <value>
		/// Максимальная дата элемента управления
		/// </value>
		public DateTime? MaxDate { get; set; }

		/// <summary>
		/// Возвращает или устанавливает локализацию для элемента управления
		/// </summary>
		/// <value>
		/// Локализация элемента управления
		/// </value>
		public CultureInfo Culture { get; set; }

		/// <summary>
		/// Возвращает или устанавливает количество месяцев, отображаемых
		/// в элементе управления
		/// </summary>
		/// <value>
		/// количество месяцев, отображаемых
		/// в элементе управления
		/// </value>
		public int MonthsToShow { get; set; }

		/// <summary>
		/// Возвращает или устанавливает показывать список годов как выпадающий список
		/// </summary>
		/// <value>
		/// Boolean. Флаг показывать список годов как выпадающий список.
		/// </value>
		public bool ShowYearDropDown { get; set; }

		/// <summary>
		/// Возвращает или устанавливает показывать список месяцев как выпадающий список
		/// </summary>
		/// <value>
		/// Boolean. Флаг показывать список месяцев как выпадающий список.
		/// </value>
		public bool ShowMonthDropDown { get; set; }

		/// <summary>
		/// Возвращает или устанавливает нужно ли показывать дни из других месяцев
		/// </summary>
		/// <value>
		/// Boolean. Флаг показывать дни из других месяцев или нет.
		/// </value>
		public bool ShowOtherMonths { get; set; }

		/// <summary>
		/// Возвращает или устанавливает можно ли выбирать дату из другого месяца
		/// </summary>
		/// <value>
		/// Boolean. Флаг можно ли выбирать дату из другого месяца или нет.
		/// </value>
		public bool SelectOtherMonths { get; set; }

		/// <summary>
		/// Пишет HTML код, представляющий набор кнопок.
		/// </summary>
		/// <param name="writer">Экземпляр класса <see cref="HtmlTextWriter"/></param>
		protected override void WriteHtml(HtmlTextWriter writer)
		{
			KescoDatePickerHtmlBuilder builder = new KescoDatePickerHtmlBuilder(this);
			writer.Write(builder.InputTag());
			base.WriteHtml(writer);
		}

		/// <summary>
		/// Пишет скрипт инициализации элемента управления.
		/// </summary>
		/// <param name="writer">Экземпляр класса <see cref="TextWriter"/></param>
		public override void WriteInitializationScript(TextWriter writer)
		{
			base.WriteInitializationScript(writer);
			List<object> buttons = new List<object>();


			var options = new
			{
				changeYear = ShowYearDropDown,
				changeMonth = ShowMonthDropDown,
				showOtherMonths = ShowOtherMonths,
				selectOtherMonths = SelectOtherMonths,
				numberOfMonths = (MonthsToShow > 0 && MonthsToShow < 13) ? MonthsToShow:1,
				minDate = MinDate,
				maxDate = MaxDate
			};


			string jsonizedOptions = new JavaScriptSerializer().Serialize(options);

			writer.WriteLine(@"
					$(document).ready(function() {{
						var options = {1};
						$.extend(options, $.datepicker.regional[ '{2}' ] )
						$('#{0}').datepicker(options);
					}});
					"
                , ID, jsonizedOptions, Culture.IetfLanguageTag.Substring(0,2));

		}

	}
}
