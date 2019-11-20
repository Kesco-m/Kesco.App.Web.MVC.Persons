using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Kesco.ComponentModel.DataAnnotations;

namespace Kesco.Web.Mvc.Validation
{

	/// <summary>
	/// Адаптер
	/// </summary>
	public class MinValueAttributeAdapter : DataAnnotationsModelValidator<MinValueAttribute>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MinValueAttributeAdapter"/> class.
		/// </summary>
		/// <param name="metadata">The metadata.</param>
		/// <param name="context">The context.</param>
		/// <param name="attribute">The attribute.</param>
		public MinValueAttributeAdapter(ModelMetadata metadata,
									  ControllerContext context,
									  MinValueAttribute attribute) :
			base(metadata, context, attribute) { }

		/// <summary>
		/// Получает коллекцию правил проверки клиента.
		/// </summary>
		/// <returns>
		/// Коллекция правил проверки клиента.
		/// </returns>
		public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
		{
			return new ModelClientValidationRule[] { new ModelClientValidationMinValueRule(ErrorMessage, Attribute.MinValue) };
		}
	}
}
