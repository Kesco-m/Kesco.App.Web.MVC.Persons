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
	public class KescoMenuHtmlBuilder
	{
		/// <summary>
		/// Gets the control.
		/// </summary>
		public KescoMenu Control { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="KescoButtonBarHtmlBuilder"/> class.
		/// </summary>
		/// <param name="control">The control.</param>
		public KescoMenuHtmlBuilder(KescoMenu control)
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
			tag.InnerHtml = MenuHtml(Control.Menu, true);
			return tag.ToString(TagRenderMode.Normal);
		}

		/// <summary>
		/// Создаёт UL HTML-тег главного меню.
		/// </summary>
		/// <returns>Строка, содержащая HTML-тег.</returns>
		public string MenuHtml(List<KescoMenuItem> menu, bool isRoot)
		{
			TagBuilder tag = new TagBuilder("UL");
			if (isRoot) tag.AddCssClass("memu");
			
			// Create menu items
			StringBuilder innerHtml = new StringBuilder();
			foreach (KescoMenuItem menuItem in menu) {
				innerHtml.AppendLine(MenuItemHtml(menuItem, isRoot));
			}

			tag.InnerHtml = innerHtml.ToString();
			return tag.ToString(TagRenderMode.Normal);
		}

		/// <summary>
		/// Создаёт UL HTML-тег главного меню.
		/// </summary>
		/// <returns>Строка, содержащая HTML-тег.</returns>
		public string MenuItemHtml(KescoMenuItem menuItem, bool isRoot)
		{
			TagBuilder tag = new TagBuilder("LI");
			if (isRoot) 
				tag.AddCssClass("memu-root");
			else if (menuItem.HasMenu)
				tag.AddCssClass("has-children");

			TagBuilder tagA = new TagBuilder("A");
			tagA.Attributes.Add("href", "javascript: void(0);");
			
			if (menuItem.IsCurrent) {
				tagA.AddCssClass("memu-current");
			}
			if (!String.IsNullOrEmpty(menuItem.CallbackFunc)) {
				tagA.Attributes.Add("onclick", menuItem.CallbackFunc);
			}

			if (!isRoot) {
				TagBuilder tagA_Div = new TagBuilder("DIV");
				if (String.IsNullOrEmpty(menuItem.PrimaryIcon)) {
					tagA_Div.AddCssClass("memu-icon");
				} else {
					tagA_Div.AddCssClass(menuItem.PrimaryIcon);
				}

				tagA.InnerHtml = tagA_Div.ToString(TagRenderMode.Normal);
			}

			tagA.InnerHtml += menuItem.Caption;
			tag.InnerHtml = tagA.ToString(TagRenderMode.Normal);
			if (menuItem.HasMenu)
				tag.InnerHtml += MenuHtml(menuItem.Menu, false);

			return tag.ToString(TagRenderMode.Normal);
		}

	}
}
