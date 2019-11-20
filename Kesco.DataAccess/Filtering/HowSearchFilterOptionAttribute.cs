using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.DataAccess.Filtering
{

	/// <summary>
	/// Определяет класс-атрибут для логического критерия фильтрации 
	/// по заданному полю.
	/// </summary>
	public class HowSearchFilterOptionAttribute : FilterOptionAttribute
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

			/*if (!context.Context.HowSearchDefined) {
				context.Context.Declarations.Add(@"
				DECLARE @ГдеИскать INT SET @ГдеИскать = 0
				");
				context.Context.HowSearchDefined = true;
			}

			if (context.Value is int) {
				return @"
				SET @ГдеИскать = {0}
				".FormatWith(context.Value);
			}*/

			return null;
		}

	}

}
