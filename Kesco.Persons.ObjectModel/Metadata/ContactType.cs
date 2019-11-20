using System.ComponentModel.DataAnnotations;
using Kesco.Persons.ObjectModel.Localization;

namespace Kesco.Persons.ObjectModel.Metadata
{
    /// <summary>
    /// ТипыКонтактов
    /// </summary>
    internal class ContactType
    {
        /// <summary>
        /// КодТипаКонтакта
        /// </summary>
        [UIHint("UniqueID")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_429",
                ShortName = "Kesco_Persons_MDL_430",
                Description = "Kesco_Persons_MDL_431",
                Prompt = "Kesco_Persons_MDL_432"
            )]
        public int ID { get; set; }

        /// <summary>
        /// ТипКонтакта
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_433",
                ShortName = "Kesco_Persons_MDL_434",
                Description = "Kesco_Persons_MDL_435",
                Prompt = "Kesco_Persons_MDL_436"
            )]
        public string Name { get; set; }

        /// <summary>
        /// ТипКонтактаЛат
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_437",
                ShortName = "Kesco_Persons_MDL_438",
                Description = "Kesco_Persons_MDL_439",
                Prompt = "Kesco_Persons_MDL_440"
            )]
        public string NameLat { get; set; }

        /// <summary>
        /// Категория
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_441",
                ShortName = "Kesco_Persons_MDL_442",
                Description = "Kesco_Persons_MDL_443",
                Prompt = "Kesco_Persons_MDL_444"
            )]
        public int Category { get; set; }

        /// <summary>
        /// icon
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_445",
                ShortName = "Kesco_Persons_MDL_446",
                Description = "Kesco_Persons_MDL_447",
                Prompt = "Kesco_Persons_MDL_448"
            )]
        public string Icon { get; set; }
    }
}
