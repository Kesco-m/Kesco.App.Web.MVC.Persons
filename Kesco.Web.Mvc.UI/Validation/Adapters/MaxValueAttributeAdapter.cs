using System.Collections.Generic;
using System.Web.Mvc;
using Kesco.ComponentModel.DataAnnotations;

namespace Kesco.Web.Mvc.Validation
{

	/// <summary>
	/// Адаптер для атрибута MaxValueAttribute
	/// </summary>
	public class MaxValueAttributeAdapter : DataAnnotationsModelValidator<MaxValueAttribute>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MaxValueAttributeAdapter"/> class.
		/// </summary>
		/// <param name="metadata">The metadata.</param>
		/// <param name="context">The context.</param>
		/// <param name="attribute">The attribute.</param>
		public MaxValueAttributeAdapter(ModelMetadata metadata,
									  ControllerContext context,
									  MaxValueAttribute attribute) :
			base(metadata, context, attribute) { }

		/// <summary>
		/// Получает коллекцию правил проверки клиента.
		/// </summary>
		/// <returns>
		/// Коллекция правил проверки клиента.
		/// </returns>
		public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
		{
			return new ModelClientValidationRule[] { new ModelClientValidationMinValueRule(ErrorMessage, Attribute.MaxValue) };
		}
	}
}
