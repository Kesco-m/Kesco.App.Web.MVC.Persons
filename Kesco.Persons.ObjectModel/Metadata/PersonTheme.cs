using System.ComponentModel.DataAnnotations;
using Kesco.Persons.ObjectModel.Localization;

namespace Kesco.Persons.ObjectModel.Metadata
{
	internal class PersonTheme
	{
		/// <summary>
		/// КодТемыЛица
		/// </summary>
		[UIHint("UniqueID")]
		[Display(ResourceType = typeof(Resources),
				Name = "Kesco_Persons_MDL_PersonTheme_ID_Name",
				ShortName = "Kesco_Persons_MDL_PersonTheme_ID_ShortName",
				Description = "Kesco_Persons_MDL_PersonTheme_ID_Description",
				Prompt = "Kesco_Persons_MDL_PersonTheme_ID_Prompt"
			)]
		public int ID { get; set; }

		/// <summary>
		/// Parent
		/// </summary>
		[UIHint("PersonThemeField", "MVC",
				"data-bind", "/* */"
			)]
		[StringLength(100)]
		public int? Parent { get; set; }

		/// <summary>
		/// ТемаЛица
		/// </summary>
		[UIHint("FixedLengthTextBox", "MVC", 
				"width", "150px",
				"data-bind", "/* */"
			)]
		[StringLength(100)]
		public string Name { get; set; }

	}
}
