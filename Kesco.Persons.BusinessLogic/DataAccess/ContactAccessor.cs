using Kesco.Persons.ObjectModel;
using Kesco.DataAccess;
using BLToolkit.DataAccess;
using System.Collections.Generic;

namespace Kesco.Persons.BusinessLogic.DataAccess
{
    public abstract class ContactAccessor : EntityAccessor<ContactAccessor, DB, Contact, ContactAccessor.SearchParameters, int>
    {
        public class SearchParameters : Kesco.DataAccess.SearchParameters { }

        /// <summary>
        /// Параметры для вызова процедуры получения текста контакта
        /// </summary>
        public class ContactTextParts
        {
            public int ТипКонтакта { get; set; }
            public string АдресИндекс { get; set; }
            public string АдресОбласть { get; set; }
            public string АдресГород { get; set; }
            public string АдресГородRus { get; set; }
            public string Адрес { get; set; }
            public int КодСтраны { get; set; }
            public string ТелефонСтрана { get; set; }
            public string ТелефонГород { get; set; }
            public string ТелефонНомер { get; set; }
            public string ТелефонДоп { get; set; }
            public string ДругойКонтакт { get; set; }
            public byte ВидКонтакта { get; set; }
        }

        [SqlQuery(@"SELECT * FROM vwКонтакты WHERE КодЛица = @personID")]
        public abstract List<Contact> GetContactsByPersonId(int personID);

        [SprocName(@"sp_Лица_Контакты")]
        public abstract List<PersonContact> GetPersonContacts(int @кодЛица);

        /// <summary>
        /// Осуществляет поиск контактов лица по заданным параметрам
        /// </summary>
        /// <param name="кодЛица">Код лица.</param>
        /// <param name="строкаПоиска">Строка поиска.</param>
        /// <param name="СписокКодовТиповКонтактов">The СПИСОК КОДОВ ТИПОВ КОНТАКТОВ.</param>
        [SprocName(@"sp_КонтактыЛица_Поиск")]
        public abstract List<PersonContactSearchResult> SearchPersonContacts(int @кодЛица, string @строкаПоиска, string @СписокКодовТиповКонтактов);

