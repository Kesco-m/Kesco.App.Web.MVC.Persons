namespace Kesco.Web.Mvc.UI.Grid
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ToolBarSettings
    {

        public ToolBarSettings()
        {
            this.ShowEditButton = false;
            this.ShowAddButton = false;
            this.ShowDeleteButton = false;
            this.ShowSearchButton = false;
            this.ShowRefreshButton = false;
            this.ShowViewRowDetailsButton = false;
            this.ShowSearchToolBar = false;
            this.ToolBarAlign = ToolBarAlign.Left;
            this.ToolBarPosition = ToolBarPosition.Bottom;
            this.CustomButtons = new List<JQGridToolBarButton>();
        }

		// Properties
		public List<JQGridToolBarButton> CustomButtons { get; set; }
		public bool ShowAddButton { get; set; }
		public bool ShowDeleteButton { get; set; }
		public bool ShowEditButton { get; set; }
		public bool ShowRefreshButton { get; set; }
		public bool ShowSearchButton { get; set; }
		public bool ShowSearchToolBar { get; set; }
		public bool ShowViewRowDetailsButton { get; set; }
		public ToolBarAlign ToolBarAlign { get; set; }
		public ToolBarPosition ToolBarPosition { get; set; }
	
	}
}
