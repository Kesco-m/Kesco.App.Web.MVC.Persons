﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc.UI.Grid
{
	public class JQGridTreeExpandData
	{
		public int ParentLevel
		{
			get;
			set;
		}
		public string ParentID
		{
			get;
			set;
		}
		public JQGridTreeExpandData()
		{
			this.ParentLevel = -1;
			this.ParentID = null;
		}
	}
}
