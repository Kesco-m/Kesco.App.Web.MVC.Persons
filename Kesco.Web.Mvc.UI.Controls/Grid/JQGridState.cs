namespace Kesco.Web.Mvc.UI.Grid
{
    using System;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;

    public class JQGridState
    {

        public JQGridState()
        {
            this.QueryString = new NameValueCollection();
            this.CurrentPageOnly = false;
        }

		public bool CurrentPageOnly
		{
			get;
			set;
		}

        public NameValueCollection QueryString
        {
            get;
            set;
        }
    }
}
