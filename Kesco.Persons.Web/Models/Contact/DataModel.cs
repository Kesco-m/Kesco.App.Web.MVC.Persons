using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kesco.Persons.ObjectModel;

namespace Kesco.Persons.Web.Models.Contact
{
	public class DataModel
	{
		public Contact Contact { get; set; }
		
		public Person Person { get; set; }

		public int? AreaID { get; set; }
		public string Direction { get; set; }

		public DataModel()
		{
			Contact = Contact.CreateInstance();
		}

	}
}