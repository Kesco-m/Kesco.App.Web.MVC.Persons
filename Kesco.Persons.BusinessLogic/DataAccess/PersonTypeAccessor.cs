using System.Collections.Generic;
using BLToolkit.DataAccess;
using Kesco.Persons.ObjectModel;
using Kesco.DataAccess;

namespace Kesco.Persons.BusinessLogic.DataAccess
{
    public abstract class PersonTypeAccessor : EntityAccessor<PersonTypeAccessor, DB, PersonType, PersonTypeAccessor.SearchParameters, int>
    {
		public class SearchParameters : Kesco.DataAccess.SearchParameters { }
		
		[SqlQuery(@"SELECT * FROM ТипыЛиц")]
        public abstract List<PersonType> GetAllPersonTypes();

        [SqlQuery(@"SELECT * FROM ТипыЛиц where КодТемыЛица = @id")]
        public abstract List<PersonType> GetListByThemeId(int @id);

		/// <summary>
		/// Возвращает список кодов типов лиц для указанного списка кодов тем.
		/// </summary>
		/// <param name="ids">Список кодов тем.</param>
		/// <returns></returns>
		[SqlQuery(@"
        SELECT ТиЛ.КодТипаЛица
        FROM vwТипыЛицСотрудника ТиЛ
	        INNER JOIN vwТемыЛиц ТеЛ ON ТиЛ.КодТемыЛица=ТеЛ.КодТемыЛица
	        INNER JOIN Каталоги К ON ТиЛ.КодКаталога=К.КодКаталога
        WHERE ТеЛ.КодТемыЛица IN (SELECT value FROM Инвентаризация.dbo.fn_SplitInts(@ids))
		")]
		public abstract List<int> GetTypeIDsByThemeIDs(string @ids);


		[SqlQuery(@"
			SELECT ТиЛ.КодТипаЛица, ТеЛ.КодТемыЛица, ТеЛ.ТемаЛица, К.КодКаталога, К.Каталог
			FROM vwТипыЛицСотрудника ТиЛ
				INNER JOIN vwТемыЛиц ТеЛ ON ТиЛ.КодТемыЛица=ТеЛ.КодТемыЛица
				INNER JOIN Каталоги К ON ТиЛ.КодКаталога=К.КодКаталога
			WHERE ТеЛ.КодТемыЛица IN (SELECT value FROM Инвентаризация.dbo.fn_SplitInts(@themeIDs))
			ORDER BY ТемаЛица, Каталог"
		)]
		public abstract List<PersonTypeInfo> GetAvailablePersonTypesByThemeIDs(string @themeIDs);

		public class PersonTypeInfo {
			public int КодТипаЛица { get; set; }
			public int КодТемыЛица { get; set; }
			public string ТемаЛица { get; set; }
			public int КодКаталога { get; set; }
			public string Каталог { get; set; }
		}

		/// <summary>
		/// Возвращает список кодов типов лиц
		/// </summary>
		/// <param name="personID">Код лица.</param>
		/// <returns>Список кодов типов лиц</returns>
		[SqlQuery(@"SELECT КодТипаЛица FROM Лица_ТипыЛиц WHERE КодЛица = @personID AND Сотрудник = 0")]
		public abstract List<int> GetPersonTypeIDListByPersonID(int @personID);

		/// <summary>
		/// Возвращает список типов лиц с информацией.
		/// </summary>
		/// <param name="typeIDs">Список кодов типов, разделённых запятой.</param>
		/// <returns>Список типов лиц с информацией.</returns>
		[SqlQuery(@"
			SELECT  КодТипаЛица, ТеЛ.КодТемыЛица, ТеЛ.ТемаЛица, К.КодКаталога, К.Каталог, ТеЛ.L
			FROM vwТипыЛицСотрудника Тил
				INNER JOIN vwТемыЛиц ТеЛ ON ТиЛ.КодТемыЛица=ТеЛ.КодТемыЛица
				INNER JOIN Каталоги К ON Тил.КодКаталога=К.КодКаталога
			WHERE КодТипаЛица IN(SELECT value FROM Инвентаризация.dbo.fn_SplitInts(@typeIDs))
			ORDER BY ТемаЛица, Каталог
		")]
		public abstract List<PersonTypeInfo> GetPersonTypesByTypeIDs(string @typeIDs);

		/// <summary>
		/// Возвращает список типов лиц с информацией.
		/// </summary>
		/// <param name="personID">Код лица</param>
		/// <returns>
		/// Список типов лиц с информацией.
		/// </returns>
		[SqlQuery(@"
			SELECT  КодТипаЛица, ТеЛ.КодТемыЛица, ТеЛ.ТемаЛица, К.КодКаталога, К.Каталог, ТеЛ.L
			FROM vwТипыЛицСотрудника Тил
				INNER JOIN vwТемыЛиц ТеЛ ON ТиЛ.КодТемыЛица=ТеЛ.КодТемыЛица
				INNER JOIN Каталоги К ON Тил.КодКаталога=К.КодКаталога
			WHERE КодТипаЛица IN( SELECT КодТипаЛица FROM Лица_ТипыЛиц WHERE КодЛица = @personID AND Сотрудник = 0 )
			ORDER BY ТемаЛица, Каталог
		")]
		public abstract List<PersonTypeInfo> GetPersonTypesByPersonID(int personID);

		/// <summary>
		/// Gets the instance by person type ID.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		[SqlQuery("SELECT * FROM ТипыЛиц where КодТипаЛица = @id")]
        public abstract PersonType GetInstanceByPersonTypeID(int @id);

	}
}
