using System.Collections.Generic;
using BLToolkit.DataAccess;
using Kesco.DataAccess;
using Kesco.Persons.ObjectModel;

namespace Kesco.Persons.BusinessLogic.DataAccess
{
	/// <summary>
	/// Проводник данных для типов контактов
	/// </summary>
    public abstract class ContactTypeAccessor : EntityAccessor<ContactTypeAccessor, DB, ContactType, ContactTypeAccessor.SearchParameters, int>
    {
		/// <summary>
		/// Параментрвы поиска
		/// </summary>
		public class SearchParameters : Kesco.DataAccess.SearchParameters { }

		/// <summary>
		/// Возвращает список типов контактов для указанного списка кодов
		/// </summary>
		/// <param name="ids">The ids.</param>
		/// <returns></returns>
		[SqlQuery(@"
			SET @ids = COALESCE(@ids, '')
			SET @categoryIDs = COALESCE(@categoryIDs, '')
			
			SELECT * FROM ТипыКонтактов
			WHERE (@ids = '' OR КодТипаКонтакта IN (SELECT value FROM Инвентаризация.dbo.fn_SplitInts(@ids)))
				AND (@categoryIDs = '' OR Категория IN (SELECT value FROM Инвентаризация.dbo.fn_SplitInts(@categoryIDs)))
			ORDER BY КодТипаКонтакта")]
		public abstract List<ContactType> GetListByIds(string ids, string categoryIDs = "");
	}
}
