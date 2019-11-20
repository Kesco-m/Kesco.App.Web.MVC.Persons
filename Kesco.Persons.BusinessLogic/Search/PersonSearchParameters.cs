using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Mapping;

namespace Kesco.Persons.BusinessLogic.Search
{
	/// <summary>
	/// Параметры для хранимой процедуры поиска лиц (Справочники.sp_Лица_Поиск)
	/// </summary>
	[MapValue(false, 0)]
	[MapValue(true, 1)]
	[Obsolete("Используйте PersonAccessor.SearchParameters")]
	public class PersonSearchParameters : Attribute
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
}
