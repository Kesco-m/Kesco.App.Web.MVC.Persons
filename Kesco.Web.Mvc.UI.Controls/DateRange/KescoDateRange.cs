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
	public class KescoDateRange : ControlBase
	{

		public KescoDatePicker DatePickerFrom { get; protected set; }
		public KescoDatePicker DatePickerTo { get; protected set; }

		/// <summary>
		/// Создаёт экземпляр элемента управления <see cref="KescoDateRange"/>.
		/// </summary>
		/// <param name="viewContext">Представляет контекст представления <see cref="ViewContext"/> для элемента управления.</param>
		public KescoDateRange(ViewContext viewContext)
			: base(viewContext)
		{
			Culture = CultureInfo.CurrentUICulture;
			DatePickerFrom = new KescoDatePicker(viewContext);
			DatePickerTo = new KescoDatePicker(viewContext);
		}

		/// <summary>
		/// Возвращает или устанавливает локализацию для элемента управления
		/// </summary>
		/// <value>
		/// Локализация элемента управления
		/// </value>
		public DateTime? Value { get; set; }

		/// <summary>
		/// Возвращает или устанавливает локализацию для элемента управления
		/// </summary>
		/// <value>
		/// Локализация элемента управления
		/// </value>
		public CultureInfo Culture { get; set; }

		public string LayoutCssClass { get; set; }
		public string LayoutCssStyle { get; set; }


		/// <summary>
		/// Пишет HTML код, представляющий таблицу со списком выбора
		/// , двумя элементами управления выбора дат и кнопками быстрой навигации.
		/// </summary>
		/// <param name="writer">Экземпляр класса <see cref="HtmlTextWriter"/></param>
		protected override void WriteHtml(HtmlTextWriter writer)
		{
			KescoDateRangeHtmlBuilder builder = new KescoDateRangeHtmlBuilder(this);
			writer.Write(builder.TableTag());
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
			};


			string jsonizedOptions = new JavaScriptSerializer().Serialize(options);

			writer.WriteLine(@"
					$(document).ready(function() {{
						KescoInitDateRangeControl('#{0}', '#{2}', '#{3}');
					}});
					"
				, ID, jsonizedOptions, DatePickerFrom.ID, DatePickerTo.ID);

		}

	}
}
