using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.UI;
using BLToolkit.Reflection;
using Kesco.Persons.Controls.ComponentModel;
using Kesco.Web.Mvc.Filtering;
using System.ComponentModel.DataAnnotations;

namespace Kesco.Persons.Web.Models.Dossier.Delete
{
	public class ClientParameters : ClientParametersBase
	{
	}

	public class DataModel
	{
		[PersonSelect(HideCommands=true)]
		[PersonSelectSearchParameters(PersonHowSearch = 1, PersonWhereSearch = 1)]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Persons_Dossier_Delete_PersonID_Label"
			)]
		public int? PersonID { get; set; }

		[PersonSelect(HideCommands = true)]
		[PersonSelectSearchParameters(PersonHowSearch = 1, PersonWhereSearch = 1)]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Persons_Dossier_Delete_ReplaceWithPersonID_Label"
			)]
		public int? ReplaceWithPersonID { get; set; }

		public bool Confirmed { get; set; }
	}


	public class ViewModel : ViewModel<DataModel>
	{
		public ClientParameters Params { get { return settings as ClientParameters; } }

		public ViewModel() : base()
		{
			PageTitle = global::Resources.Resources.Persons_Dossier_Delete_PageTitle;
			Model.PersonID = UniqueIdQSFilterSetting.CreateInstance().InitFromQueryString("id", 0).GetValue();
		}

		protected override void CreateSettings()
		{
			settings = TypeAccessor<ClientParameters>.CreateInstanceEx();
		}

	}
}