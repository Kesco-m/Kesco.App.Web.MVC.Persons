using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.DataAccess.Filtering
{
	/// <summary>
	/// Определяет класс-атрибут для критерия фильтрации - строка поиска
	/// </summary>
	public class RootNodesFilterOptionAttribute : FilterOptionAttribute
	{
		class State
		{
			public int[] AdjustedValue { get; set; }
		}


		/// <summary>
		/// Строит подготовительный SQL-код для критерия фильтрации - строка поиска.
		/// </summary>
		/// <param name="context">контекст для атрибута критерия фильтрации.</param>
		/// <returns>
		/// SQL-код для критерия фильтрации, который должен быть выполнен перед основным запросом
		/// </returns>
		public override string BuildPrecode(FilterOptionAttributeContext context)
		{
			var state = new State();
			context.State = state;

			if (context.Value != null && context.Value is IEnumerable) {
				var defValue = default(int);
				var list = new List<int>();
				var values = context.Value as IEnumerable;
				foreach (var value in values) {
					if (value is IComparable && !value.Equals(defValue)) {
						list.Add(Convert.ToInt32(value));
					}
				}
				state.AdjustedValue = list.ToArray();
			}

			if (state.AdjustedValue == null || state.AdjustedValue.Length == 0)
				return null;


			var ids = state.AdjustedValue;

			if (ids.Length > 1) {

				context.Context.Declarations.Add(@"
				-- Таблица с индексами LR для ограничения поиска
				DECLARE @ГраницыLRДляОграниченияПоиска TABLE(L int, R int)
				".FormatWith(FieldName));

				StringBuilder sql = new StringBuilder();

				sql.AppendFormat(@"
				;INSERT @ГраницыLRДляОграниченияПоиска 
				SELECT L, R FROM $(TableName) WHERE $(KeyField) IN ({0})
				", String.Join(",", ids.Select(num => num.ToString()).ToArray()));

				return sql.ToString();
			} else {

				context.Context.Declarations.Add(@"
				-- Индексы LR: {0}
				DECLARE @Родительский_L int, @Родительский_R int
				".FormatWith(FieldName));

				return @"
				SELECT @Родительский_L = L, @Родительский_R = R FROM $(TableName) WHERE $(KeyField) = {0}
				".FormatWith(ids[0]);

			}

		}

		/// <summary>
		/// Строит предикат - SQL-код условия фильтрации в конструкции WHERE.
		/// </summary>
		/// <param name="context">контекст для атрибута критерия фильтрации.</param>
		/// <returns>
		/// SQL-код условия фильтрации в конструкции WHERE
		/// </returns>
		public override string BuildPredicate(FilterOptionAttributeContext context)
		{
			
			var state = context.State as State;
			if (state == null || state.AdjustedValue == null || state.AdjustedValue.Length == 0)
				return null;

			if (state.AdjustedValue.Length > 1) {
				return @"EXISTS(SELECT COUNT(*) FROM @ГраницыLRДляОграниченияПоиска WHERE $(TableAlias).L >= L AND  $(TableAlias).R <= R)";
			}

			return @"($(TableAlias).L >= @Родительский_L AND $(TableAlias).R <= @Родительский_R)";

		}

	}
}
