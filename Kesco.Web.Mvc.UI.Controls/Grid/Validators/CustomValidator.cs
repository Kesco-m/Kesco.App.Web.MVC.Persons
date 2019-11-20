namespace Kesco.Web.Mvc.UI.Grid
{
    using System;
    using System.Runtime.CompilerServices;

    public class CustomValidator : JQGridEditClientSideValidator
    {
		public string ValidationFunction { get; set; }
    }
}
