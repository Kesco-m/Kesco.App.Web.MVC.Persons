using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kesco.Persons.ObjectModel;
using Kesco.Persons.BusinessLogic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Kesco.ComponentModel.Json;

namespace Kesco.Persons.Web.Models.Card
{
	public class DataModel
	{

		public int PersonID { get; set; }

		[UIHint("DateTimeField")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_Requisites_From_Name",
				ShortName = "Models_Requisites_From_ShortName",
				Description = "Models_Requisites_From_Description",
				Prompt = "Models_Requisites_From_Prompt"
			)]
		[JsonConverter(typeof(JsonShortDateConverter))]
		public DateTime? From { get; set; }

		[UIHint("DateTimeField")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_Requisites_To_Name",
				ShortName = "Models_Requisites_To_ShortName",
				Description = "Models_Requisites_To_Description",
				Prompt = "Models_Requisites_To_Prompt"
			)]
		[JsonConverter(typeof(JsonShortDateConverter))]
		public DateTime? To { get; set; }

		public Person Person { get; set; }


		public DataModel()
		{
		}

		public void InitFromPerson(int personID)
		{
			Person = Repository.Persons.GetInstance(personID);
			if (Person != null) {
				PersonID = Person.ID;
			}
		}

	}
}