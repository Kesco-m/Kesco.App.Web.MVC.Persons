using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Data;
using BLToolkit.Reflection;

namespace Kesco.DataAccess
{
	/// <summary>
	/// Определяет базовый класс для менеджеров данных
	/// </summary>
	/// <typeparam name="D"></typeparam>
	/// <typeparam name="A"></typeparam>
	[Obsolete("Не испошльзовать, рекомендуется использовать класс Database")]
	public static class Manager<D, A>
		where D : Database, new()
		where A : AccessorBase<D, A>
	{
		#region Accessor

		/// <summary>
		/// Gets the accessor.
		/// </summary>
		public static A Accessor
		{
			[System.Diagnostics.DebuggerStepThrough]
			get { return TypeAccessor<A>.CreateInstance(); }
		}

		#endregion
	}
}
