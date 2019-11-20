using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kesco.Persons.ObjectModel;
using Kesco.Employees.ObjectModel;
using Kesco.Persons.BusinessLogic.Persons;

namespace Kesco.Persons.Web.Models.Synchronize
{
	/// <summary>
	/// Модель данных для синхронизации лица с сотрудником
	/// </summary>
	public class DataModel
	{
		/// <summary>
		/// Gets or sets the person.
		/// </summary>
		/// <value>
		/// The person.
		/// </value>
		public Person Person { get; set; }

		/// <summary>
		/// Gets or sets the person card.
		/// </summary>
		/// <value>
		/// The person card.
		/// </value>
		public PersonCardNatural PersonCard { get; set; }

		/// <summary>
		/// Gets or sets the person positions.
		/// </summary>
		/// <value>
		/// The person positions.
		/// </value>
		public List<PersonPosition> PersonPositions { get; set; }

		/// <summary>
		/// Gets or sets the employee.
		/// </summary>
		/// <value>
		/// The employee.
		/// </value>
		public Employee Employee { get; set; }

		/// <summary>
		/// Gets or sets the person employee positions.
		/// </summary>
		/// <value>
		/// The person employee positions.
		/// </value>
		public List<PersonPosition> EmployeePositions { get; set; }

	}
}