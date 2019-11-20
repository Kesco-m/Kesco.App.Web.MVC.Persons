using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.DataAccess.Filtering
{
	/// <summary>
	/// Определяет класс-атрибут для критерия фильтрации - строка поиска
	/// </summary>
	public class SearchStringFilterOptionAttribute : FilterOptionAttribute
	{
		class State
		{
			public string AdjustedValue { get; set; }
			public string[] Words { get; set; }
		}

		public string FieldNameAlias { get; set; }

		/// <summary>
		/// Возвращает или указывает флаг является ли значение поля в RL.
		/// </summary>
		/// <value>
		///   <c>true</c> если значение поля в RL; иначе, <c>false</c>.
		/// </value>
		public bool FieldValueInRL { get; set; }

		/// <summary>
		/// Возвращает или указывает флаг использовать конвертацию в RL
		/// для слов из строки поиска.
		/// </summary>
		/// <value>
		///   <c>true</c> если использовать конвертацию в RL; иначе, <c>false</c>.
		/// </value>
		public bool UseConvertionToRL { get; set; }

		/// <summary>
		/// Возвращает или устанавливает дополнительное выражение для конкатенации с полем поиска.
		/// В этом случае поиск будет производиться не только по полю, но и в строке выражения
		/// </summary>
		/// <value>
		/// Дополнительное выражение для конкатенации с полем поиска.
		/// </value>
		public string ConcatinationExpression { get; set; }

		/// <summary>
		/// Строит подготовительный SQL-код для критерия фильтрации - строка поиска.
		/// </summary>
		/// <param name="context">контекст для атрибута критерия фильтрации.</param>
		/// <returns>
		/// SQL-код для критерия фильтрации, который должен быть выполнен перед основным запросом
		/// </returns>
		public override string BuildPrecode(FilterOptionAttributeContext context)
		{
			var alias = FieldNameAlias ?? FieldName;
			var state = new State();
			context.State = state;
			state.AdjustedValue = ((context.Value == null) ? String.Empty : context.Value.ToString()).SplitWords();
			if (String.IsNullOrEmpty(state.AdjustedValue)) 
				return null;

			string howSearchExpression = context.Context.HowSearchDefined
			                             	? "CASE WHEN @ГдеИскать = 1 THEN ' ' ELSE '' END +"
			                             	: String.Empty;

			var list = state.Words = state.AdjustedValue.Split(' ');

			if (list.Length > 1) {
				//DECLARE @ИспользоватьRL_{0} bit SET @ИспользоватьRL_{0} = {1}
				//DECLARE @I_{0} int, @S_{0} varchar(200), @W_{0} varchar(200), @WRL_{0} varchar(200)
				//DECLARE @СтрокаПоиска_{0} varchar(4000) 

				context.Context.Declarations.Add(@"
				-- Таблица со словами для поиска в поле: {0}
				DECLARE @СписокСловДляПоискаПоПолю_{0} TABLE(Слово varchar(200))
				".FormatWith(alias, UseConvertionToRL ? 1 : 0));

				StringBuilder sql = new StringBuilder();

				list.All(word => {
					sql.AppendFormat(@"
					;INSERT @СписокСловДляПоискаПоПолю_{0} VALUES({3} {1}(Инвентаризация.dbo.fn_ReplaceKeySymbols('{2}')))
					", alias, UseConvertionToRL ? "Инвентаризация.dbo.fn_ReplaceRusLat" : String.Empty, word.Replace("'", "''"), howSearchExpression);

					return true;
				});

				return sql.ToString();
			} else {
				
				context.Context.Declarations.Add(@"
				-- Слово для поиска в поле: {0}
				DECLARE @СловоДляПоискаПоПолю_{0} varchar(200)
				".FormatWith(alias));

				return @"
				SET @СловоДляПоискаПоПолю_{0} = '%'+{3} {1}(Инвентаризация.dbo.fn_ReplaceKeySymbols('{2}'))+'%'
				".FormatWith(alias, UseConvertionToRL ? "Инвентаризация.dbo.fn_ReplaceRusLat" : String.Empty, state.Words[0].Replace("'", "''"), howSearchExpression);
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
			var alias = FieldNameAlias ?? FieldName;
			var state = context.State as State;
			if (state == null || state.AdjustedValue == null || state.AdjustedValue.Length == 0)
				return null;

			string fieldExpression =  String.Empty;
			string concatExpression = String.Empty;

			if (!String.IsNullOrEmpty(ConcatinationExpression)) {
				concatExpression = ConcatinationExpression;
			}
			if (!String.IsNullOrEmpty(FieldName)) {
				fieldExpression = "T0.{0}".FormatWith(FieldName);
				if (concatExpression.Length > 0)
					concatExpression = concatExpression.Insert(0, " + ");
			}

			fieldExpression = "{0}{1}".FormatWith(fieldExpression, concatExpression);

			if (UseConvertionToRL && !FieldValueInRL)
				fieldExpression = "Инвентаризация.dbo.fn_ReplaceRusLat({0})".FormatWith(fieldExpression);

			if (state.Words.Length > 1) {
				return @"((SELECT COUNT(*) FROM @СписокСловДляПоискаПоПолю_{0} WHERE ' '+{1} LIKE '%'+Слово+'%') = {2})"
					.FormatWith(alias, fieldExpression, state.Words.Length);
			}

			return @"(' '+{1} LIKE @СловоДляПоискаПоПолю_{0})"
				.FormatWith(alias, fieldExpression);
		}

	}
}
