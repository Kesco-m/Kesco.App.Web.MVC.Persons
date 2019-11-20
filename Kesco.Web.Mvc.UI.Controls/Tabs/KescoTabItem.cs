using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc.UI
{
	public class KescoTabItem
	{
		/// <summary>
		/// Устанавливает или возвращает заголовок закладки.
		/// </summary>
		/// <value>
		/// Заголовок закладки.
		/// </value>
		public string Caption { get; set; }

		/// <summary>
		/// Устанавливает или возвращает адрес веб-ресурса, загружаемого в закладку.
		/// </summary>
		/// <value>
		/// Адрес веб-ресурса.
		/// </value>
		public string Uri { get; set; }

		/// <summary>
		/// Устанавливает или возвращает HTML-содержимое закладки.
		/// </summary>
		/// <value>
		/// HTML-содержимое закладки.
		/// </value>
		public string Content { get; set; }

	}
}
