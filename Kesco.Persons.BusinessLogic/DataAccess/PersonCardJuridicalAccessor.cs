using Kesco.Persons.ObjectModel;
using Kesco.DataAccess;
using BLToolkit.DataAccess;
using System.Collections.Generic;

namespace Kesco.Persons.BusinessLogic.DataAccess
{
	public abstract class PersonCardJuridicalAccessor : EntityAccessor<PersonCardJuridicalAccessor, DB, PersonCardJuridical, PersonCardJuridicalAccessor.SearchParameters, int>
	{
		public class SearchParameters : Kesco.DataAccess.SearchParameters { }

		[SqlQuery("SELECT * FROM vwКарточкиЮрЛиц WHERE КодЛица = @id")]
		public abstract List<PersonCardJuridical> GetInstancesByPersonID(int @id);
	}
}
