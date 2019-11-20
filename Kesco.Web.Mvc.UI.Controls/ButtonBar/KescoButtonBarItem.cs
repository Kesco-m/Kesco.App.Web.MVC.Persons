using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc.UI
{
	public class KescoButtonBarItem
	{
		/// <summary>
		/// Устанавливает или возвращает заголовок кнопки.
		/// </summary>
		/// <value>
		/// Заголовок кнопки.
		/// </value>
		public string Caption { get; set; }

		/// <summary>
		/// Устанавливает или возвращает адрес веб-ресурса, загружаемого в диалоговое окно.
		/// </summary>
		/// <value>
		/// Адрес веб-ресурса.
		/// </value>
		public string Uri { get; set; }

		public string ButtonType { get; set; }

		public string CallbackFunc { get; set; }

		public string TargetForm { get; set; }

		public string PrimaryIcon { get; set; }
		public string SecondaryIcon { get; set; }

		/// <summary>
		/// Устанавливает или возвращает ширину диалогового окна.
		/// </summary>
		/// <value>
		/// Ширина диалогового окна.
		/// </value>
		public int? DialogWidth { get; set; }

		/// <summary>
		/// Устанавливает или возвращает высоту диалогового окна.
		/// </summary>
		/// <value>
		/// Высота диалогового окна.
		/// </value>
		public int? DialogHeight { get; set; }
	}
}
