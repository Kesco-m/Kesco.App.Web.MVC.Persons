using System;
using System.Linq;
using Kesco.Persons.BusinessLogic.DataAccess;
using Kesco.Persons.ObjectModel;
using Kesco.Web.Mvc.UI.Controls.DataAccess;

namespace Kesco.Persons.Controls.DataAccess
{
	/// <summary>
	/// Реализует проводник данных для элемента управления Выбор Лица
	/// </summary>
	public abstract class PersonLinkSelectAccessor : PersonLinkAccessor, IKescoSelectAccessor
	{
		public override object GetInstance(object id)
		{
			return SearchExtended<PersonLinkExtended>(new SearchParameters { IDs = id.ToString(), Limit=1 }).FirstOrDefault();
		}

		/// <summary>
		/// Возвращает отображаемое имя Лица.
		/// </summary>
		/// <param name="instance">Экземпляр.</param>
		/// <returns>Отображаемое имя Лица</returns>
		public string GetInstanceDisplayName(object instance)
		{
			if (instance is PersonLinkExtended) {
				return ((PersonLinkExtended)instance).GetInstanceFriendlyName();
			} else if (instance is PersonLink) {
				return ((PersonLink)instance).GetInstanceFriendlyName();
			} else
				throw new ArgumentException(String.Format("Ожидается аргумент следующего типа {0}", typeof(PersonLink), "instance"));
		}

		/// <summary>
		/// Возвращает код лица.
		/// </summary>
		/// <param name="instance">Экземпляр сущности.</param>
		/// <returns> Код лица. </returns>
		public string GetInstanceKeyValue(object instance)
		{
			if (instance is PersonLink)
			{
				return ((PersonLink)instance).GetUniqueID();
			} else
				throw new ArgumentException(String.Format("Ожидается аргумент следующего типа {0}", typeof(PersonLink), "instance"));
		}

	}
}