using System;
using BLToolkit.Mapping;
using Kesco.Persons.BusinessLogic.DataAccess;

namespace Kesco.Persons.BusinessLogic
{
	
	public static class Repository
	{
		public static PersonSimpleAccessor PersonPartials { get { return PersonSimpleAccessor.Accessor; } }
		public static PersonAccessor Persons { get { return PersonAccessor.Accessor; } }
		public static IncorporationFormAccessor IncorporationForms { get { return IncorporationFormAccessor.Accessor; } }
        public static PersonThemeAccessor PersonTheme { get { return PersonThemeAccessor.Accessor; } }
		public static PersonCardJuridicalAccessor JuridicalPersonCards { get { return PersonCardJuridicalAccessor.Accessor; } }
		public static PersonCardNaturalAccessor NaturalPersonCards { get { return PersonCardNaturalAccessor.Accessor; } }
		public static CaseAccessor Cases { get { return CaseAccessor.Accessor; } }
		public static CatalogAccessor Catalogs { get { return CatalogAccessor.Accessor; } }
		public static ContactAccessor Contacts { get { return ContactAccessor.Accessor; } }
		public static ContactActualAccessor ContactActuals { get { return ContactActualAccessor.Accessor; } }
		public static ContactTypeAccessor ContactTypes { get { return ContactTypeAccessor.Accessor; } }
		public static DepartmentAccessor Departments { get { return DepartmentAccessor.Accessor; } }
		public static DistributionCertificateAccessor DistributionCertificates { get { return DistributionCertificateAccessor.Accessor; } }
		public static FormatRegistrationAccessor FormatRegistrations { get { return FormatRegistrationAccessor.Accessor; } }
		public static LogotypeAccessor Logotypes { get { return LogotypeAccessor.Accessor; } }
		public static MenuItemAccessor MenuItems { get { return MenuItemAccessor.Accessor; } }
		public static PersonLinkAccessor Links { get { return PersonLinkAccessor.Accessor; } }
		public static PersonLinkTypeAccessor PersonLinkTypes { get { return PersonLinkTypeAccessor.Accessor; } }
		public static PersonTypeAccessor PersonTypes { get { return PersonTypeAccessor.Accessor; } }
		public static PersonThemeAccessor PersonThemes { get { return PersonThemeAccessor.Accessor; } }
		public static ResponsibleEmployeeAccessor ResponsibleEmployees { get { return ResponsibleEmployeeAccessor.Accessor; } }
		public static RulePersonTypeAccessor RulePersonTypes { get { return RulePersonTypeAccessor.Accessor; } }

		#region Параметры поиска

		/// <summary>
		/// Параметры для хранимой процедуры поиска лиц (Справочники.sp_Лица_Поиск)
		/// </summary>
		[MapValue(false, 0)]
		[MapValue(true, 1)]
		public class SearchPersonParameters2
		{
			/// <summary>
			/// Строка поиска
			/// </summary>
			public string Search { get; set; }

			/// <summary>
			/// Где ищем информацию: 0, 7- везде; 1 - Псевдоним(vwЛица); 2 - Реквизиты(vwКарточкиЮрЛиц и vwКарточкиФизЛиц); 4 - Контакты
			/// </summary>
			public int PersonWhereSearch { get; set; }

			/// <summary>
			/// Как ищем: Данные 1- Содержат; 2 - Начинаются с искомого текста
			/// </summary>
			public int PersonHowSearch { get; set; }

			/// <summary>
			/// Тип лица: 0, 7 - Все; 1 - Юр.лица; 2 - Физ.лица; 4 - Банки
			/// </summary>
			public int? PersonType { get; set; }

			/// <summary>
			/// Бизнес-проект
			/// </summary>
			public int? PersonBProject { get; set; }

			/// <summary>
			/// Искать организации по подченённым бизнеспроектам
			/// </summary>
			public int PersonSubBProject { get; set; }

			/// <summary>
			/// Лица с бизнес-проектами (наши)
			/// </summary>
			public int? PersonAllBProject { get; set; }

			/// <summary>
			/// Проверенность лица: 0, 3 -Все лица; 1-Лица проверенные; 2- Лица не проверенные
			/// </summary>
			public int? PersonCheck { get; set; }

			/// <summary>
			/// Организационно-правовая форма для поиска лица
			/// </summary>
			public int? PersonOPForma { get; set; }

			/// <summary>
			/// Список тем лиц, коды тем указываются через запятую
			/// </summary>
			public string PersonThemes { get; set; }

			/// <summary>
			/// Поиск в уточняющих темах
			/// </summary>
			public int PersonSubThemes { get; set; }

			/// <summary>
			/// Список ответственных за информацию по лицам, коды сотрудников указываются через запятую
			/// </summary>
			public string PersonUsers { get; set; }

			/// <summary>
			/// Страна регистрации лица
			/// </summary>
			public int? PersonArea { get; set; }

			/// <summary>
			/// Выбирать лица, зарегистрированные в странах, входящих в таможенный союз
			/// </summary>
			public int? PersonTUnion { get; set; }

			/// <summary>
			/// Лицо, по которому выбираются связанные лица
			/// </summary>
			public int? PersonLink { get; set; }
            public string PersonLinkNickname { get; set; }

			/// <summary>
			/// Типы связей лиц
			/// </summary>
			public int? PersonLinkType { get; set; }

			/// <summary>
			/// Ограничивает связанные лица по действующим на указанную дату связям
			/// </summary>
			public DateTime? PersonLinkValidAt { get; set; }

			/// <summary>
			/// Ограничивает связанные лица по значению поля Параметр(наличия права подписи при связи Работник-МестоРаботы)
			/// </summary>
			public int? PersonSignType { get; set; }

			/// <summary>
			/// Ограничивает лиц по действующим на указанную дату карточкам
			/// </summary>
			public DateTime? PersonValidAt { get; set; }
            public long PersonValidAtTicks { get; set; }

			/// <summary>
			/// Ограничивает контакты лиц факсами и email
			/// </summary>
			public int PersonForSend { get; set; }

			/// <summary>
			/// Ограничивает поиск по реквизитам указнными полями: 1-ИНН; 2-БИК; 3-КS; 4-SWIFT
			/// </summary>
			public int? PersonAdvSearch { get; set; }

			/// <summary>
			/// TOP N - ограничивает, количество, возвращаемых процедурой записей
			/// </summary>
			public int? PersonSelectTop { get; set; }

		}

		#endregion
	}
}
