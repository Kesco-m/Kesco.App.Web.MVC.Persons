using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc.UI.Grid
{
	public enum SearchToolBarAction
	{
		SearchOnEnter,
		SearchOnKeyPress
	}
	
	public class SearchToolBarSettings
	{
		public SearchToolBarAction SearchToolBarAction	{ get; set; }

		public SearchToolBarSettings()
		{
			this.SearchToolBarAction = SearchToolBarAction.SearchOnEnter;
		}
	}
}
