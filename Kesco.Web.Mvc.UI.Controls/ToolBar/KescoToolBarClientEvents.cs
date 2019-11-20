using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc.UI
{
	public class KescoToolBarButtonClientEvents
	{
		/// <summary>
		/// Устанавливает или возвращает вызов клиентского кода, 
		/// который будет выполнен при нажатии на кнопку.
		/// </summary>
		/// <value>
		/// Клиентский код, который будет выполнен при нажатии на кнопку.
		/// </value>
		public string OnClick { get; set; }
	}

}
