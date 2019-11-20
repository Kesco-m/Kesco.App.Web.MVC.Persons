using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using Kesco.Web.Mvc.ComponentModel.DataAnnotations;

namespace Kesco.Web.Mvc.Validation
{
	public class ValidationResults
	{		
		public List<object> Messages { get; set; }
		public string Delimiter { get; set; }
		public string ValidatedControlAttribute { get; set; }
	}
}
