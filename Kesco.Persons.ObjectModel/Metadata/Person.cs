using System;
using System.ComponentModel.DataAnnotations;
using Kesco.Persons.ObjectModel.Localization;

namespace Kesco.Persons.ObjectModel.Metadata
{
	internal class Person
	{
		/// <summary>
		/// КодЛица
		/// </summary>
		[ScaffoldColumn(false)]
		[UIHint("UniqueID")]
		[Display(ResourceType = typeof(Resources),
				Name = "Kesco_Persons_MDL_313",
				ShortName = "Kesco_Persons_MDL_314",
				Description = "Kesco_Persons_MDL_315",
				Prompt = "Kesco_Persons_MDL_316"
			)]
		public int ID { get; set; }

		/// <summary>
		/// ТипЛица
		/// </summary>
		[ScaffoldColumn(false)]
		[Display(ResourceType = typeof(Resources),
				Name = "Kesco_Persons_MDL_113",
				ShortName = "Kesco_Persons_MDL_114",
				Description = "Kesco_Persons_MDL_115",
				Prompt = "Kesco_Persons_MDL_116"
			)]
		public byte PersonType { get; set; }

		/// <summary>
		/// Код бизнес-проекта
		/// </summary>
		[UIHint("BusinessProjectField")]
		[Display(ResourceType = typeof(Resources),
				Name = "Kesco_Persons_MDL_117",
				ShortName = "Kesco_Persons_MDL_118",
				Description = "Kesco_Persons_MDL_119",
				Prompt = "Kesco_Persons_MDL_120"
			)]			
		public int BusinessProjectID { get; set; }

		/// <summary>
		/// Проверено
		/// </summary>
		[ScaffoldColumn(false)]
		[UIHint("Verified")]
		[Display(ResourceType = typeof(Resources),
				Name = "Kesco_Persons_MDL_121",
				ShortName = "Kesco_Persons_MDL_122",
				Description = "Kesco_Persons_MDL_123",
				Prompt = "Kesco_Persons_MDL_124"
			)]
		public bool Verified { get; set; }

		[UIHint("TextBox")]
		[Display(ResourceType=typeof(Resources),
				Name = "Kesco_Persons_MDL_101",
				ShortName = "Kesco_Persons_MDL_103",
				Description = "Kesco_Persons_MDL_102",
				Prompt = "Kesco_Persons_MDL_104"
			)]
        [Required(ErrorMessage="ОШИБКА - укажите кличку лица")]
		public string Nickname { get; set; }

		[ScaffoldColumn(false)]
		[Display(ResourceType = typeof(Resources),
				Name = "Kesco_Persons_MDL_105",
				ShortName = "Kesco_Persons_MDL_106",
				Description = "Kesco_Persons_MDL_107",
				Prompt = "Kesco_Persons_MDL_108"
			)]
		public string NicknameRL { get; set; }

