using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BLToolkit.DataAccess;
using Kesco.DataAccess;
using Kesco.DataAccess.Filtering;
using Kesco.Employees.ObjectModel;

namespace Kesco.Employees.BusinessLogic.DataAccess
{
	[FilterSqlBuilder(
		TableOrViewName = "РолиСотрудников",
		UniqueIdField = "КодРоли",
        FieldList = "КодРоли, КодСотрудника, КодЛица, Кличка",
		ParametersType = typeof (SearchParameters)
		)]
	public abstract class EmployeesRolesAccessor :
		EntityAccessor
			<EmployeesRolesAccessor, DB, EmployeesRoles,
			EmployeesRolesAccessor.SearchParameters, int>
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
			[SearchStringFilterOption(FieldName = "КодРоли"
				, FieldValueInRL = false, UseConvertionToRL = false)]
			internal string SearchString
			{
				get { return Search; }
			}

			[InIntArrayFilterOption(FieldName = "КодРоли")]
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
			[PageSizeFilterOption(FieldName = "КодРоли")]
			public int MaxEntries { get; set; }

			/// <summary>
			/// Возвращает или устанавливает первоначальный индекс, 
			/// с которой вернуть записи, соотвествующие заданным критериям поиска.
			/// </summary>
			/// <value>
			/// Первоначальный индекс, с которой вернуть записи
			/// </value>
			[RowStartIndexFilterOption(FieldName = "КодРоли")]
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
            /// Возвращает имя роли.
            /// </summary>
            /// <value>
            /// Возвращает имя роли.
            /// </value>
            [SearchStringFilterOption(FieldName = "Роль")]
            public List<string> RoleName { get; set; }

			/// <summary>
			/// Initializes a new instance of the <see cref="SearchParameters" /> class.
			/// </summary>
			public SearchParameters()
				: base()
			{
				IDs = new List<int>();
				EmployeeIDs = new List<int>();

				OrderBy = new List<string>();
				//if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "en")
				//	OrderBy.Add("КодРоли");
				//else
					OrderBy.Add("КодРоли");
			}
		}

        [SqlQuery(@"
            SELECT РолиСотрудников.КодРоли, РолиСотрудников.КодСотрудника, РолиСотрудников.КодЛица, vwРоли.Роль, ISNULL(ЛицаЗаказчики.Кличка, '') Кличка from Инвентаризация.dbo.РолиСотрудников
            INNER JOIN Инвентаризация.dbo.vwРоли ON РолиСотрудников.КодРоли = vwРоли.КодРоли
            LEFT JOIN Инвентаризация.dbo.ЛицаЗаказчики ON РолиСотрудников.КодЛица = ЛицаЗаказчики.КодЛица
            where КодСотрудника = @id
        ")]
        public abstract List<EmployeesRoles> GetAllEmployeRoles(int id);

		public override List<EmployeesRoles> Search(SearchParameters criteria)
		{
			return SearchInternal(criteria);
		}
	}
}
