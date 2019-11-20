using System;
using System.Linq;
using System.Collections.Generic;
using BLToolkit.DataAccess;
using Kesco.DataAccess;
using Kesco.Persons.ObjectModel;

namespace Kesco.Persons.BusinessLogic.DataAccess
{
    public abstract class CaseAccessor : EntityAccessor<CaseAccessor, DB, Case, CaseAccessor.SearchParameters, int>
    {
		public class SearchParameters : Kesco.DataAccess.SearchParameters { }

		public override List<Case> Search(SearchParameters criteria)
		{
			List<Case> links = new List<Case>();
			var extendedLinks = SearchExtended(criteria);
			extendedLinks.All(l => { links.Add(l); return true; });
			return links;
		}

		[SqlQuery(@"
			SELECT * 
			FROM Справочники.dbo.Падежи
			WHERE 
				EXISTS(SELECT * FROM Инвентаризация.dbo.fn_SplitIntoWords(@Search) WHERE Падежи.[Именительный] = Слово)
			")]
		protected abstract IList<Case> SearchExtended(SearchParameters parameters);

	}
}
