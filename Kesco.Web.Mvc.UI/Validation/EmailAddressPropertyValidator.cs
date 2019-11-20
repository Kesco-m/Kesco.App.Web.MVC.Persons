using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation.Validators;

namespace Kesco.Web.Mvc.Validation
{
	/// <summary>
	/// Выполняет проверку электронного адреса.
	/// </summary>
	public class EmailAddressPropertyValidator : PropertyValidator
	{
		/// <summary>
		/// Хранит список неверных электронных адресов при проверке
		/// методом <see cref="EmailAddressMustBeValid" />
		/// </summary>
		protected List<string> lastInvalidEmails = new List<string>();

		public EmailAddressPropertyValidator()
			: base(() => Kesco.Web.Mvc.Localization.Resources.EmailAddressPropertyValidator_EmailInvalid)
		{
			CustomMessageFormatArguments.Add((arg, obj) => {
				return String.Join("; ", lastInvalidEmails);
			});
		}

		protected override bool IsValid(PropertyValidatorContext context)
		{
			var emails = context.PropertyValue as string;

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
	}
}
