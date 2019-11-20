namespace Kesco.Web.Mvc.UI.Grid
{
    using System;
    using System.Runtime.CompilerServices;

    public class PagerSettings
    {
		public int PageSize { get; set; }
		public int CurrentPage { get; set; }
		public string PageSizeOptions { get; set; }
		public string NoRowsMessage { get; set; }
		public bool ScrollBarPaging { get; set; }
		public string PagingMessage { get; set; }

		public bool ShowPageInputBox { get; set; } // !!! FDV 
		public bool ShowPagerButtons { get; set; } // !!! FDV 

		public PagerSettings()
		{
			this.PageSize = 30;
			this.CurrentPage = 1;
			this.PageSizeOptions = "[30,50,100]";
			this.NoRowsMessage = "";
			this.ScrollBarPaging = false;
			this.PagingMessage = "";
			this.ShowPagerButtons = true; // !!! FDV 
			this.ShowPageInputBox = true; // !!! FDV 

		}
	}
}
