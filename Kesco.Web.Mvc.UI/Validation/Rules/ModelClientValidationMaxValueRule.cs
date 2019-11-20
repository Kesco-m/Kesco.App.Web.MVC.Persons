using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Kesco.Web.Mvc.Validation
{
	public class ModelClientValidationMaxValueRule : ModelClientValidationRule
	{
		public ModelClientValidationMaxValueRule(string errorMessage, object maxValue)
		{
			base.ErrorMessage = errorMessage;
			base.ValidationType = "max";
			base.ValidationParameters["max"] = maxValue;
		}
	}
}
