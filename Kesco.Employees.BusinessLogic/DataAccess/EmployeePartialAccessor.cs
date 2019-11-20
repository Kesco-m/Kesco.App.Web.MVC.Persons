using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kesco.DataAccess;
using Kesco.Employees.ObjectModel;
using Kesco.DataAccess.Filtering;
using System.Threading;
using Kesco.Employees.BusinessLogic.Filtering;

namespace Kesco.Employees.BusinessLogic.DataAccess
{

	/// <summary>
	/// Проводник данных для получения краткой информации об сотрудниках.
	/// </summary>
	[FilterSqlBuilder(
		TableOrViewName = "Инвентаризация.dbo.Сотрудники",
		UniqueIdField = "КодСотрудника",
		FieldList = "КодСотрудника, Сотрудник, Employee, КодЛица, DisplayName, КодЛицаЗаказчика, Состояние, Email, Login",
		ParametersType = typeof(SearchParameters)
	)]
	public abstract class EmployeePartialAccessor : EntityAccessor<EmployeePartialAccessor, DB, EmployeePartial, EmployeePartialAccessor.SearchParameters, int>
	{
		/// <summary>
		/// Класс определяет набор критериев поиска по сотрудникам
		/// </summary>
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
			[EmployeeSearchStringFilterOption(FieldName = "Сотрудник", FieldValueInRL = false, UseConvertionToRL = true)]
			internal string SearchString { get { return Search; } }

			/// <summary>
			/// Gets or sets the account disabled.
			/// </summary>
			/// <value>
			/// The account disabled.
			/// </value>
			[BooleanFilterOption(
					FieldName = "AccountDisabled",
					CustomTrueExpression = "(T0.AccountDisabled IS NULL)",
					CustomFalseExpression = "(T0.AccountDisabled IS NOT NULL)"
				)]
			public bool? AccountDisabled { get; set; }

			/// <summary>
			/// Возвращает или устанавливает критерий поиска - список состояний сотрудников.
			/// Cотрудники со состояниями из списка ДОЛЖНЫ попасть в результат поиска.
			/// </summary>
			[InIntArrayFilterOption(FieldName = "Состояние")]
			public List<int> States { get; set; }

			[NotInIntArrayFilterOption(FieldName = "КодСотрудника")]
			public List<int> NotIDs { get; set; }

			[InIntArrayFilterOption(FieldName = "КодСотрудника")]
			public List<int> IDs { get; set; }

			[InStringArrayFilterOption(FieldName = "Login")]
			public List<string> Logins { get; set; }

			/// <summary>
			/// Возвращает или устанавливает критерий поиска - список email адресов сотрудников.
			/// Cотрудники email адресами из списка ДОЛЖНЫ попасть в результат поиска.
			/// </summary>
			[InStringArrayFilterOption(FieldName = "Email")]
			public List<string> Emails { get; set; }

			/// <summary>
			/// Возвращает или устанавливает критерий поиска - список состояний, 
			/// Cотрудники со состояниями из списка НЕ ДОЛЖНЫ попасть в результат поиска.
			/// </summary>
			[NotInIntArrayFilterOption(FieldName = "Состояние")]
			public List<int> NotStates { get; set; }

			/// <summary>
			/// Возвращает или устанавливает критерий поиска - максимальное кол-во записей.
			/// В результат поиска попадёт заданное максимальное кол-во записей.
			/// </summary>
			/// <value>
			/// Максимальное кол-во записей.
			/// </value>
			[PageSizeFilterOption(FieldName = "КодСотрудника")]
			public int MaxEntries { get; set; }

			/// <summary>
			/// Возвращает или устанавливает первоначальный индекс, 
			/// с которой вернуть записи, соотвествующие заданным критериям поиска.
			/// </summary>
			/// <value>
			/// Первоначальный индекс, с которой вернуть записи
			/// </value>
			[RowStartIndexFilterOption(FieldName = "КодЛица")]
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
				States = new List<int>();
				NotStates = new List<int>();
				Emails = new List<string>();
				OrderBy = new List<string>();
				NotIDs = new List<int>();
				IDs = new List<int>();
				Logins = new List<string>();
				//if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "en")
				//	OrderBy.Add("Employee");
				//else
					OrderBy.Add("Сотрудник");
			}
		}

		/// <summary>
		/// Осуществялет поиск сотрудников в соотвествии с указанными критериями.
		/// </summary>
		/// <param name="criteria">Критерии поиска.</param>
		/// <returns>Список сотрудников</returns>
		public override List<EmployeePartial> Search(EmployeePartialAccessor.SearchParameters criteria)
		{
			if (criteria.States.Count == 0)
			{
				criteria.States = new List<int>() { 0, 1, 2 };
			}
			return SearchInternal(criteria);
		}
	}
}
