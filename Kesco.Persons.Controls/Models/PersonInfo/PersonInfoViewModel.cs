using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kesco.Web.Mvc;

namespace Kesco.Persons.Controls.Models.PersonInfo
{
	/// <summary>
	/// Класс модели представления контакта лица для PersonInfo
	/// </summary>
	public class ContactInfo
	{
		public string ContactName { get; set; }
		public int ContactType { get; set; }
		public string Contact { get; set; }
		public string PhoneNumber { get; set; }
		public string Note { get; set; }
		public string @Type { get; set; }
		public int Order { get; set; }
		public bool InDictionary { get; set; }
	}


	/// <summary>
	/// Класс модели представления для PersonInfo
	/// </summary>
	public class ViewModel : DialogViewModel
	{
		/// <summary>
		/// Gets or sets the person ID.
		/// </summary>
		/// <value>
		/// The person ID.
		/// </value>
		public int PersonID { get; set; }

		/// <summary>
		/// Gets or sets the person.
		/// </summary>
		/// <value>
		/// The person.
		/// </value>
		public Kesco.Persons.ObjectModel.Person Person { get; protected set; }

		/// <summary>
		/// Gets or sets the employee.
		/// </summary>
		/// <value>
		/// The employee.
		/// </value>
		public Kesco.Employees.ObjectModel.Employee Employee { get; protected set; }

		/// <summary>
		/// Gets or sets the employee.
		/// </summary>
		/// <value>
		/// The employee.
		/// </value>
		public Kesco.Employees.ObjectModel.EmployeePassage LastPassage { get; protected set; }

		/// <summary>
		/// Gets or sets all contacts.
		/// </summary>
		/// <value>
		/// All contacts.
		/// </value>
		public List<ContactInfo> AllContacts { get; protected set; }

		/// <summary>
		/// Возвращает или устанавливает признак, указыващий имеет ли лицо логотипы.
		/// </summary>
		/// <value>
		/// 	<c>true</c> если лицо имеет логотипы; иначе, <c>false</c>.
		/// </value>
		public bool HasLogotypes { get; protected set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PersonInfoViewModel"/> class.
		/// </summary>
		/// <param name="personID">The person ID.</param>
		public ViewModel(int personID)
		{
			List<Kesco.Employees.ObjectModel.EmployeeContact> employeeContacts = new List<Kesco.Employees.ObjectModel.EmployeeContact>();
			List<Kesco.Persons.ObjectModel.PersonContact> personContacts = new List<Kesco.Persons.ObjectModel.PersonContact>();
			PersonID = personID;

			Person = Kesco.Persons.BusinessLogic.Repository.Persons.GetInstance(personID);
			if (Person != null)
			{
				personContacts = Kesco.Persons.BusinessLogic.Repository.Contacts.GetPersonContacts(Person.ID);
				HasLogotypes = Kesco.Persons.BusinessLogic.Repository.Logotypes.GetLogotypeCountByPersonID(Person.ID) > 0;
				if (Person.PersonType != Kesco.Persons.ObjectModel.PersonCardType.Juridical)
				{
					Employee = Kesco.Employees.BusinessLogic.Repository.Employees.GetEmployeeByPersonID(Person.ID);
					if (Employee != null)
					{
						LastPassage = Kesco.Employees.BusinessLogic.Repository.Employees.GetEmployeeLastPassage(Employee.ID);
						employeeContacts = Kesco.Employees.BusinessLogic.Repository.Employees.GetEmployeeContacts(Employee.ID, null);
					}
				}
			}

			AllContacts = personContacts.Select(c => new ContactInfo
			{
				ContactName = Person.Nickname,
				ContactType = c.ContactTypeID,
				Contact = c.ContactText,
				PhoneNumber = (c.ContactTypeID >= 20 && c.ContactTypeID <= 39) ? ("+"+c.PhoneNumber) : "",
				Note = c.Comment,
				@Type = c.ContactTypeDesc,
				Order = c.Order,
				InDictionary = true
			}).Concat(employeeContacts.Select(c => new ContactInfo
			{
				ContactName = Person.Nickname,
				ContactType = c.ContactType,
				Contact = c.Contact,
				PhoneNumber = (c.ContactType == -1) ? c.Contact : String.Empty,
				Note = c.Note,
				@Type = c.ContactTypeDesc,
				Order = c.Order,
				InDictionary = c.InDictionary
			})
			).OrderBy(c => c.Order).ToList();
		}


		protected override void CreateSettings()
		{
			settings = new { Dummy = 1 };
		}
	}
}