using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Kesco.Web.Mvc;

namespace Kesco.Web.Mvc.UI
{
	/// <summary>
	/// Класс-помощник реализует HTML построение элемента управления
	/// <see cref="KescoSelect"/>
	/// </summary>
	internal class KescoSelectHtmlBuilder
	{
		/// <summary>
		/// Gets the control.
		/// </summary>
		public KescoSelect Control { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="KescoSelectHtmlBuilder"/> class.
		/// </summary>
		/// <param name="control">The control.</param>
		public KescoSelectHtmlBuilder(KescoSelect control)
		{
			Control = control; 
		}

		/// <summary>
		/// Создаёт текстовый HTML-тег для пользовательского ввода.
		/// </summary>
		/// <returns>Строка, содержащая HTML-тег.</returns>
		public string TextBoxTag()
		{
			TagBuilder tag = new TagBuilder("INPUT");
			tag.Attributes.Add("type", "text");
			tag.Attributes.Add("id", Control.ID);
			if (!String.IsNullOrEmpty(Control.Name))
				tag.Attributes.Add("name", Control.Name);
			tag.Attributes.Add("value", Control.Value ?? "");
			tag.Attributes.Add("data-value", Control.Value ?? "");
			tag.Attributes.Add("data-label", Control.DisplayValue ?? "");
			Control.HtmlAttributes.AddStyleAttribute("visibility", "hidden");
			Control.HtmlAttributes.AddStyleAttribute("width", "1px");
			Control.HtmlAttributes.AddStyleAttribute("border", "0px solid transparent");
			tag.MergeAttributes(Control.HtmlAttributes, false);
			return tag.ToString(TagRenderMode.SelfClosing);
		}

	}
}
