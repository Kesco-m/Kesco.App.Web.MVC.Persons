namespace Kesco.Web.Mvc.UI.Grid
{
    using System;
    using System.Runtime.CompilerServices;

    public class NumberFormatter : JQGridColumnFormatter
    {
		// Properties
		public int DecimalPlaces { get; set; }
		public string DecimalSeparator { get; set; }
		public string DefaultValue { get; set; }
		public string ThousandsSeparator { get; set; }

    }
}
