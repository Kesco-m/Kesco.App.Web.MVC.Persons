namespace Kesco.Web.Mvc.UI.Grid
{
    using System;
    using System.Runtime.CompilerServices;

    public class IntegerFormatter : JQGridColumnFormatter
    {

		public IntegerFormatter() { }

		public string DefaultValue { get; set; }

        public string ThousandsSeparator { get; set; }
    }
}
