namespace Kesco.Web.Mvc.UI
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Web.Script.Serialization;

    public class ChartTitleSettings
    {

        public ChartTitleSettings()
        {
            this.Align = ChartHorizontalAlign.Center;
            this.Floating = false;
            this.Margin = 15;
            this.Text = "Chart Title";
            this.VerticalAlign = ChartVerticalAlign.Top;
            this.X = 0;
            this.Y = 0x19;
        }

        internal Hashtable ToHashtable()
        {
            Hashtable hashtable = new Hashtable();
            if (this.Align != ChartHorizontalAlign.Center)
            {
                hashtable.Add("align", this.Align.ToString().ToLower());
            }
            if (this.Floating)
            {
                hashtable.Add("floating", true);
            }
            if (this.Margin != 15)
            {
                hashtable.Add("margin", this.Margin);
            }
            if (!string.IsNullOrEmpty(this.Text))
            {
                hashtable.Add("text", this.Text);
            }
            if (this.VerticalAlign != ChartVerticalAlign.Top)
            {
                hashtable.Add("verticalAlign", this.VerticalAlign.ToString().ToLower());
            }
            if (this.X != 0)
            {
                hashtable.Add("x", this.X);
            }
            if (this.Y != 0x19)
            {
                hashtable.Add("y", this.Y);
            }
            return hashtable;
        }

        internal string ToJSON()
        {
            return new JavaScriptSerializer().Serialize(this.ToHashtable());
        }

		// Properties
		public ChartHorizontalAlign Align { get; set; }
		public bool Floating { get; set; }
		public int Margin { get; set; }
		public string Text { get; set; }
		public ChartVerticalAlign VerticalAlign { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
	}
}
