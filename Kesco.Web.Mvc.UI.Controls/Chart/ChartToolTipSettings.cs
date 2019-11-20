namespace Kesco.Web.Mvc.UI
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Web.Script.Serialization;

    public class ChartToolTipSettings
    {

        public ChartToolTipSettings()
        {
            this.BackgroundColor = "rgba(255, 255, 255, .85)";
            this.BorderColor = "auto";
            this.BorderRadius = 5;
            this.BorderWidth = 2;
            this.Formatter = "";
            this.Enabled = true;
            this.XAxisCrossHair = new ChartCrossHairSettings();
            this.YAxisCrossHair = new ChartCrossHairSettings();
        }

        internal string ToJSON()
        {
            Hashtable hashtable = new Hashtable();
            if (this.BackgroundColor != "rgba(255, 255, 255, .85)")
            {
                hashtable.Add("backgroundColor", this.BackgroundColor);
            }
            if (this.BorderColor != "auto")
            {
                hashtable.Add("borderColor", this.BorderColor);
            }
            if (this.BorderRadius != 5)
            {
                hashtable.Add("borderRadius", this.BorderRadius);
            }
            if (this.BorderWidth != 2)
            {
                hashtable.Add("borderWidth", this.BorderWidth);
            }
            ChartCrossHairSettings[] settingsArray = new ChartCrossHairSettings[] { this.XAxisCrossHair, this.YAxisCrossHair };
            hashtable.Add("crosshairs", settingsArray);
            if (!this.Enabled)
            {
                hashtable.Add("enabled", false);
            }
            return new JavaScriptSerializer().Serialize(hashtable);
        }

		// Properties
		public string BackgroundColor { get; set; }
		public string BorderColor { get; set; }
		public int BorderRadius { get; set; }
		public int BorderWidth { get; set; }
		public bool Enabled { get; set; }
		public string Formatter { get; set; }
		public ChartCrossHairSettings XAxisCrossHair { get; set; }
		public ChartCrossHairSettings YAxisCrossHair { get; set; }
	}
}
