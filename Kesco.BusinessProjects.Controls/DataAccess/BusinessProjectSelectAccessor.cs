using System;
using Kesco.BusinessProjects.BusinessLogic;
using Kesco.BusinessProjects.ObjectModel;
using Kesco.Web.Mvc.UI.Controls.DataAccess;

namespace Kesco.BusinessProjects.Controls.DataAccess
{

	/// <summary>
	/// Реализует проводник данных для элемента управления Выбор бизнес-проекта
	/// Аналог Dso4Select
	/// </summary>
	public abstract class BusinessProjectSelectAccessor : BusinessProjectAccessor, IKescoSelectAccessor
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
			if (instance is BusinessProject) {
				return ((BusinessProject)instance).Name;
			} else
				throw new ArgumentException(String.Format("Ожидается аргумент следующего типа {0}", typeof(BusinessProject), "instance"));
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
			if (instance is BusinessProject) {
				return ((BusinessProject)instance).GetUniqueID();
			} else
				throw new ArgumentException(String.Format("Ожидается аргумент следующего типа {0}", typeof(BusinessProject), "instance"));
		}
	}
}