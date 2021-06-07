using System.ComponentModel.DataAnnotations;
using Kesco.ObjectModel;
using Kesco.Persons.Controls.ComponentModel;
using Kesco.Territories.Controls.ComponentModel;

namespace Kesco.Persons.Web.Models.Contact
{
	public class Contact : TrackableEntity<Contact, int>
	{
		/// <summary>
		/// КодКонтакта
		/// </summary>
		[UIHint("UniqueID")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_341",
				ShortName = "Kesco_Persons_MDL_342",
				Description = "Kesco_Persons_MDL_343",
				Prompt = "Kesco_Persons_MDL_344"
			)]
		public override int ID { get; set; }

        /// <summary>
		/// КодЛица
		/// </summary>
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_345",
				ShortName = "Kesco_Persons_MDL_346",
				Description = "Kesco_Persons_MDL_347",
				Prompt = "Kesco_Persons_MDL_348"
			)]
		[PersonSelect(CLID = 2)]
		public int PersonID { get; set; }

		/// <summary>
		/// КодСвязиЛиц
		/// </summary>
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_349",
				ShortName = "Kesco_Persons_MDL_350",
				Description = "Kesco_Persons_MDL_351",
				Prompt = "Kesco_Persons_MDL_352"
			)]
		[PersonLinkSelect]
		[PersonLinkSearchParameters(LinkTypeIDs = "1")]
		public int? PersonLinkID { get; set; }

		/// <summary>
		/// КодТипаКонтакта
		/// </summary>
		[UIHint("ContactTypeField")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_353",
				ShortName = "Kesco_Persons_MDL_354",
				Description = "Kesco_Persons_MDL_355",
				Prompt = "Kesco_Persons_MDL_356"
			)]
		[Required]
		public int? ContactTypeID { get; set; }

		/// <summary>
		/// Контакт
		/// </summary>
		[UIHint("TextBox")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_357",
				ShortName = "Kesco_Persons_MDL_358",
				Description = "Kesco_Persons_MDL_359",
				Prompt = "Kesco_Persons_MDL_360"
			)]
		[Required(AllowEmptyStrings = true)]
		public string ContactText { get; set; }

		/// <summary>
		/// КонтактRL
		/// </summary>
		[ScaffoldColumn(false)]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_361",
				ShortName = "Kesco_Persons_MDL_362",
				Description = "Kesco_Persons_MDL_363",
				Prompt = "Kesco_Persons_MDL_364"
			)]
		public string ContactTextRL { get; set; }

		/// <summary>
		/// КодСтраны
		/// </summary>
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_365",
				ShortName = "Kesco_Persons_MDL_366",
				Description = "Kesco_Persons_MDL_367",
				Prompt = "Kesco_Persons_MDL_368"
			)]
		[TerritorySelect]
		[TerritorySelectSearchParameters(CLID=66, TAreaID=2, Limit=9)]
		[Required]
		public int? CountryID { get; set; }

		/// <summary>
		/// АдресИндекс
		/// </summary>
		[UIHint("FixedLengthTextBox")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_369",
				ShortName = "Kesco_Persons_MDL_370",
				Description = "Kesco_Persons_MDL_371",
				Prompt = "Kesco_Persons_MDL_372"
			)]
		public string Zip { get; set; }

		/// <summary>
		/// АдресОбласть
		/// </summary>
		[UIHint("TextBox")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_373",
				ShortName = "Kesco_Persons_MDL_374",
				Description = "Kesco_Persons_MDL_375",
				Prompt = "Kesco_Persons_MDL_376"
			)]
		public string Region { get; set; }

		/// <summary>
		/// АдресГород
		/// </summary>
		[UIHint("TextBox")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_377",
				ShortName = "Kesco_Persons_MDL_378",
				Description = "Kesco_Persons_MDL_379",
				Prompt = "Kesco_Persons_MDL_380"
			)]
		public string CityName { get; set; }

		/// <summary>
		/// АдресГородRus
		/// </summary>
		[UIHint("TextBox")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_381",
				ShortName = "Kesco_Persons_MDL_382",
				Description = "Kesco_Persons_MDL_383",
				Prompt = "Kesco_Persons_MDL_384"
			)]
		public string CityNameRus { get; set; }

		/// <summary>
		/// Адрес
		/// </summary>
		[UIHint("TextBox")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_385",
				ShortName = "Kesco_Persons_MDL_386",
				Description = "Kesco_Persons_MDL_387",
				Prompt = "Kesco_Persons_MDL_388"
			)]
		public string Address { get; set; }

		/// <summary>
		/// ТелефонСтрана
		/// </summary>
		[UIHint("MaskedTextBox", "MVC", "width", "100px")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_389",
				ShortName = "Kesco_Persons_MDL_390",
				Description = "Kesco_Persons_MDL_391",
				Prompt = "Kesco_Persons_MDL_392"
			)]
		[Required(AllowEmptyStrings=true)]
		[StringLength(20)]
		public string CountryPhoneCode { get; set; }

		/// <summary>
		/// ТелефонГород
		/// </summary>
		[UIHint("MaskedTextBox", "MVC", "width", "100px")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_393",
				ShortName = "Kesco_Persons_MDL_394",
				Description = "Kesco_Persons_MDL_395",
				Prompt = "Kesco_Persons_MDL_396"
			)]
		[StringLength(20)]
		public string CityPhoneCode { get; set; }

		/// <summary>
		/// ТелефонНомер
		/// </summary>
		[UIHint("FixedLengthTextBox", "MVC", "width", "150px")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_397",
				ShortName = "Kesco_Persons_MDL_398",
				Description = "Kesco_Persons_MDL_399",
				Prompt = "Kesco_Persons_MDL_400"
			)]
		[Required(AllowEmptyStrings = true)]
		public string PhoneNumber { get; set; }

		/// <summary>
		/// ТелефонДоп
		/// </summary>
		[UIHint("FixedLengthTextBox", "MVC", "width", "50px")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_401",
				ShortName = "Kesco_Persons_MDL_402",
				Description = "Kesco_Persons_MDL_403",
				Prompt = "Kesco_Persons_MDL_404"
			)]
		public string PhoneNumberAdd { get; set; }

		/// <summary>
		/// ТелефонКорпНомер
		/// </summary>
		[UIHint("TextBox")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_405",
				ShortName = "Kesco_Persons_MDL_406",
				Description = "Kesco_Persons_MDL_407",
				Prompt = "Kesco_Persons_MDL_408"
			)]
		public string PhoneNumberCorporative { get; set; }

		/// <summary>
		/// ДругойКонтакт
		/// </summary>
		[UIHint("TextBox")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_409",
				ShortName = "Kesco_Persons_MDL_410",
				Description = "Kesco_Persons_MDL_411",
				Prompt = "Kesco_Persons_MDL_412"
			)]
		[Required(AllowEmptyStrings = true)]
		public string OtherContact { get; set; }

		/// <summary>
		/// Примечание
		/// </summary>
		[UIHint("TwoLinesTextBox")]
		[Display(ResourceType = typeof(Kesco.Persons.ObjectModel.Localization.Resources),
				Name = "Kesco_Persons_MDL_413",
				ShortName = "Kesco_Persons_MDL_414",
				Description = "Kesco_Persons_MDL_415",
				Prompt = "Kesco_Persons_MDL_416"
			)]
		public string Comment { get; set; }

		public override string GetInstanceFriendlyName()
		{
			return ContactText ?? System.String.Format("#{0}", ID);
		}

        public int value { get { return ID; } }
        public bool isAddress { get { return true; } }


	}
}