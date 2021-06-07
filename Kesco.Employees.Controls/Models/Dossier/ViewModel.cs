using System;
using System.Collections.Generic;
using System.Linq;
using Kesco.Employees.BusinessLogic;
using Kesco.Employees.Controls.Models.EmployeeInfo;
using Kesco.Employees.ObjectModel;

namespace Kesco.Employees.Controls.Models.Dossier.EmployeeInfo
{
	/// <summary>
	/// Объединение контактов по их типу
	/// </summary>
	public class ContactTypeGroup
	{
		/// <summary>
		/// название типа
		/// </summary>
		public string ContactType { get; set; }
		// Id типа контактов
		public int ContactTypeID { get; set; }

		/// <summary>
		/// список контактов такого типа
		/// </summary>
		public List<EmployeeInfoContact> Items { get; set; }
	}

	/// <summary>
	/// Представляет модель представления для EmployeeInfo
	/// </summary>
	public class ViewModel
	{

		/// <summary>
		/// Gets or sets the employee ID.
		/// </summary>
		/// <value>
		/// The employee ID.
		/// </value>
		public int EmployeeID { get; set; }

        /// <summary>
        /// Отсутствует
        /// </summary>
        public bool Replaced { get; set; }

		/// <summary>
		/// Gets or sets the employee.
		/// </summary>
		/// <value>
		/// The employee.
		/// </value>
		public Employee Employee { get; protected set; }

        /// <summary>
        /// Gets or sets the Employee.
        /// </summary>
        /// <value>
        /// The Employee.
        /// </value>
        public Employee EmployeeSupervisor { get; protected set; }


		/// <summary>
		/// Gets or sets the person.
		/// </summary>
		/// <value>
		/// The person.
		/// </value>
		public EmployeeCustomer EmployeeCustomer { get; protected set; }

		public EmployeeCustomer EmployeeChangedBy { get; protected set; }


       

		/// <summary>
		/// Последний проход
		/// </summary>
		public EmployeePassage LastPassage { get; protected set; }

		/// <summary>
		/// Статус сотрудника
		/// </summary>
		public string EmployeeStatus { get; protected set; }

		/// <summary>
		/// Текущий пользователь может изменять месторасположение сотрудника
		/// </summary>
		public bool CanChangeWorkPlace { get; protected set; }


        /// <summary>
        /// Замещает
        /// </summary>
        public List<EmployeeReplacement> EmployeeRepresentatives { get; set; }

        /// <summary>
        /// Замещающие
        /// </summary>
        public List<EmployeeReplacement> EmployeeReplacements { get; set; }

		/// <summary>
		/// Контакты
		/// </summary>
		public List<EmployeeContact> Contacts { get; set; }

		/// <summary>
		/// Gets or sets all contacts.
		/// </summary>
		/// <value>
		/// All contacts.
		/// </value>
		public List<ContactTypeGroup> ContactGroups { get; set; }

		/// <summary>
		/// Gets or sets the positions.
		/// </summary>
		/// <value>
		/// The positions.
		/// </value>
		public List<EmployeePosition> Positions { get; set; }

		public List<EmployeeWorkPlace> WorkPlaces { get; set; }

		public List<EmployeeCoWorker> CoWorkers { get; set; }

        public List<EmployeesRoles> Roles { get; set; }

        public Dictionary<EmployeesRoles, List<EmployeesRoles>> RolesWithPersonNames = new Dictionary<EmployeesRoles, List<EmployeesRoles>>();

		public bool IsPersonAdministrator { get; set; }
        public bool IsPersonCardAccess { get; set; }

        public List<ObjectModel.Employee> CommonEmployees { get; set; }

        public Employee CommonEmployeeGroup { get; set; }

        public ObjectModel.Employee ChangedByName { get; set; }

