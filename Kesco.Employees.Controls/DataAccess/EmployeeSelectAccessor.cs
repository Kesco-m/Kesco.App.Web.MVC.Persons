using System;
using Kesco.Employees.BusinessLogic.DataAccess;
using Kesco.Employees.ObjectModel;
using Kesco.Web.Mvc.UI.Controls.DataAccess;

namespace Kesco.Employees.Controls.DataAccess
{
	/// <summary>
	/// Реализует проводник данных для элемента управления Выбор Сотрудника
	/// Аналог Dso4Select
	/// </summary>
	public abstract class EmployeeSelectAccessor : EmployeePartialAccessor, IKescoSelectAccessor
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
			if (instance is EmployeePartial)
			{
				return ((EmployeePartial)instance).GetInstanceFriendlyName();
			}
			else
				throw new ArgumentException(String.Format("Ожидается аргумент следующего типа {0}", typeof(EmployeePartial), "instance"));
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
			if (instance is EmployeePartial)
			{
				return ((EmployeePartial)instance).GetUniqueID();
			}
			else
				throw new ArgumentException(String.Format("Ожидается аргумент следующего типа {0}", typeof(EmployeePartial), "instance"));
		}
	}
}