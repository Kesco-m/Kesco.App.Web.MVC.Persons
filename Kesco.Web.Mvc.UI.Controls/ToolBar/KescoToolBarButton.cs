using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc.UI
{

	/// <summary>
	/// 
	/// </summary>
	public class KescoToolBarButton
	{
		public KescoToolBarButton()
		{
			ClientEvents = new KescoToolBarButtonClientEvents();
		}

		/// <summary>
		/// Устанавливает или возвращает заголовок кнопки.
		/// </summary>
		/// <value>
		/// Заголовок кнопки.
		/// </value>
		public string Caption { get; set; }

		/// <summary>
		/// Устанавливает или возвращает вызов клиентского кода функции возврата, 
		/// которая будет запущена при нажатии на ссылку.
		/// </summary>
		/// <value>
		/// Имя клиентской функции возврата
		/// </value>
		
		public KescoToolBarButtonClientEvents ClientEvents { get; protected set; }

		/// <summary>
		/// Устанавливает или возвращает CSS класс для иконки
		/// </summary>
		/// <value>
		/// CSS класс для иконки.
		/// </value>
		public string PrimaryIcon { get; set; }

		/// <summary>
		/// Устанавливает или возвращает CSS класс для вторичной иконки.
		/// </summary>
		/// <value>
		/// CSS класс для иконки.
		/// </value>
		public string SecondaryIcon { get; set; }

	}
}
