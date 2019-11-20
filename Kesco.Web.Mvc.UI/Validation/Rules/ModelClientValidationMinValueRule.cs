using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Kesco.Web.Mvc.Validation
{
	/// <summary>
	/// Правил
	/// </summary>
	public class ModelClientValidationMinValueRule : ModelClientValidationRule
	{
		public ModelClientValidationMinValueRule(string errorMessage, object minValue)
		{
			base.ErrorMessage = errorMessage;
			base.ValidationType = "min";
			base.ValidationParameters["min"] = minValue;
		}
	}
}
