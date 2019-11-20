using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Kesco.Territories.BusinessLogic
{
	public static class Repository
	{
		public static TerritoryAccessor Territories { get { return TerritoryAccessor.Accessor; } }
	}
}
