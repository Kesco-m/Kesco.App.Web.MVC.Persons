using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Territories.BusinessLogic
{
	public class AreaPhoneInfo
	{
		public string Направление { get; set; }
		public string ТелКодСтраны { get; set; }
		public string ТелКодВСтране { get; set; }
		public int? ДлинаКодаВСтране { get; set; }
		public string Территория { get; set; }

		public AreaPhoneInfo()
		{
			ДлинаКодаВСтране = 0;
			Направление =
			ТелКодСтраны =
			ТелКодВСтране =
			Территория = String.Empty;
		}
	}
}
