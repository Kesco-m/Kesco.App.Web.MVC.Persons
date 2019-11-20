using Kesco.Persons.ObjectModel;
using Kesco.DataAccess;

namespace Kesco.Persons.BusinessLogic.DataAccess
{
    public abstract class DepartmentAccessor : EntityAccessor<DepartmentAccessor, DB, Department, DepartmentAccessor.SearchParameters, int>
    {
		public class SearchParameters : Kesco.DataAccess.SearchParameters { }
	}
}
