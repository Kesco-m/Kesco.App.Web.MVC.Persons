using Kesco.Persons.ObjectModel;
using Kesco.DataAccess;

namespace Kesco.Persons.BusinessLogic.DataAccess
{
    public abstract class PersonLinkTypeAccessor : EntityAccessor<PersonLinkTypeAccessor, DB, PersonLinkType, PersonLinkTypeAccessor.SearchParameters, int>
    {
		public class SearchParameters : Kesco.DataAccess.SearchParameters { }
	}
}
