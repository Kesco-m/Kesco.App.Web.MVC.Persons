namespace Kesco.Web.Mvc.UI
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Web.Script.Serialization;

    public class ChartBaseAxisSettings
    {

        public ChartBaseAxisSettings()
        {
            this.AllowDecimals = true;
            this.AlternateGridColor = "";
            this.Categories = new List<string>();
            this.EndOnTick = false;
            this.GridLineColor = "#C0C0C0";
            this.GridLineDashStyle = ChartGridLineDashStyle.ShortDot;
            this.GridLineWidth = 0;
            this.ID = "";
            this.Labels = new ChartAxisLabelSettings();
            this.LineColor = "#C0D0E0";
            this.LineWidth = 1;
            this.LinkedTo = 0;
            this.Max = double.PositiveInfinity;
            this.MaxPadding = 0.01;
            this.MaxZoom = 0;
            this.Min = double.NegativeInfinity;
            this.MinorGridLineColor = "#E0E0E0";
            this.TickInterval = null;
            this.TickWidth = 1;
            this.Title = new ChartTitleSettings();
        }

        internal string ToJSON()
        {
            Hashtable hashtable = new Hashtable();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            if (!this.AllowDecimals)
            {
                hashtable.Add("allowDecimals", false);
            }
            if (!string.IsNullOrEmpty(this.AlternateGridColor))
            {
                hashtable.Add("alternateGridColor", this.AlternateGridColor);
            }
            if (this.Categories.Count > 0)
            {
                hashtable.Add("categories", this.Categories);
            }
            if (this.EndOnTick)
            {
                hashtable.Add("endOnTick", true);
            }
            if (this.GridLineColor != "#C0C0C0")
            {
                hashtable.Add("gridLineColor", this.GridLineColor);
            }
            if (this.GridLineDashStyle != ChartGridLineDashStyle.ShortDot)
            {
                hashtable.Add("gridLineDashStyle", this.GridLineDashStyle.ToString().ToLower());
            }
            if (this.GridLineWidth != 0)
            {
                hashtable.Add("gridLineWidth", this.GridLineWidth);
            }
            if (!string.IsNullOrEmpty(this.ID))
            {
                hashtable.Add("id", this.ID);
            }
            if (this.LineColor != "#C0D0E0")
            {
                hashtable.Add("lineColor", this.LineColor);
            }
            if (this.LineWidth != 1)
            {
                hashtable.Add("lineWidth", this.LineWidth);
            }
            if (this.LinkedTo != 0)
            {
                hashtable.Add("linkedTo", this.LinkedTo);
            }
            if (this.Max != double.PositiveInfinity)
            {
                hashtable.Add("max", this.Max);
            }
            if (this.MaxPadding != 0.01)
            {
                hashtable.Add("maxPadding", this.MaxPadding);
            }
            if (this.MaxZoom != 0)
            {
                hashtable.Add("maxZoom", this.MaxZoom);
            }
            if (this.Min != double.NegativeInfinity)
            {
                hashtable.Add("min", this.Min);
            }
            if (this.MinorGridLineColor != "#E0E0E0")
            {
                hashtable.Add("minorGridLineColor", this.MinorGridLineColor);
            }
            hashtable.Add("title", this.Title.ToHashtable());
            if (this.TickInterval.HasValue)
            {
                hashtable.Add("tickInterval", this.TickInterval);
            }
            if (this.TickWidth != 1)
            {
                hashtable.Add("tickWidth", this.TickWidth);
            }
            if (this.Type != ChartAxisType.Linear)
            {
                hashtable.Add("type", this.Type.ToString().ToLower());
            }
            return serializer.Serialize(hashtable);
        }

		// Properties
		public bool AllowDecimals { get; set; }
		public string AlternateGridColor { get; set; }
		public List<string> Categories { get; set; }
		public bool EndOnTick { get; set; }
		public string GridLineColor { get; set; }
		public ChartGridLineDashStyle GridLineDashStyle { get; set; }
		public int GridLineWidth { get; set; }
		public string ID { get; set; }
		public ChartAxisLabelSettings Labels { get; set; }
		public string LineColor { get; set; }
		public int LineWidth { get; set; }
		public int LinkedTo { get; set; }
		public double Max { get; set; }
		public double MaxPadding { get; set; }
		public int MaxZoom { get; set; }
		public double Min { get; set; }
		public string MinorGridLineColor { get; set; }
		public int? TickInterval { get; set; }
		public int TickWidth { get; set; }
		public ChartTitleSettings Title { get; set; }
		public ChartAxisType Type { get; set; }
	}
}
