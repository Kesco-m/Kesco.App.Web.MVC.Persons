using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BLToolkit.TypeBuilder;
using Kesco.Persons.Controls.ComponentModel;
using Kesco.Employees.Controls.ComponentModel;
using Kesco.Persons.ObjectModel;
using Newtonsoft.Json;
using Kesco.ComponentModel.Json;
using Kesco.Web.Mvc.ComponentModel.DataAnnotations;

namespace Kesco.Persons.Web.Models.Requisites
{
	public class Requisites : Kesco.ObjectModel.TrackableEntity<Requisites, int>
	{
		/// <summary>
		/// Код
		/// </summary>
		public override int ID { get; set; }

		public PersonCardType PersonType { get; set; }

		public int? PersonID { get; set; }

		public int? TerritoryID { get; set; }

        /// <summary>
        /// Код операции (при создании/при редактировании)
        /// </summary>
        public int? OperationTypeId { get; set; }

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

		/// <summary>
		/// Код организационно-правовой формы
		/// </summary>
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_IncorporationForm_Name",
				ShortName = "Models_JuridicalPersonCard_IncorporationForm_ShortName",
				Description = "Models_JuridicalPersonCard_IncorporationForm_Description",
				Prompt = "Models_JuridicalPersonCard_IncorporationForm_Prompt"
			)]
		[IncorporationFormSelect(
			EntityAccessorType = typeof(Kesco.Persons.BusinessLogic.DataAccess.IncorporationFormAccessor),
			PersonKind = 1)]
		public int? IncorporationFormID { get; set; }

		[UIHint("TextBox")]
		[Parameter("")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_ShortNameRus_Name",
				ShortName = "Models_JuridicalPersonCard_ShortNameRus_ShortName",
				Description = "Models_JuridicalPersonCard_ShortNameRus_Description",
				Prompt = "Models_JuridicalPersonCard_ShortNameRus_Prompt"
			)]
		[StringLength(200)]
		public string ShortNameRus { get; set; }

		[UIHint("TextBox")]
		[Parameter("")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_ShortNameLat_Name",
				ShortName = "Models_JuridicalPersonCard_ShortNameLat_ShortName",
				Description = "Models_JuridicalPersonCard_ShortNameLat_Description",
				Prompt = "Models_JuridicalPersonCard_ShortNameLat_Prompt"
			)]
		[StringLength(200)]
		public string ShortNameLat { get; set; }

		[UIHint("TwoLinesTextBox")]
		[Parameter("")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_FullName_Name",
				ShortName = "Models_JuridicalPersonCard_FullName_ShortName",
				Description = "Models_JuridicalPersonCard_FullName_Description",
				Prompt = "Models_JuridicalPersonCard_FullName_Prompt"
			)]
		[StringLength(300)]
		public string FullName { get; set; }

		[UIHint("MaskedTextBox", "MVC", "width", "70", "maskOptions", "{ mask: '99999' }")]
		[Parameter("")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_OKONH_Name",
				ShortName = "Models_JuridicalPersonCard_OKONH_ShortName",
				Description = "Models_JuridicalPersonCard_OKONH_Description",
				Prompt = "Models_JuridicalPersonCard_OKONH_Prompt"
			)]
		[StringLength(5)]
		public string OKONH { get; set; }

		[UIHint("FixedLengthTextBox", "MVC", "width", "100")]
		[Parameter("")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_OKVED_Name",
				ShortName = "Models_JuridicalPersonCard_OKVED_ShortName",
				Description = "Models_JuridicalPersonCard_OKVED_Description",
				Prompt = "Models_JuridicalPersonCard_OKVED_Prompt"
			)]
		[StringLength(8)]
		public string OKVED { get; set; }

		[UIHint("FixedLengthTextBox")]
		[Parameter("")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_KPP_Name",
				ShortName = "Models_JuridicalPersonCard_KPP_ShortName",
				Description = "Models_JuridicalPersonCard_KPP_Description",
				Prompt = "Models_JuridicalPersonCard_KPP_Prompt"
			)]
		[StringLength(20)]
		public string KPP { get; set; }

		[UIHint("FixedLengthTextBox")]
		[Parameter("")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_RwID_Name",
				ShortName = "Models_JuridicalPersonCard_RwID_ShortName",
				Description = "Models_JuridicalPersonCard_RwID_Description",
				Prompt = "Models_JuridicalPersonCard_RwID_Prompt"
			)]
		[StringLength(35)]
		public string RwID { get; set; }

		[UIHint("TextBox")]
		[Parameter("")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_AddressLegal_Name",
				ShortName = "Models_JuridicalPersonCard_AddressLegal_ShortName",
				Description = "Models_JuridicalPersonCard_AddressLegal_Description",
				Prompt = "Models_JuridicalPersonCard_AddressLegal_Prompt"
			)]
		[StringLength(300)]
		public string AddressLegal { get; set; }

		[UIHint("TextBox")]
		[Parameter("")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_AddressLegalLat_Name",
				ShortName = "Models_JuridicalPersonCard_AddressLegalLat_ShortName",
				Description = "Models_JuridicalPersonCard_AddressLegalLat_Description",
				Prompt = "Models_JuridicalPersonCard_AddressLegalLat_Prompt"
			)]
		[StringLength(300)]
		public string AddressLegalLat { get; set; }

		[UIHint("TextBox")]
		[Parameter("")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_ShortNameRusGenitive_Name",
				ShortName = "Models_JuridicalPersonCard_ShortNameRusGenitive_ShortName",
				Description = "Models_JuridicalPersonCard_ShortNameRusGenitive_Description",
				Prompt = "Models_JuridicalPersonCard_ShortNameRusGenitive_Prompt"
			)]
		[StringLength(200)]
		public string ShortNameRusGenitive { get; set; }

		public override string GetInstanceFriendlyName()
		{
			return ShortNameRus ?? FullName ?? ShortNameLat ?? String.Format("#{0}", ID);
		}
	}


}