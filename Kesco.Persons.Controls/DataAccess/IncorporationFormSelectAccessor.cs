using System;
using Kesco.Persons.BusinessLogic.DataAccess;
using Kesco.Persons.ObjectModel;
using Kesco.Web.Mvc.UI.Controls.DataAccess;

namespace Kesco.Persons.Controls.DataAccess
{
	/// <summary>
	/// Реализует проводник данных для элемента управления Организационно-правовая форма
	/// </summary>
	public abstract class IncorporationFormSelectAccessor : IncorporationFormAccessor, IKescoSelectAccessor
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
			if (instance is IncorporationForm) {
				return ((IncorporationForm)instance).Name;
			} else
				throw new ArgumentException(String.Format("Ожидается аргумент следующего типа {0}", typeof(IncorporationForm), "instance"));
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
			if (instance is IncorporationForm) {
				return ((IncorporationForm)instance).GetUniqueID();
			} else
				throw new ArgumentException(String.Format("Ожидается аргумент следующего типа {0}", typeof(IncorporationForm), "instance"));
		}
	}
}