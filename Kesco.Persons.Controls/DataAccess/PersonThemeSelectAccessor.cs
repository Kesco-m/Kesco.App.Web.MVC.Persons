using System;
using System.Data;
using BLToolkit.Data;
using Kesco.Persons.BusinessLogic.DataAccess;
using Kesco.Persons.ObjectModel;
using Kesco.Web.Mvc.UI.Controls.DataAccess;

namespace Kesco.Persons.Controls.DataAccess
{
	/// <summary>
	/// Реализует проводник данных для элемента управления Организационно-правовая форма
	/// </summary>
	public abstract class PersonThemeSelectAccessor : PersonThemeAccessor, IKescoSelectAccessor
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
            if (instance is PersonTheme)
            {
                return ((PersonTheme)instance).Name;
			} else
                throw new ArgumentException(String.Format("Ожидается аргумент следующего типа {0}", typeof(PersonTheme), "instance"));
		}

	    string IKescoSelectAccessor.GetInstanceDisplayName(object instance)
	    {
	        return GetInstanceDisplayName(instance);
	    }

	    string IKescoSelectAccessor.GetInstanceKeyValue(object instance)
	    {
	        return GetInstanceKeyValue(instance);
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
            if (instance is PersonTheme)
            {
                return ((PersonTheme)instance).GetUniqueID();
			} else
                throw new ArgumentException(String.Format("Ожидается аргумент следующего типа {0}", typeof(PersonTheme), "instance"));
		}
	}
}