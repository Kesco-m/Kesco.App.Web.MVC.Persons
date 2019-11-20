using System;
using System.Linq;
using System.Collections.Generic;
using BLToolkit.Reflection;
using Kesco.Employees.ObjectModel;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.ObjectModel;
using Kesco.Web.Mvc;
using System.Web;

namespace Kesco.Persons.Web.Models.Juridicals
{
	public class ViewModel : ViewModel<PersonModel>
	{
		/// <summary>
		/// Настройки пользователя для формы, хранящиеся в БД
		/// </summary>
		public ClientParameters Params { get { return settings as ClientParameters; } }
		public bool HidePersonTypesSection { get; set; }
		public List<int> HasRolesForBusinessProjects { get; internal set; }
		public List<int> HasRolesForPersonAdministration { get; internal set; }

        public string EmployeeRoles { get; internal set; }

		public List<FormatRegistration> FormatRegistrations { get; internal set; }
		public FormatRegistration FormatRegistration { get; internal set; }

		protected override void CreateSettings()
		{
			settings = TypeAccessor<ClientParameters>.CreateInstanceEx();
		}

		public ViewModel() : base()
		{
			PageTitle = String.IsNullOrWhiteSpace(HttpContext.Current.Request["title"])
						? ((Model.Card.ID == 0)
							? Kesco.Persons.Web.Localization.Resources.Views_CreateJuridical_PageTitle
							: Kesco.Persons.Web.Localization.Resources.Views_CreateJuridical_PageTitle)
						: HttpContext.Current.Request["title"];
			Params.Width = Configuration.AppSettings.Width;
			Params.Height = Configuration.AppSettings.Height;
			LoadSettings(Params);

			//if (!String.IsNullOrEmpty(HttpContext.Current.Request["text"]))
				//Model.Card.Nickname = HttpContext.Current.Request["text"];
			FormatRegistrations = Repository
				.FormatRegistrations
					.GetAllFormats();

			FormatRegistrations.ForEach(f => {
					if (!String.IsNullOrEmpty(f.OGRNName_LocalizationKey))
						f.OGRNName = Kesco.Persons.Web.Localization.Resources.ResourceManager.GetString(f.OGRNName_LocalizationKey);
					if (!String.IsNullOrEmpty(f.INNName_LocalizationKey))
						f.INNName = Kesco.Persons.Web.Localization.Resources.ResourceManager.GetString(f.INNName_LocalizationKey);
					if (!String.IsNullOrEmpty(f.OKPOName_LocalizationKey))
						f.OKPOName = Kesco.Persons.Web.Localization.Resources.ResourceManager.GetString(f.OKPOName_LocalizationKey);
			});
			FormatRegistration = new FormatRegistration();
            EmployeeRoles = Repository.Persons.GetEmployeeRoles();
			HelpTopic = "CreateJuridical";
			HasRolesForBusinessProjects = Kesco.Employees.BusinessLogic.Repository.Employees.HasRolesForBProject();
			HasRolesForPersonAdministration = Kesco.Employees.BusinessLogic.Repository.Employees.HasRolesForPersonAdministration();
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
            Model.ResponsibleEmployees.Add(new Naturals.PersonModel.SimplePersonModelClass() { FullName = UserContext.Current.EmployeeInfo.LastNameWithInitials, ID = UserContext.Current.EmployeeInfo.ID.ToString() });
            InitRegistrationFormats();
		}

		/// <summary>
		/// Инициализирует модель из данных об лице
		/// </summary>
		/// <param name="personID">The person ID.</param>
		public void InitFromPerson(int personID)
		{
			PageTitle = Kesco.Persons.Web.Localization.Resources.Views_JuridicalPerson_Edit_PageTitle;
			Model.InitFromPerson(personID);
            List<Employee> test = Repository.ResponsibleEmployees.GetResponsibleEmployeesByPersonId(personID);
            foreach (var employee in test)
            {
                Model.ResponsibleEmployees.Add(new Naturals.PersonModel.SimplePersonModelClass() { FullName = employee.FullName, ID = employee.ID.ToString() });
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
