namespace Kesco
{
    using System;

	/// <summary>
	/// Класс, предоставлящий методы для проверки параметров.
	/// </summary>
    internal static class Guard
    {
		/// <summary>
		/// Проверяет является ли ссылка на параметр пустой.
		/// Генерирует исключение <see cref="System.ArgumentNullException"/>
		/// </summary>
		/// <param name="parameter">Ссылка на параметр.</param>
		/// <param name="parameterName">Имя параметра.</param>
        public static void IsNotNull(object parameter, string parameterName)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(parameterName, " cannot be null.");
            }
        }

		/// <summary>
		/// Проверяет является ли ссылка на параметр пустой.
		/// Генерирует исключение <see cref="System.ArgumentNullException"/>
		/// </summary>
		/// <param name="parameter">Ссылка на параметр.</param>
		/// <param name="parameterName">Имя параметра.</param>
		/// <param name="message">Сообщение об ошибке.</param>
        public static void IsNotNull(object parameter, string parameterName, string message)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(parameterName, " " + message);
            }
        }

		/// <summary>
		/// Проверяет является строка пустой.
		/// Генерирует исключение <see cref="System.ArgumentNullException"/>
		/// </summary>
		/// <param name="parameter">Строка.</param>
		/// <param name="parameterName">Имя параметра.</param>
		public static void IsNotNullOrEmpty(string parameter, string parameterName)
        {
            if (string.IsNullOrEmpty(parameter))
            {
                throw new ArgumentNullException(parameterName, " cannot be null or empty.");
            }
        }

		/// <summary>
		/// Проверяет является ли строка пустой.
		/// Генерирует исключение <see cref="System.ArgumentNullException"/>
		/// </summary>
		/// <param name="parameter">Ссылка на параметр.</param>
		/// <param name="parameterName">Имя параметра.</param>
		/// <param name="message">Сообщение об ошибке.</param>
		public static void IsNotNullOrEmpty(string parameter, string parameterName, string message)
        {
            if (string.IsNullOrEmpty(parameter))
            {
                throw new ArgumentNullException(parameterName, " " + message);
            }
        }
    }
}
