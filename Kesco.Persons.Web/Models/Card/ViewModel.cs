using System;
using System.Collections.Generic;
using BLToolkit.Reflection;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.ObjectModel;
using Kesco.Web.Mvc;
using System.Text.RegularExpressions;
using System.Web;

namespace Kesco.Persons.Web.Models.Card
{
	public class ViewModel : ViewModel<DataModel>
	{
		public ClientParameters Params { get { return settings as ClientParameters; } }

		public ViewModel() : base()
		{
			PageTitle = String.IsNullOrWhiteSpace(HttpContext.Current.Request["title"])
						? Kesco.Persons.Web.Localization.Resources.Views_Card_PageTitle
						: HttpContext.Current.Request["title"];
		}

		protected override void CreateSettings()
		{
			settings = TypeAccessor<ClientParameters>.CreateInstanceEx();
		}

		public void InitFromPerson(int personID)
		{
			Model.InitFromPerson(personID);
		}

	}
}