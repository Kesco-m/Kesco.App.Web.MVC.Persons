using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Kesco.Web.Mvc.UI
{
	/// <summary>
	/// Класс-помощник реализует HTML построение элемента управления
	/// <see cref="KescoButtonBar"/>
	/// </summary>
	internal class KescoTreeViewHtmlBuilder
	{
		/// <summary>
		/// Gets the control.
		/// </summary>
		public KescoTreeView Control { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="KescoButtonBarHtmlBuilder"/> class.
		/// </summary>
		/// <param name="control">The control.</param>
		public KescoTreeViewHtmlBuilder(KescoTreeView control)
		{
			Control = control; 
		}

		/// <summary>
		/// Создаёт div HTML-тег, контайнер для кнопок.
		/// </summary>
		/// <returns>Строка, содержащая HTML-тег.</returns>
		public string DivTag()
		{
			TagBuilder tag = new TagBuilder("DIV");
			tag.Attributes.Add("id", Control.ID);
			tag.MergeAttributes(Control.HtmlAttributes, true);
			return tag.ToString(TagRenderMode.SelfClosing);
		}

	}
}
