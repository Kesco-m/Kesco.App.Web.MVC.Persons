using Kesco.Persons.ObjectModel;
using Kesco.DataAccess;
using BLToolkit.DataAccess;
using System.Collections.Generic;

namespace Kesco.Persons.BusinessLogic.DataAccess
{
	public abstract class PersonCardNaturalAccessor : EntityAccessor<PersonCardNaturalAccessor, DB, PersonCardNatural, PersonCardNaturalAccessor.SearchParameters, int>
	{
		public class SearchParameters : Kesco.DataAccess.SearchParameters { }

		[SqlQuery("SELECT * FROM vwКарточкиФизЛиц WHERE КодЛица = @id")]
		public abstract List<PersonCardNatural> GetInstancesByPersonID(int @id);

		[SqlQuery("SELECT * FROM vwКарточкиФизЛиц WHERE КодЛица = @кодЛица AND От <= GETDATE() AND До > GETDATE()")]
		public abstract PersonCardNatural GetCurrentPersonCard(int @кодЛица);

	}
}
