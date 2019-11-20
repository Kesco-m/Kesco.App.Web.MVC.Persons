using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kesco.ObjectModel;

namespace Kesco.DataAccess
{
	
	public static class Extensions
	{
		public static T GetInstance<D, A, T, TID>(this A accessor, TID id)
			where D : Database, new()
			where A : AccessorBase<D, A>
			where T : Entity<T, TID>, IUniqueID<TID>
		{
			return accessor.Query.SelectByKey<T>(id);
		}
	}
}
