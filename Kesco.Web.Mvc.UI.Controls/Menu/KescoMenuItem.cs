using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc.UI
{
	public class KescoMenuItem
	{
		public KescoMenuItem()
		{
			EnsureMenuCreated();
		}
		/// <summary>
		/// Устанавливает или возвращает заголовок элемента меню.
		/// </summary>
		/// <value>
		/// Заголовок элемента меню.
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
		/// Устанавливает или возвращает имя клиентской функции возврата, 
		/// которая будет запущена при нажатии на ссылку.
		/// </summary>
		/// <value>
		/// Имя клиентской функции возврата
		/// </value>
		public string CallbackFunc { get; set; }

		public string TargetForm { get; set; }

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

		/// <summary>
		/// Возвращает или устанавливает список кнопок
		/// </summary>
		/// <value>
		/// Список кнопок
		/// </value>
		public List<KescoMenuItem> Menu { get; protected set; }

		/// <summary>
		/// Возвращает или устанавливает флаг, является ли пункт меню текущим
		/// </summary>
		/// <value>
		/// Флаг, является ли пункт меню текущим
		/// </value>
		public bool IsCurrent { get; set; }

		public bool HasMenu
		{ 
			get {
				return Menu == null || Menu.Count > 0;
			}
		}

		public void AddMenuItem(string caption, Action<KescoMenuItem> customization)
		{
			AddMenuItem(caption, customization, true);
		}

		public void AddMenuItem(string caption, Action<KescoMenuItem> customization, bool condition)
		{
			Guard.IsNotNull(customization, "customization");
			if (condition) {
				KescoMenuItem menuItem = new KescoMenuItem { Caption = caption };
				if (customization != null) {
					customization(menuItem);
				}
				AddMenuItem(menuItem);
			}
		}

		public void AddMenuItem(KescoMenuItem menuItem) {
			Guard.IsNotNull(menuItem, "menuItem");
			EnsureMenuCreated();
			Menu.Add(menuItem);
		}

		protected virtual void EnsureMenuCreated()
		{
			if (Menu == null) {
				Menu = new List<KescoMenuItem>();
			}
		}

	}

}
