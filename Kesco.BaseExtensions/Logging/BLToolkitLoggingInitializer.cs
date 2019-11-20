using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Aspects;
using System.Collections;
using System.Globalization;
//using Microsoft.Practices.EnterpriseLibrary.Logging;
using BLToolkit.Data;

namespace Kesco.Logging
{
	/// <summary>
	/// Класс, инициализирующий внутренние логирование библиотеки BLToolkit.
	/// </summary>
	[Obsolete("Временно не используется.")]
	public class BLToolkitLoggingInitializer
	{
		/// <summary>
		/// Иницилизирует внутреннию операцию логирования для аспекта LoggingAspect
		/// </summary>
		public static void Init()
		{
			LoggingAspect.LogOperation = LogOperationInternal;
		}

		/// <summary>
		/// Реализует внутреннию операцию логирования
		/// </summary>
		/// <param name="info">Информацию перехвата вызова.</param>
		/// <param name="parameters">Параметры, устанановленные для аспекта LoggingAspect.</param>
		private static void LogOperationInternal(InterceptCallInfo info, BLToolkit.Aspects.LoggingAspect.Parameters parameters)
		{
			DateTime end = DateTime.Now;
			int time = (int)((end - info.BeginCallTime).TotalMilliseconds);

			if (info.Exception != null && parameters.LogExceptions ||
				info.Exception == null && time >= parameters.MinCallTime) {
				string callParameters = null;
				int plen = info.ParameterValues.Length;

				if (parameters.LogParameters && plen > 0) {
					StringBuilder sb = new StringBuilder();
					object[] values = info.ParameterValues;

					FormatParameter(values[0], sb);
					for (int i = 1; i < plen; i++) {
						FormatParameter(values[i], sb.Append(", "));
					}

					callParameters = sb.ToString();
				}

				string exText = null;

				if (info.Exception != null)
					exText = string.Format(
						" with exception '{0}' - \"{1}\"",
						info.Exception.GetType().FullName,
						info.Exception.Message);

				string msg = string.Format("{1}.{2}({3}) - {4} ms{5}{6}.",
						end,
						info.CallMethodInfo.MethodInfo.DeclaringType.Name,
						info.CallMethodInfo.MethodInfo.Name,
						callParameters,
						time,
						info.Cached ? " from cache" : null,
						exText);
				Exception ex = info.Exception;
				if (ex != null && ex is DataException) {
					ex = ex.InnerException;
				}

				/*LogEntry logEntry = (ex != null) ? new ExceptionLogEntry(ex) : new LogEntry();
				logEntry.Severity = (ex != null) ? System.Diagnostics.TraceEventType.Error : System.Diagnostics.TraceEventType.Information;
				logEntry.Message = msg;
				if (ex != null) {
					logEntry.Categories.Add("Errors");
					logEntry.AddErrorMessage(ex.Message);
				} else {
					logEntry.Categories.Add("Database Events");
				}
				Logger.WriteEx(logEntry);
				*/
				/*
				LoggingAspect.LogOutput(
					string.Format("{0}: {1}.{2}({3}) - {4} ms{5}{6}.",
						end,
						info.CallMethodInfo.MethodInfo.DeclaringType.FullName,
						info.CallMethodInfo.MethodInfo.Name,
						callParameters,
						time,
						info.Cached ? " from cache" : null,
						exText),
					parameters.FileName);
				 */
			}
		}

		private static void FormatParameter(object parameter, StringBuilder sb)
		{
			if (parameter == null)
				sb.Append("<null>");
			else if (parameter is string)
				sb.Append('"').Append((string)parameter).Append('"');
			else if (parameter is char)
				sb.Append('\'').Append((char)parameter).Append('\'');
			else if (parameter is IEnumerable) {
				sb.Append('[');
				bool first = true;
				foreach (object item in (IEnumerable)parameter) {
					FormatParameter(item, first ? sb : sb.Append(','));
					first = false;
				}
				sb.Append(']');
			} else if (parameter is IFormattable) {
				IFormattable formattable = (IFormattable)parameter;
				sb.Append(formattable.ToString(null, CultureInfo.CurrentCulture));
			} else
				sb.Append(parameter.ToString());
		}

	}
}
