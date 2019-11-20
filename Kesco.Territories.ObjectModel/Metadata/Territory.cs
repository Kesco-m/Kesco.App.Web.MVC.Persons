using System.ComponentModel.DataAnnotations;
using Kesco.Territories.ObjectModel.Localization;

namespace Kesco.Territories.ObjectModel.Metadata
{
    /// <summary>
    /// Территории
    /// </summary>
    internal class Territory
	{
        /// <summary>
        /// КодТерритории
        /// </summary>
		[UIHint("UniqueID")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Territories_MDL_101",
                ShortName = "Kesco_Territories_MDL_102",
                Description = "Kesco_Territories_MDL_103",
                Prompt = "Kesco_Territories_MDL_104"
            )]  
		public int ID { get; set; }

        /// <summary>
        /// Территория
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Territories_MDL_105",
                ShortName = "Kesco_Territories_MDL_106",
                Description = "Kesco_Territories_MDL_107",
                Prompt = "Kesco_Territories_MDL_108"
            )]    
        public string Name { get; set; }

	}

	
}