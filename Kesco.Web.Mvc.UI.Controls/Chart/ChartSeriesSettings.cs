namespace Kesco.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ChartSeriesSettings
    {

        public ChartSeriesSettings()
        {
            this.Name = "";
            this.Data = null;
        }

		// Properties
		public IEnumerable<ChartPoint> Data { get; set; }
		public string Name { get; set; }

	}
}
