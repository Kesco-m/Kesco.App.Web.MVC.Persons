using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kesco.Web.Mvc.UI;
using Kesco.Persons.ObjectModel;
using Kesco.Web.Mvc.Filtering;

namespace Kesco.Persons.Web.Models.Contact
{
	public class PersonsListContactFilterSetting : ListQSFilterSetting<PersonsListContactFilterSetting, ContactType> { }

	public class ClientParameters : ClientParametersBase
	{
		public string PersonsListContact { get; set; }

		public ClientParameters() : base() {
			PersonsListContact = String.Empty;
		}
	}
}