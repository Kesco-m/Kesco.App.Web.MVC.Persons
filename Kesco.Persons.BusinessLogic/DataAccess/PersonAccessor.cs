using System;
using System.Collections.Generic;
using System.Linq;
using BLToolkit.Data;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using Kesco;
using Kesco.DataAccess;
using Kesco.Persons.BusinessLogic.Dossier;
using Kesco.Persons.BusinessLogic.Localization;
using Kesco.Persons.BusinessLogic.Persons;
using Kesco.Persons.ObjectModel;

namespace Kesco.Persons.BusinessLogic.DataAccess
{

	[MapValue(true, 1)]
	[MapValue(false, 0)]
	public abstract class PersonAccessor : EntityAccessor<PersonAccessor, DB, Person, PersonAccessor.SearchParameters, int>
	{
		/// <summary>
		/// Критерии поиска лица
		/// </summary>
		public class SearchParameters : Kesco.DataAccess.SearchParameters
		{
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

		[SqlQuery("SELECT * FROM vwЛица WHERE INN = @inn")]
		public abstract List<Person> GetPersonsByINN(string inn);

        [SqlQuery(@"SELECT Фамилия +' '+ Имя +' '+ Отчество FROM Инвентаризация.dbo.Сотрудники 
                    WHERE КодСотрудника= @employeeId")]
	    public abstract string GetEmployeeFIOByID(string employeeId);

        [SqlQuery(@"SELECT TOP 1 к.КодЛица
                    FROM Инвентаризация.dbo.Сотрудники с
	                INNER JOIN Справочники.dbo.vwКодыЛиц к ON с.КодЛица = к.КодЛица
                    WHERE КодСотрудника= @employeeID")]
        public abstract int? GetPersonByEmployeeID(string employeeID);

        [SqlQuery(@"SELECT [КодОргПравФормы]
                    FROM [Справочники].[dbo].[vwКарточкиФизЛиц]  WHERE КодЛица = @personID")]
        public abstract int GetIncorporationFormID(int personID);

        /// <summary>
        /// Сохранение физлица
        /// </summary>
        /// <param name="card"></param>
        /// <param name="confirmed"></param>
        /// <returns></returns>
        public int SaveCard(PersonCardNatural card, bool confirmed)
        {
            var duplicateNicknames = GetDuplicatesByNickname(card.Person.Nickname, 0);

            if (duplicateNicknames.Count > 0)
            {
                throw new DuplicateNicknameException(duplicateNicknames);
            }

            var c = new IndividualCardForSave
                        {
                            NewID = 0,
                            WhatDo = Persons.SaveAction.СоздатьЛицо,
                            Check = confirmed,
                            КодЛица = card.PersonID,
                            КодКарточки = card.ID,
                            Кличка = card.Person.Nickname,
                            КодБизнесПроекта = card.Person.BusinessProjectID,
                            КодТерритории = card.Person.TerritoryID,
                            ИНН = card.Person.INN,
                            ОГРН = card.Person.OGRN,
                            ОКПО = card.Person.OKPO,
							ДатаРождения = card.Person.Birthday,
							Примечание = card.Person.Comment,
							Проверено = card.Person.Verified,
							От = card.From ?? new DateTime(1980, 1, 1),
							До = card.To ?? new DateTime(2050, 1, 1),
                            КодОргПравФормы = card.IncorporationFormID,
                            ФамилияРус = card.LastNameRus,
                            ИмяРус = card.FirstNameRus,
                            ОтчествоРус = card.MiddleNameRus,
                            ФамилияЛат = card.LastNameLat,
                            ИмяЛат = card.FirstNameLat,
                            ОтчествоЛат = card.MiddleNameLat,
                            Пол = card.Sex,
                            ОКОНХ = card.OKONH,
                            ОКВЭД = card.OKVED,
                            КПП = card.KPP,
                            КодЖД = card.RwID,
                            АдресЮридический = card.AddressLegal,
                            АдресЮридическийЛат = card.AddressLegalLat
                        };

			var issues = new List<SaveIssue> {
					new SaveIssue { Field ="kaka", Nickname="бяка", PersonID = -1 }
			};
			//var issues = SaveIndividual(c);

            // логика обработки результата работы хранимой процедуры
            if (issues.Count > 0)
                throw new SavePersonException(issues);

            return c.NewID;
        }

		public int TryToSaveIndividual(IndividualCardForSave card)
		{

			var issues = SaveIndividual(card);

			// логика обработки результата работы хранимой процедуры
			if (issues.Count > 0)
				throw new SavePersonException(issues);

			return card.NewID;
		}

		/// <summary>
		/// Создаёт или сохраняет изменения сделанные для .
		/// </summary>
		/// <param name="action">The action.</param>
		/// <param name="data">Данные о лице.</param>
		/// <param name="confirmed">Если установлено в <c>true</c> [confirmed].</param>
		/// <returns></returns>
		public int SaveJuridicalCard(Persons.SaveAction action, dynamic data, bool confirmed)
		{
			return 0;
		}

		public int TryToSaveJuridical(JuridicalCardForSave card)
		{

			var issues = SaveJuridical(card);

			// логика обработки результата работы хранимой процедуры
			if (issues.Count > 0)
				throw new SavePersonException(issues);

			return card.NewID;
		}

		public int SaveJuridicalCard(PersonCardJuridical card, bool confirmed)
        {
            var duplicateNicknames = GetDuplicatesByNickname(card.Person.Nickname, 0);

            if (duplicateNicknames.Count > 0)
            {
                throw new DuplicateNicknameException(duplicateNicknames);
            }

            var c = new JuridicalCardForSave()
            {
                NewID = 0,
                WhatDo = Persons.SaveAction.СоздатьЛицо,
                Check = confirmed,
                КодЛица = card.PersonID,
                КодКарточки = card.ID,
                Кличка = card.Person.Nickname,
				КодБизнесПроекта = card.Person.BusinessProjectID,
                КодТерритории = card.Person.TerritoryID,
                ГосОрганизация = card.Person.IsStateOrganization ? 1 : 0,
                БИК = card.Person.BIK,
                ИНН = card.Person.INN,
                ОГРН = card.Person.OGRN,
                ОКПО = card.Person.OKPO,
                КорСчет = card.Person.LoroConto,
                БИКРКЦ = card.Person.BIKRKC,
                SWIFT = card.Person.SWIFT,
				Примечание = card.Person.Comment,
                Проверено = card.Person.Verified,

                От = card.From,
				До = card.To,
                КодОргПравФормы = card.IncorporationFormID,
                КраткоеНазваниеРус = card.ShortNameRus,
                КраткоеНазваниеРусРП = card.ShortNameRusGen,
                КраткоеНазваниеЛат = card.ShortNameLat,
                ПолноеНазвание = card.FullName,
                ОКОНХ = card.OKONH,
                ОКВЭД = card.OKVED,
                КПП = card.KPP,
                КодЖД = card.RwID,
                АдресЮридический = card.AddressLegal,
                АдресЮридическийЛат = card.AddressLegalLat
            };

            var issues = SaveJuridical(c);

            // логика обработки результата работы хранимой процедуры
            if (issues.Count > 0)
                throw new SavePersonException(issues);

            return c.NewID;
        }



		/// <summary>
		/// Параметры вызова хранимой процедуры изменения типа лица
		/// </summary>
		public class PersonTypeForSave
		{
			public int КодЛица { get; set; }
			public int КодТипаЛица { get; set; }
			/// <summary>
			/// Что делать, 1 - добавить, 2 - удалить
			/// </summary>
			public int WhatDo { get; set; }
		}


        /// <summary>
        /// Параметры для процедуры поиска досье
        /// </summary>
        public class PersonsDossierContextParameters
        {
            public int Retval { get; set; }
            public int КодЛица { get; set; }
            public byte DateOld { get; set; }
			public byte @Версия { get; set; }
			public string @СписокКодовЗагружаемыхСекций { get; set; }
        }


        /// <summary>
        /// Параметры для процедуры меню для лица
        /// </summary>
        public class PersonsDossierMenuParameters
        {
            public int Retval { get; set; }
            public int КодЛица { get; set; }
            public int ТипЛица { get; set; }
        }


        /// <summary>
        /// Досье лица
        /// </summary>
        public class PersonDossierContext
        {
            // Код int, Надпись varchar(50), Поле varchar(300),  КодВкладки int, Ссылка int, Порядок int, Sort float, Sprt float, Изменил varchar(50), Изменено datetime
            [MapField("Код")]
            public int ID { get; set; }

            [MapField("Надпись")]
            public string Caption { get; set; }

            [MapField("Поле")]
            public string Field { get; set; }

            [MapField("КодВкладки")]
            public int TabID { get; set; }

            [MapField("Ссылка")]
            public int Link { get; set; }

			[MapField("ТекстСсылки")]
			public string LinkText { get; set; }

            [MapField("Порядок")]
            public int Order { get; set; }

            [MapField("Sort")]
            public float Sort { get; set; }

            [MapField("Sprt")]
            public double Sprt { get; set; }

			[MapField("Изменил")]
            public string ChangedByFIO { get; set; }

            [MapField("КодСотрудникаКтоИзменил")]
			public int? ChangedBy { get; set; }

            [MapField("Изменено")]
            public DateTime? Changed { get; set; }

            [MapField("Icon")]
            public string Icon { get; set; }

        }


        /// <summary>
        /// ПунктыМенюЛиц
        /// </summary>
        public class PersonDossierSection
        {
			[MapField("КодПунктаМенюЛиц")]
			public int ID { get; set; }

			[MapField("ТипЛица")]
			public byte PersonType { get; set; }

			[MapField("Представление")]
			public string ViewName { get; set; }

			[MapField("КомандаРедактированияСписка")]
			public string ListEditCommand { get; set; }

			[MapField("ПунктДосье")]
			public string Caption { get; set; }

			[MapField("MinДоступ_Проверено")]
			public int MinLevel_Checked { get; set; }

			[MapField("MinДоступ_НеПроверено")]
			public int MinLevel_NotChecked { get; set; }

			[MapField("ПунктДосьеLocalizationKey")]
			public string CaptionKey { get; set; }

			[MapField("Рисунок")]
			public string Image { get; set; }

			[MapField("URLФормы")]
			public string FormURL { get; set; }

			[MapField("URLПараметры")]
			public string URLParameters { get; set; }

			[MapField("ФормаРедактирования_aspx")]
			public string EditForm { get; set; }
		}


		/*protected abstract List<SaveIssue> SaveIndividual(PersonSaveAction whatDo, bool check, int? кодЛица, int? кодКарточки, string кличка, int? кодБизнесПроекта, 
			int? кодТерритории, string инн, string огрн, string окпо, DateTime? датаРождения, string примечание, bool проверено, DateTime? от, DateTime? до, 
			int? кодОргПравФормы, string фамилияРус, string имяРус, string отчествоРус, string фамилияЛат, string имяЛат, string отчествоЛат, 
			char пол, string оконх, string оквэд, string кпп, string кодЖД, string адресЮридический, string адресЮридическийЛат,
			[Direction.ReturnValue("@RETURN_VALUE")] out int новыйКодЛица); */

		[SprocName("sp_Лица_InsUpd_КарточкиФизЛиц")]
		protected abstract List<SaveIssue> SaveIndividual([Direction.ReturnValue("NewID")] IndividualCardForSave card);


        [SprocName("sp_Лица_InsUpd_КарточкиЮрЛиц")]
        protected abstract List<SaveIssue> SaveJuridical([Direction.ReturnValue("NewID")] JuridicalCardForSave card);


		[SqlQuery(@"
			SELECT * FROM vwЛица
			WHERE КодЛица IN( SELECT value FROM Инвентаризация.dbo.fn_SplitInts(@ids) )
			ORDER BY Кличка")]
		public abstract List<Person> GetListByIds(string ids);

        [SqlQuery(@"
			SELECT КодЛица FROM vwЛица
            WHERE Кличка=@nick AND КодЛица <> @globalPerson")]
		public abstract List<int> GetDuplicatesByNickname(string nick, int globalPerson);

		[SqlQuery(@"
			SELECT IsExist = CASE WHEN COALESCE(COUNT(*), 0) > 0 THEN 1 ELSE 0 END
			FROM vwЛица
			WHERE Кличка=@Кличка AND КодЛица <> COALESCE(@КодЛица, 0)"
		)]
		public abstract bool HasPersonWithTheSameNickname(string @Кличка, int @КодЛица);

		/// <summary>
		/// Добавляет или удаляет тип для лица
		/// </summary>
		/// <param name="personTypeForSave">Параметры вызова хранимой процедуры</param>
		[SprocName("sp_Лица_InsDel_ТипыЛиц")]
		public abstract void AssignPersonTypeToPerson(PersonTypeForSave personTypeForSave);

		/// <summary>
		/// Лицо имеет действующие реквизиты на текущее время
		/// </summary>
		/// <param name="id">идентификатор лица</param>
		/// <returns>true - лицо действует; false - лицо не действует</returns>
		public bool IsValidNow(int id)
		{
			return IsValidAt(id, DateTime.Now);
		}

		/// <summary>
		/// Лицо имеет действующие реквизиты на указанную дату время
		/// </summary>
		/// <param name="id">идентификатор лица</param>
		/// <param name="date">дата, на которую выполняется проверка</param>
		/// <returns>true - лицо действует; false - лицо не действует</returns>
		[SqlQuery(@"
SELECT CASE WHEN COALESCE(COUNT(*), 0) > 0 THEN 1 ELSE 0 END
FROM vwКарточкиФизЛиц WHERE КодЛица = @id AND От <= @date AND До > @date")]
		public abstract bool IsValidAt(int id, DateTime date);

		#region Операции, относящиеся к досье

		public bool CheckPersonAbacusStatus(int personID)
		{
			return Accounting.CheckPersonAbacusStatus(personID);
		}

		public bool CheckPerson1SStatus(int personID)
		{
			return Accounting.CheckPerson1SStatus(personID);
		}

		[SprocName("sp_Лица_Досье_Context")]
        public abstract List<PersonDossierContext> PersonDossierData([Direction.ReturnValue("Retval")] PersonsDossierContextParameters parameters);

        [SqlQuery(@"SELECT П.* FROM	ПунктыМенюЛиц П WHERE П.ПунктДосье IS NOT NULL AND ТипЛица=@personType")]
        public abstract List<PersonDossierSection> PersonDossierSections(byte personType);

        //exec [sp_Лица_Досье_$Меню] @КодЛица = N'113', @ТипЛица = 1
        [SprocName("sp_Лица_Досье_$Меню")]
        public abstract List<PersonMenuItem> PersonsDossierMenus([Direction.ReturnValue("Retval")] PersonsDossierMenuParameters parameters);

		[SqlQuery("SELECT dbo.fn_Лица_УровеньДоступа(@personId) AS AccessLevel")]
		public abstract PersonAccessLevel GetPersonAccessLevel(int personId);

        /// <summary>
        /// Возвращает текущие роли лица
        /// </summary>
        [SqlQuery(@"DECLARE @ТекущиеРоли VARCHAR(500) 
        SELECT @ТекущиеРоли = COALESCE(@ТекущиеРоли + ',', '') +  CAST( КодРоли as varchar(5)) 
        FROM Инвентаризация.dbo.fn_ТекущиеРоли()
        select @ТекущиеРоли")]
        public abstract string GetEmployeeRoles();

        /// <summary>
        /// Возвращает код сотрудника, который редактирует
        /// </summary>
        [SqlQuery(@"SELECT TOP 1 КодСотрудника FROM Инвентаризация.dbo.Сотрудники WHERE SID = SUSER_SID()")]
        public abstract int GetCurrentUserEmployeeID();

		/// <summary>
		/// Добавление ответственного сотрудника
		/// </summary>
		/// <param name="@КодЛица">ID лица</param>
		/// <param name="@КодСотрудника">Код сотрудника.</param>
		[SqlQuery(@"

			IF (NOT EXISTS(SELECT * FROM vwЛица_Сотрудники ЛС WHERE	КодСотрудника = @КодСотрудника AND КодЛица = @КодЛица))
			INSERT INTO vwЛица_Сотрудники(КодЛица,КодСотрудника)
			SELECT @КодЛица, @КодСотрудника 
			
		")]
		public abstract void AssignResponsibleEmployee(int @КодЛица, int @КодСотрудника);

		/// <summary>
		/// Добавление ответственных сотрудников + удаление неиспользуемых
		/// </summary>
		/// <param name="personID">ID лица</param>
		/// <param name="ids">строка с ID сотрудников через запятую</param>
		[SqlQuery(@"
			DECLARE @Коды table(КодСотрудника int)

			INSERT @Коды SELECT * FROM Инвентаризация.dbo.fn_SplitInts(@ids)

			INSERT INTO vwЛица_Сотрудники(КодЛица,КодСотрудника)
			SELECT @personID, Коды.КодСотрудника FROM @Коды Коды
				INNER JOIN Инвентаризация.dbo.Сотрудники Empl ON Empl.КодСотрудника = Коды.КодСотрудника
			WHERE NOT EXISTS( SELECT * FROM vwЛица_Сотрудники WHERE vwЛица_Сотрудники.КодСотрудника = Коды.КодСотрудника AND vwЛица_Сотрудники.КодЛица = @personID )
			
			IF (EXISTS(SELECT * FROM vwЛица_Сотрудники WHERE КодЛица = @personID AND КодСотрудника NOT IN ( SELECT КодСотрудника FROM @Коды )))
				DELETE FROM vwЛица_Сотрудники WHERE КодЛица = @personID AND КодСотрудника NOT IN ( SELECT КодСотрудника FROM @Коды )
		")]
		public abstract void MergeResponsibleEmployees(int personID, string ids);

		/// <summary>
		/// Сохраняет и устанавливает новые типы для лица
		/// </summary>
		/// <param name="personID">Код лица.</param>
		/// <param name="typeIDs">Коды типов лиц.</param>
		public void SavePersonTypes(int personID, string typeIDs)
		{
			var currentIDs = PersonTypeAccessor.Accessor.GetPersonTypeIDListByPersonID(personID);
			var newIDs = typeIDs.ToArray<int>(new char[] { ',' }, s => Int32.Parse(s));
			using (DbManager db = CreateDbManager()) {
				SetDbManager(db, false);
				this.BeginTransaction();
				try {
					var toDeleteIDs = currentIDs.Where(id => !newIDs.Contains(id));

					foreach (int idToDelete in toDeleteIDs)
						AssignPersonTypeToPerson(new PersonAccessor.PersonTypeForSave {
							КодЛица = personID,
							КодТипаЛица = idToDelete,
							WhatDo = 2
						});

					foreach (int idToAdd in newIDs)
                        if (!currentIDs.Contains(idToAdd))
						AssignPersonTypeToPerson(new PersonAccessor.PersonTypeForSave {
							КодЛица = personID,
							КодТипаЛица = idToAdd,
							WhatDo = 1
						});

					this.CommitTransaction();
				} catch (Exception ex) {
					this.RollbackTransaction();
					throw ex;
				}
			}
		}

		#endregion

		class DbUser : Database
		{
			public DbUser() : base("DS_User") {}
		}

		/// <summary>
		/// Удаление лица (DS_USER:master.dbo.sp_Delete_Лицо).
		/// </summary>
		/// <param name="КодЛицаУдаляемого">The КОД ЛИЦА УДАЛЯЕМОГО.</param>
		/// <param name="КодЛицаЗамещающего">The КОД ЛИЦА ЗАМЕЩАЮЩЕГО.</param>
		/// <param name="Выполнить">ВЫПОЛНИТЬ ЛИ КОМАНДУ ИЛИ НЕТ.</param>
		public void DeletePerson(int КодЛицаУдаляемого, int КодЛицаЗамещающего, int Выполнить = 1)
		{
			using(DbUser db = new DbUser()) {
				db
					.SetSpCommand("master.dbo.sp_Delete_Лицо", 
							КодЛицаУдаляемого, 
							КодЛицаЗамещающего, 
							Выполнить
						)
					.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// Checks the date range.
		/// </summary>
		/// <param name="КодЛица">The КОД ЛИЦА.</param>
		/// <param name="от">The от.</param>
		/// <param name="до">The до.</param>
		/// <returns></returns>
		[SqlQuery(@"
	
			DECLARE @От DATETIME SET @От = CONVERT(datetime, ISNULL(@From, '19800101'))
			DECLARE @До DATETIME SET @До = CONVERT(datetime, ISNULL(@To, '20500101'))
			
			SET NOCOUNT ON
	
			SET @От = FLOOR(CONVERT(float, @От))
			SET @До = FLOOR(CONVERT(float, @До))
	
			IF @От >= @До
			BEGIN 
				RAISERROR('Дата начала действия реквизитов не может быть больше даты их окончания!', 12, 1) 
			END

			DECLARE @ДиапазоныДат TABLE(От datetime, До datetime)
			DECLARE @ОтMin datetime, @ДоMin datetime, @ДоMax datetime
	
			INSERT INTO @ДиапазоныДат SELECT От, До FROM vwКарточкиЮрЛиц WHERE КодЛица = @КодЛица
			IF @@ROWCOUNT = 0
				INSERT INTO @ДиапазоныДат SELECT От, До FROM vwКарточкиФизЛиц WHERE КодЛица = @КодЛица
	
			WHILE EXISTS(SELECT * FROM @ДиапазоныДат)
			BEGIN
				SELECT @ОтMin = MIN(От), @ДоMin = MIN(До), @ДоMax = MAX(До) FROM @ДиапазоныДат
			
				--вытягиваем диапазон на последнее место
				IF (@От >= @ОтMin AND @От < @ДоMax AND @До > @ДоMax) OR @От >= @ДоMax BEGIN SET @От = @ДоMax BREAK END 
				--вытягиваем диапазон на первое место
				IF (@От < @ОтMin AND @До <= @ДоMax AND @До > @ОтMin) OR @До <= @ОтMin BEGIN SET @До = @ОтMin BREAK END 				

				IF @От < @ДоMin SET @От = @ДоMin 
	
				DELETE @ДиапазоныДат WHERE От = @ОтMin
			END
	
			SELECT @КодЛица КодЛица, CASE WHEN @От < @До THEN 1 ELSE 0 END Валидность, @От От, @До До
		")]
		public abstract CheckDateRangeResult CheckDateRange(
				int @кодЛица, 
				DateTime? from,
				DateTime? to
			);

		public class CheckDateRangeResult
		{
			public int КодЛица { get; set; }
			public int Валидность { get; set; }
			public DateTime? От { get; set; }
			public DateTime? До { get; set; }
		}

		/// <summary>
		/// Traslits the sentence.
		/// </summary>
		/// <param name="sentence">The sentence.</param>
		/// <returns></returns>
		[SqlQuery(@"SELECT Инвентаризация.dbo.fn_TransLit(@sentence) AS Транслитерация")]
		public abstract string TraslitSentence(string sentence);

		/// <summary>
		/// Gets the person employee positions.
		/// </summary>
		/// <param name="employeeID">The employee ID.</param>
		/// <returns></returns>
		[SqlQuery(@"
			SELECT
				Лица.КодЛица, Кличка AS Организация, Должность, NULL AS КодСвязиЛиц
			FROM
				Инвентаризация.dbo.Должности Работа
				INNER JOIN Справочники.dbo.vwЛица Лица ON Работа.КодЛица=Лица.КодЛица
			WHERE КодСотрудника = @employeeID
		")]
		public abstract List<PersonPosition> GetPersonEmployeePositions(int employeeID);

		/// <summary>
		/// Gets the person current positions.
		/// </summary>
		/// <param name="personID">The person ID.</param>
		/// <returns></returns>
		[SqlQuery(@"
			SELECT КодСвязиЛиц, Лица.КодЛица, Кличка AS Организация, Описание Должность
			FROM Справочники.dbo.vwЛица Лица
				INNER JOIN Справочники.dbo.vwСвязиЛиц Работа ON Работа.КодЛицаРодителя=Лица.КодЛица AND КодТипаСвязиЛиц=1
			WHERE КодЛицаПотомка = @personID AND От <= GETDATE() AND До > GETDATE()
		")]
		public abstract List<PersonPosition> GetPersonCurrentPositions(int personID);

	}
}
