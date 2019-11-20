namespace Kesco.Web.Mvc.UI.Grid
{
    using System;
    using System.Runtime.CompilerServices;

    public class CurrencyFormatter : JQGridColumnFormatter
    {
		public string ThousandsSeparator { get; set; }

		public string DefaultValue { get; set; }

		public string DecimalSeparator { get; set; }

		public int DecimalPlaces { get; set; }

		public string Prefix { get; set; }

		public string Suffix { get; set; }
	}
}
