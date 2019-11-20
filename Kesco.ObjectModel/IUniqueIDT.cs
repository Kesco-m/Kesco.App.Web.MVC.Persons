using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.ObjectModel
{
	/// <summary>
	/// Декларирует интерфейс для объекта, 
	/// поддерживающего уникальный идентификатор
	/// </summary>
	/// <remarks>
	/// Многие объекты, хранящиеся в базе данных, имеют уникальный идентификатор
	/// </remarks>
	public interface IUniqueID<T>
	{
		/// <summary>
		/// Возвращает значение уникального идентификатора
		/// </summary>
		T ID { get; }
	}
}
