using System.ComponentModel.DataAnnotations;
using Kesco.Persons.ObjectModel.Localization;

namespace Kesco.Persons.ObjectModel.Metadata
{
    /// <summary>
    /// ТипыЛиц
    /// </summary>
    internal class PersonType
    {
        /// <summary>
        /// КодТипаЛица
        /// </summary>
        [UIHint("UniqueID")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_653",
                ShortName = "Kesco_Persons_MDL_654",
                Description = "Kesco_Persons_MDL_655",
                Prompt = "Kesco_Persons_MDL_656"
            )]
        public int ID { get; set; }

        /// <summary>
        /// КодТемыЛица
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_661",
                ShortName = "Kesco_Persons_MDL_662",
                Description = "Kesco_Persons_MDL_663",
                Prompt = "Kesco_Persons_MDL_664"
            )]
        public string PersonThemeID { get; set; }

    }
}
