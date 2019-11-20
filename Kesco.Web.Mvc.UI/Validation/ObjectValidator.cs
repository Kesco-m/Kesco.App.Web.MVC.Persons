using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Kesco.Web.Mvc.Validation
{
	/// <summary>
	/// Класс расширяет базовый валидатор, 
	/// добавляя часто-используемые методы 
	/// валидации свойств объекта
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ObjectValidator<T> : AbstractValidator<T>
	{
		/// <summary>
		/// Хранит список неверных электронных адресов при проверке
		/// методом <see cref="EmailAddressMustBeValid" />
		/// </summary>
		protected List<string> lastInvalidEmails = new List<string>();

		/// <summary>
		/// Проверяет содержит ли строка только латинские буквы или нет.
		/// </summary>
		/// <param name="instance">Экземпляр объекта.</param>
		/// <param name="text">Значение текстового поля</param>
		/// <returns>[true], если строка содержит только латинские буквы.</returns>
		protected bool StringHasOnlyLatinChars(T instance, string text)
		{
			return String.IsNullOrEmpty(text) || StringExtensions.HasOnlyLatinChars(text);
		}

		/// <summary>
		/// Проверяет содержит ли строка правильно указанный электронный адрес или нет.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <param name="emails">The contact text.</param>
		/// <remarks>
		/// Электронный адрес может представлять собой список эл. адресов, 
		/// разделённых точкой запятой.
		/// При проверке, неправильные электронные адреса добавляються в список <see cref="lastInvalidEmails"/>
		/// </remarks>
		/// <returns>[true], если строка содержит правильно указанный электронный адрес или их список.</returns>
		protected bool EmailAddressMustBeValid(T instance, string emails)
		{
			lastInvalidEmails.Clear();

			if (String.IsNullOrWhiteSpace(emails)) return true;

			emails
				// Текст контакта может содержать несколько эл. адресов
				// разделённых точкой с запятой
				.Split(';')
				// пропускаем пустые ячейки
				.Where(email => !String.IsNullOrWhiteSpace(email))
				// поэтому проверяем каждый адрес
				.All((email) => {
					// электронный адрес может содержать 
					// начальные и конечный знаки пробела
					email = email.Trim();
					if (!Kesco.Utilities.RegexValidators.ValidateEmail(email)) {
						lastInvalidEmails.Add(email);
					}
					// вернём true, чтобы проверить каждый адрес.
					return true;
				});
			return lastInvalidEmails.Count == 0;
		}

		/// <summary>
		/// Проверяет является ли строка правильно сформированном Url-адресом.		
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <param name="url">The URL.</param>
		/// <returns>[true], если строка является правильнsv Url-адресом.</returns>
		protected bool UrlMustBeValid(T instance, string url)
		{
			if (String.IsNullOrWhiteSpace(url)) return true;
			return Kesco.Utilities.RegexValidators.ValidateUrl(url);
		}
	}
}
