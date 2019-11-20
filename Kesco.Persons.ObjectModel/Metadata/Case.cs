using System.ComponentModel.DataAnnotations;
using Kesco.Persons.ObjectModel.Localization;

namespace Kesco.Persons.ObjectModel.Metadata
{
    /// <summary>
    /// Падежи
    /// </summary>
    internal class Case
    {
        /// <summary>
        /// Код
        /// </summary>
        [UIHint("UniqueID")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_317",
                ShortName = "Kesco_Persons_MDL_318",
                Description = "Kesco_Persons_MDL_319",
                Prompt = "Kesco_Persons_MDL_320"
            )]
        public int ID { get; set; }

        /// <summary>
        /// Именительный
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_321",
                ShortName = "Kesco_Persons_MDL_322",
                Description = "Kesco_Persons_MDL_323",
                Prompt = "Kesco_Persons_MDL_324"
            )]
        public string Nominative { get; set; }

        /// <summary>
        /// Дательный
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_325",
                ShortName = "Kesco_Persons_MDL_326",
                Description = "Kesco_Persons_MDL_327",
                Prompt = "Kesco_Persons_MDL_328"
            )]
        public string Dative { get; set; }

        /// <summary>
        /// Родительный
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_329",
                ShortName = "Kesco_Persons_MDL_330",
                Description = "Kesco_Persons_MDL_331",
                Prompt = "Kesco_Persons_MDL_332"
            )]
        public string Genitive { get; set; }
    }
}
