using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Kesco.Web.Mvc.UI
{
	/// <summary>
	/// Класс-помощник реализует построение HTML кода для элемента управления
	/// <see cref="KescoDatePicker"/>
	/// </summary>
	internal class KescoDatePickerHtmlBuilder
	{
		/// <summary>
		/// Возвращает элемент управления.
		/// </summary>
		public KescoDatePicker Control { get; private set; }

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="KescoDatePickerHtmlBuilder"/> класса.
		/// </summary>
		/// <param name="control">The control.</param>
		public KescoDatePickerHtmlBuilder(KescoDatePicker control)
		{
			Control = control; 
		}

		/// <summary>
		/// Создаёт div HTML-тег, контайнер для кнопок.
		/// </summary>
		/// <returns>Строка, содержащая HTML-тег.</returns>
		internal string InputTag()
		{
			TagBuilder tag = new TagBuilder("INPUT");
			tag.Attributes.Add("type", "text");
			tag.Attributes.Add("id", Control.ID);
			tag.Attributes.Add("name", Control.Name);
			tag.Attributes.Add("size", "10");
			tag.Attributes.Add("maxlength", "10");
			if (Control.Value.HasValue)
				tag.Attributes.Add("value", Control.Value.Value.ToLocalTime().ToShortDateString());
			return tag.ToString(TagRenderMode.SelfClosing);
		}

	}
}
