using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using BLToolkit.Aspects;
using BLToolkit.Data;
using BLToolkit.DataAccess;
using Kesco.ObjectModel;

namespace Kesco.DataAccess
{
	/// <summary>
	/// Класс расширяет базовый класс SqlQuery, реализующий базовые CRUD операции,
	/// с возможностью указания специфического для приложения 
	/// менеджера базы данных <see cref="BLToolkit.Data.DbManager">DbManager</see>.
	/// </summary>
	/// <example>
	/// Код в примере реализует базовый класс для проводника данных. Проводник определяет внутри себя 
	/// свой класс, производный от <see cref="SqlQueryEx&lt;D&gt;"/>, для выполнения основных CRUD-операций,
	/// а также свойство данного типа.
	/// <code>
	/// public abstract class AccessorBase&lt;D, A&gt; : DataAccessor
	/// 	where D : DbManager, new()
	/// 	where A : AccessorBase&lt;D, A&gt;
	/// {
	///		...
	/// 	public abstract class DbQuery : SqlQueryEx&lt;D&gt; { }
	/// 	
	/// 	public DbQuery Query
	/// 	{
	/// 		get { return TypeAccessor&lt;DbQuery&gt;.CreateInstance(); }
	/// 	}
	/// 
	/// 	...
	/// }
	/// </code>
	/// </example>
	/// <typeparam name="D">Тип менеджера быза данных, производного от <see cref="BLToolkit.Data.DbManager">DbManager</see></typeparam>
	/// <remarks>
	/// </remarks>
	public abstract class SqlQueryEx<D> : SqlQuery
		where D: DbManager, new()
	{
		/// <summary>
		/// Создаёт экземпляр менеджера базы данных, производного от 
		/// <see cref="BLToolkit.Data.DbManager">DbManager</see>, 
		/// с типом, указанным в спецификации класса.
		/// </summary>
		/// <returns>Менеджер базы данных, производного от 
		/// <see cref="BLToolkit.Data.DbManager">DbManager</see>, 
		/// с типом, указанным в спецификации класса.</returns>
		[NoInterception]
		protected override DbManager CreateDbManager()
		{
			return new D();
		}

		#region InsertAndGetIdentity, InsertEx

		/// <summary>
		/// Метод выполняет добавление записи и возвращает идентификатор новой записи.
		/// </summary>
		/// <typeparam name="TID">Тип идентификатора.</typeparam>
		/// <param name="db">Экземпляр менеджера базы данных, производного от <see cref="BLToolkit.Data.DbManager">DbManager</see>.</param>
		/// <param name="obj">Экземпляр объекта, для которого будет добавлена новая запись.</param>
		/// <returns></returns>
		public virtual TID InsertAndGetIdentity<TID>(DbManager db, object obj)
			where TID : struct
		{
			SqlQueryInfo query = GetSqlQueryInfo(db, obj.GetType(), "InsertAndGetIdentity");

			return db
				.SetCommand(query.QueryText, query.GetParameters(db, obj))
				.ExecuteScalar<TID>();
		}

		/// <summary>
		/// Метод выполняет добавление записи и возвращает идентификатор новой записи.
		/// </summary>
		/// <param name="db">Экземпляр менеджера базы данных, производного от <see cref="BLToolkit.Data.DbManager">DbManager</see>.</param>
		/// <param name="obj">Экземпляр объекта, для которого будет добавлена новая запись.</param>
		/// <returns></returns>
		public virtual TID InsertAndGetIdentity<T, TID>(DbManager db, T obj)
			where T : IUniqueID<TID>
			where TID : struct
		{
			SqlQueryInfo query = GetSqlQueryInfo(db, obj.GetType(), "InsertAndGetIdentity");

			return db
				.SetCommand(query.QueryText, query.GetParameters(db, obj))
				.ExecuteScalar<TID>();
		}

		/// <summary>
		/// Метод выполняет добавление записи и возвращает идентификатор новой записи.
		/// </summary>
		/// <param name="obj">Экземпляр объекта, для которого будет добавлена новая запись.</param>
		/// <returns></returns>
		public virtual TID InsertAndGetIdentity<T, TID>(T obj)
			where T : IUniqueID<TID>
			where TID : struct
		{
			DbManager db = GetDbManager();

			try {
				return InsertAndGetIdentity<T,TID>(db, obj);
			} finally {
				Dispose(db);
			}
		}

		/// <summary>
		/// Метод выполняет добавление записи и возвращает идентификатор новой записи.
		/// </summary>
		/// <param name="db">Экземпляр менеджера базы данных, производного от <see cref="BLToolkit.Data.DbManager">DbManager</see>.</param>
		/// <param name="obj">Экземпляр объекта, для которого будет добавлена новая запись.</param>
		/// <returns></returns>
		public virtual int InsertAndGetIdentity(DbManager db, object obj)
		{
			SqlQueryInfo query = GetSqlQueryInfo(db, obj.GetType(), "InsertAndGetIdentity");

			return db
				.SetCommand(query.QueryText, query.GetParameters(db, obj))
				.ExecuteScalar<int>();
		}

		/// <summary>
		/// Метод выполняет добавление записи и возвращает идентификатор новой записи.
		/// </summary>
		/// <param name="obj">Экземпляр объекта, для которого будет добавлена новая запись.</param>
		/// <returns></returns>
		public virtual int InsertAndGetIdentity(object obj)
		{
			DbManager db = GetDbManager();

			try
			{
				return InsertAndGetIdentity(db, obj);
			}
			finally
			{
				Dispose(db);
			}
		}

		/// <summary>
		/// Метод выполняет добавление новой записи и возвращает соответсвующий объект. 
		/// </summary>
		/// <typeparam name="T">Тип объекта</typeparam>
		/// <param name="db">Экземпляр менеджера базы данных, производного от <see cref="BLToolkit.Data.DbManager">DbManager</see>.</param>
		/// <param name="obj">Экземпляр объекта, для которого будет добавлена новая запись.</param>
		/// <returns>Экземпляр объекта, для которого была добавлена новая запись.</returns>
		/// <example>
		/// Следующий метод реализует добавление объекта типа <c>CashFlowItem</c> в базу данных 
		/// и возвращает обновлённый объект
		/// <code>
		/// [ClearCache("GetAllCashFlowItems")]
		/// public CashFlowItem InsertCashFlowItem(CashFlowItem item)
		/// {
		/// 	return Query.InsertEx&lt;CashFlowItem&gt;(item);
		/// }
		/// </code>
		/// </example>
		public virtual T InsertEx<T>(DbManager db, T obj)
		{
			return SelectByKey<T>(db, InsertAndGetIdentity(db, obj));
		}

		/// <summary>
		/// Метод выполняет добавление новой записи и возвращает соответсвующий объект. 
		/// </summary>
		/// <typeparam name="T">Тип объекта</typeparam>
		/// <param name="obj">Экземпляр объекта, для которого будет добавлена новая запись.</param>
		/// <example>
		/// Следующий метод реализует добавление объекта типа <c>CashFlowItem</c> в базу данных 
		/// и возвращает обновлённый объект
		/// <code>
		/// [ClearCache("GetAllCashFlowItems")]
		/// public CashFlowItem InsertCashFlowItem(CashFlowItem item)
		/// {
		/// 	return Query.InsertEx&lt;CashFlowItem&gt;(item);
		/// }
		/// </code>
		/// </example>
		/// <returns>Экземпляр объекта, для которого была добавлена новая запись.</returns>
		public virtual T InsertEx<T>(T obj)
		{
			DbManager db = GetDbManager();

			try
			{
				return InsertEx<T>(db, obj);
			}
			finally
			{
				Dispose(db);
			}
		}

		/// <summary>
		/// Метод добавляет новую 'InsertAndGetIdentity' операцию базы данных для экземпляра объекта.
		/// </summary>
		/// <param name="db">Экземпляр менеджера базы данных, производного от <see cref="BLToolkit.Data.DbManager">DbManager</see>.</param>
		/// <param name="type">Тип объекта, для которого будет выполнена указанная операция.</param>
		/// <param name="actionName">Название операции.</param>
		/// <returns>Информацию для выполнения запроса, представленную экземпляром <see cref="SqlQueryInfo"/>.</returns>
		[NoInterception]
		protected override SqlQueryInfo CreateSqlText(DbManager db, Type type, string actionName)
		{
			switch (actionName)
			{
				case "InsertAndGetIdentity":
					SqlQueryInfo qi = CreateInsertSqlText(db, type, -1);

					qi.QueryText += "\nSELECT Cast(@@IDENTITY as int)";

					return qi;

				default:
					return base.CreateSqlText(db, type, actionName);
			}
		}

		#endregion
	}
}
