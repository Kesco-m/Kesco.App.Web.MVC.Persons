using System.Collections.Generic;
using System.Linq;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.ObjectModel;
using Kesco.Persons.ObjectModel;
using Kesco.Persons.BusinessLogic;
using System;

namespace Kesco.Employees.Controls.Models.EmployeeInfo
{
	public class EmployeeInfoContact
	{
		public string Contact { get; set; }
		public int ContactType { get; set; }
		public string Icon { get; set; }
		public string PhoneNumber { get; set; }
		public string Note { get; set; }
		public string @Type { get; set; }
		public int Order { get; set; }
		public bool InDictionary { get; set; }
	}

	/// <summary>
	/// Представляет модель представления для EmployeeInfo
	/// </summary>
	public class ViewModel : Kesco.Web.Mvc.DialogViewModel
	{

		/// <summary>
		/// Gets or sets the employee ID.
		/// </summary>
		/// <value>
		/// The employee ID.
		/// </value>
		public int EmployeeID { get; set; }

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
		/// Gets or sets the contacts.
		/// </summary>
		/// <value>
		/// The contacts.
		/// </value>
		public List<Kesco.Employees.ObjectModel.EmployeeContact> Contacts { get; set; }

		/// <summary>
		/// Gets or sets the person.
		/// </summary>
		/// <value>
		/// The person.
		/// </value>
		public Person Person { get; set; }

		/// <summary>
		/// Gets or sets the person contacts.
		/// </summary>
		/// <value>
		/// The person contacts.
		/// </value>
		public List<PersonContact> PersonContacts { get; set; }

		/// <summary>
		/// Gets or sets all contacts.
		/// </summary>
		/// <value>
		/// All contacts.
		/// </value>
		public List<EmployeeInfoContact> AllContacts { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ViewModel"/> class.
		/// </summary>
		/// <param name="employeeID">The employee ID.</param>
		public ViewModel(int employeeID)
		{
			Contacts = new List<Kesco.Employees.ObjectModel.EmployeeContact>();
			PersonContacts = new List<PersonContact>();
			EmployeeID = employeeID;
			Employee = Kesco.Employees.BusinessLogic.Repository.Employees.GetInstance(EmployeeID);
			if (Employee != null) {
				LastPassage = Kesco.Employees.BusinessLogic.Repository.Employees.GetEmployeeLastPassage(EmployeeID);
				Contacts = Kesco.Employees.BusinessLogic.Repository.Employees.GetEmployeeContacts(EmployeeID, null);
				if (Employee.PersonID.HasValue) {
					Person = Repository.Persons.GetInstance(Employee.PersonID.Value);
					if (Person != null)
						PersonContacts = Repository.Contacts.GetPersonContacts(Person.ID);
				}
			}

			AllContacts = Contacts.Select(c => new EmployeeInfoContact {
				Contact = c.Contact,
				ContactType = c.ContactType,
				Icon = c.Icon,
				PhoneNumber = (c.ContactType == -1) ? c.Contact : String.Empty,
				Note = c.Note,
				@Type = c.ContactTypeDesc,
				Order = c.Order,
				InDictionary = c.InDictionary
			}).Concat(
				PersonContacts.Select(c => new EmployeeInfoContact {
					Contact = c.ContactText,
					ContactType = c.ContactTypeID,
					PhoneNumber = (c.ContactTypeID >= 20 && c.ContactTypeID <= 39) ? ("+" + c.PhoneNumber) : "",
					Note = c.Comment,
					@Type = c.ContactTypeDesc,
					Order = c.Order,
					InDictionary = true
				})
			).OrderBy(c => c.Order).ToList();
		}


		protected override void CreateSettings()
		{
			settings = new { dummy = 1};
		}
	}
}