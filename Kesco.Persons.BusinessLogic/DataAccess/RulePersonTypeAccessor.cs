using System.Collections.Generic;
using System.Threading;
using BLToolkit.DataAccess;
using Kesco.DataAccess.Filtering;
using Kesco.Persons.ObjectModel;
using Kesco.DataAccess;

namespace Kesco.Persons.BusinessLogic.DataAccess
{
	[FilterSqlBuilder(
		TableOrViewName = "ПраваТипыЛиц",
		UniqueIdField = "КодПраваТипыЛиц",
		FieldList = "КодПраваТипыЛиц, КодСотрудника, КодКаталога, КодТемыЛица, МожетДаватьПрава",
		ParametersType = typeof(SearchParameters)
	)]
    public abstract class RulePersonTypeAccessor : EntityAccessor<RulePersonTypeAccessor, DB, RulePersonType, RulePersonTypeAccessor.SearchParameters, int>
    {

        public class SearchParameters : Kesco.DataAccess.SearchParameters
        {
            /// <summary>
            /// Gets or sets the how search.
            /// </summary>
            /// <value>
            /// The how search.
            /// </value>
            [HowSearchFilterOption]
            public int HowSearch { get; set; }

            /// <summary>
            /// Gets the search string.
            /// </summary>
            /// <value>
            /// The search string.
            /// </value>
			[SearchStringFilterOption(FieldName = "КодПраваТипыЛиц"
                , FieldValueInRL = false, UseConvertionToRL = false)]
            internal string SearchString { get { return Search; } }

			[InIntArrayFilterOption(FieldName = "КодПраваТипыЛиц")]
            public List<int> IDs { get; set; }

			[InIntArrayFilterOption(FieldName = "КодСотрудника")]
			public List<int> EmployeeIDs { get; set; }

            /// <summary>
            /// Возвращает или устанавливает критерий поиска - максимальное кол-во записей.
            /// В результат поиска попадёт заданное максимальное кол-во записей.
            /// </summary>
            /// <value>
            /// Максимальное кол-во записей.
            /// </value>
			[PageSizeFilterOption(FieldName = "КодПраваТипыЛиц")]
            public int MaxEntries { get; set; }

            /// <summary>
            /// Возвращает или устанавливает первоначальный индекс, 
            /// с которой вернуть записи, соотвествующие заданным критериям поиска.
            /// </summary>
            /// <value>
            /// Первоначальный индекс, с которой вернуть записи
            /// </value>
			[RowStartIndexFilterOption(FieldName = "КодПраваТипыЛиц")]
            public int RowStartIndex { get; set; }

            /// <summary>
            /// Возвращает или устанавливает порядок сортировки.
            /// </summary>
            /// <value>
            /// The order by.
            /// </value>
            [OrderByFilterOption]
            public List<string> OrderBy { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="SearchParameters" /> class.
            /// </summary>
            public SearchParameters()
                : base()
            {
                IDs = new List<int>();

                OrderBy = new List<string>();
                if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "en")
					OrderBy.Add("КодПраваТипыЛиц");
                else
					OrderBy.Add("КодПраваТипыЛиц");
            }
        }

        public override List<RulePersonType> Search(SearchParameters criteria)
        {
            return SearchInternal(criteria);
        }
		

	}
}
