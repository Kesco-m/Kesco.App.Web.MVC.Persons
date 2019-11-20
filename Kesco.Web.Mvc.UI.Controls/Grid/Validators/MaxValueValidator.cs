namespace Kesco.Web.Mvc.UI.Grid
{
    using System;
    using System.Runtime.CompilerServices;

    public class MaxValueValidator : JQGridEditClientSideValidator
    {
		public MaxValueValidator() { }

		public double MaxValue { get; set; }
    }
}
