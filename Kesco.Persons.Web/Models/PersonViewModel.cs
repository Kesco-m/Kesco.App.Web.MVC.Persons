using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kesco.BusinessProjects.ObjectModel;
using Kesco.Persons.ObjectModel;
using Kesco.Territories.ObjectModel;
using Kesco.Web.Mvc;
using Kesco.Persons.BusinessLogic;


namespace Kesco.Persons.Web.Models
{
	public class PersonViewModel : DialogViewModel
	{
		[UIHint("PersonInfo")]
		public Person Person { get; internal set; }

		[UIHint("ResponsibleEmployees")]
		public List<Kesco.Employees.ObjectModel.Employee> ResponsibleEmployees { get; internal set; }

		public Territory PersonTerritory { get; internal set; }

		public BusinessProject PersonBusinessProject { get; internal set; }
		
		public List<ContactType> ContactTypes { get; internal set; }

		public List<Kesco.Persons.ObjectModel.Contact> Contacts { get; internal set; }

		public PersonViewModel(int id) : base()
		{
			Person = Repository.Persons.Query.SelectByKey<Person>(id);
			if (Person == null)
				throw new Exception( "Лицо не найдено" );

			if (Person.TerritoryID.HasValue) {
				PersonTerritory = Territories.BusinessLogic.Repository.Territories.GetInstance(Person.TerritoryID.Value);
			}

			if (Person.BusinessProjectID.HasValue) {
				PersonBusinessProject = BusinessProjects.BusinessLogic.Repository.BusinessProjects.GetInstance(Person.BusinessProjectID.Value);
			}

			ResponsibleEmployees = Repository.ResponsibleEmployees.GetResponsibleEmployeesByPersonId(Person.ID);

			ContactTypes = Repository.ContactTypes.Query.SelectAll<ContactType>();

			Contacts = Repository.Contacts.GetContactsByPersonId(Person.ID);
			foreach (Kesco.Persons.ObjectModel.Contact c in Contacts)
				c.ContactType = Repository.Contacts.Query.SelectByKey<ContactType>(c.ContactTypeID);
		}

		protected override void CreateSettings()
		{
			settings = new object();
		}

	}
}