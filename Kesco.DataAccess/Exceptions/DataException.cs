using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.DataAccess
{
	/// <summary>
	/// Определяет базовый класс для исключений, возникающих при работе с базой данных
	/// </summary>
	public class DataException : BaseException
	{
		/// <summary>
		/// Инициализируется новый экземпляр класса <see cref="DataException"/>.
		/// </summary>
		public DataException()
			: base()
		{
		}

		/// <summary>
		/// Инициализируется новый экземпляр класса  <see cref="DataException"/>
		/// с укзанием сообщения об ошибке.
		/// </summary>
		/// <param name="message">Сообщение для отображения клиенту когда возникает искулючение.</param>
		/// <seealso cref="Exception.Message"/>
		public DataException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DataException"/> class
		/// with the specified error message and InnerException property.
		/// </summary>
		/// <param name="message">The message to display to the client when the
		/// exception is thrown.</param>
		/// <param name="innerException">The InnerException, if any, that threw
		/// the current exception.</param>
		/// <seealso cref="Exception.Message"/>
		/// <seealso cref="Exception.InnerException"/>
		public DataException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DataException"/> class
		/// with the InnerException property.
		/// </summary>
		/// <param name="innerException">The InnerException, if any, that threw
		/// the current exception.</param>
		/// <seealso cref="Exception.InnerException"/>
		public DataException(Exception innerException)
			: base(innerException.Message, innerException)
		{
		}

	}
}
