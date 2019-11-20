using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Kesco.Web.Mvc.UI
{
	/// <summary>
	/// Класс-помощник реализует HTML построение элемента управления
	/// <see cref="KescoSelectTextBox"/>
	/// </summary>
	public class KescoSelectTextBoxHtmlBuilder
	{
		/// <summary>
		/// Gets the control.
		/// </summary>
		public KescoSelectTextBox Control { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="KescoSelectTextBoxHtmlBuilder"/> class.
		/// </summary>
		/// <param name="control">The control.</param>
		public KescoSelectTextBoxHtmlBuilder(KescoSelectTextBox control)
		{
			Control = control; 
		}

		/// <summary>
		/// Создаёт скрытый hidden HTML-тег, хранящий значение элемента.
		/// </summary>
		/// <returns>Строка, содержащая HTML-тег.</returns>
		public string HiddenTag()
		{
			TagBuilder tag = new TagBuilder("INPUT");
			tag.Attributes.Add("type", "hidden");
			tag.Attributes.Add("id", Control.ID);
			tag.Attributes.Add("name", Control.Name);
			tag.Attributes.Add("value", Control.Value);
			tag.Attributes.Add("readonly", "readonly");
			tag.Attributes.Add("style", "width: 25px; display2: none;");
			return tag.ToString(TagRenderMode.SelfClosing);
		}

		/// <summary>
		/// Создаёт текстовый HTML-тег для пользовательского ввода.
		/// </summary>
		/// <returns>Строка, содержащая HTML-тег.</returns>
		public string TextBoxTag()
		{
			TagBuilder tag = new TagBuilder("INPUT");
			tag.Attributes.Add("type", "text");
			tag.Attributes.Add("id", Control.ID + "___AUTOCOMPLETE");
			tag.Attributes.Add("value", Control.DisplayValue);
			tag.MergeAttributes(Control.HtmlAttributes, false);
			//tag.Attributes.Add("style", "width: 25px;");
			return tag.ToString(TagRenderMode.SelfClosing);
		}

		/// <summary>
		/// Создаёт текстовый HTML-тег для пользовательского ввода.
		/// </summary>
		/// <returns>Строка, содержащая HTML-тег.</returns>
		public string SearchButtonTag()
		{
			TagBuilder tag = new TagBuilder("INPUT");
			tag.Attributes.Add("type", "button");
			tag.Attributes.Add("id", Control.ID + "___SEARCH_BUTTON");
			tag.Attributes.Add("value", "...");
			tag.Attributes.Add("style", Control.SearchButtonCssStyle);
			return tag.ToString(TagRenderMode.SelfClosing);
		}

		/// <summary>
		/// Создаёт текстовый HTML-тег кнопки для просмотра деталей.
		/// </summary>
		/// <returns>Строка, содержащая HTML-тег.</returns>
		public string DetailsButtonTag()
		{
			if (Control.DetailsLink == null || String.IsNullOrWhiteSpace(Control.DetailsLink.Uri)) return String.Empty;
			TagBuilder tag = new TagBuilder("INPUT");
			tag.Attributes.Add("type", "button");
			tag.Attributes.Add("id", Control.ID + "___DETAILS_BUTTON");
			tag.Attributes.Add("value", Control.DetailsLink.Caption);
			tag.Attributes.Add("style", String.Format("{0}, display: none;", Control.DetailsLinkCssStyle));
			return tag.ToString(TagRenderMode.SelfClosing);
		}
	}
}
