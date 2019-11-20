using System.ComponentModel.DataAnnotations;

namespace Kesco.Web.Mvc.ComponentModel.DataAnnotations
{
	public class ValidationResultsAttribute : UIHintAttribute
	{
		public ValidationResultsAttribute(string uiHint = "ValidationResults")
			: base(uiHint)
		{
		}
	}
}