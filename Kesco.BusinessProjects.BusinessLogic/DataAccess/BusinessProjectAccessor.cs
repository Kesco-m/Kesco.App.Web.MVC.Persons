using System;
using System.Collections.Generic;
using BLToolkit.DataAccess;
using Kesco.DataAccess;
using Kesco.BusinessProjects.ObjectModel;
using Kesco.DataAccess.Filtering;

namespace Kesco.BusinessProjects.BusinessLogic
{

	/// <summary>
	/// Проводник данных для бизнес-проектов
	/// </summary>
	[FilterSqlBuilder(
		TableOrViewName = "Справочники.dbo.vwБизнесПроекты",
		UniqueIdField = "КодБизнесПроекта",
		FieldList = "КодБизнесПроекта, БизнесПроект",
		ParametersType = typeof(SearchParameters)
	)]
	public abstract class BusinessProjectAccessor : EntityAccessor<BusinessProjectAccessor, DB, BusinessProject, BusinessProjectAccessor.SearchParameters, int>
    {
		/// <summary>
		/// Параметры поиска/фильтрации для бизнес-проектов
		/// </summary>
		public class SearchParameters : Kesco.DataAccess.SearchParameters {

			/// <summary>
			/// Возвращает или устанавливает значение где искать совпадение со строкой поиска.
			/// Если значение равно 1, то совпадение должно начинаться со словом, иначе содержит слово
			/// </summary>
			/// <value>
			/// где искать совпадение со строкой поиска.
			/// </value>
			[HowSearchFilterOption]
			public int HowSearch { get; set; }

			/// <summary>
			/// Возвращает строку поиска.
			/// </summary>
			/// <value>
			/// Cтроку поиска.
			/// </value>
			/// <remarks>
			/// Свойство напрямую не используется, строка поиска задаётся в свойстве <see cref="Kesco.DataAccess.SearchParameters.Search"/> 
			/// Необходимо, чтобы установить атрибут-дескриптор по какому полю искать и преобразовывать ли в RL
			/// </remarks>
			[SearchStringFilterOption(FieldName = "БизнесПроект", FieldValueInRL = false, UseConvertionToRL = true)]
			internal string SearchName { get { return Search; } }

			/// <summary>
			/// Возвращает или устанавливает максимальное количество записей
			/// для возврата.
			/// </summary>
			/// <value>
			/// Максимальное количество записей.
			/// </value>
			[PageSizeFilterOption(FieldName = "КодБизнесПроекта")]
			public int MaxEntries { get; set; }

			/// <summary>
			/// Возвращает или устанавливает значение порядок сортировки
			/// </summary>
			/// <value>
			/// Порядок сортировки
			/// </value>
			[OrderByFilterOption]
			public List<string> OrderBy { get; set; }

			/// <summary>
			/// Initializes a new instance of the <see cref="SearchParameters" /> class.
			/// </summary>
			public SearchParameters()
				: base()
			{
				OrderBy = new List<string> { "БизнесПроект" };
			}
		}

		/// <summary>
		/// Осуществляет поиск бизнес-проектов в соответствии с заданными критериями.
		/// </summary>
		/// <param name="parameters">Критерии поиск/фильтрации.</param>
		/// <returns>Список бизнес-проектов, соответствующий критериям поиска.</returns>
		[SqlQuery(@"
			DECLARE @pattern varchar(100)
			SET @pattern = Инвентаризация.dbo.fn_ReplaceRusLat(Инвентаризация.dbo.fn_ReplaceKeySymbols(@search))+'%'
			SET @pattern = COALESCE(@pattern, '')
			SET @root = COALESCE(@root, 0)
			SELECT TOP {0} * 
			FROM Справочники.dbo.vwБизнесПроекты 
			WHERE 
				@pattern = '' OR Инвентаризация.dbo.fn_ReplaceRusLat(БизнесПроект) LIKE @pattern
			ORDER BY БизнесПроект
		")]
		[Obsolete]
		protected abstract List<BusinessProject> SearchBusinessProjects(object parameters, [Format(0)] int limit);

		/// <summary>
		/// Осуществляет поиск бизнес-проектов в соответствии с заданными критериями.
		/// </summary>
		/// <param name="criteria">Критерии поиск/фильтрации.</param>
		/// <returns>Список бизнес-проектов, соответствующий критериям поиска.</returns>
		public override List<BusinessProject> Search(SearchParameters criteria)
		{
			return SearchInternal(criteria);
		}
    }
}
