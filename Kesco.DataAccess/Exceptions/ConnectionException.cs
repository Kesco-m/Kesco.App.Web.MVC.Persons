using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Data;

namespace Kesco.DataAccess
{
    /// <summary>
    /// The exception that is thrown when database returns a connect error. 
    /// </summary>
    public class ConnectionException : DataException
    { 
		/// <summary>
		/// Initializes a new instance of the <see cref="ConnectionException"/> class.
		/// </summary>
		public ConnectionException()
			: base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ConnectionException"/> class
		/// with the specified error message.
		/// </summary>
		/// <param name="message">The message to display to the client when the
		/// exception is thrown.</param>
		/// <seealso cref="Exception.Message"/>
		public ConnectionException(string message)
			: base(message) 
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ConnectionException"/> class
		/// with the specified error message and InnerException property.
		/// </summary>
		/// <param name="message">The message to display to the client when the
		/// exception is thrown.</param>
		/// <param name="innerException">The InnerException, if any, that threw
		/// the current exception.</param>
		/// <seealso cref="Exception.Message"/>
		/// <seealso cref="Exception.InnerException"/>
        public ConnectionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
		/// Initializes a new instance of the <see cref="ConnectionException"/> class
		/// with the InnerException property.
		/// </summary>
		/// <param name="innerException">The InnerException, if any, that threw
		/// the current exception.</param>
		/// <seealso cref="Exception.InnerException"/>
		public ConnectionException(Exception innerException)
			: base(innerException.Message, innerException)
		{
		}

    }
}
