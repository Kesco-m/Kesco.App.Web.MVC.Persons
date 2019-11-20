using Kesco.Persons.ObjectModel;
using Kesco.DataAccess;

namespace Kesco.Persons.BusinessLogic.DataAccess
{
    public abstract class MenuItemAccessor : EntityAccessor<MenuItemAccessor, DB, MenuItem, MenuItemAccessor.SearchParameters, int>
    {
		public class SearchParameters : Kesco.DataAccess.SearchParameters { }

    }
}
