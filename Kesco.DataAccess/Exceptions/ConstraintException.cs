using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.DataAccess
{
	/// <summary>
	/// The exception that is thrown when database server returns a constraint error. 
	/// </summary>
	public class ConstraintException : DataException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ConstraintException"/> class.
		/// </summary>
		public ConstraintException()
			: base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ConstraintException"/> class
		/// with the specified error message.
		/// </summary>
		/// <param name="message">The message to display to the client when the
		/// exception is thrown.</param>
		/// <seealso cref="Exception.Message"/>
		public ConstraintException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ConstraintException"/> class
		/// with the specified error message and InnerException property.
		/// </summary>
		/// <param name="message">The message to display to the client when the
		/// exception is thrown.</param>
		/// <param name="innerException">The InnerException, if any, that threw
		/// the current exception.</param>
		/// <seealso cref="Exception.Message"/>
		/// <seealso cref="Exception.InnerException"/>
		public ConstraintException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ConstraintException"/> class
		/// with the InnerException property.
		/// </summary>
		/// <param name="innerException">The InnerException, if any, that threw
		/// the current exception.</param>
		/// <seealso cref="Exception.InnerException"/>
		public ConstraintException(Exception innerException)
			: base(innerException.Message, innerException)
		{
		}

	}
}
