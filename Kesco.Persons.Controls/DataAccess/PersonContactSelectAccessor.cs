using System;
using Kesco.Persons.BusinessLogic.DataAccess;
using Kesco.Persons.ObjectModel;
using Kesco.Web.Mvc.UI.Controls.DataAccess;

namespace Kesco.Persons.Controls.DataAccess
{
	/// <summary>
	/// Реализует проводник данных для элемента управления Выбор Контакта Лица
	/// </summary>
	public abstract class PersonContactSelectAccessor : PersonContactAccessor, IKescoSelectAccessor
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
			if (instance is PersonContact) {
				return ((PersonContact)instance).ContactText;
			} else
				throw new ArgumentException(String.Format("Ожидается аргумент следующего типа {0}", typeof(PersonContact), "instance"));
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
			if (instance is PersonContact) {
				return ((PersonContact)instance).GetUniqueID();
			} else
				throw new ArgumentException(String.Format("Ожидается аргумент следующего типа {0}", typeof(PersonContact), "instance"));
		}
	}
}