using System.Collections.Generic;
using BLToolkit.DataAccess;
using Kesco.DataAccess;
using Kesco.Territories.ObjectModel;
using Kesco.DataAccess.Filtering;

namespace Kesco.Territories.BusinessLogic
{

	[FilterSqlBuilder(
		TableOrViewName = "Инвентаризация.dbo.Территории",
		UniqueIdField = "КодТерритории",
		FieldList = "КодТерритории, Территория, Caption, Parent, L, R, Изменил, Изменено",
		ParametersType = typeof(SearchParameters)
	)]
	public abstract class TerritoryAccessor : EntityAccessor<TerritoryAccessor, DB, Territory, TerritoryAccessor.SearchParameters, int>
	{
		/// <summary>
		/// 
		/// </summary>
		public class SearchParameters : Kesco.DataAccess.SearchParameters
		{
			[HowSearchFilterOption]
			public int HowSearch { get; set; }

			/// <summary>
			/// Возвращает или устанавливает код территории.
			/// </summary>
			[InIntArrayFilterOption(FieldName = "КодТТерритории")]
			public List<int> AreaIDs { get; set; }

			[SearchStringFilterOption(FieldName = "Территория", FieldValueInRL = false, UseConvertionToRL = true)]
			internal string SearchNameRL { get { return Search; } }

			[PageSizeFilterOption(FieldName = "КодТерритории")]
			public int MaxEntries { get; set; }

			[OrderByFilterOption]
			public List<string> OrderBy { get; set; }

			public SearchParameters()
				: base()
			{
				AreaIDs = new List<int> { };
				OrderBy = new List<string> { "Территория" };
			}
		}

		public override List<Territory> Search(SearchParameters parameters)
		{
			return SearchInternal(parameters);
		}

		/// <summary>
		/// Осуществляет поиск территории в соответствии с заданными критериями.
		/// </summary>
		/// <param name="criteria">Критерии поиск/фильтрации.</param>
		/// <returns>Список бизнес-проектов, соответствующий критериям поиска.</returns>
		protected List<Territory> SearchTerritory(SearchParameters criteria)
		{
			return SearchInternal(criteria);
			//return SearchTerritory(criteria, criteria.Limit ?? System.Int32.MaxValue);
		}

//        [SqlQuery(@"
//			DECLARE @ter varchar(100)
//			SET @ter=dbo.fn_ReplaceRusLat(dbo.fn_ReplaceKeySymbols(@search))
//			SET @ter=COALESCE(@ter, '')
//
//			IF (@ter = '')
//				SELECT TOP {0} * FROM Территории
//				WHERE 
//					(@TAreaID = 0 OR КодТТерритории = @TAreaID)
//				ORDER BY Территория
//			ELSE
//				SELECT TOP {0} * FROM Территории
//				WHERE 
//					(@TAreaID = 0 OR КодТТерритории = @TAreaID) AND 
//					((dbo.fn_ReplaceRusLat(Территория) LIKE @ter+'%') 
//					OR (dbo.fn_ReplaceRusLat(Территория1) LIKE @ter+'%')
//					OR (dbo.fn_ReplaceRusLat(Caption) LIKE @ter+'%') 
//					OR (Caption1 LIKE @ter+'%')
//					OR (Аббревиатура = @ter) 
//					OR (ТелКодСтраны = @ter))
//				ORDER BY Территория
//			")]
//        protected abstract List<Territory> SearchTerritory(SearchParameters parameters, [Format(0)] int limit);

		[SqlQuery(@"
			SELECT * FROM Территории
			WHERE КодТТерритории = 1
			ORDER BY Территория
			")]
		public abstract List<Territory> GetAllCountries();

		/// <summary>
		/// Gets the phone code.
		/// </summary>
		/// <param name="areaID">The area ID.</param>
		/// <param name="full">Если установлено в <c>true</c> [full].</param>
		/// <returns></returns>
		[SqlQuery(@"		
			IF (@full IS NULL) SET @full = 0
						SELECT TOP 1 
							CASE КодТТерритории
								WHEN 2 THEN  ТелКодСтраны
								WHEN 5 THEN  ТелКодСтраны 
								ELSE 
									CASE WHEN @full = 1 THEN ТелКодСтраны ELSE '' END + SUBSTRING(ТелефонныйКод, ISNULL(LEN(ТелКодСтраны),0)+1, LEN(ТелефонныйКод))
							END
			FROM Территории INNER JOIN ТелефонныеКоды ON ТелефонныеКоды.КодТерритории = Территории.КодТерритории
						WHERE Территории.КодТерритории = @areaID 
		")]
		public abstract string GetPhoneCode(int areaID, bool full);

		/// <summary>
		/// Gets the area info.
		/// </summary>
		/// <param name="phone">The phone.</param>
		/// <returns></returns>
		[SqlQuery(@"  
			SET @phone=COALESCE(@phone, '')
			SELECT TOP 1 
				Направление,
				ТелКодСтраны ТелКодСтраны,
				SUBSTRING(ТелефонныйКод, ISNULL(LEN(ТелКодСтраны),0)+1, LEN(ТелефонныйКод)) ТелКодВСтране,
				ТелефонныеКоды.ДлинаКодаОбласти ДлинаКодаВСтране,
				Территория
			FROM ТелефонныеКоды INNER JOIN Территории ON ТелефонныеКоды.КодТерритории = Территории.КодТерритории
			WHERE (@phone LIKE ТелефонныеКоды.ТелефонныйКод + '%')
			ORDER BY LEN(ТелефонныеКоды.ТелефонныйКод) DESC
		")]
		public abstract AreaPhoneInfo GetAreaPhoneInfo(string phone);


	}
}
