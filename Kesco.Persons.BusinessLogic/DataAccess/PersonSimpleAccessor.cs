using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using BLToolkit.DataAccess;
using Kesco.DataAccess;
using Kesco.DataAccess.Filtering;
using Kesco.Persons.ObjectModel;

namespace Kesco.Persons.BusinessLogic.DataAccess
{

	/// <summary>
	/// Класс-ассессор для данных типа <see cref="PersonSimple"/>
	/// </summary>
	[FilterSqlBuilderAttribute(
			TableOrViewName = "Справочники.dbo.vwЛица",
			UniqueIdField = "КодЛица",
			FieldList = "КодЛица, Кличка, ИНН",
			ParametersType = typeof(SearchParameters)
		)]
	public abstract class PersonSimpleAccessor : EntityAccessor<PersonSimpleAccessor, DB, PersonSimple, PersonSimpleAccessor.SearchParameters, int>
	{

		/// <summary>
		/// Список параметров для фильтрации/поиска
		/// </summary>
		public class SearchParameters : Kesco.DataAccess.SearchParameters
		{
			/// <summary>
			/// Возвращает или устанавливает критерий фильтрации, 
			/// как искать совпадение в строке 
			/// 0 - содержит
			/// 1 - начинаются с
			/// </summary>
			/// <value>
			/// Как искать совпадение в строке.
			/// </value>
			[HowSearchFilterOption]
			public int HowSearch { get; set; }

			/// <summary>
			/// Возвращает или устанавливает критерий фильтрации
			/// - список кодов лиц, среди которых искать лица
			/// </summary>
			/// <value>
			/// Список кодов лиц
			/// </value>
			[InIntArrayFilterOption(FieldName = "КодЛица")]
			public List<int> IDs { get; set; }

			/// <summary>
			/// Возвращает или устанавливает критерий фильтрации
			/// - список кодов лиц, которые не должны попадать 
			/// в результат
			/// </summary>
			/// <value>
			/// Список кодов лиц
			/// </value>
			[NotInIntArrayFilterOption(FieldName = "КодЛица")]
			public List<int> NotIDs { get; set; }

			/// <summary>
			/// Возвращает или устанавливает критерий фильтрации
			/// - строка поиска среди кличек лиц
			/// </summary>
			/// <value>
			/// Строка поиска
			/// </value>
			[SearchStringFilterOption(FieldName = "НазваниеRL", FieldValueInRL = true, UseConvertionToRL = true)]
			protected string SearchNameRL { get { return Search; } }

			/// <summary>
			/// Возвращает или устанавливает критерий фильтрации
			/// - Список типов лиц
			/// </summary>
			/// <value>
			/// Список типов лиц.
			/// </value>
			[InIntArrayFilterOption(FieldName = "ТипЛица")]
			public List<int> PersonTypes { get; set; }

			/// <summary>
			/// Возвращает или устанавливает критерий фильтрации
			/// - искать ли среди гос. организаций или нет
			/// </summary>
			/// <value>
			/// Искать ли среди гос. организаций или нет. Если <c>null</c>, то параметр не учитывается
			/// </value>
			[BooleanFilterOption(FieldName = "ГосОрганизация")]
			public bool? IsState { get; set; }

			/// <summary>
			/// Возвращает или устанавливает критерий фильтрации
			/// - достоверность лица - искать ли среди провереных лиц или нет
			/// </summary>
			/// <value>
			/// Достоверность лица - искать ли среди провереных лиц или нет
			/// Если <c>null</c>, то параметр не учитывается
			/// </value>
			[BooleanFilterOption(FieldName = "Проверено")]
			public bool? IsChecked { get; set; }

			/// <summary>
			/// Возвращает или устанавливает критерий фильтрации
			/// - искать ли среди банков или нет 			
			/// </summary>
			/// <value>
			/// Искать ли среди банков или нет. Если <c>null</c>, то параметр не учитывается
			/// </value>
			[BooleanFilterOption(FieldName = "ТипЛица"
					, CustomTrueExpression = "(T0.БИК<>'' OR T0.SWIFT<>'')"
					, CustomFalseExpression = "(T0.БИК='' AND T0.SWIFT='')"
				)]
			public bool? IsBank { get; set; }

