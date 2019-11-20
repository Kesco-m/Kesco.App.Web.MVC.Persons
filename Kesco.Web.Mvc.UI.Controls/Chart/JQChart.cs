namespace Kesco.Web.Mvc.UI
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Web.Script.Serialization;

    public class JQChart
    {

        public JQChart()
        {
            this.AlignTicks = true;
            this.Animation = true;
            this.BackgroundColor = "#FFFFFF";
            this.BorderColor = "#4572A7";
            this.BorderRadius = 5;
            this.BorderWidth = 0;
            this.ClassName = "";
            this.Height = 350;
            this.ID = "";
            this.IgnoreHiddenSeries = true;
            this.Inverted = false;
            this.MarginTop = 0;
            this.MarginRight = 50;
            this.MarginBottom = 70;
            this.MarginLeft = 80;
            this.PlotBackgroundColor = "";
            this.PlotBackgroundImage = "";
            this.PlotBorderColor = "#C0C0C0";
            this.PlotBorderWidth = 0;
            this.PlotShadow = true;
            this.Reflow = true;
            this.SpacingBottom = 15;
            this.SpacingLeft = 10;
            this.SpacingRight = 10;
            this.SpacingTop = 10;
            this.ToolTip = new ChartToolTipSettings();
            this.Type = ChartType.Line;
            this.Width = 350;
            this.ZoomType = ChartZoomType.None;
            this.Title = new ChartTitleSettings();
            this.SubTitle = new ChartTitleSettings();
            this.XAxis = new ChartXAxisSettings();
            this.YAxis = new ChartYAxisSettings();
            this.Legend = new ChartLegendSettings();
            this.Series = new List<ChartSeriesSettings>();
        }

        internal string SeriesToJSON()
        {
            List<Hashtable> list = new List<Hashtable>();
            foreach (ChartSeriesSettings settings in this.Series)
            {
                Hashtable item = new Hashtable();
                if (!string.IsNullOrEmpty(settings.Name))
                {
                    item.Add("name", settings.Name);
                }
                if (settings.Data.Count<ChartPoint>() > 0)
                {
                    List<double?> list2 = new List<double?>();
                    foreach (ChartPoint point in settings.Data)
                    {
                        list2.Add(point.X);
                    }
                    item.Add("data", list2);
                }
                list.Add(item);
            }
            return new JavaScriptSerializer().Serialize(list);
        }

        internal string ToJSON()
        {
            Hashtable hashtable = new Hashtable();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            hashtable.Add("renderTo", this.ID);
            if (!this.AlignTicks)
            {
                hashtable.Add("alignKeys", false);
            }
            if (!this.Animation)
            {
                hashtable.Add("animation", false);
            }
            if (this.BackgroundColor != "#FFFFFF")
            {
                hashtable.Add("backgroundColor", this.BackgroundColor);
            }
            if (this.BorderColor != "#4572A7")
            {
                hashtable.Add("borderColor", this.BorderColor);
            }
            if (this.BorderRadius != 5)
            {
                hashtable.Add("borderRadius", this.BorderRadius);
            }
            if (this.BorderWidth != 0)
            {
                hashtable.Add("borderWidth", this.BorderWidth);
            }
            if (!string.IsNullOrEmpty(this.ClassName))
            {
                hashtable.Add("className", this.ClassName);
            }
            if (this.Height != 0)
            {
                hashtable.Add("height", this.Height);
            }
            if (!this.IgnoreHiddenSeries)
            {
                hashtable.Add("IgnoreHiddenSeries", false);
            }
            if (this.Inverted)
            {
                hashtable.Add("inverted", true);
            }
            if (this.MarginBottom != 70)
            {
                hashtable.Add("marginBottom", this.MarginBottom);
            }
            if (this.MarginLeft != 80)
            {
                hashtable.Add("marginLeft", this.MarginLeft);
            }
            if (this.MarginRight != 50)
            {
                hashtable.Add("marginRight", this.MarginRight);
            }
            if (this.MarginTop != 0)
            {
                hashtable.Add("marginTop", this.MarginTop);
            }
            if (!string.IsNullOrEmpty(this.PlotBackgroundColor))
            {
                hashtable.Add("plotBackgroundColor", this.PlotBackgroundColor);
            }
            if (!string.IsNullOrEmpty(this.PlotBackgroundImage))
            {
                hashtable.Add("plotBackgroundImage", this.PlotBackgroundImage);
            }
            if (this.PlotBorderColor != "#C0C0C0")
            {
                hashtable.Add("plotBorderColor", this.PlotBorderColor);
            }
            if (this.PlotBorderWidth != 0)
            {
                hashtable.Add("plotBorderWidth", this.PlotBorderWidth);
            }
            if (this.PlotShadow)
            {
                hashtable.Add("plotShadow", true);
            }
            if (!this.Reflow)
            {
                hashtable.Add("reflow", false);
            }
            if (this.Shadow)
            {
                hashtable.Add("shadow", true);
            }
            if (this.SpacingBottom != 15)
            {
                hashtable.Add("spacingBottom", this.SpacingBottom);
            }
            if (this.SpacingLeft != 10)
            {
                hashtable.Add("spacingLeft", this.SpacingLeft);
            }
            if (this.SpacingRight != 10)
            {
                hashtable.Add("spacingRight", this.SpacingRight);
            }
            if (this.SpacingTop != 10)
            {
                hashtable.Add("spacingTop", this.SpacingTop);
            }
            hashtable.Add("type", this.Type.ToString().ToLower());
            if (this.Width != 0)
            {
                hashtable.Add("width", this.Width);
            }
            if (this.ZoomType != ChartZoomType.None)
            {
                hashtable.Add("zoomType", this.ZoomType.ToString().ToLower());
            }
            return serializer.Serialize(hashtable);
        }

		// Properties
		public bool AlignTicks { get; set; }
		public bool Animation { get; set; }
		public string BackgroundColor { get; set; }
		public string BorderColor { get; set; }
		public int BorderRadius { get; set; }
		public int BorderWidth { get; set; }
		public string ClassName { get; set; }
		public int Height { get; set; }
		public string ID { get; set; }
		public bool IgnoreHiddenSeries { get; set; }
		public bool Inverted { get; set; }
		public ChartLegendSettings Legend { get; set; }
		public int MarginBottom { get; set; }
		public int MarginLeft { get; set; }
		public int MarginRight { get; set; }
		public int MarginTop { get; set; }
		public string PlotBackgroundColor { get; set; }
		public string PlotBackgroundImage { get; set; }
		public string PlotBorderColor { get; set; }
		public int PlotBorderWidth { get; set; }
		public bool PlotShadow { get; set; }
		public bool Reflow { get; set; }
		public List<ChartSeriesSettings> Series { get; set; }
		public bool Shadow { get; set; }
		public bool ShowAxes { get; set; }
		public int SpacingBottom { get; set; }
		public int SpacingLeft { get; set; }
		public int SpacingRight { get; set; }
		public int SpacingTop { get; set; }
		public ChartTitleSettings SubTitle { get; set; }
		public ChartTitleSettings Title { get; set; }
		public ChartToolTipSettings ToolTip { get; set; }
		public ChartType Type { get; set; }
		public int Width { get; set; }
		public ChartXAxisSettings XAxis { get; set; }
		public ChartYAxisSettings YAxis { get; set; }
		public ChartZoomType ZoomType { get; set; }

    }
}
