using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using FluentValidation.Results;

namespace Kesco.Web.Mvc.Validation
{
	/// <summary>
	/// Класс-расширение валидации данных
	/// </summary>
	public static class FluentValidationExtensions
	{

		public static IRuleBuilderOptions<T, string> EmailAddressMustBeValid<T>(
				this IRuleBuilder<T, string> ruleBuilder,
				ICollection<Func<object, object, object>> customArgs = null
			)
		{
			var validator = new EmailAddressPropertyValidator();
			if (customArgs != null) {
				customArgs.All((arg) => {
					//validator.CustomMessageFormatArguments.Add(arg);
					return true;
				});
			}
			return ruleBuilder.SetValidator(new EmailAddressPropertyValidator());
		}

		/*public static ValidationResult Validate<T>(this IValidator<T> validator, T dataModel, ValidationResults validationResults)
		where T : class, new()
		{

			var results = validator.Validate(dataModel);
			if (!results.IsValid)
			{
				if (validationResults == null)
					throw new ArgumentNullException("validationResults");
				validationResults.Messages = results.Errors.Select(er => er.ErrorMessage).ToList();
			}
			return results;
		}*/
	}
}
