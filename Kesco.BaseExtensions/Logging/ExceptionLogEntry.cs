using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Diagnostics;

namespace Kesco.Logging
{
	/*
	/// <summary>
	/// 
	/// </summary>
	public class ExceptionLogEntry : LogEntry
	{
		public Exception Exception { get; protected set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ExceptionLogEntry"/> class.
		/// </summary>
		/// <param name="exception">The exception.</param>
		public ExceptionLogEntry(Exception exception)
			: this(exception, false)
		{}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExceptionLogEntry"/> class.
		/// </summary>
		/// <param name="exception">The exception.</param>
		/// <param name="isCritical">if set to <c>true</c> [is critical].</param>
		public ExceptionLogEntry(Exception exception, bool isCritical)
			: base()
		{
			Exception = exception;
			Severity = isCritical ? TraceEventType.Critical : TraceEventType.Error;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExceptionLogEntry"/> class.
		/// </summary>
		/// <param name="exception">The exception.</param>
		/// <param name="message">The message.</param>
		/// <param name="category">The category.</param>
		/// <param name="priority">The priority.</param>
		/// <param name="eventId">The event id.</param>
		/// <param name="severity">The severity.</param>
		/// <param name="title">The title.</param>
		/// <param name="properties">The properties.</param>
		public ExceptionLogEntry(Exception exception, object message, string category, int priority, int eventId,
						TraceEventType severity, string title, IDictionary<string, object> properties)
			: base(message, category, priority, eventId, severity, title, properties)
		{
			Exception = exception;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExceptionLogEntry"/> class.
		/// </summary>
		/// <param name="exception">The exception.</param>
		/// <param name="message">The message.</param>
		/// <param name="categories">The categories.</param>
		/// <param name="priority">The priority.</param>
		/// <param name="eventId">The event id.</param>
		/// <param name="severity">The severity.</param>
		/// <param name="title">The title.</param>
		/// <param name="properties">The properties.</param>
		public ExceptionLogEntry(Exception exception, object message, ICollection<string> categories, int priority, int eventId,
						TraceEventType severity, string title, IDictionary<string, object> properties)
			: base(message, categories, priority, eventId, severity, title, properties) 
		{
			Exception = exception;
		}

		public string ComponentName { get; set; }
		public string FunctionName { get; set; }

	}
*/
}
