using System;
using Kesco.Territories.BusinessLogic;
using Kesco.Web.Mvc.UI.Controls.DataAccess;
using Kesco.Territories.ObjectModel;

namespace Kesco.Territories.Controls.DataAccess
{

	/// <summary>
	/// Реализует проводник данных для элемента управления Выбор территории
	/// Аналог Dso4Select
	/// </summary>
	public abstract class TerritorySelectAccessor : TerritoryAccessor, IKescoSelectAccessor
	{

		/// <summary>
		/// Возвращает отображаемое имя экземпляра.
		/// </summary>
		/// <param name="instance">Экземпляр.</param>
		/// <returns>
		/// Отображаемое имя экземпляра.
		/// </returns>
		/// <exception cref="System.ArgumentException"></exception>
		public string GetInstanceDisplayName(object instance)
		{
			if (instance is Territory) {
				return ((Territory) instance).Name;
			} else
				throw new ArgumentException(String.Format("Ожидается аргумент следующего типа {0}", typeof(Territory), "instance"));
		}

		/// <summary>
		/// Возвращает значение экземпляра.
		/// </summary>
		/// <param name="instance">Экземпляр сущности.</param>
		/// <returns>
		/// Значение экземпляра.
		/// </returns>
		/// <exception cref="System.ArgumentException"></exception>
		public string GetInstanceKeyValue(object instance)
		{
			if (instance is Territory) {
				return ((Territory) instance).GetUniqueID();
			} else
				throw new ArgumentException(String.Format("Ожидается аргумент следующего типа {0}", typeof(Territory), "instance"));
		}
	}
}