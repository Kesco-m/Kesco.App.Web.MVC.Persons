using System.Collections.Generic;
using System.Linq;

namespace Kesco.DataAccess
{
	/// <summary>
	/// Описывает общий интерфейс проводника для сущности
	/// </summary>
	/// <typeparam name="T">Тип сущности</typeparam>
	/// <typeparam name="TID">Тип уникального идентификатора сущности.</typeparam>
	public interface IEntityAccessor<T, S, TID> : IAccessor, IEntitySearchable
		where T : Kesco.ObjectModel.IUniqueID<TID>
		where S : SearchParameters, new()
		where TID : struct
	{
		/// <summary>
		/// Возвращает объект, чьи свойства задают критерии поиска сущности.
		/// </summary>
		/// <returns>Объект с критериями поиска сущности</returns>
		S CreateSearchParameters();

		/// <summary>
		/// Возвращает интерфейс IQueryable для сущности, производной от Т.
		/// </summary>
		/// <typeparam name="TE">Тип сущности, производной от Т.</typeparam>
		/// <returns>
		/// Интерфейс IQueryable для сущности, производной от Т.
		/// </returns>
		IQueryable<TE> GetAll<TE>()
			where TE: class, T;

		/// <summary>
		/// Возвращает экземпляр сущности, c типом производным от T
		/// </summary>
		/// <typeparam name="TE">Тип сущности, производной от Т.</typeparam>
		/// <param name="id">Уникальный код сущности</param>
		/// <returns>
		/// Экземпляр объекта
		/// </returns>
		TE GetInstance<TE>(TID id)
			where TE: class, T;

		/// <summary>
		/// Возвращает экземпляр сущности
		/// </summary>
		/// <param name="id">Уникальный код сущности</param>
		/// <returns>Экземпляр объекта</returns>
		T GetInstance(TID id);

		/// <summary>
		/// Осуществляет поиск сущности, удовлетворяющих критерию поиска.
		/// </summary>
		/// <param name="criteria">Объект, чьи свойства задают критерии поиска</param>
		/// <returns>Список сущностей, совпадающих критерию поиска.</returns>
		List<T> Search(S criteria);

		/// <summary>
		/// Добавляет экземпляр сущности в базу данных
		/// </summary>
		/// <param name="instance">Экземпляр сущности</param>
		/// <returns>Новый экземпляр сущности</returns>
		T Insert(T instance);

		/// <summary>
		/// Обновляет экземпляр сущности в базе данных
		/// </summary>
		/// <param name="instance">Экземпляр сущности</param>
		/// <returns>Обновлённый экземпляр сущности</returns>
		T Update(T instance);

		/// <summary>
		/// Удаляет экземпляр сущности в базе данных
		/// </summary>
		/// <param name="instance">Экземпляр сущности</param>
		/// <returns></returns>
		void Delete(T instance);
	}
}
