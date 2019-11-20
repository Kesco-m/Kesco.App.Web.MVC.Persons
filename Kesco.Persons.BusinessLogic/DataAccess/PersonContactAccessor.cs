using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kesco.DataAccess;
using Kesco.Persons.ObjectModel;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;

namespace Kesco.Persons.BusinessLogic.DataAccess
{
	public abstract class PersonContactAccessor : EntityAccessor<PersonContactAccessor, DB, PersonContact, PersonContactAccessor.SearchParameters, int>
	{
		[MapField("СтрокаПоиска", "Search")]
		public class SearchParameters : Kesco.DataAccess.SearchParameters
		{
			[MapField("КодЛица")]
			public int PersonID { get; set; }

			[MapField("СписокКодовТиповКонтактов")]
			public string ContactTypeIDList { get; set; }
		}

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		[SqlQuery(@"
			DECLARE @Язык char(2)
			SET @Язык = 'ru' SELECT @Язык = Язык  FROM Инвентаризация.dbo.Сотрудники WHERE SID = SUSER_SID()

			SELECT 
				КЛ.КодКонтакта, 
				ТК.icon, 
				КЛ.КодТипаКонтакта, 
				CASE WHEN @Язык = 'ru' THEN ТК.ТипКонтакта ELSE ТК.ТипКонтактаЛат END ТипКонтакта, 
				КЛ.Контакт, 
				CASE  WHEN КЛ.КодСвязиЛиц IS NULL THEN 100 + КЛ.КодТипаКонтакта ELSE 200 + КЛ.КодТипаКонтакта  END AS Порядок, 
				CASE 
					WHEN @Язык = 'ru' THEN LTRIM(RTRIM(КЛ.Примечание + ' ' + ISNULL(vwСвязиЛиц.Описание, '')))
					ELSE Инвентаризация.dbo.fn_TransLit(LTRIM(RTRIM(КЛ.Примечание + ' ' +ISNULL(vwСвязиЛиц.Описание, ''))))
				END Примечание,
				CASE WHEN КЛ.КодТипаКонтакта BETWEEN 20 AND 39 THEN '+'+LTRIM(КЛ.КонтактRL)ELSE '' END НомерМеждународный
			FROM vwКонтакты КЛ (nolock)
				INNER JOIN ТипыКонтактов ТК ON КЛ.КодТипаКонтакта = ТК.КодТипаКонтакта
				LEFT JOIN vwСвязиЛиц (nolock) ON КЛ.КодСвязиЛиц = vwСвязиЛиц.КодСвязиЛиц
			WHERE КЛ.КодКонтакта = @id

		")]
		public new abstract PersonContact GetInstance(int id);

		/// <summary>
		/// Осуществляет поиск контактов лица по заданным параметрам
		/// </summary>
		/// <param name="кодЛица">Код лица.</param>
		/// <param name="строкаПоиска">Строка поиска.</param>
		/// <param name="СписокКодовТиповКонтактов">The СПИСОК КОДОВ ТИПОВ КОНТАКТОВ.</param>
		/// <returns></returns>
		[SprocName(@"sp_КонтактыЛица_Поиск")]
		public abstract List<PersonContactSearchResult> SearchPersonContacts(int @кодЛица, string @строкаПоиска, string @СписокКодовТиповКонтактов);
	}
}
