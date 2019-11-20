using System.ComponentModel.DataAnnotations;
using Kesco.Persons.ObjectModel.Localization;

namespace Kesco.Persons.ObjectModel.Metadata
{
    /// <summary>
    /// vwКаталоги
    /// </summary>
    internal class Catalog
    {
        /// <summary>
        /// КодКаталога
        /// </summary>
        [UIHint("UniqueID")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_333",
                ShortName = "Kesco_Persons_MDL_334",
                Description = "Kesco_Persons_MDL_335",
                Prompt = "Kesco_Persons_MDL_336"
            )]
        public int ID { get; set; }

        /// <summary>
        /// Каталог
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_337",
                ShortName = "Kesco_Persons_MDL_338",
                Description = "Kesco_Persons_MDL_339",
                Prompt = "Kesco_Persons_MDL_340"
            )]
        public string CatalogName { get; set; }
    }
}
