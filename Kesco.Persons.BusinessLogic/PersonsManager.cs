using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kesco.DataAccess;
using Kesco.Persons.ObjectModel;

namespace Kesco.Persons.BusinessLogic
{
	public static class PersonsManager 
	{
		public static PersonListItem GetPersonShortInfo(int id)
		{
			return Accessor.GetPersonShortInfo(id);
		}

		public static List<PersonListItem> SearchPersons(string keyword, int limit)
		{
			List<PersonListItem> results = null;
			if (!String.IsNullOrWhiteSpace(keyword)) {
				results = Accessor.SearchPersons("%" + keyword + "%", limit);
			} else {
				results = Accessor.SearchPersons("%", limit);
			}

			return results;
		}

		public static List<PersonListItem> GetPersonList(int[] personIDs)
		{
			List<PersonListItem> results = null;
			if (personIDs != null && personIDs.Length != 0) {
				//List<string> list = new List<string>();
				//personIDs.ToList<int>().ForEach(id => list.Add(id.ToString()));
				results = Accessor.GetPersonList(String.Join(",", personIDs));
			}

			return results;
		}
        		

		#region Accessor

		static PersonsAccessor Accessor
		{
			[System.Diagnostics.DebuggerStepThrough]
			get { return PersonsAccessor.CreateInstance(); }
		}

		#endregion
	}
}
