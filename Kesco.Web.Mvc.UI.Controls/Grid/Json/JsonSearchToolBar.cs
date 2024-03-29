﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.Script.Serialization;

namespace Kesco.Web.Mvc.UI.Grid
{
	internal class JsonSearchToolBar
	{
		private Hashtable _jsonValues;
		private JQGrid _grid;
		public JsonSearchToolBar(JQGrid grid)
		{
			this._jsonValues = new Hashtable();
			this._grid = grid;
		}
		public string Process()
		{
			SearchToolBarSettings searchToolBarSettings = this._grid.SearchToolBarSettings;
			if (searchToolBarSettings.SearchToolBarAction == SearchToolBarAction.SearchOnKeyPress) {
				this._jsonValues["searchOnEnter"] = false;
			}
			return new JavaScriptSerializer().Serialize(this._jsonValues);
		}
	}
}
