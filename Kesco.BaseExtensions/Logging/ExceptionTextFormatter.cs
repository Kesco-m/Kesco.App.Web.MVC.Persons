using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
*/
using System.Collections.Specialized;

namespace Kesco.Logging
{
	/*
	[ConfigurationElementType(typeof(TextFormatterData))]
	public class ExceptionTextFormatter : TextFormatter
	{
		private readonly static Dictionary<string, TokenHandler<LogEntry>> extraTokenHandlers;

		static ExceptionTextFormatter()
		{
			extraTokenHandlers = new Dictionary<string, TokenHandler<LogEntry>>();
			extraTokenHandlers["component"]
				= GenericTextFormatter<LogEntry>.CreateSimpleTokenHandler(le => (le is ExceptionLogEntry)? (le as ExceptionLogEntry).ComponentName : "");
			extraTokenHandlers["function"]
				= GenericTextFormatter<LogEntry>.CreateSimpleTokenHandler(le => (le is ExceptionLogEntry) ? (le as ExceptionLogEntry).FunctionName : "");
		}

		/// <summary>
		/// Initializes a new instance of a <see cref="TextFormatter"></see> with a default template.
		/// </summary>
		public ExceptionTextFormatter() : base() { }

        /// <summary>
        /// Initializes a new instance of a <see cref="TextFormatter"> with a template and no extra token handlers.</see>
        /// </summary>
        /// <param name="template">Template to be used when formatting.</param>
		public ExceptionTextFormatter(string template)
            : base(template, extraTokenHandlers)
        { }

		/// <summary>
        /// Initializes a new instance of a <see cref="TextFormatter"> with a template and additional token handlers.</see>
        /// </summary>
        /// <param name="template">Template to be used when formatting.</param>
        /// <param name="extraTokenHandlers">The additional token handlers to use when processing the template.</param>
		protected ExceptionTextFormatter(string template, IDictionary<string, TokenHandler<LogEntry>> extraTokenHandlers)
			: base(template, extraTokenHandlers)
        { }
	}
	*/
}
