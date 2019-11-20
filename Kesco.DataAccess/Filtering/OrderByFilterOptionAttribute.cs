using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.DataAccess.Filtering
{

	/// <summary>
	/// Определяет класс-атрибут для фильтрации записей
	/// в заданном порядке.
	/// </summary>
	public class OrderByFilterOptionAttribute : FilterOptionAttribute
	{
		/// <summary>
		/// Строит подготовительный SQL-код для критерия фильтрации.
		/// </summary>
		/// <param name="context">контекст для атрибута критерия фильтрации.</param>
		/// <returns>
		/// SQL-код для критерия фильтрации, который должен быть выполнен перед основным запросом
		/// </returns>
		public override string BuildPrecode(FilterOptionAttributeContext context)
		{

			context.Context.OrderBy = context.Value as List<string>;

			return null;
		}

	}

}
