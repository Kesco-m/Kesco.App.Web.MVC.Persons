using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kesco.Persons.Web.Models
{

	public enum ContactDisplayMode : int
	{
		Simple = 0,
		Icon = 1,
		Popup = 2
	}

	public class ContactAssistantModel
	{
		public ContactDisplayMode HowToDisplay { get; set; }
		public int ContactType { get; set; }
		public dynamic ContactDetails { get; set; }
		/*
		public string Icon { get; set; }
		public string Contact { get; set; }
		public string Note { get; set; }
		public string PhoneNumber { get; set; }
		public string QuickCall { get; set; }
		public int? PhoneCenterID { get; set; }
		public string PhoneNumberType { get; set; }
		public bool PhoneNumberType { get; set; }
		*/
	}
}