        public int ChnagedBy { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ViewModel" /> class.
		/// </summary>
		/// <param name="employeeID">The employee ID.</param>
		public ViewModel(int employeeID)
		{
			Contacts = new List<EmployeeContact>();
			Positions = new List<EmployeePosition>();
			WorkPlaces = new List<EmployeeWorkPlace>();
			CoWorkers = new List<ObjectModel.EmployeeCoWorker>();
            Roles = new List<EmployeesRoles>();
            RolesWithPersonNames = new Dictionary<EmployeesRoles, List<EmployeesRoles>>();
            CommonEmployees = new List<ObjectModel.Employee>();
            CommonEmployeeGroup = new ObjectModel.Employee();
            ChangedByName = new ObjectModel.Employee();
            Replaced = false;

			EmployeeID = employeeID;
			Employee = Repository.Employees.GetInstance(EmployeeID);
			if (Employee != null) {
                if (Employee.ChangedBy != null)
                    ChnagedBy = Convert.ToInt32(Employee.ChangedBy);
                /*ФИО кто изменил */
                if (Employee.ChangedBy != null)
                    ChangedByName = Repository.Employees.GetInstance(Convert.ToInt32(Employee.ChangedBy));

                /*Общие сотрудники */
                if (Employee.CommonEmployeeID == null && Employee.Status == 0 && Employee.PersonID == null)
                {
                    CommonEmployees = Repository.Employees.GetListCommonEmployeesByIds(Employee.ID.ToString());
                }
                else if (Employee.CommonEmployeeID != null)
                {
                    CommonEmployeeGroup = Repository.Employees.GetInstance(Convert.ToInt32(Employee.CommonEmployeeID));
                }

                /* Замещение */
                EmployeeRepresentatives = Repository.Employees.GetEmployeeRepresentativeById(employeeID);
                EmployeeReplacements = Repository.Employees.GetEmployeeReplacementById(employeeID);

                Replaced = EmployeeReplacements.Count != 0;
				EmployeeStatus = Employee.GetEmployeeStatus(Employee.Status);
				LastPassage = Repository.Employees.GetEmployeeLastPassage(EmployeeID);
				IsPersonAdministrator = Repository.Employees.BelongsToPersonAdministators(Employee.ID);
                var listRoles = System.Configuration.ConfigurationManager.AppSettings["accessCardEmployee"];
                IsPersonCardAccess = Repository.Employees.BelongsToPersonCard(Employee.ID, listRoles);

				if (Employee.Status != 3) {
					Contacts = Repository.Employees.GetEmployeeContacts(EmployeeID, null);
					
					if (Employee.EmployerID.HasValue) {
						EmployeeCustomer = Repository
							.Employees.GetEmployeeCustomer(Employee.ID);
					}

				    EmployeeSupervisor = Repository.Employees.GetEmployeeSupervisor(Employee.ID);

                    Roles = Repository.EmployeesRoles.GetAllEmployeRoles(Employee.ID);

                    if (Roles.Count != 0)
                        foreach(var role in Roles)
                        {
                            if(RolesWithPersonNames.Where(t => t.Key.ID == role.ID).ToList().Count == 0)
                                RolesWithPersonNames.Add(role, Roles.Where(t => t.ID == role.ID).ToList());
                        }

					Positions = Repository
							.Employees.GetEmployeePositions(Employee.ID);

					WorkPlaces = Repository
							.Employees.GetEmployeeWorkPlaces(Employee.ID);

					WorkPlaces.ForEach(wp => {
						wp.CoWorkers = Repository.Employees
							.GetEmployeeCoWorkersByWorkPlace(Employee.ID, wp.WorkPlaceID);
					});
				}
			}

			ContactGroups = Contacts
				.Where(c => c.ContactType == -1)
				.GroupBy(c => c.ContactType)
				.Select(g => new ContactTypeGroup {
					ContactTypeID = g.Key,
					Items = g.Select(c => new EmployeeInfoContact {
						Contact = c.Contact,
						ContactType = c.ContactType,
						Icon = String.IsNullOrEmpty(c.Icon) ? null : c.Icon,
						PhoneNumber = (c.ContactType == -1) ? c.Contact : String.Empty,
						Note = c.Note,
						@Type = c.ContactTypeDesc,
						Order = c.Order,
						InDictionary = c.InDictionary
					})
					.OrderBy(c => c.Order).ToList()
				}).ToList();

			CanChangeWorkPlace = BusinessLogic.Repository.Employees.HasRightsToChangeUserWorkPlace(EmployeeID) == "1";
		}

	}
}