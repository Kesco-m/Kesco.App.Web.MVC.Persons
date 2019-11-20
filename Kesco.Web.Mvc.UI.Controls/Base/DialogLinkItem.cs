using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc.UI
{
	/// <summary>
	/// Представляет ссылку на веб-ресурс, открываемый в диалоговом окне.
	/// </summary>
	public class DialogLinkItem
	{
		/// <summary>
		/// Устанавливает или возвращает заголовок ссылки.
		/// </summary>
		/// <value>
		/// Заголовок ссылки.
		/// </value>
		public string Caption { get; set; }

		/// <summary>
		/// Устанавливает или возвращает адрес веб-ресурса, загружаемого в диалоговое окно.
		/// </summary>
		/// <value>
		/// Адрес веб-ресурса.
		/// </value>
		public string Uri { get; set; }
		
		/// <summary>
		/// Устанавливает или возвращает значение, указывающее является ли диалог модальным.
		/// </summary>
		/// <value>
		///   <c>true</c> если диалог будет открыт как модальное окно; иначе <c>false</c>.
		/// </value>
		public bool OpenAsModal { get; set; }
		
		/// <summary>
		/// Устанавливает или возвращает заголовок диалогового окна.
		/// </summary>
		/// <value>
		/// Заголовок диалогового окна
		/// </value>
		public string DialogTitle { get; set; }
		
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
