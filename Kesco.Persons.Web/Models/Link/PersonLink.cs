using System;
using System.ComponentModel.DataAnnotations;
using Kesco.Persons.Controls.ComponentModel;
using Kesco.ObjectModel;
using Newtonsoft.Json;
using Kesco.ComponentModel.Json;

namespace Kesco.Persons.Web.Models.PersonLink
{
	public class PersonLink : TrackableEntity<PersonLink, int>
	{
		/// <summary>
		/// КодСвязиЛиц
		/// </summary>
		[UIHint("UniqueID")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_577",
				ShortName = "Kesco_Persons_MDL_578",
				Description = "Kesco_Persons_MDL_579",
				Prompt = "Kesco_Persons_MDL_580"
			)]
		public override int ID { get; set; }

		/// <summary>
		/// КодТипаСвязиЛиц
		/// </summary>
		[PersonLinkSelect]
		public int PersonLinkTypeID { get; set; }

		[UIHint("DateTimeField")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_585",
				ShortName = "Kesco_Persons_MDL_586",
				Description = "Kesco_Persons_MDL_587",
				Prompt = "Kesco_Persons_MDL_588"
			)]
		[JsonConverter(typeof(JsonShortDateConverter))]
		public DateTime? From { get; set; }

		[UIHint("DateTimeField")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_589",
				ShortName = "Kesco_Persons_MDL_590",
				Description = "Kesco_Persons_MDL_591",
				Prompt = "Kesco_Persons_MDL_592"
			)]
		[JsonConverter(typeof(JsonShortDateConverter))]
		public DateTime? To { get; set; }

		/// <summary>
		/// КодЛицаРодителя
		/// </summary>
		[PersonSelect(CLID = 2)]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_Link_ParentID_Name",
				ShortName = "Kesco_Persons_MDL_Link_ParentID_ShortName",
				Description = "Kesco_Persons_MDL_Link_ParentID_Description",
				Prompt = "Kesco_Persons_MDL_Link_ParentID_Prompt"
			)]
		[Required]
		public int? ParentPersonID { get; set; }

		/// <summary>
		/// КодЛицаПотомка
		/// </summary>
		[PersonSelect(CLID = 2, OnRequestClientFunction="ChildPersonID_OnRequest")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_Link_ChildID_Name",
				ShortName = "Kesco_Persons_MDL_Link_ChildID_ShortName",
				Description = "Kesco_Persons_MDL_Link_ChildID_Description",
				Prompt = "Kesco_Persons_MDL_Link_ParentID_Prompt"
			)]
		[Required]
		public int? ChildPersonID { get; set; }

		/// <summary>
		/// Описание
		/// </summary>
		[UIHint("TwoLinesTextBox")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_601",
				ShortName = "Kesco_Persons_MDL_602",
				Description = "Kesco_Persons_MDL_603",
				Prompt = "Kesco_Persons_MDL_604"
			)]
		public string Description { get; set; }

		/// <summary>
		/// Параметр
		/// </summary>
		[UIHint("PersonLinkParameter")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_605",
				ShortName = "Kesco_Persons_MDL_606",
				Description = "Kesco_Persons_MDL_607",
				Prompt = "Kesco_Persons_MDL_608"
			)]
		public int Parameter { get; set; }


		public override string GetInstanceFriendlyName()
		{
			return Description ?? System.String.Format("#{0}", ID);
		}
	}
}
