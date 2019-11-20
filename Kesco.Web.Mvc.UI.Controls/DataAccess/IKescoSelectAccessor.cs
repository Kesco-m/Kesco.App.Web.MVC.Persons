using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kesco.DataAccess;

namespace Kesco.Web.Mvc.UI.Controls.DataAccess
{
	/// <summary>
	/// Определяет интерфейс проводника данных для элемента управления
	/// <see cref="Kesco.Web.Mvc.UI.KescoSelect" />.
	/// </summary>
	public interface IKescoSelectAccessor
	{

		/// <summary>
		/// Возвращает значение экземпляра.
		/// </summary>
		/// <param name="instance">Экземпляр сущности.</param>
		/// <returns>
		/// Значение экземпляра.
		/// </returns>
		string GetInstanceKeyValue(object instance);

		/// <summary>
		/// Возвращает отображаемое имя экземпляра.
		/// </summary>
		/// <param name="instance">Экземпляр.</param>
		/// <returns>
		/// Отображаемое имя экземпляра.
		/// </returns>
		string GetInstanceDisplayName(object instance);

	}
}
