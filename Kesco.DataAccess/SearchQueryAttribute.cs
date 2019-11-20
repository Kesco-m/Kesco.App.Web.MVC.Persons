using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.DataAccess;
using BLToolkit.Data;

namespace Kesco.DataAccess
{
	public class SearchQueryAttribute : SqlQueryAttribute
	{
		public SearchQueryAttribute()
		{
			IsDynamic = true;
		}

		public override string GetSqlText(DataAccessor accessor, DbManager dbManager)
		{
			throw new ApplicationException(string.Format("Unknown data provider '{0}'", dbManager.DataProvider.Name));
		}
	}
}
