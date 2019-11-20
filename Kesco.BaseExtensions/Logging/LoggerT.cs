using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kesco.Lib.Log;
using BLToolkit.Data;
using System.Data.SqlClient;


namespace Kesco.Logging
{
	/// <summary>
	/// Статический класс определяет методы для логирования ошибок.
	/// Данный класс является обёрткой (wrapper) вокруг системы логирования Kesco.Log.
	/// Введён для сведения к минимуму зависимостей особенностей логирования системой Kesco.Log.
	/// </summary>
	public static class Logger
	{
		private static object sync = new object();
		private static volatile LogModule log = null;

		/// <summary>
		/// Возвращает текущий модуль логирования Kesco.Log.
		/// </summary>
		public static LogModule Log {
			get {
				return log;
			}
		}

		/// <summary>
		/// Инициализирует класс-обёртку с указанием модуля логирования Kesco.Log.
		/// </summary>
		/// <param name="logModule">Модуль логирования Kesco.Log.</param>
		public static void Init(LogModule logModule)
		{
			lock (sync) {
				LogModule oldLog = log;
				log = logModule;
				Kesco.Lib.Log.Logger.Init(logModule);
				if (oldLog != null && oldLog is IDisposable) {
					(oldLog as IDisposable).Dispose();
				}
			}
		}

		/// <summary>
		/// Записывает исключение с указанным сообщением и деталями об ошибке в журнал логирования
		/// </summary>
		/// <param name="message">Сообщение.</param>
		/// <param name="ex">Исключение.</param>
		/// <param name="details">Детальная информация.</param>
		public static void WriteEx(string message, Exception ex, string details)
		{
			if (Log != null) {
				Kesco.Lib.Log.Logger.WriteEx(new DetailedException(message,  ex,  details));
			}
		}

		/// <summary>
		/// Записывает исключение с указанием сообщением об ошибке в журнал логирования
		/// </summary>
		/// <param name="message">Сообщение.</param>
		/// <param name="ex">Исключение.</param>
		public static void WriteEx(string message, Exception ex)
		{
			WriteEx(message, ex, null);
		}

		/// <summary>
		/// Записывает исключение с указанными деталями об ошибке в журнал логирования
		/// </summary>
		/// <param name="ex">Сообщение.</param>
		/// <param name="details">Исключение.</param>
		public static void WriteEx(Exception ex, string details)
		{
			WriteEx(null, ex, details);
		}

		/// <summary>
		/// Записывает исключение с указанными деталями об ошибке в журнал логирования
		/// </summary>
		/// <param name="ex">Исключение.</param>
		public static void WriteEx(Exception ex)
		{
			WriteEx(ex.Message, ex, null);
		}

		/*
		public static void WriteEx(Priority priority, string component, string func, string message, Exception ex, params object[] additionalInfo)
		{
			ex = ex ?? new Exception(message);
			List<object> list = new List<object> {
				ex
			};

			if (additionalInfo != null)
				list.AddRange(additionalInfo);


			if (Log != null) {
				if (ex is DataException) {
					SqlException sex = ex.InnerException as SqlException;

					if (sex != null) {
						Log.WriteSEx(priority, component, func, null, sex);
					} else {
						Log.WriteEx(priority, component, func, list.ToArray());
					}
				} else {
					Log.WriteEx(priority, component, func, list.ToArray());
				}
			}
		}
		*/
	}
}
