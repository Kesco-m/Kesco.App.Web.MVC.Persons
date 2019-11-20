using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Common;
using BLToolkit.Mapping;

namespace Kesco.ObjectModel
{
	/// <summary>
	/// Класс декларирует сущность с уникальным кодом.
	/// </summary>
	/// <typeparam name="T">Тип сущности.</typeparam>
	/// <typeparam name="TID">Тип уникального кода сущности.</typeparam>
	[Trimmable(false)]
	public abstract class Entity<T, TID> : EntityBase<T>, 
		IUnique, IUniqueID<TID>, IFriendlyNamed
		where T : Entity<T, TID>, IUniqueID<TID>
	{

		public abstract string GetInstanceFriendlyName();

		#region Члены IUnique

		/// <summary>
		/// Получить текстовое представление уникального кода
		/// </summary>
		/// <returns>Строка, представляющая уникальный код.</returns>
		public virtual string GetUniqueID()
		{
			return ID.ToString();
		}

		#endregion

		#region Члены IUniqueID<TID>

		/// <summary>
		/// Возвращает или устанавливает уникальный код сущности.
		/// </summary>
		/// <value>
		/// Уникальный код сущности.
		/// </value>
		public abstract TID ID { get; set; }

		#endregion

	}
}
