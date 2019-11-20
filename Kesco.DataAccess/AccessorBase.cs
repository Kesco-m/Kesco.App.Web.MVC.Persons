using BLToolkit.Aspects;
using BLToolkit.Data;
using BLToolkit.DataAccess;
using BLToolkit.Reflection;
using Kesco.ObjectModel;

namespace Kesco.DataAccess
{
	/// <summary>
	/// Представляет собой абстрактный базовый класс для всех проводников данных.
	/// </summary>
	/// <typeparam name="D">Тип менеджера базы данных, производного от <see cref="BLToolkit.Data.DbManager">DbManager</see></typeparam>
	/// <typeparam name="A">Тип проводника данных, производного от <see cref="BLToolkit.DataAccess.DataAccessor">DataAccessor</see></typeparam>
	/// <example>
	/// <code>
	/// public abstract class CashFlowGuideAccessor : AccessorBase&lt;CashFlowGuideAccessor.DB, CashFlowGuideAccessor&gt;
	/// {
	///		...
	/// 	public class DB : DbManager { public DB() : base("CashFlowItemsDatabase") { } }
	/// 	...
	/// }
	/// </code>
	/// </example>
	public abstract class AccessorBase<D, A> : DataAccessor
		where D : Database, new()
		where A : AccessorBase<D, A>
	{
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

		#region CreateInstance

		/// <summary>
		/// Создаёт экземпляр проводника данных, производного от 
		/// <see cref="BLToolkit.DataAccess.DataAccessor">DataAccessor</see>, 
		/// с типом, указанным в спецификации.
		/// </summary>
		/// <returns>Экземпляр проводника данных, производного от 
		/// <see cref="BLToolkit.DataAccess.DataAccessor">DataAccessor</see>, 
		/// с типом, указанным в спецификации класса.</returns>
		/// <example>
		/// <code>
		/// public static class CashFlowManager
		/// {
		///		#region Accessor
		/// 
		///		static CashFlowGuideAccessor Accessor
		///		{
		///			[System.Diagnostics.DebuggerStepThrough]
		///			get { return CashFlowGuideAccessor.CreateInstance(); }
		///		}
		/// 
		///		public static string GetCashFlowItemFullName(int id)
		///		{
		///			return Accessor.GetCashFlowItemFullName(id);
		///		}
		///		#endregion
		/// }
		/// 
		/// public abstract class CashFlowGuideAccessor : AccessorBase&lt;CashFlowGuideAccessor.DB, CashFlowGuideAccessor&gt;
		/// {
		///		public class DB : DbManager { public DB() : base("CashFlowItemsDatabase") { } }
		///		...
		///		[SqlQuery(@"SELECT dbo.[fn_Tree_СтатьиДвиженияДенежныхСредств_FullPath](@id, 0) AS СтатьяДвиженияДенежныхСредствПолноеНазвание")]
		///		[ScalarFieldName("СтатьяДвиженияДенежныхСредствПолноеНазвание")]
		///		public abstract string GetCashFlowItemFullName(int @id);
		///		...
		/// }
		/// </code>
		/// </example>
		public static A CreateInstance()
		{
			return DataAccessor.CreateInstance<A>();
		}

		#endregion

		#region Query

		/// <summary>
		/// Определяет для себя базовый класс для выполнения основных CRUD операций,
		/// передавая заданный в спецификации менеджер базы данных,
		/// производный от <see cref="BLToolkit.Data.DbManager">DbManager</see>.
		/// </summary>
		public abstract class DbQuery : SqlQueryEx<D> {}

		/// <summary>
		/// Возвращает экземпляр класса <see cref="DbQuery"/> для выполнения
		/// основных CRUD операций.
		/// <example>
		/// Пример ниже показывает использование свойства <see cref="Query"/> в производном классе.
		/// Метод UpdateCashFlowItem выполняет обновление экземпляра типа <c>CashFlowItem</c> и читает 
		/// его заново из базы
		/// <code>
		/// public abstract class CashFlowGuideAccessor : AccessorBase&lt;CashFlowGuideAccessor.DB, CashFlowGuideAccessor&gt;
		/// {
		///		public class DB : DbManager { public DB() : base("CashFlowItemsDatabase") { } }
		///		...
		///		[ClearCache("GetAllCashFlowItems")]
		///		public CashFlowItem UpdateCashFlowItem(CashFlowItem item)
		///		{
		/// 		DbQuery query = Query;
		///			Query.Update(item);
		///			return Query.SelectByKey&lt;CashFlowItem&gt;(item.ID);
		///		}
		///		...
		/// }
		/// </code>
		/// </example>
		/// </summary>
		public DbQuery Query
		{
			get { return TypeAccessor<DbQuery>.CreateInstance(); }
		}

		#endregion

		/// <summary>
		/// Перезагружает сущность
		/// </summary>
		/// <typeparam name="T">Тип сущности, реализующий интерфейс <see cref="IUnique"/></typeparam>
		/// <param name="entry">Сущность.</param>
		/// <returns>Сущность</returns>
		public T ReloadEntry<T>(T entry)
			where T : IUnique
		{
			DbManager db = GetDbManager(); 
			return Query.SelectByKey<T>(db, entry.GetUniqueID());
		}

		/// <summary>
		/// Добавляет сущность в базу данных и возвращает идентификатор для неё.
		/// </summary>
		/// <typeparam name="T">Тип сущности</typeparam>
		/// <typeparam name="TID">Тип идентификтора.</typeparam>
		/// <param name="entry">Сущность.</param>
		/// <returns>Идентификатор новой сущности</returns>
		public TID InsertAndGetID<T, TID>(T entry)
			where T: IUniqueID<TID>
			where TID: struct
		{
			CacheAspect.ClearCache(this.GetType());
			DbManager db = GetDbManager();
			return Query.InsertAndGetIdentity<T, TID>(db, entry);
		}

		/// <summary>
		/// Добавляет сущность в базу данных.
		/// </summary>
		/// <typeparam name="T">Тип сущности.</typeparam>
		/// <param name="entry">Сущность.</param>
		public void Insert2<T>(T entry)
		{
			CacheAspect.ClearCache(this.GetType());
			DbManager db = GetDbManager(); 
			Query.Insert(db, entry);
		}

		/// <summary>
		/// Добавляет сущность в базу данных и возвращает новую сущность.
		/// </summary>
		/// <typeparam name="T">Тип сущности.</typeparam>
		/// <param name="entry">Сущность.</param>
		/// <returns></returns>
		public T Insert<T>(T entry)
		{
			CacheAspect.ClearCache(this.GetType());
			DbManager db = GetDbManager();
			return Query.InsertEx(db, entry);
		}

		/// <summary>
		/// Обновляет указанную сущность в базе данных.
		/// </summary>
		/// <typeparam name="T">Тип сущности.</typeparam>
		/// <param name="entry">Сущность.</param>
		/// <returns>Обновлённая сущность.</returns>
		public T Update<T>(T entry)
			where T : IUnique
		{
			CacheAspect.ClearCache(this.GetType());
			DbQuery query = Query;
			DbManager db = GetDbManager();
			query.Update(db, entry);
			return query.SelectByKey<T>(db, entry.GetUniqueID());
		}

		/// <summary>
		/// Обновляет указанную сущность в базе данных.
		/// </summary>
		/// <typeparam name="T">Тип сущности.</typeparam>
		/// <param name="entry">Обновлённая сущность.</param>
		public void Update2<T>(T entry)
		{
			CacheAspect.ClearCache(this.GetType());
			DbQuery query = Query;
			query.Update(GetDbManager(), entry);
		}

		/// <summary>
		/// Удаляет указанную сущность в базе данных.
		/// </summary>
		/// <typeparam name="T">Тип сущности.</typeparam>
		/// <param name="entry">Сущность.</param>
		public void Delete<T>(T entry)
			where T : IUnique
		{
			CacheAspect.ClearCache(this.GetType());
			Query.DeleteByKey<T>(GetDbManager(), entry.GetUniqueID());
		}


	}
}
