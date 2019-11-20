namespace Kesco.Web.Mvc.UI.Grid
{
    using System;
    using System.Runtime.CompilerServices;

    public class AppearanceSettings
    {

		public bool ShowRowNumbers { get; set; }

		public int RowNumbersColumnWidth { get; set; }

		public bool AlternateRowBackground { get; set; }

		public string AlternateRowCssClass { get; set; }

		public bool HighlightRowsOnHover { get; set; }

		public string Caption { get; set; }

		public int ScrollBarOffset { get; set; }

		public bool RightToLeft { get; set; }

		public bool ShowFooter { get; set; }

		public bool ShrinkToFit { get; set; }

		public AppearanceSettings()
		{
			this.ShowRowNumbers = this.AlternateRowBackground = this.HighlightRowsOnHover = this.RightToLeft = this.ShowFooter = false;
			this.RowNumbersColumnWidth = 25;
			this.Caption = "";
			this.ScrollBarOffset = 18;
			this.ShrinkToFit = true;
		}
	}
}
