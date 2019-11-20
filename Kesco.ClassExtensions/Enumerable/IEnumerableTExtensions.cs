using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco
{

	/// <summary>
	/// Класс-расширение, расширяющее типизированный перечислитель коллекции.
	/// </summary>
	public static class EnumerableExtensions
	{
		/// <summary>
		/// Возвращает индекс элемента в Indexes the of.
		/// </summary>
		/// <typeparam name="T">Тип перечислителя коллекции</typeparam>
		/// <param name="source">Перечислитель коллекции.</param>
		/// <param name="predicate">Функция-предикат, проверящая удовлетворяет ли элемент условиям</param>
		/// <returns>Индекс элемента в коллекции</returns>
		public static int IndexOf<T>(this IEnumerable<T> source, Func<T, bool> predicate)
		{
			if (source == null) throw new ArgumentNullException("source");
			if (predicate == null) throw new ArgumentNullException("predicate");

			var i = 0;
			foreach (var item in source) {
				if (predicate(item)) return i;
				i++;
			}

			return -1;
		}
	}

}
