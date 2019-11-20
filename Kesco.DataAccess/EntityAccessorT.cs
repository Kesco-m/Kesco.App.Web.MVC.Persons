using System;
using System.Collections.Generic;
using BLToolkit.Aspects;
using BLToolkit.Data;
using BLToolkit.DataAccess;
using BLToolkit.Reflection;
using System.Linq;
using System.Reflection;
using System.Data;

namespace Kesco.DataAccess
{

	/// <summary>
	/// Реализует базовый класс проводника для сущности
	/// </summary>
	/// <typeparam name="A"></typeparam>
	/// <typeparam name="D">менеджера базы данных, производного от <see cref="Kesco.DataAccess.Database">Database</see></typeparam>
	/// <typeparam name="T">Тип сущности</typeparam>
	/// <typeparam name="S">Тип критериев поиска</typeparam>
	/// <typeparam name="TID">Тип уникального идентификатора сущности.</typeparam>
    public abstract class EntityAccessor<A, D, T, S, TID> : DataAccessor, IEntityAccessor<T, S, TID>
		where A : EntityAccessor<A, D, T, S, TID>
		where D : Database, new()
		where S : SearchParameters, new()
		where T : Kesco.ObjectModel.IUniqueID<TID>
		where TID : struct
	{
		static class FilteringSettings
		{
			internal static Filtering.FilterSqlBuilderAttribute FilterSqlBuilderAttribute { get; set; }
		}

		/// <summary>
		/// Возвращает объект, чьи свойства задают критерии поиска сущности.
		/// </summary>
		/// <returns>Объект с критериями поиска сущности</returns>
		public S CreateSearchParameters()
		{
			return TypeAccessor<S>.CreateInstance();
		}

		/// <summary>
		/// Возвращает экземпляр данного проводника
		/// </summary>
		public static A Accessor { get { return TypeAccessor<A>.CreateInstance(); } }

		/// <summary>
		/// Специализация типа <see cref="Kesco.DataAccess.SqlQueryEx&gt;D&lt;">SqlQueryEx&gt;D&lt;</see>
		/// для данного проводника
		/// </summary>
		public abstract class DbQuery : SqlQueryEx<D> { }

		/// <summary>
		/// Возвращает запрос
		/// </summary>
		public DbQuery Query
		{
			get { return TypeAccessor<DbQuery>.CreateInstance(); }
		}

		#region Overrides

		/// <summary>
		/// Создаёт экземпляр менеджера базы данных.
		/// </summary>
		/// <returns>Менеджер базы данных, производный от <see cref="BLToolkit.Data.DbManager">DbManager</see></returns>
		[NoInterception]
		protected override DbManager CreateDbManager()
		{
			return new D();
		}

		#endregion

		public virtual IQueryable<TE> GetAll<TE>()
			where TE: class, T
		{
			using(DbManager db = GetDbManager()) {
				return db.GetTable<TE>().AsQueryable();
			}
		}

		/// <summary>
		/// Возвращает экземпляр сущности
		/// </summary>
		/// <param name="id">Уникальный код сущности</param>
		/// <returns>Экземпляр объекта</returns>
		public virtual T GetInstance(TID id)
		{
			return Query.SelectByKey<T>(id);
		}

		/// <summary>
		/// Возвращает экземпляр сущности
		/// </summary>
		/// <param name="id">Уникальный код сущности</param>
		/// <returns>Экземпляр объекта</returns>
		public virtual TE GetInstance<TE>(TID id)
			where TE: class, T
		{
			return Query.SelectByKey<TE>(id);
		}

		/// <summary>
		/// Возвращает экземпляр сущности
		/// </summary>
		/// <param name="type">Тип сущности</param>
		/// <param name="id">Уникальный код сущности</param>
		/// <returns>
		/// Экземпляр объекта
		/// </returns>
		public virtual object GetInstance(Type type, object id)
		{
			return Query.SelectByKey(type, id);
		}

		/// <summary>
		/// Возвращает экземпляр сущности
		/// </summary>
		/// <param name="id">Уникальный код сущности</param>
		/// <returns>Экземпляр объекта</returns>
		protected T GetInstance(DbManager db, TID id)
		{
			DbQuery query = Query;
			return query.SelectByKey<T>(db, id);
		}

		/// <summary>
		/// Осуществляет поиск сущности, удовлетворяющих критерию поиска.
		/// </summary>
		/// <param name="criteria">Объект, чьи свойства задают критерии поиска</param>
		/// <returns>Список сущностей, совпадающих критерию поиска.</returns>
		public abstract List<T> Search(S criteria);

		/// <summary>
		/// Осуществляет поиск сущности, удовлетворяющих критерию поиска.
		/// </summary>
		/// <param name="criteria">Объект, чьи свойства задают критерии поиска</param>
		/// <returns>Список сущностей, совпадающих c критерием поиска.</returns>
		protected virtual List<T> SearchInternal(S criteria)
		{
			List<T> list = new List<T>();
			using (DbManager dbManager = this.GetDbManager()) {
				Type typeFromHandle = typeof(T);
				
				if (FilteringSettings.FilterSqlBuilderAttribute == null)
				{
					FilteringSettings.FilterSqlBuilderAttribute = this.GetFilterSqlQueryBuilderAttribute(typeof(A));
				}
				
				if (FilteringSettings.FilterSqlBuilderAttribute == null)
					throw new Exception(String.Format("FilterSqlBuilderAttribute не установлен для типа-аксессора {0}.", typeof(A)));
				
				FilteringSettings.FilterSqlBuilderAttribute
					.BuildAndPrepareCommand(this, dbManager, criteria);

				dbManager
					.ExecuteList(list, typeFromHandle);
			}
			return list;
		}

		protected virtual Filtering.FilterSqlBuilderAttribute GetFilterSqlQueryBuilderAttribute(Type accessorType)
		{
			return ((Filtering.FilterSqlBuilderAttribute[]) accessorType.GetCustomAttributes(typeof(Filtering.FilterSqlBuilderAttribute), true))
				.FirstOrDefault();
		}

		//public abstract List<object> Search(object criteria);

		/// <summary>
		/// Добавляет экземпляр сущности в базу данных
		/// </summary>
		/// <param name="instance">Экземпляр сущности</param>
		/// <returns>Новый экземпляр сущности</returns>
		[ClearCache]
		public T Insert(T instance)
		{
			using(DbManager db = GetDbManager()) {
				return Insert(db, instance);
			}
		}

		/// <summary>
		/// Добавляет экземпляр сущности в базу данных
		/// </summary>
		/// <param name="instance">Экземпляр сущности</param>
		/// <returns>
		/// Новый экземпляр сущности
		/// </returns>
		public object Insert(object instance)
		{
			using (DbManager db = GetDbManager())
			{
				return Insert(db, (T) instance);
			}
		}

		/// <summary>
		/// Добавляет экземпляр сущности в базу данных, используя переданное соединение
		/// </summary>
		/// <param name="db">Экземпляр менеджера базы данных</param>
		/// <param name="instance">Экземпляр сущности</param>
		/// <returns>Новый экземпляр сущности</returns>
		protected virtual T Insert(DbManager db, T instance)
		{
			DbQuery query = Query;
			TID id = query.InsertAndGetIdentity<T, TID>(db, instance);
			return query.SelectByKey<T>(db, id);
		}

		/// <summary>
		/// Обновляет экземпляр сущности в базе данных
		/// </summary>
		/// <param name="instance">Экземпляр сущности</param>
		/// <returns>Обновлённый экземпляр сущности</returns>
		[ClearCache]
		public virtual T Update(T instance)
		{
			using (DbManager db = GetDbManager()) {
				return Update(db, instance);
			}
		}

		/// <summary>
		/// Обновляет экземпляр сущности в базе данных
		/// </summary>
		/// <param name="instance">Экземпляр сущности</param>
		/// <returns>
		/// Обновлённый экземпляр сущности
		/// </returns>
		[ClearCache]
		public virtual object Update(object instance)
		{
			using (DbManager db = GetDbManager())
			{
				return Update(db, (T)instance);
			}
		}

		/// <summary>
		/// Обновляет экземпляр сущности в базе данных, используя переданное соединение
		/// </summary>
		/// <param name="db">Экземпляр менеджера базы данных</param>
		/// <param name="instance">Экземпляр сущности</param>
		/// <returns>Обновлённый экземпляр сущности</returns>
		protected T Update(DbManager db, T instance)
		{
			DbQuery query = Query;
			query.Update(db, instance);
			return query.SelectByKey<T>(db, instance.ID);
		}

		/// <summary>
		/// Удаляет экземпляр сущности в базе данных
		/// </summary>
		/// <param name="instance">Экземпляр сущности</param>
		/// <returns></returns>
		public void Delete(T instance)
		{
			Query.Delete(instance);
		}

		[ClearCache]
		public void Delete(object instance)
		{
			Query.Delete((T) instance);
		}

		[ClearCache]
		public void DeleteByKey(params object[] key)
		{
			Query.DeleteByKey<T>(key);
		}
		/// <summary>
		/// Удаляет экземпляр сущности в базе данных, используя переданное соединение
		/// </summary>
		/// <param name="db">Экземпляр менеджера базы данных</param>
		/// <param name="instance">Экземпляр сущности</param>
		[ClearCache]
		protected void Delete(DbManager db, T instance)
		{
			Query.Delete(db, instance);
		}


		/// <summary>
		/// Возвращает тип сущности.
		/// </summary>
		/// <returns>
		/// Тип сущности
		/// </returns>
		public Type GetEntityType()
		{
			return typeof(T);
		}

		/// <summary>
		/// Возвращает тип критериев поиска для сущности.
		/// </summary>
		/// <returns></returns>
		public Type GetSearchParametersType()
		{
			return typeof(S);
		}


		public virtual object GetInstance(object id)
		{
			return GetInstance(GetEntityType(), id);
		}


	}

}