		/// <summary>
		/// НазваниеRL
		/// </summary>
		[ScaffoldColumn(false)]
		[UIHint("TwoLinesTextBox")]
		[Display(ResourceType = typeof(Resources),
				Name = "Kesco_Persons_MDL_125",
				ShortName = "Kesco_Persons_MDL_126",
				Description = "Kesco_Persons_MDL_127",
				Prompt = "Kesco_Persons_MDL_128"
			)]
		public string NameRL { get; set; }

        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_129",
                ShortName = "Kesco_Persons_MDL_130",
                Description = "Kesco_Persons_MDL_131",
                Prompt = "Kesco_Persons_MDL_132"
            )]
		[UIHint("TerritoryField")]		
		public int TerritoryID { get; set; }
		
		/// <summary>
		/// ГосОрганизация
		/// </summary>
		[UIHint("CheckBox")]
		[Display(ResourceType = typeof(Resources),
				Name = "Kesco_Persons_MDL_133",
				ShortName = "Kesco_Persons_MDL_134",
				Description = "Kesco_Persons_MDL_135",
				Prompt = "Kesco_Persons_MDL_136"
			)]
        public bool IsStateOrganization { get; set; }

		/// <summary>
		/// ИНН
		/// </summary>
		[UIHint("FixedLengthTextBox")]
		[Display(ResourceType = typeof(Resources),
				Name = "Kesco_Persons_MDL_137",
				ShortName = "Kesco_Persons_MDL_138",
				Description = "Kesco_Persons_MDL_139",
				Prompt = "Kesco_Persons_MDL_140"
			)]
		public string INN { get; set; }

		/// <summary>
		/// ОГРН
		/// </summary>
		[UIHint("FixedLengthTextBox")]
		[Display(ResourceType = typeof(Resources),
				Name = "Kesco_Persons_MDL_141",
				ShortName = "Kesco_Persons_MDL_142",
				Description = "Kesco_Persons_MDL_143",
				Prompt = "Kesco_Persons_MDL_144"
			)]
		public string OGRN { get; set; }

		/// <summary>
		/// ОКПО
		/// </summary>
		[UIHint("FixedLengthTextBox")]
		[Display(ResourceType = typeof(Resources),
				Name = "Kesco_Persons_MDL_145",
				ShortName = "Kesco_Persons_MDL_146",
				Description = "Kesco_Persons_MDL_147",
				Prompt = "Kesco_Persons_MDL_148"
			)]
		public string OKPO { get; set; }

		/// <summary>
		/// БИК
		/// </summary>
		[UIHint("FixedLengthTextBox")]
		[Display(ResourceType = typeof(Resources),
				Name = "Kesco_Persons_MDL_149",
				ShortName = "Kesco_Persons_MDL_151",
				Description = "Kesco_Persons_MDL_150",
				Prompt = "Kesco_Persons_MDL_152"
			)]
		public string BIK { get; set; }

		/// <summary>
		/// КорСчет
		/// </summary>
		[UIHint("FixedLengthTextBox")]
		[Display(ResourceType = typeof(Resources),
				Name = "Kesco_Persons_MDL_153",
				ShortName = "Kesco_Persons_MDL_154",
				Description = "Kesco_Persons_MDL_155",
				Prompt = "Kesco_Persons_MDL_156"
			)]
		public string LoroConto { get; set; }

		/// <summary>
		/// БИКРКЦ
		/// </summary>
		[UIHint("FixedLengthTextBox")]
		[Display(ResourceType = typeof(Resources),
				Name = "Kesco_Persons_MDL_157",
				ShortName = "Kesco_Persons_MDL_158",
				Description = "Kesco_Persons_MDL_159",
				Prompt = "Kesco_Persons_MDL_160"
			)]
		public string BIKRKC { get; set; }

		/// <summary>
		/// SWIFT
		/// </summary>
		[UIHint("FixedLengthTextBox")]
		[Display(ResourceType = typeof(Resources),
				Name = "Kesco_Persons_MDL_161",
				ShortName = "Kesco_Persons_MDL_162",
				Description = "Kesco_Persons_MDL_163",
				Prompt = "Kesco_Persons_MDL_164"
			)]
		public string SWIFT { get; set; }

		/// <summary>
		/// ДатаРождения
		/// </summary>
		[UIHint("DateTimeField")]
		[Display(ResourceType = typeof(Resources),
				Name = "Kesco_Persons_MDL_165",
				ShortName = "Kesco_Persons_MDL_166",
				Description = "Kesco_Persons_MDL_167",
				Prompt = "Kesco_Persons_MDL_168"
			)]
		[DataType(DataType.DateTime)]
		public DateTime? Birthday { get; set; }

		/// <summary>
		/// Примечание
		/// </summary>
		[UIHint("TwoLinesTextBox")]
		[Display(ResourceType = typeof(Resources),
				Name = "Kesco_Persons_MDL_169",
				ShortName = "Kesco_Persons_MDL_170",
				Description = "Kesco_Persons_MDL_171",
				Prompt = "Kesco_Persons_MDL_172"
			)]
		public string Comment { get; set; }
	}
}