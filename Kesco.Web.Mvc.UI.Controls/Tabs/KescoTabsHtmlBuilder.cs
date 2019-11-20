using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Kesco.Web.Mvc.UI
{
	/// <summary>
	/// Класс-помощник реализует HTML построение элемента управления
	/// <see cref="KescoTabs"/>
	/// </summary>
	public class KescoTabsHtmlBuilder
	{
		/// <summary>
		/// Возвращает элемент управления <see cref="KescoTabs"/>.
		/// </summary>
		public KescoTabs Control { get; private set; }

		/// <summary>
		/// Инициализирует экземпляр <see cref="KescoTabsHtmlBuilder"/> класса.
		/// </summary>
		/// <param name="control">The control.</param>
		public KescoTabsHtmlBuilder(KescoTabs control)
		{
			Control = control; 
		}

		/// <summary>
		/// Создаёт div HTML-тег, контайнер для кнопок.
		/// </summary>
		/// <returns>Строка, содержащая HTML-тег.</returns>
		public string ContainerTag()
		{
			TagBuilder tag = new TagBuilder("DIV");
			tag.Attributes.Add("id", Control.ID);
			tag.Attributes.Add("name", Control.Name);
			if (Control.AutoResize) {
				tag.Attributes.Add("style", "overflow: hidden");
			}
			tag.InnerHtml += CreateLinks();
			tag.InnerHtml += CreateTabs();
			return tag.ToString(TagRenderMode.Normal);
		}

		/// <summary>
		/// Создаёт UL HTML-тег со списком ссылок на закладки.
		/// </summary>
		/// <returns>Строка, содержащая UL-тег со списком ссылок на закладки.</returns>
		protected virtual string CreateLinks()
		{
			TagBuilder tag = new TagBuilder("UL");
			foreach(KescoTabItem tab in Control.Tabs) {
				TagBuilder li = new TagBuilder("LI");
				TagBuilder a = new TagBuilder("A");
				TagBuilder span = new TagBuilder("SPAN");
				a.Attributes.Add("href", tab.Uri);
				span.InnerHtml = tab.Caption;
				a.InnerHtml = span.ToString(TagRenderMode.Normal);
				li.InnerHtml = a.ToString(TagRenderMode.Normal);
				tag.InnerHtml += li.ToString(TagRenderMode.Normal);
			}
			return tag.ToString(TagRenderMode.Normal);
		}

		/// <summary>
		/// Создаёт div HTML-тег, контейнер для кнопок.
		/// </summary>
		/// <returns>Строка, содержащая HTML-тег.</returns>
		protected virtual string CreateTabs()
		{
			StringBuilder sb = new StringBuilder();
			foreach (KescoTabItem tab in Control.Tabs) {

				if (!String.IsNullOrEmpty(tab.Uri) && tab.Uri.StartsWith("#")) {
					TagBuilder div = new TagBuilder("DIV");
					div.Attributes.Add("id", tab.Uri.Substring(1));
					div.InnerHtml = tab.Content;
					sb.AppendLine(div.ToString(TagRenderMode.Normal));
				}

			}
			return sb.ToString();
		}
	}
}
