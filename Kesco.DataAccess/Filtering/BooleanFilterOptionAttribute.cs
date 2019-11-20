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
	public class BooleanFilterOptionAttribute : FilterOptionAttribute
	{
		class State
		{
			public bool? AdjustedValue { get; set; }
		}

		public string CustomTrueExpression { get; set; }
		
		public string CustomFalseExpression { get; set; }

		/// <summary>
		/// Строит подготовительный SQL-код для критерия фильтрации.
		/// </summary>
		/// <param name="context">контекст для атрибута критерия фильтрации.</param>
		/// <returns>
		/// SQL-код для критерия фильтрации, который должен быть выполнен перед основным запросом
		/// </returns>
		public override string BuildPrecode(FilterOptionAttributeContext context)
		{
			var state = new State();
			context.State = state;
			state.AdjustedValue = context.Value as bool?;
			return null;
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
			if (state == null || state.AdjustedValue == null || !state.AdjustedValue.HasValue)
				return null;

			if (!String.IsNullOrWhiteSpace(CustomTrueExpression) && state.AdjustedValue.Value)
				return String.Format(CustomTrueExpression, FieldName);

			if (!String.IsNullOrWhiteSpace(CustomFalseExpression) && !state.AdjustedValue.Value)
				return String.Format(CustomFalseExpression, FieldName);

			string expression = String.Format(" = {0}", state.AdjustedValue.Value ? 1 : 0);
			return String.Format("(T0.{0} {1})", FieldName, expression);
		}

	}

}
