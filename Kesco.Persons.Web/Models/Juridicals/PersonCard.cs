using System.ComponentModel.DataAnnotations;
using BLToolkit.Reflection;
using Kesco.BusinessProjects.Controls.ComponentModel;
using Kesco.Persons.Controls.ComponentModel;
using Kesco.Persons.Web.Localization;
using Kesco.Territories.Controls.ComponentModel;
using System;
using Newtonsoft.Json;
using Kesco.ComponentModel.Json;

namespace Kesco.Persons.Web.Models.Juridicals
{
	public class PersonCard : Kesco.ObjectModel.Trackable<PersonCard>
	{
		/// <summary>
		/// Код карточки юридического лица
		/// </summary>
		public int? ID { get; set; }

		/// <summary>
		/// Код юридического лица
		/// </summary>
		public int? PersonID { get; set; }

		/// <summary>
		/// Псевдоним
		/// </summary>
		[UIHint("TextBox")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_Nickname_Name",
				ShortName = "Models_JuridicalPersonCard_Nickname_ShortName",
				Description = "Models_JuridicalPersonCard_Nickname_Description",
				Prompt = "Models_JuridicalPersonCard_Nickname_Prompt"
			)]
		[Required(
				ErrorMessageResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				ErrorMessageResourceName = "Models_JuridicalPersonCard_Nickname_Required",
				AllowEmptyStrings = false
			)]
		public string Nickname { get; set; }

		/// <summary>
		/// КодТерритории
		/// </summary>
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_TerritoryID_Name",
				ShortName = "Models_JuridicalPersonCard_TerritoryID_ShortName",
				Description = "Models_JuridicalPersonCard_TerritoryID_Description",
				Prompt = "Models_JuridicalPersonCard_TerritoryID_Prompt"
		    )]
		[Required(
				ErrorMessageResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				ErrorMessageResourceName = "Models_JuridicalPersonCard_TerritoryID_Required",
				AllowEmptyStrings = false
			)]
		[TerritorySelect]
		[TerritorySelectSearchParameters(CLID=66, TAreaID = 2, Limit = 9)]
		public int? TerritoryID { get; set; }

		/// <summary>
		/// ГосОрганизация
		/// </summary>
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_IsStateOrganization_Name",
				ShortName = "Models_JuridicalPersonCard_IsStateOrganization_ShortName",
				Description = "Models_JuridicalPersonCard_IsStateOrganization_Description",
				Prompt = "Models_JuridicalPersonCard_IsStateOrganization_Prompt"
			)]
		[UIHint("Bool")]
		public bool IsStateOrganization { get; set; }

		/// <summary>
		/// ИНН
		/// </summary>
		[UIHint("MaskedTextBox")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_INN_Name",
				ShortName = "Models_JuridicalPersonCard_INN_ShortName",
				Description = "Models_JuridicalPersonCard_INN_Description",
				Prompt = "Models_JuridicalPersonCard_INN_Prompt"
			)]
		[StringLength(20)]
		public string INN { get; set; }

		/// <summary>
		/// ОГРН
		/// </summary>
		[UIHint("MaskedTextBox")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_OGRN_Name",
				ShortName = "Models_JuridicalPersonCard_OGRN_ShortName",
				Description = "Models_JuridicalPersonCard_OGRN_Description",
				Prompt = "Models_JuridicalPersonCard_OGRN_Prompt"
			)]
		[StringLength(20)]
		public string OGRN { get; set; }

		/// <summary>
		/// ОКПО
		/// </summary>
		[UIHint("MaskedTextBox")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_OKPO_Name",
				ShortName = "Models_JuridicalPersonCard_OKPO_ShortName",
				Description = "Models_JuridicalPersonCard_OKPO_Description",
				Prompt = "Models_JuridicalPersonCard_OKPO_Prompt"
			)]
		[StringLength(20)]
		public string OKPO { get; set; }

		/// <summary>
		/// Код Проекта
		/// </summary>
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_BusinessProjectID_Name",
				ShortName = "Models_JuridicalPersonCard_BusinessProjectID_ShortName",
				Description = "Models_JuridicalPersonCard_BusinessProjectID_Description",
				Prompt = "Models_JuridicalPersonCard_BusinessProjectID_Prompt"
			)]
		[BusinessProjectSelect]
		[BusinessProjectSelectSearchParameters(CLID=20, Limit = 9)]
		public int? BusinessProjectID { get; set; }

		public Kesco.Persons.Web.Models.Requisites.Requisites Requisites { get; internal set; }

		/// <summary>
		/// Банк
		/// </summary>
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_IsBank_Name",
				ShortName = "Models_JuridicalPersonCard_IsBank_ShortName",
				Description = "Models_JuridicalPersonCard_IsBank_Description",
				Prompt = "Models_JuridicalPersonCard_IsBank_Prompt"
			)]
		[UIHint("Bool")]
		public bool IsBank { get; set; }

		/// <summary>
		/// БИК
		/// </summary>
		[UIHint("TextBox")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_BIK_Name",
				ShortName = "Models_JuridicalPersonCard_BIK_ShortName",
				Description = "Models_JuridicalPersonCard_BIK_Description",
				Prompt = "Models_JuridicalPersonCard_BIK_Prompt"
			)]
		[StringLength(15)]
		public string BIK { get; set; }

		/// <summary>
		/// КорСчет
		/// </summary>
		[UIHint("TextBox")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_LoroConto_Name",
				ShortName = "Models_JuridicalPersonCard_LoroConto_ShortName",
				Description = "Models_JuridicalPersonCard_LoroConto_Description",
				Prompt = "Models_JuridicalPersonCard_LoroConto_Prompt"
			)]
		[StringLength(20)]
		public string LoroConto { get; set; }

		/// <summary>
		/// БИКРКЦ
		/// </summary>
		[UIHint("TextBox")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_BIKRKC_Name",
				ShortName = "Models_JuridicalPersonCard_BIKRKC_ShortName",
				Description = "Models_JuridicalPersonCard_BIKRKC_Description",
				Prompt = "Models_JuridicalPersonCard_BIKRKC_Prompt"
			)]
		[StringLength(9)]
		public string BIKRKC { get; set; }

		/// <summary>
		/// SWIFT
		/// </summary>
		[UIHint("TextBox")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_SWIFT_Name",
				ShortName = "Models_JuridicalPersonCard_SWIFT_ShortName",
				Description = "Models_JuridicalPersonCard_SWIFT_Description",
				Prompt = "Models_JuridicalPersonCard_SWIFT_Prompt"
			)]
		[StringLength(15)]
		public string SWIFT { get; set; }

		/// <summary>
		/// Примечание
		/// </summary>
		[UIHint("TwoLinesTextBox")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_Comment_Name",
				ShortName = "Models_JuridicalPersonCard_Comment_Description",
				Description = "Models_JuridicalPersonCard_Comment_ShortName",
				Prompt = "Models_JuridicalPersonCard_Comment_Prompt"
			)]
		public string Comment { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="PersonCard" /> is verified.
		/// </summary>
		/// <value>
		///   <c>true</c> if verified; otherwise, <c>false</c>.
		/// </value>
		public bool Verified { get; set; }

		public PersonCard()
		{
			Requisites = TypeAccessor<Kesco.Persons.Web.Models.Requisites.Requisites>.CreateInstanceEx();
		}
	}
}