using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.DataAccess
{
	/// <summary>
	/// Описывает нетипизированный интерфейс 
	/// проводника данных для сущности
	/// </summary>
    public interface IAccessor
    {
		/// <summary>
		/// Возвращает тип сущности.
		/// </summary>
		/// <returns>Тип сущности</returns>
		Type GetEntityType();

		/// <summary>
		/// Возвращает экземпляр сущности
		/// </summary>
		/// <param name="id">Уникальный код сущности</param>
		/// <returns>
		/// Экземпляр объекта
		/// </returns>
		object GetInstance(object id);

		/// <summary>
		/// Добавляет экземпляр сущности в базу данных
		/// </summary>
		/// <param name="instance">Экземпляр сущности</param>
		/// <returns>
		/// Новый экземпляр сущности
		/// </returns>
		object Insert(object instance);

		/// <summary>
		/// Обновляет экземпляр сущности в базе данных
		/// </summary>
		/// <param name="instance">Экземпляр сущности</param>
		/// <returns>
		/// Обновлённый экземпляр сущности
		/// </returns>
		object Update(object instance);

		/// <summary>
		/// Удаляет экземпляр сущности в базе данных
		/// </summary>
		/// <param name="instance">Экземпляр сущности</param>
		void Delete(object instance);
    }
}
