using Kesco.DataAccess;
using Kesco.Persons.ObjectModel;
using BLToolkit.DataAccess;
using System.Collections.Generic;

namespace Kesco.Persons.BusinessLogic.DataAccess
{
	public abstract class IncorporationFormAccessor : EntityAccessor<IncorporationFormAccessor, DB, IncorporationForm, IncorporationFormAccessor.SearchParameters, int>
	{
		/// <summary>
        /// Ovverride search parameters, set PersonType (1 - Juridical, 2 - Natural)
		/// </summary>
		public class SearchParameters : Kesco.DataAccess.SearchParameters {

			public int PersonKind { get; set; }
		}
		
		[SqlQuery(@"
			SELECT * FROM ОргПравФормы 
			ORDER BY ОргПравФорма")]
        public abstract List<IncorporationForm> GetAll();

		[SqlQuery(@"
			SELECT * FROM ОргПравФормы 
			WHERE ТипЛица = @personKind 
			ORDER BY ОргПравФорма")]
		public abstract List<IncorporationForm> GetAllByPersonKind(int personKind);

		public override List<IncorporationForm> Search(SearchParameters criteria)
		{
			return SearchForms(criteria);
		}

		[SqlQuery(@"
			SET @personKind = COALESCE(@personKind,0)
			SELECT * FROM ОргПравФормы 
			WHERE 
				@personKind = 0 OR ТипЛица = @personKind 
			ORDER BY ОргПравФорма")]
		protected abstract List<IncorporationForm> SearchForms(SearchParameters parameters);
	}
}
