namespace Kesco.Web.Mvc.UI
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Web.Script.Serialization;

    public class ChartAxisLabelSettings
    {

        public ChartAxisLabelSettings()
        {
            this.Align = ChartHorizontalAlign.Center;
            this.Enabled = true;
            this.Formatter = "";
            this.Rotation = 0;
            this.StaggerLines = 0;
            this.Step = 0;
            this.X = 0;
            this.Y = 0;
        }

        internal Hashtable ToHashtable()
        {
            Hashtable hashtable = new Hashtable();
            if (this.Align != ChartHorizontalAlign.Center)
            {
                hashtable.Add("align", this.Align.ToString().ToLower());
            }
            if (!this.Enabled)
            {
                hashtable.Add("enabled", false);
            }
            if (this.Rotation != 0)
            {
                hashtable.Add("rotation", this.Rotation);
            }
            if (this.StaggerLines != 0)
            {
                hashtable.Add("staggerLines", this.StaggerLines);
            }
            if (this.Step != 0)
            {
                hashtable.Add("step", this.Step);
            }
            if (this.X != 0)
            {
                hashtable.Add("x", this.X);
            }
            if (this.Y != 0)
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
		public ChartHorizontalAlign Align {  get;  set; }
		public bool Enabled {  get;  set; }
		public string Formatter {  get;  set; }
		public int Rotation {  get;  set; }
		public int StaggerLines {  get;  set; }
		public int Step {  get;  set; }
		public int X {  get;  set; }
		public int Y {  get;  set; }
	}
}
