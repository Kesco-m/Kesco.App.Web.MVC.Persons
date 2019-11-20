using System;
using Kesco.Persons.BusinessLogic.DataAccess;
using Kesco.Persons.ObjectModel;
using Kesco.Web.Mvc.UI.Controls.DataAccess;

namespace Kesco.Persons.Controls.DataAccess
{
	/// <summary>
	/// Реализует проводник данных для элемента управления Выбор Лица
	/// </summary>
	public abstract class PersonSimpleSelectAccessor : PersonSimpleAccessor, IKescoSelectAccessor
	{
		/// <summary>
		/// Возвращает отображаемое имя Лица.
		/// </summary>
		/// <param name="instance">Экземпляр.</param>
		/// <returns>
		/// Отображаемое имя Лица.
		/// </returns>
		/// <exception cref="System.ArgumentException"></exception>
		public string GetInstanceDisplayName(object instance)
		{
			if (instance is PersonSimple) {
				return ((PersonSimple) instance).Nickname;
			} else
				throw new ArgumentException(String.Format("Ожидается аргумент следующего типа {0}", typeof(PersonSimple), "instance"));
		}

		/// <summary>
		/// Возвращает код лица.
		/// </summary>
		/// <param name="instance">Экземпляр сущности.</param>
		/// <returns>
		/// Код лица.
		/// </returns>
		/// <exception cref="System.ArgumentException"></exception>
		public string GetInstanceKeyValue(object instance)
		{
			if (instance is PersonSimple) {
				return ((PersonSimple)instance).GetUniqueID();
			} else
				throw new ArgumentException(String.Format("Ожидается аргумент следующего типа {0}", typeof(PersonSimple), "instance"));
		}

	}
}