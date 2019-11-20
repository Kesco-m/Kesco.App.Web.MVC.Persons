using System.Text.RegularExpressions;
using Kesco.DataAccess;
using Kesco.Persons.ObjectModel;
using BLToolkit.DataAccess;
using System.Collections.Generic;

namespace Kesco.Persons.BusinessLogic.DataAccess
{
    public abstract class PersonThemeAccessor : EntityAccessor<PersonThemeAccessor, DB, PersonTheme, PersonThemeAccessor.SearchParameters, int>
	{
		/// <summary>
        /// Ovverride search parameters, set PersonType (1 - Juridical, 2 - Natural)
		/// </summary>
		public class SearchParameters : Kesco.DataAccess.SearchParameters {}

        [SqlQuery(
        @"
		SELECT КодТемыЛица, ТемаЛица FROM Справочники..vwТемыЛиц_Администрирование WHERE КодТемыЛица IS NOT NULL AND ТемаЛица LIKE '%'+@search+'%'
	    UNION
	    SELECT КодТемыЛица,ТемаЛица FROM Справочники..vwТемыЛиц_Tree
	    WHERE КодТемыЛица NOT IN (SELECT КодТемыЛица FROM Справочники..vwТемыЛиц_Администрирование WHERE КодТемыЛица IS NOT NULL)
        AND ТемаЛица LIKE '%'+@search+'%'
        OPTION (MAXDOP 1)")]
        public abstract List<PersonTheme> SearchLike(string search);

        [SqlQuery(
        @"
		SELECT КодТемыЛица FROM Справочники..vwТемыЛиц_Администрирование WHERE КодТемыЛица IS NOT NULL AND Parent IN (@themeIDs)
	    UNION
	    SELECT КодТемыЛица FROM Справочники..vwТемыЛиц_Tree
	    WHERE КодТемыЛица NOT IN (SELECT КодТемыЛица FROM Справочники..vwТемыЛиц_Администрирование WHERE КодТемыЛица IS NOT NULL)
        AND Parent IN (@themeIDs)")]
        public abstract List<int> GetPersonThemesChildsByThemeIds(int themeIDs);


        [SqlQuery(
        @"
		SELECT КодТемыЛица, ТемаЛица, Parent FROM Справочники..vwТемыЛиц_Администрирование WHERE КодТемыЛица IS NOT NULL
	    UNION
	    SELECT КодТемыЛица,ТемаЛица, Parent FROM Справочники..vwТемыЛиц_Tree
	    WHERE КодТемыЛица NOT IN (SELECT КодТемыЛица FROM Справочники..vwТемыЛиц_Администрирование WHERE КодТемыЛица IS NOT NULL)
        OPTION (MAXDOP 1)")]
        public abstract List<PersonTheme> GetAll();

        public override List<PersonTheme> Search(SearchParameters criteria)
        {
            string clearSearchString = null;
            if (!string.IsNullOrEmpty(criteria.Search))
            {
                clearSearchString += Regex.Replace(criteria.Search.Trim(), @"\s+", "%");
            }
            if(string.IsNullOrEmpty(criteria.Search) )
            {
                return GetAll();
            }
            return SearchLike(clearSearchString);
        }


        [SqlQuery(@"
			SELECT * FROM vwТемыЛиц
			WHERE КодТемыЛица IN( SELECT value FROM Инвентаризация.dbo.fn_SplitInts(@ids) )
			ORDER BY ТемаЛица")]
        public abstract List<PersonTheme> GetListByIds(string ids);

        [SqlQuery(@"
			SELECT DISTINCT ТЛС.КодТемыЛица, ТЛ.ТемаЛица
			FROM vwТипыЛицСотрудника ТЛС
				INNER JOIN vwТемыЛиц ТЛ ON ТЛ.КодТемыЛица = ТЛС.КодТемыЛица
				INNER JOIN Лица_ТипыЛиц ЛТЛ ON ЛТЛ.КодТипаЛица = ТЛС.КодТипаЛица
			WHERE ЛТЛ.КодЛица = @personID AND ТЛС.КодТемыЛица IN (
					SELECT КодТемыЛица FROM Лица_ТипыЛиц
						INNER JOIN vwТипыЛицСотрудника ON vwТипыЛицСотрудника.КодТипаЛица = Лица_ТипыЛиц.КодТипаЛица
					WHERE КодЛица = @personID and Сотрудник = 0 )")]
        public abstract List<PersonTheme> GetPersonThemesByPersonId(int personID);


        /// <summary>
        /// Search themes for person
        /// </summary>
        /// <returns></returns>
        [SqlQuery(@"
			SELECT КодТемыЛица, ТемаЛица FROM Справочники..vwТемыЛиц_Администрирование WHERE КодТемыЛица IS NOT NULL  AND ТемаЛица Like '%@ТемаЛица%'
	        UNION
	        SELECT КодТемыЛица,ТемаЛица FROM Справочники..vwТемыЛиц_Tree
	        WHERE КодТемыЛица NOT IN (SELECT КодТемыЛица FROM Справочники..vwТемыЛиц_Администрирование WHERE КодТемыЛица IS NOT NULL)
            AND ТемаЛица Like '%@ТемаЛица%' OPTION (MAXDOP 1)")]
        public abstract List<PersonTheme> SearchPersonThemes(SearchParameters criteria);
        
       
	}
}
