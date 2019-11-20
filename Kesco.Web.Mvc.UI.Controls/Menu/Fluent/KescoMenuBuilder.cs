using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc.UI.Fluent
{

	/// <summary>
	/// Класс, реализующий построитель для элемента управления KescoSelectTextBox.
	/// </summary>
	public class KescoMenuBuilder : ControlBuilderBase<KescoMenu, KescoMenuBuilder>
	{

		public KescoMenuBuilder(KescoMenu control) : base(control) { }

		/// <summary>
		/// Добавляет элемент меню, настраиваемый действием.
		/// </summary>
		/// <param name="caption">Заголовок.</param>
		/// <param name="customization">Делегат, который производит настройку элемента меню.</param>
		/// <param name="condition">Условие</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoMenuBuilder AddMenuItem(string caption, Action<KescoMenuItem> customization, bool condition)
		{
			if (condition) {
				KescoMenuItem menuItem = new KescoMenuItem { Caption = caption };
				this.control.Menu.Add(menuItem);
				if (customization != null) customization(menuItem);
			}
			return this;

		}

		public KescoMenuBuilder AddMenuItem(string caption, Action<KescoMenuItem> customization)
		{
			return AddMenuItem(caption, customization, true);

		}


	}
}