        /// <summary>
        /// Загрузка контактов для досье
        /// </summary>
        /// <param name="ids">идентификаторы лиц, для которых ищем контакты</param>
        /// <param name="IdClient">идентификатор родителя</param>
        [SqlQuery(@"
SET NOCOUNT ON
IF OBJECT_ID('tempdb..#Tbl0') IS NOT NULL DROP TABLE #Tbl0
CREATE TABLE #Tbl0(КодЛица int PRIMARY KEY)
DECLARE @id varchar(50)
DECLARE @Язык nvarchar(2)

SELECT @Язык = Язык FROM Инвентаризация.dbo.Сотрудники WHERE SID = SUSER_SID()

INSERT INTO #Tbl0 
SELECT DISTINCT value FROM Инвентаризация.dbo.fn_SplitInts( @ids )

SELECT	X.КодКонтакта, X.КодЛица, NULL КодСвязиЛиц,
	    NULL КодЛицаСвязи, '' НадписьЛица,
	    NULL КодЛицаСвязанный,
	    2 ПоКлиенту, Типы.Icon,
	    X.КодТипаКонтакта, CASE @Язык WHEN 'ru' THEN Типы.ТипКонтакта ELSE Типы.ТипКонтактаЛат END ТипКонтакта, X.Контакт,
	    CASE WHEN X.КодТипаКонтакта BETWEEN 20 AND 39 THEN '+'+LTRIM(X.КонтактRL) ELSE '' END НомерМеждународный,
	    ISNULL(X.Примечание, '') Примечание,
	    CASE @Язык WHEN 'ru' THEN Сотрудники.ФИО ELSE Сотрудники.FIO END ИзменилФИО, X.Изменил, X.Изменено 
FROM (	SELECT Контакты.* 
	    FROM	vwКонтакты Контакты (nolock) INNER JOIN 
		        #Tbl0 Коды ON Коды.КодЛица = Контакты.КодЛица
	    WHERE	Контакты.КодСвязиЛиц IS NULL) X INNER JOIN 
	            ТипыКонтактов Типы ON Типы.КодТипаКонтакта = X.КодТипаКонтакта INNER JOIN 
	            Инвентаризация.dbo.Сотрудники Сотрудники ON Сотрудники.КодСотрудника = X.Изменил
UNION
SELECT 	КодКонтакта, NULL КодЛица, NULL КодСвязиЛиц,
	    X.КодЛица КодЛицаСвязи,
	    '(' + Л.Кличка + ')' НадписьЛица,
	    CASE WHEN X.КодЛица = X.КодЛицаРодителя THEN X.КодЛицаПотомка ELSE X.КодЛицаРодителя END КодЛицаСвязанный,
	    CASE WHEN @IdClient IN (X.КодЛицаРодителя, X.КодЛицаПотомка) THEN 1 ELSE 3 END ПоКлиенту, Типы.Icon,
	    X.КодТипаКонтакта, CASE @Язык WHEN 'ru' THEN Типы.ТипКонтакта ELSE Типы.ТипКонтактаЛат END ТипКонтакта, X.Контакт,
	    CASE WHEN X.КодТипаКонтакта BETWEEN 20 AND 39 THEN '+'+LTRIM(X.КонтактRL) ELSE '' END НомерМеждународный,
	    ISNULL(X.Примечание, '') Примечание,
	    CASE @Язык WHEN 'ru' THEN Сотрудники.ФИО ELSE Сотрудники.FIO END ИзменилФИО, X.Изменил, X.Изменено 
FROM (	SELECT	Контакты.* 
	    FROM	vwКонтактыЛица Контакты (nolock) INNER JOIN 
		        #Tbl0 Коды ON Коды.КодЛица = Контакты.КодЛица
	    WHERE	Контакты.КодСвязиЛиц IS NOT NULL AND Контакты.КодЛицаРодителя = @idClient) X INNER JOIN 
	            vwЛица Л (nolock) ON Л.КодЛица = CASE WHEN X.КодЛица = X.КодЛицаРодителя THEN X.КодЛицаПотомка ELSE X.КодЛицаРодителя END INNER JOIN 
	            ТипыКонтактов Типы ON Типы.КодТипаКонтакта = X.КодТипаКонтакта INNER JOIN 
	            Инвентаризация.dbo.Сотрудники Сотрудники ON Сотрудники.КодСотрудника = X.Изменил
")]
        public abstract List<PersonLinkedContact> GetContactsForPersonLinks(string ids, int IdClient);



        /// <summary>
        /// Сохранение изменений в контакте
        /// </summary>
        /// <param name="instance">модель представления</param>
        public void Save(Contact instance)
        {
            if (instance.ID == 0)
                CreateContact(instance);
            else
                Repository.Contacts.Update(instance);
        }


        /// <summary>
        /// Создание нового контакта
        /// </summary>
        /// <param name="instance">модель представления</param>
        [SqlQuery(@"INSERT Справочники.dbo.vwКонтакты (КодЛица,КодСвязиЛиц, КодТипаКонтакта, КодСтраны,
										АдресИндекс,АдресОбласть,АдресГород,АдресГородRus,Адрес,
										ТелефонСтрана, ТелефонГород, ТелефонНомер,ТелефонДоп,
										ДругойКонтакт,Примечание)
                    SELECT @КодЛица,@КодСвязиЛиц, @КодТипаКонтакта, @КодСтраны,
										@АдресИндекс,@АдресОбласть,@АдресГород,@АдресГородRus,@Адрес,
										@ТелефонСтрана, @ТелефонГород, @ТелефонНомер,@ТелефонДоп,
										@ДругойКонтакт,@Примечание
                    SELECT SCOPE_IDENTITY();
        ")]
        public abstract string CreateContact(Contact instance);

        [SqlQuery(@"SELECT dbo.fn_Лица_ФормированиеКонтакта(
					@ТипКонтакта,
					@АдресИндекс,
					@АдресОбласть,
					@АдресГород,
					@АдресГородRus,
					@Адрес,
					@КодСтраны,
					@ТелефонСтрана,
					@ТелефонГород,
					@ТелефонНомер,
					@ТелефонДоп,
					@ДругойКонтакт,
					@ВидКонтакта
		)")]
        public abstract string UpdateContactText(ContactTextParts parameters);


    }
}
