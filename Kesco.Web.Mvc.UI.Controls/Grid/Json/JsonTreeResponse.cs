using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Kesco.Web.Mvc.UI.Grid
{
	internal class JsonTreeResponse
	{
		public int page
		{
			get;
			set;
		}
		
		public int total
		{
			get;
			set;
		}
		
		public int records
		{
			get;
			set;
		}
		
		public Hashtable[] rows
		{
			get;
			set;
		}

		public Hashtable userdata
		{
			get;
			set;
		}
		
		public JsonTreeResponse()
		{
		}
		
		public JsonTreeResponse(int currentPage, int totalPagesCount, int totalRowCount, int pageSize, int actualRows, Hashtable userData)
		{
			this.page = currentPage;
			this.total = totalPagesCount;
			this.records = totalRowCount;
			this.rows = new Hashtable[actualRows];
			this.userdata = userData;
		}
	}
}
