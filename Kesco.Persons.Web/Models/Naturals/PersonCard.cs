using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using BLToolkit.Reflection;
using Kesco.BusinessProjects.Controls.ComponentModel;
using Kesco.ObjectModel;
using Kesco.Persons.Controls.ComponentModel;
using Kesco.Persons.Web.Localization;
using Kesco.Territories.Controls.ComponentModel;
using Newtonsoft.Json;
using Kesco.ComponentModel.Json;
using Kesco.Web.Mvc.ComponentModel.DataAnnotations;

namespace Kesco.Persons.Web.Models.Naturals
{
	public class PersonCard : Trackable<PersonCard>
	{
		public static readonly DateTime MinFromDate = new DateTime(1980, 1, 1);
		public static readonly DateTime MaxToDate = new DateTime(2050, 1, 1);
		/// <summary>
		/// Код карточки физического лица
		/// </summary>
		public int? ID { get; set; }

		public int? PersonID { get; set; }

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
		/// Фамилия
		/// </summary>
		[UIHint("TextBox")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_PersonCard_LastNameRus_Name",
				ShortName = "Models_PersonCard_LastNameRus_ShortName",
				Description = "Models_PersonCard_LastNameRus_Description",
				Prompt = "Models_PersonCard_LastNameRus_Prompt"
			)]
		[StringLength(50,
			ErrorMessageResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources), 
			ErrorMessageResourceName = "Kesco_Persons_MDL_2002")]
		public string LastNameRus { get; set; }

		/// <summary>
		/// Имя
		/// </summary>
		[UIHint("TextBox", "MVC")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_249",
				ShortName = "Kesco_Persons_MDL_250",
				Description = "Kesco_Persons_MDL_251",
				Prompt = "Kesco_Persons_MDL_252"
			)]
		[StringLength(50,
			ErrorMessageResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources), 
			ErrorMessageResourceName = "Kesco_Persons_MDL_2000")]
		public string FirstNameRus { get; set; }

		/// <summary>
		/// Отчество
		/// </summary>
		[UIHint("TextBox", "MVC")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_253",
				ShortName = "Kesco_Persons_MDL_254",
				Description = "Kesco_Persons_MDL_255",
				Prompt = "Kesco_Persons_MDL_256"
			)]
		[StringLength(50,
			ErrorMessageResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources), 
			ErrorMessageResourceName = "Kesco_Persons_MDL_2001")]
		public string MiddleNameRus { get; set; }

		/// <summary>
		/// Фамилия - английская (латинская транскрипция)
		/// </summary>
		[UIHint("TextBox")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_265",
				ShortName = "Kesco_Persons_MDL_266",
				Description = "Kesco_Persons_MDL_267",
				Prompt = "Kesco_Persons_MDL_268"
			)]
		public string LastNameLat { get; set; }

		/// <summary>
		/// Имя- английское (латинская транскрипция)
		/// </summary>
		[StringLength(50)]
		[UIHint("TextBox")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_269",
				ShortName = "Kesco_Persons_MDL_270",
				Description = "Kesco_Persons_MDL_271",
				Prompt = "Kesco_Persons_MDL_272"
			)]
		public string FirstNameLat { get; set; }

		/// <summary>
		/// Отчество - английское (латинская транскрипция)
		/// </summary>
		[UIHint("TextBox")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_273",
				ShortName = "Kesco_Persons_MDL_274",
				Description = "Kesco_Persons_MDL_275",
				Prompt = "Kesco_Persons_MDL_276"
			)]
		[StringLength(50)]
		public string MiddleNameLat { get; set; }

		/// <summary>
		/// Пол
		/// </summary>
		[UIHint("Sex")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_277",
				ShortName = "Kesco_Persons_MDL_278",
				Description = "Kesco_Persons_MDL_279",
				Prompt = "Kesco_Persons_MDL_280"
			)]
		public char? Sex { get; set; }

		[UIHint("DateTimeField")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_165",
				ShortName = "Kesco_Persons_MDL_166",
				Description = "Kesco_Persons_MDL_167",
				Prompt = "Kesco_Persons_MDL_168"
			)]
		[DataType(DataType.DateTime)]
		[JsonConverter(typeof(JsonShortDateConverter))]
		public DateTime? Birthday { get; set; }

		/// <summary>
		/// Псевдоним
		/// </summary>
		[UIHint("TextBox")]
		[Display(
				ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
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
		/// Код Проекта
		/// </summary>
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_BusinessProjectID_Name",
				ShortName = "Models_JuridicalPersonCard_BusinessProjectID_ShortName",
				Description = "Models_JuridicalPersonCard_BusinessProjectID_Description",
				Prompt = "Models_JuridicalPersonCard_BusinessProjectID_Prompt"
			)]
		[BusinessProjectSelect]
		[BusinessProjectSelectSearchParameters(CLID = 20, Limit = 9)]
		public int? BusinessProjectID { get; set; }

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
			PersonKind = 2)]
		public int? IncorporationFormID { get; set; }

		/// <summary>
		/// КодТерритории
		/// </summary>
		//[Required(
		//        ErrorMessageResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
		//        ErrorMessageResourceName = "Models_JuridicalPersonCard_TerritoryID_Required",
		//        AllowEmptyStrings = false
		//    )]
		[Display(
				ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_TerritoryID_Name",
				ShortName = "Models_JuridicalPersonCard_TerritoryID_ShortName",
				Description = "Models_JuridicalPersonCard_TerritoryID_Description",
				Prompt = "Models_JuridicalPersonCard_TerritoryID_Prompt"
		    )]
		[TerritorySelect]
		[TerritorySelectSearchParameters(CLID = 66, TAreaID=2, Limit=9)]
		public int? TerritoryID { get; set; }

		/// <summary>
		/// ИНН
		/// </summary>
		[UIHint("MaskedTextBox")]
		[Display(
				ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
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

		[UIHint("MaskedTextBox", "MVC", "width", "70px", "maskOptions", "{ mask: '99999' }")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_OKONH_Name",
				ShortName = "Models_JuridicalPersonCard_OKONH_ShortName",
				Description = "Models_JuridicalPersonCard_OKONH_Description",
				Prompt = "Models_JuridicalPersonCard_OKONH_Prompt"
			)]
		public string OKONH { get; set; }

		[UIHint("MaskedTextBox", "MVC", "width", "150", "maskOptions", "{ mask: '99999999' }")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_OKVED_Name",
				ShortName = "Models_JuridicalPersonCard_OKVED_ShortName",
				Description = "Models_JuridicalPersonCard_OKVED_Description",
				Prompt = "Models_JuridicalPersonCard_OKVED_Prompt"
			)]
		public string OKVED { get; set; }

		[UIHint("FixedLengthTextBox")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_KPP_Name",
				ShortName = "Models_JuridicalPersonCard_KPP_ShortName",
				Description = "Models_JuridicalPersonCard_KPP_Description",
				Prompt = "Models_JuridicalPersonCard_KPP_Prompt"
			)]
		public string KPP { get; set; }

		[UIHint("FixedLengthTextBox")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_JuridicalPersonCard_RwID_Name",
				ShortName = "Models_JuridicalPersonCard_RwID_ShortName",
				Description = "Models_JuridicalPersonCard_RwID_Description",
				Prompt = "Models_JuridicalPersonCard_RwID_Prompt"
			)]
		public string RwID { get; set; }

		[UIHint("TextBox")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_NaturalPersonCard_AddressLegal_Name",
				ShortName = "Models_NaturalPersonCard_AddressLegal_ShortName",
				Description = "Models_NaturalPersonCard_AddressLegal_Description",
				Prompt = "Models_NaturalPersonCard_AddressLegal_Prompt"
			)]
		public string AddressLegal { get; set; }

		[UIHint("TextBox")]
		[Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
				Name = "Models_NaturalPersonCard_AddressLegalEn_Name",
				ShortName = "Models_NaturalPersonCard_AddressLegalEn_ShortName",
				Description = "Models_NaturalPersonCard_AddressLegalEn_Description",
				Prompt = "Models_NaturalPersonCard_AddressLegal_Prompt"
			)]
		public string AddressLegalLat { get; set; }

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

	    public bool Verified { get; set; }

		public PersonCard()
		{
		}
	}
}