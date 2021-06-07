using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;
using FluentValidation;
using FluentValidation.Mvc;
using Kesco.DataAccess;
using Kesco.DataAccess.Filtering;
using Kesco.ObjectModel;
using Kesco.Web.Mvc.ComponentModel.DataAnnotations;
using Kesco.Web.Mvc.Infrastructure;
using Kesco.Web.Mvc.UI.ComponentModel.DataAnnotations;
using Kesco.Web.Mvc.UI.Controls.DataAccess;
using Kesco.Web.Mvc.Validation;

namespace Kesco.Web.Mvc.Test.Models.SelectBox
{
	[TableName("vwЛица")]
	public class PersonSimple : Entity<PersonSimple, int>
	{
		/// <summary>
		/// КодЛица
		/// </summary>
		[MapField("КодЛица")]
		[PrimaryKey, NonUpdatable]
		public override int ID { get; set; }

		/// <summary>
		/// Кличка
		/// </summary>
		[MapField("Кличка")]
		[BLToolkit.Validation.MaxLength(50)]
		[BLToolkit.Validation.Required]
		public string Nickname { get; set; }

		/// <summary>
		/// ИНН
		/// </summary>
		[MapField("ИНН")]
		[BLToolkit.Validation.MaxLength(20)]
		[BLToolkit.Validation.Required]
		public string INN { get; set; }

		/// <summary>
		/// Возвращает отображаемое имя.
		/// </summary>
		/// <returns>Отображаемое имя</returns>
		public override string GetInstanceFriendlyName()
		{
			return Nickname ?? String.Format("#{0}", GetUniqueID());
		}

	}

	/// <summary>
	/// Реализует проводник данных для элемента управления Выбор единицы измерения
	/// </summary>
	public abstract class PersonSelectBoxAccessor : PersonSimpleAccessor, IKescoSelectAccessor
	{
		public string GetInstanceKeyValue(object instance)
		{
			if (instance is PersonSimple) {
				return ((PersonSimple)instance).GetInstanceFriendlyName();
			} else
				throw new ArgumentException(String.Format("Ожидается аргумент следующего типа {0}", typeof(PersonSimple), "instance"));
		}

		public string GetInstanceDisplayName(object instance)
		{
			if (instance is PersonSimple) {
				return ((PersonSimple)instance).GetUniqueID();
			} else
				throw new ArgumentException(String.Format("Ожидается аргумент следующего типа {0}", typeof(PersonSimple), "instance"));
		}
	}

	[FilterSqlBuilder(
			TableOrViewName = "Справочники.dbo.vwЛица",
			UniqueIdField = "КодЛица",
			FieldList = "КодЛица, Кличка, ИНН",
			ParametersType = typeof(SearchParameters)
		)]
	public abstract class PersonSimpleAccessor : EntityAccessor<
		PersonSimpleAccessor, 
		PersonSimpleAccessor.DB, 
		PersonSimple, 
		PersonSimpleAccessor.SearchParameters, int>, IKescoSelectAccessor
	{
		public string GetInstanceDisplayName(object instance)
		{
			if (instance is PersonSimple)
			{
				return ((PersonSimple)instance).Nickname;
			}
			else
				throw new ArgumentException(String.Format("Ожидается аргумент следующего типа {0}", typeof(PersonSimple), "instance"));
		}

		public string GetInstanceKeyValue(object instance)
		{
			if (instance is PersonSimple)
			{
				return ((PersonSimple)instance).GetUniqueID();
			}
			else
				throw new ArgumentException(String.Format("Ожидается аргумент следующего типа {0}", typeof(PersonSimple), "instance"));
		}

		public class DB : Database
		{

			public DB() : base("DS_Person") { }

			protected override IDbCommand OnInitCommand(IDbCommand command)
			{
				IDbCommand dbCommand = base.OnInitCommand(command);
				dbCommand.CommandTimeout = 30 * 3;
				return dbCommand;
			}

		}

		/// <summary>
		/// Определяет набор параметров для поиска по лицам
		/// </summary>
		public class SearchParameters : Kesco.DataAccess.SearchParameters
		{
			
			[InIntArrayFilterOption(FieldName = "КодЛица")]
			public int[] IDs { get; set; }

			[NotInIntArrayFilterOption(FieldName = "КодЛица")]
			public int[] NotIDs { get; set; }

			[SearchStringFilterOption(
					FieldNameAlias="СоставноеНазвание", 
					FieldValueInRL = true, 
					UseConvertionToRL = true,
					ConcatinationExpression = "T0.НазваниеRL+' '+T0.ИНН+' '+CAST(T0.КодЛица AS nvarchar)"
				)]
			internal string SearchNameRL { get { return Search; } }

			[InIntArrayFilterOption(FieldName = "ТипЛица")]
			public int[] PersonTypes { get; set; }

			[BooleanFilterOption(FieldName = "ГосОрганизация")]
			public bool? IsState { get; set; }

			[BooleanFilterOption(FieldName = "Проверено")]
			public bool? IsChecked { get; set; }

			[BooleanFilterOption(FieldName = "ТипЛица"
					, CustomTrueExpression = "(T0.БИК<>'' OR T0.SWIFT<>'')"
					, CustomFalseExpression = "(T0.БИК='' AND T0.SWIFT='')"
				)]
			public bool? IsBank { get; set; }

			[BooleanFilterOption(FieldName = "ИНН"
					, CustomTrueExpression = "(T0.ИНН IS NULL)"
					, CustomFalseExpression = "(T0.ИНН IS NOT NULL)"
				)]
			public bool? INNEmpty { get; set; }

			[InStringArrayFilterOption(FieldName = "ИНН")]
			public string[] INNs { get; set; }

			[NotInStringArrayFilterOption(FieldName = "ИНН")]
			public string[] NotINNs { get; set; }

			[InIntArrayFilterOption(FieldName = "КодТерритории")]
			public int[] TerritoryIDs { get; set; }

			[NotInIntArrayFilterOption(FieldName = "КодТерритории")]
			public int[] NotTerritoryIDs { get; set; }

			[InIntArrayFilterOption(FieldName = "КодБизнесПроекта")]
			public int[] BusinessProjectIDs { get; set; }

			[NotInIntArrayFilterOption(FieldName = "КодБизнесПроекта")]
			public int[] NotBusinessProjectIDs { get; set; }

			[RowStartIndexFilterOption(FieldName = "КодЛица")]
			public int RowStartIndex { get; set; }

			[PageSizeFilterOption(FieldName = "КодЛица")]
			public int MaxEntries { get; set; }

			[OrderByFilterOption()]
			public List<string> OrderBy { get; set; }

			[DateTimeFilterOption(FieldName="ДатаРождения", DateTimeFormat="dd.MM.yyyy")]
			public DateTimeRange BirthDate { get; set; }

			public SearchParameters()
				: base()
			{
				IDs = new int[] { };
				NotIDs = new int[] { };
				PersonTypes = new int[] { };
				INNs = new string[] { };
				NotINNs = new string[] { };
				TerritoryIDs = new int[] { };
				NotTerritoryIDs = new int[] { };
				BusinessProjectIDs = new int[] { };
				NotBusinessProjectIDs = new int[] { };
				BirthDate = new DateTimeRange() { };
				OrderBy = new List<string>();
			}
		}

		public override List<PersonSimple> Search(SearchParameters criteria)
		{
			return SearchInternal(criteria);
		}

	}
}