namespace Kesco.Web.Mvc.UI
{
    using System;
    using System.Runtime.CompilerServices;

    public class ChartPoint
    {

        public ChartPoint()
        {
            this.X = null;
            this.Y = null;
        }

        public ChartPoint(double? x) : this()
        {
            this.X = x;
        }

        public ChartPoint(double? x, double? y) : this()
        {
            this.X = x;
            this.Y = y;
        }

		// Properties
		public double? X { get; set; }
		public double? Y { get; set; }
	}
}
