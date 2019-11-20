namespace Kesco.Web.Mvc.UI.Grid
{
    using System;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;

    public class JQGridEditData
    {

		public NameValueCollection RowData
		{
			get;
			set;
		}

		public string RowKey
		{
			get;
			set;
		}

		public string ParentRowKey
		{
			get;
			set;
		}

    }
}
