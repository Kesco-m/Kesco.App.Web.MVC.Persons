using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.ApplicationServices
{
	public static class InventoryManager
	{

		public static string GetEmployeeLastNameWithInitials(int id)
		{
			return Accessor.GetEmployeeLastNameWithInitials(id, 0);
		}

		#region Accessor

		static InventoryAccessor Accessor
		{
			[System.Diagnostics.DebuggerStepThrough]
			get { return InventoryAccessor.CreateInstance(); }
		}

		#endregion
	}
}
