using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kesco.DataAccess.Filtering;

namespace Kesco.Employees.BusinessLogic.Filtering
{
	public class EmployeeSearchStringFilterOptionAttribute : SearchStringFilterOptionAttribute
	{
		/// <summary>
		/// Строит подготовительный SQL-код для критерия фильтрации - строка поиска.
		/// </summary>
		/// <param name="context">контекст для атрибута критерия фильтрации.</param>
		/// <returns>
		/// SQL-код для критерия фильтрации, который должен быть выполнен перед основным запросом
		/// </returns>
		public override string BuildPrecode(FilterOptionAttributeContext context)
		{
			string value = ((context.Value == null) ? String.Empty : context.Value.ToString()).SplitWords();
			if (String.IsNullOrEmpty(value))
				return null;

			var list = value.Split(' ');

			var res = string.Empty;			
			for (int i = 0; i < list.Count(); i++)
			{
				context.Context.Declarations.Add(@"
				-- Слово для поиска в поле: {0}_{1}
				DECLARE @СловоДляПоискаПоПолю_{0}_{1} varchar(200)
				".FormatWith(FieldName, i + 1));

				res += @"
				SET @СловоДляПоискаПоПолю_{0}_{3} = '%'+{1}(Инвентаризация.dbo.fn_ReplaceKeySymbols('{2}'))+'%'
				".FormatWith(FieldName, UseConvertionToRL ? "Инвентаризация.dbo.fn_ReplaceRusLat" : String.Empty, list[i].Replace("'", "''"), i + 1);				
			}

			return res;
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
			var value = ((context.Value == null) ? String.Empty : context.Value.ToString()).SplitWords();
			if (value == null || value.Length == 0)
				return null;

			var list = value.Split(' ');
			//string concatExpression = String.Empty;

			//if (!String.IsNullOrEmpty(ConcatinationExpression))
			//{
			//	concatExpression = " + " + ConcatinationExpression;
			//}

			int checkID = 0;
			if (list.Length == 1) int.TryParse(list[0], out checkID);

			string res = string.Empty;

			switch (list.Length)
			{
				case 1:
					if (checkID > 0) res += "T0.КодСотрудника = {0} OR ".FormatWith(checkID);
					res += "(T0.ФамилияRL LIKE @СловоДляПоискаПоПолю_{0}_1 OR T0.LastName LIKE @СловоДляПоискаПоПолю_{0}_1 OR T0.ИмяRL LIKE @СловоДляПоискаПоПолю_{0}_1 OR T0.FirstName LIKE @СловоДляПоискаПоПолю_{0}_1)".FormatWith(FieldName);
					break;
				case 2:
					res += @"(
( (T0.ФамилияRL LIKE @СловоДляПоискаПоПолю_{0}_1 OR T0.LastName LIKE @СловоДляПоискаПоПолю_{0}_1) AND (T0.ИмяRL LIKE @СловоДляПоискаПоПолю_{0}_2 OR T0.FirstName LIKE @СловоДляПоискаПоПолю_{0}_2) ) OR
( (T0.ИмяRL LIKE @СловоДляПоискаПоПолю_{0}_1 OR T0.FirstName LIKE @СловоДляПоискаПоПолю_{0}_1) AND (T0.ФамилияRL LIKE @СловоДляПоискаПоПолю_{0}_2 OR T0.LastName LIKE @СловоДляПоискаПоПолю_{0}_2) ) OR
( (T0.ИмяRL LIKE @СловоДляПоискаПоПолю_{0}_1 OR T0.FirstName LIKE @СловоДляПоискаПоПолю_{0}_1) AND (T0.ОтчествоRL LIKE @СловоДляПоискаПоПолю_{0}_2 OR T0.MiddleName LIKE @СловоДляПоискаПоПолю_{0}_2) ) )".FormatWith(FieldName);
					break;
				case 3:
					res += @"(
( (T0.ФамилияRL LIKE @СловоДляПоискаПоПолю_{0}_1 OR T0.LastName LIKE @СловоДляПоискаПоПолю_{0}_1) AND (T0.ИмяRL LIKE @СловоДляПоискаПоПолю_{0}_2 OR T0.FirstName LIKE @СловоДляПоискаПоПолю_{0}_2) AND (T0.ОтчествоRL LIKE @СловоДляПоискаПоПолю_{0}_3 OR T0.MiddleName LIKE @СловоДляПоискаПоПолю_{0}_3) ) OR
( (T0.ИмяRL LIKE @СловоДляПоискаПоПолю_{0}_1 OR T0.FirstName LIKE @СловоДляПоискаПоПолю_{0}_1) AND (T0.ОтчествоRL LIKE @СловоДляПоискаПоПолю_{0}_2 OR T0.MiddleName LIKE @СловоДляПоискаПоПолю_{0}_2) AND (T0.ФамилияRL LIKE @СловоДляПоискаПоПолю_{0}_3 OR T0.LastName LIKE @СловоДляПоискаПоПолю_{0}_3) ) )".FormatWith(FieldName);
					break;
			}

			return res;
		}
	}
}