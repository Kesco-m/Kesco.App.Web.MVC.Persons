using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLToolkit.Reflection;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.ObjectModel;
using Kesco.Territories.ObjectModel;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.UI;

namespace Kesco.Persons.Web.Models
{
    public abstract class PersonCardViewModel : DialogViewModel
    {
        public List<IncorporationForm> IncorporationForms { get; internal set; }
        public List<Territory> Territories { get; internal set; }
        public List<FormatRegistration> FormatRegistrations { get; internal set; }
        
		[UIHint("Person")]
        public int? ResponsibleEmployeeID { get; internal set; }

        public List<PersonTheme> PersonThemes { get; internal set; }
        public List<PersonType> PersonTypes { get; internal set; }

        public List<string> PersonTypeIDs { get; internal set; }

        public List<int> HasRolesForBProject { get; internal set; }
        public List<int> HasRolesForPersonAdministration { get; internal set; }

        /// <summary>
        /// Настройки пользователя для формы, хранящиеся в БД
        /// </summary>
        public ClientParameters Params { get { return settings as ClientParameters; } }
        
        public bool Confirmed { get; set; }

        public PersonCardViewModel()
            : base()
        {
            Params.Width = Configuration.AppSettings.Width;
            Params.Height = Configuration.AppSettings.Height;
            LoadSettings(Params);

            Territories = Kesco.Territories.BusinessLogic.Repository.Territories.GetAllCountries();
            FormatRegistrations = Repository.FormatRegistrations.GetAllFormats();

            ResponsibleEmployeeID = UserContext.Current.EmployeeInfo.ID;
            //ResponsibleEmployees = new List<Employee>
            //                           {
            //                               DALC.Employees.Repository.Employees.GetInstance(UserContext.Current.EmployeeInfo.PersonID)
            //                           };

            Confirmed = false;

            PersonThemes = new List<PersonTheme>();

            PersonTypeIDs = new List<string>();

            HasRolesForBProject = Kesco.Employees.BusinessLogic.Repository.Employees.HasRolesForBProject();
			HasRolesForPersonAdministration = Kesco.Employees.BusinessLogic.Repository.Employees.HasRolesForPersonAdministration();
        }

		protected override void CreateSettings()
		{
            settings = TypeAccessor<ClientParameters>.CreateInstanceEx();
		}

        public class ClientParameters : ClientParametersBase
        {
        }
    }


}