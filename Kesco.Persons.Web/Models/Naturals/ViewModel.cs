using System;
using System.Linq;
using System.Collections.Generic;
using BLToolkit.Reflection;
using Kesco.Employees.ObjectModel;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.ObjectModel;
using Kesco.Web.Mvc;
using System.Web;
using Kesco.Territories.ObjectModel;

namespace Kesco.Persons.Web.Models.Naturals
{
	public class ViewModel : ViewModel<PersonModel>
	{
		/// <summary>
		/// Настройки пользователя для формы, хранящиеся в БД
		/// </summary>
		public ClientParameters Params { get { return settings as ClientParameters; } }

		public List<int> HasRolesForBusinessProjects { get; internal set; }
		public List<int> HasRolesForPersonAdministration { get; internal set; }

		public List<FormatRegistration> FormatRegistrations { get; internal set; }
		public FormatRegistration FormatRegistration { get; internal set; }

		public bool ShowPersonData { get; internal set; }
		public bool ShowPersonRequisites { get; internal set; }

        public string EmployeeRoles { get; internal set; }

		protected override void CreateSettings()
		{
			settings = TypeAccessor<ClientParameters>.CreateInstanceEx();
		}

		public ViewModel() : base()
		{
			ShowPersonData = true;
			ShowPersonRequisites = true;
			PageTitle = String.IsNullOrWhiteSpace(HttpContext.Current.Request["title"])
						? (((Model.Card.PersonID ?? 0) == 0)
							? Kesco.Persons.Web.Localization.Resources.Views_NaturalPerson_Create_PageTitle
							: Kesco.Persons.Web.Localization.Resources.Views_NaturalPerson_Edit_PageTitle)
						: HttpContext.Current.Request["title"];
			Params.Width = Configuration.AppSettings.Width;
			Params.Height = Configuration.AppSettings.Height;
			LoadSettings(Params);
            

			FormatRegistrations = Repository.FormatRegistrations.GetAllFormats();
			FormatRegistrations.ForEach(f => {
				if (!String.IsNullOrEmpty(f.OGRNName_LocalizationKey))
					f.OGRNName = Kesco.Persons.Web.Localization.Resources.ResourceManager.GetString(f.OGRNName_LocalizationKey);
				if (!String.IsNullOrEmpty(f.INNName_LocalizationKey))
					f.INNName = Kesco.Persons.Web.Localization.Resources.ResourceManager.GetString(f.INNName_LocalizationKey);
				if (!String.IsNullOrEmpty(f.OKPOName_LocalizationKey))
					f.OKPOName = Kesco.Persons.Web.Localization.Resources.ResourceManager.GetString(f.OKPOName_LocalizationKey);
			});
			FormatRegistration = new FormatRegistration();

			HelpTopic = "CreateNatural";
			HasRolesForBusinessProjects = Kesco.Employees.BusinessLogic.Repository.Employees.HasRolesForBProject();
			HasRolesForPersonAdministration = Kesco.Employees.BusinessLogic.Repository.Employees.HasRolesForPersonAdministration();
            EmployeeRoles = Repository.Persons.GetEmployeeRoles();
		}

		public void InitFromContext()
		{
			InitRegistrationFormats();
		}

		/// <summary>
		/// Inits this instance.
		/// </summary>
		public void Init()
		{

            Model.ResponsibleEmployees.Add(new PersonModel.SimplePersonModelClass() { FullName = UserContext.Current.EmployeeInfo.LastNameWithInitials, ID = UserContext.Current.EmployeeInfo.ID.ToString() });

			InitRegistrationFormats();
		}

		public void InitFromClientContext(dynamic requisites)
		{
			PageTitle = Kesco.Persons.Web.Localization.Resources.Views_NaturalPerson_CreateRequisites_PageTitle;
			Kesco.Guard.IsNotNull(requisites, "requisites");
			ShowPersonData = false;
			Model.EditCard = true;
			Model.Card.ID = requisites.ID;
			if (requisites.From == PersonCard.MinFromDate) Model.Card.From = null;
			else Model.Card.From = requisites.From;
			if (requisites.To == PersonCard.MaxToDate) Model.Card.To = null;
			else Model.Card.To = requisites.To;

			if (requisites.ID != null && requisites.ID != 0)
				Model.InitFromCard((int) requisites.ID);
			else if (requisites.PersonID != null && requisites.PersonID != 0) {
				Model.InitFromPerson((int) requisites.PersonID);
			}
			Init();
		}

		/// <summary>
		/// Инициализирует модель из данных об лице
		/// </summary>
		public void InitFromCard(int cardID)
		{
			ShowPersonData = false;
			ShowPersonRequisites = true;
			PageTitle = Kesco.Persons.Web.Localization.Resources.Views_NaturalPerson_EditRequisites_PageTitle;
			Model.InitFromCard(cardID);
			InitRegistrationFormats();
		}

		/// <summary>
		/// Инициализирует модель из данных об лице
		/// </summary>
		/// <param name="personID">The person ID.</param>
		/// <param name="action">0 - данные физ лица, иначе карточка.</param>
		public void InitFromPerson(int personID, int action)
		{
			ShowPersonData = (action == 0);
			ShowPersonRequisites = (action == 1);
			Model.EditCard = (action == 1);
			PageTitle = (action == 0)
				? Kesco.Persons.Web.Localization.Resources.Views_NaturalPerson_Edit_PageTitle
				: Kesco.Persons.Web.Localization.Resources.Views_NaturalPerson_EditRequisites_PageTitle;
			Model.InitFromPerson(personID);
		    List<Employee> test = Repository.ResponsibleEmployees.GetResponsibleEmployeesByPersonId(personID);
		    foreach (var employee in test)
		    {
		        Model.ResponsibleEmployees.Add(new PersonModel.SimplePersonModelClass(){FullName = employee.FullName, ID = employee.ID.ToString()});
		    }

            //Model.ResponsibleEmployees.AddRange(
            //        Repository.ResponsibleEmployees.GetResponsibleEmployeesByPersonId(personID)
            //    );
			InitRegistrationFormats();
		}

		/// <summary>
		/// Инициализирует форматы регистрации для разных стран
		/// </summary>
		protected void InitRegistrationFormats()
		{
			FormatRegistration = FormatRegistrations.FirstOrDefault(f => f.ID == Model.Card.TerritoryID);
			if (FormatRegistration == null) {
				FormatRegistration = new FormatRegistration() {
					OGRNName = Kesco.Persons.Web.Localization.Resources.Models_JuridicalPersonCard_OGRN_ShortName,
					//OGRNFormat1 = "99999999999999999999"
                    OGRNFormat = "99999999999999999999"
				};
			}
		}
	}
}
