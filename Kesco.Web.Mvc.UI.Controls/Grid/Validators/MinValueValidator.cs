namespace Kesco.Web.Mvc.UI.Grid
{
    using System;
    using System.Runtime.CompilerServices;

    public class MinValueValidator : JQGridEditClientSideValidator
    {
		public MinValueValidator() {}

		public double MinValue { get; set; }
    }
}
