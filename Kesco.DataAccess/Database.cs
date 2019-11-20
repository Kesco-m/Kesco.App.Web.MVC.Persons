using System;
using System.Collections.Generic;
using System.Data;
using BLToolkit.Data;
using BLToolkit.Data.Sql;
using Kesco.Lib.Log;
using System.Data.SqlClient;
using Kesco.DataAccess.Localization;
using SqlException = System.Data.SqlClient.SqlException;

namespace Kesco.DataAccess
{
	/// <summary>
	/// Класс определяет базовый класс менеджера для работы с базой данных
	/// </summary>
	public class Database : DbManager
	{
		private string _connectionString;

		/// <summary>
		/// Инициализирует экземпляр класса <see cref="Database"/>.
		/// </summary>
		/// <param name="configurationString">Строка соединения.</param>
		public Database(string configurationString) : base(configurationString) {
			_connectionString = GetConnectionString(configurationString);
			InitDatabase();
		}

		/// <summary>
		/// Инициализирует экземпляр класса <see cref="Database"/>.
		/// </summary>
		/// <param name="connection">Соединение с базой данных</param>
		public Database(IDbConnection connection) : base(connection) {
			_connectionString = connection.ConnectionString;
			InitDatabase();
		}

		/// <summary>
		/// Инициализирует менеджер базы данных, устанавливая обработчик исключительной ситуации,
		/// возникшей при работе с базой данных.
		/// </summary>
		protected virtual void InitDatabase()
		{
			OperationException += new OperationExceptionEventHandler(Database_OperationException);
			this.SetCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED").ExecuteNonQuery();			
		}

		/// <summary>
		/// Обработчик исключительной ситуации, возникшей при работе с базой данных.
		/// </summary>
		/// <param name="sender">Источник исключительной ситуации.</param>
		/// <param name="ea">Экземпляр аргументов <see cref="BLToolkit.Data.OperationExceptionEventArgs"/>, содержащие данные об исключительной информации.</param>
		void Database_OperationException(object sender, OperationExceptionEventArgs ea)
		{
			// sql connection error numbers
			int[] nums = new int[] 
			{
				-1, 2, 53,	// cann't establish a connection to the server 
				-2,			// timeout expired
				11,			// general network error
				233,		// transport-level error - no process is on the other end of the pipe
				1205		// deadlock resources
			};

			int? num = null; try { num = ea.Exception.Number; } catch { } // workaround for number getter exception

			Exception ex = ea.Exception.InnerException;

			string message = ex.Message;
			Priority priority = Priority.Error;
			if (ex != null && ex is SqlException)
			{
				switch ((ex as SqlException).Number)
				{
					case -2:
						message = Resources.TimeoutExpired_ValidationError;
						priority = Priority.ExternalError;
						break;
					case 2:
						message = Resources.NotRunningServer_ValidationError;
						priority = Priority.ExternalError;
						break;
					case 17:
						message = Resources.ServerDBNotExistOrAccessDenied_ValidationError;
						priority = Priority.ExternalError;
						break;
					case 53:
						message = Resources.ConnectionFailure_ValidationError;
						priority = Priority.ExternalError;
						break;
					case 229:
					case 230:
						message = Resources.AccessDenied_ValidationError;
						priority = Priority.ExternalError;
						break;
					case 1205:
						message = Resources.DeadLockDetected_ValidationError;
						priority = Priority.ExternalError;
						break;
					case 4060:
						message = Resources.RequestedDataBaseNotAvaliable_ValidationError;
						priority = Priority.ExternalError;
						break;
					case 10054:
					case 18456:
						message = Resources.RemoteHostUnreachable_ValidationError;
						priority = Priority.ExternalError;
						break;
				}
			}
			// В случае открытия соединения и/или проблемы с сетью обращение к свойству this.Command приводит к рекурсивному исключению
			if (ea.Operation == OperationType.OpenConnection || (num != null && Array.Exists(nums, n => { return num == n; })))
			{
				//TODO: посмотреть поле LastQuery - возможно его можно выводить в качестве текста последней команды
				SqlCommand cmd = new SqlCommand();
			    cmd.CommandText = this.LastQuery;
				cmd.Connection = new SqlConnection(_connectionString);
				throw new DetailedException(message, ex, cmd, priority);
			}
			else
				// TODO: проверить на разных типах запросов: Ins, Upd, Del, SP...
				throw new DetailedException(message, ex, this.Command, priority);
		}

	}
}
