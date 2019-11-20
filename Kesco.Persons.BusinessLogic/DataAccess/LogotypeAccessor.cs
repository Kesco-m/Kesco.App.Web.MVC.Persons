using Kesco.Persons.ObjectModel;
using Kesco.DataAccess;
using BLToolkit.DataAccess;
using System.Collections.Generic;

namespace Kesco.Persons.BusinessLogic.DataAccess
{
	/// <summary>
	/// Реализует класс проводника данных для сущности <see cref="Logotype"/>
	/// </summary>
    public abstract class LogotypeAccessor : EntityAccessor<LogotypeAccessor, DB, Logotype, LogotypeAccessor.SearchParameters, int>
    {

		public class SearchParameters : Kesco.DataAccess.SearchParameters { }

		[SqlQuery("SELECT * FROM ЛоготипыЛиц WHERE КодЛица = @personID")]
		public abstract List<Logotype> GetLogotypesByPersonID(int @personID);

		[SqlQuery("SELECT COALESCE(COUNT(*), 0) as КоличествоЛоготипов FROM ЛоготипыЛиц WHERE КодЛица = @personID")]
		public abstract int GetLogotypeCountByPersonID(int @personID);

		/// <summary>
		/// Возвращает логотип лица.
		/// </summary>
		/// <param name="кодЛица">The КОД ЛИЦА.</param>
		/// <param name="кодЛоготипа">The КОД ЛОГОТИПА.</param>
		/// <returns>Логотип лица</returns>
		[SqlQuery(@"
				SELECT TOP 1 * 
				FROM ЛоготипыЛиц
				WHERE	(@КодЛица IS NULL OR КодЛица = @КодЛица)
					AND	(@КодЛоготипаЛица IS NULL OR КодЛоготипаЛица = @КодЛоготипаЛица)
				ORDER BY ДатаСохранения DESC
		")]
		public abstract Logotype GetPersonLogotype(int? @кодЛица, int? @кодЛоготипаЛица);
	}

}
