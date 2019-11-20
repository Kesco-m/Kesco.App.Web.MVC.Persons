using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.ExpressionUtil;
using Kesco.ComponentModel.DataAnnotations;

namespace Kesco.Web.Mvc.Validation
{

	/// <summary>
	/// 
	/// </summary>
	public class RemoteAttributeAdapter : DataAnnotationsModelValidator<Kesco.ComponentModel.DataAnnotations.RemoteAttribute>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RemoteAttributeAdapter"/> class.
		/// </summary>
		/// <param name="metadata">The metadata.</param>
		/// <param name="context">The context.</param>
		/// <param name="attribute">The attribute.</param>
		public RemoteAttributeAdapter(ModelMetadata metadata,
									  ControllerContext context,
									  Kesco.ComponentModel.DataAnnotations.RemoteAttribute attribute) :
			base(metadata, context, attribute) { }

		/// <summary>
		/// Получает коллекцию правил проверки клиента.
		/// </summary>
		/// <returns>
		/// Коллекция правил проверки клиента.
		/// </returns>
		public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
		{
			ModelClientValidationRule rule = new ModelClientValidationRule() {
				ErrorMessage = ErrorMessage,
				ValidationType = "remote"
			};

			//rule.ValidationParameters["url"] = Attribute.GetUrl(ControllerContext);
			rule.ValidationParameters["url"] = new UrlHelper(ControllerContext.RequestContext)
				.Action(Attribute.ControllerName, Attribute.ActionName);
			rule.ValidationParameters["parameterName"] = Attribute.ParameterName;
			return new ModelClientValidationRule[] { rule };
		}
	}
}