			/// <summary>
			/// Возвращает или устанавливает критерий фильтрации
			/// - список ИНН лиц, среди которых искать лица			
			/// </summary>
			/// <value>
			/// Список ИНН лиц
			/// </value>
			[InStringArrayFilterOption(FieldName = "ИНН")]
			public List<string> INNs { get; set; }

			/// <summary>
			/// Возвращает или устанавливает критерий фильтрации
			/// - список ИНН лиц, которые не должны попадать 
			/// в результат			
			/// </summary>
			/// <value>
			/// Список ИНН лиц.
			/// </value>
			[NotInStringArrayFilterOption(FieldName = "ИНН")]
			public List<string> NotINNs { get; set; }

			/// <summary>
			/// Возвращает или устанавливает критерий фильтрации
			/// - список кодов территоррий (стран) для лиц, которые 
			/// должны попадать в результат
			/// </summary>
			/// <value>
			/// список кодов территоррий (стран)
			/// </value>
			[InIntArrayFilterOption(FieldName = "КодТерритории")]
			public List<int> TerritoryIDs { get; set; }

			/// <summary>
			/// Возвращает или устанавливает критерий фильтрации
			/// - список кодов территоррий (стран) для лиц, которые 
			/// НЕ ДОЛЖНЫ попадать в результат
			/// </summary>
			/// <value>
			/// список кодов территоррий (стран)
			/// </value>
			[NotInIntArrayFilterOption(FieldName = "КодТерритории")]
			public List<int> NotTerritoryIDs { get; set; }

			/// <summary>
			/// Возвращает или устанавливает критерий фильтрации
			/// - список кодов бизнес-проектов для лиц, которые 
			/// должны попадать в результат
			/// </summary>
			/// <value>
			/// список кодов бизнес-проектов для лиц
			/// </value>
			[InIntArrayFilterOption(FieldName = "КодБизнесПроекта")]
			public List<int> BusinessProjectIDs { get; set; }

			/// <summary>
			/// Возвращает или устанавливает критерий фильтрации
			/// - список кодов бизнес-проектов для лиц, которые 
			/// НЕ ДОЛЖНЫ попадать в результат
			/// </summary>
			/// <value>
			/// список кодов бизнес-проектов для лиц
			/// </value>
			[NotInIntArrayFilterOption(FieldName = "КодБизнесПроекта")]
			public List<int> NotBusinessProjectIDs { get; set; }

			/// <summary>
			/// Gets or sets the start index of the row.
			/// </summary>
			/// <value>
			/// The start index of the row.
			/// </value>
			[RowStartIndexFilterOption(FieldName = "КодЛица")]
			public int RowStartIndex { get; set; }

			/// <summary>
			/// Gets or sets the max entries.
			/// </summary>
			/// <value>
			/// The max entries.
			/// </value>
			[PageSizeFilterOption(FieldName = "КодЛица")]
			public int MaxEntries { get; set; }

			/// <summary>
			/// Gets or sets the order by.
			/// </summary>
			/// <value>
			/// The order by.
			/// </value>
			[OrderByFilterOption()]
			public List<string> OrderBy { get; set; }

			/// <summary>
			/// Initializes a new instance of the <see cref="SearchParameters" /> class.
			/// </summary>
			public SearchParameters()
				: base()
			{
				IDs = new List<int>();
				NotIDs = new List<int>();
				PersonTypes = new List<int>();
				INNs = new List<string>();
				NotINNs = new List<string>();
				TerritoryIDs = new List<int>();
				NotTerritoryIDs = new List<int>();
				BusinessProjectIDs = new List<int>();
				NotBusinessProjectIDs = new List<int>();
				OrderBy = new List<string> { "T0.Кличка" };
			}
		}

		/// <summary>
		/// Searches the specified criteria.
		/// </summary>
		/// <param name="criteria">The criteria.</param>
		/// <returns></returns>
		public override List<PersonSimple> Search(SearchParameters criteria)
		{
			return SearchInternal(criteria);
		}

	}
}
