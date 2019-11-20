using System;
using System.Linq;
using System.Collections.Generic;
using BLToolkit.Reflection;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.ObjectModel;
using Kesco.Web.Mvc;
using System.Web;
using Kesco.Territories.ObjectModel;
using Newtonsoft.Json.Linq;

namespace Kesco.Persons.Web.Models.NaturalDuplicates
{
	public class ViewModel : ViewModel<DuplicatesModel>
	{
		/// <summary>
		/// Настройки пользователя для формы, хранящиеся в БД
		/// </summary>
		public ClientParameters Params { get { return settings as ClientParameters; } }

		public List<int> HasRolesForPersonAdministration { get; set; }

		public bool AllowSave { get; internal set; }

		protected override void CreateSettings()
		{
			settings = TypeAccessor<ClientParameters>.CreateInstanceEx();
		}

		public ViewModel() : base()
		{
			PageTitle = String.IsNullOrWhiteSpace(HttpContext.Current.Request["title"])
						? Kesco.Persons.Web.Localization.Resources.Views_NaturalPerson_Duplicate_PageTitle
						: HttpContext.Current.Request["title"];
			Params.Width = Configuration.AppSettings.Width;
			Params.Height = Configuration.AppSettings.Height;
			HelpTopic = "PersonDuplicate";
			LoadSettings(Params);
			HasRolesForPersonAdministration = Kesco.Employees.BusinessLogic.Repository.Employees.HasRolesForPersonAdministration();
			AllowSave = HasRolesForPersonAdministration.Count > 0;
		}

		/// <summary>
		/// Инициализирует спорные вопросы по возможным дубликатам.
		/// </summary>
		/// <param name="requisites">The requisites.</param>
		public void InitFromClientContext(dynamic duplicates)
		{
			bool fullConcurrence = false;
			Kesco.Guard.IsNotNull(duplicates, "duplicates");
			foreach (var duplicate in duplicates) {
				var d = new Duplicate {
					PersonID = duplicate.PersonID,
					Nickname = duplicate.Nickname,
					Issues = new List<Issue>()
				};
				foreach (var issue in duplicate.Issues) {
					if (issue.R == 0) fullConcurrence = true;
					d.Issues.Add(new Issue {
						Field = issue.Field,
						Granted = issue.Granted,
						R = issue.R,
						Value = issue.Value
					});
				}
				Model.Duplicates.Add(d);
			}

			AllowSave = HasRolesForPersonAdministration.Count > 0 || !fullConcurrence;
		}

		/// <summary>
		/// Inits this instance.
		/// </summary>
		public void Init()
		{
		}

	}
}
