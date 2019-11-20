using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.DataAccess
{
	/// <summary>
	/// Определяет класс для исключений, возникающих при работе с данными
	/// </summary>
	public class BaseException : System.Data.DataException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BaseException"/> class.
		/// </summary>
		public BaseException()
			: base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseException"/> class
		/// with the specified error message.
		/// </summary>
		/// <param name="message">The message to display to the client when the
		/// exception is thrown.</param>
		/// <seealso cref="Exception.Message"/>
		public BaseException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseException"/> class
		/// with the specified error message and InnerException property.
		/// </summary>
		/// <param name="message">The message to display to the client when the
		/// exception is thrown.</param>
		/// <param name="innerException">The InnerException, if any, that threw
		/// the current exception.</param>
		/// <seealso cref="Exception.Message"/>
		/// <seealso cref="Exception.InnerException"/>
		public BaseException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseException"/> class
		/// with the InnerException property.
		/// </summary>
		/// <param name="innerException">The InnerException, if any, that threw
		/// the current exception.</param>
		/// <seealso cref="Exception.InnerException"/>
		public BaseException(Exception innerException)
			: base(innerException.Message, innerException)
		{
		}

	}
}
