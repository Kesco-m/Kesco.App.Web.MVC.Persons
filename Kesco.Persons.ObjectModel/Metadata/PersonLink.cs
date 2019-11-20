using System;
using System.ComponentModel.DataAnnotations;
using Kesco.Persons.ObjectModel.Localization;

namespace Kesco.Persons.ObjectModel.Metadata
{
    /// <summary>
    /// vwСвязиЛиц
    /// </summary>
    public class PersonLink
    {
        /// <summary>
        /// КодСвязиЛиц
        /// </summary>
        [UIHint("UniqueID")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_577",
                ShortName = "Kesco_Persons_MDL_578",
                Description = "Kesco_Persons_MDL_579",
                Prompt = "Kesco_Persons_MDL_580"
            )]
        public int ID { get; set; }

        [UIHint("Date")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_585",
                ShortName = "Kesco_Persons_MDL_586",
                Description = "Kesco_Persons_MDL_587",
                Prompt = "Kesco_Persons_MDL_588"
            )]
        public DateTime From { get; set; }

        [UIHint("Date")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_589",
                ShortName = "Kesco_Persons_MDL_590",
                Description = "Kesco_Persons_MDL_591",
                Prompt = "Kesco_Persons_MDL_592"
            )]
        public DateTime To { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        [UIHint("TwoLinesTextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_601",
                ShortName = "Kesco_Persons_MDL_602",
                Description = "Kesco_Persons_MDL_603",
                Prompt = "Kesco_Persons_MDL_604"
            )]
        public string Description { get; set; }

        /// <summary>
        /// Параметр
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_605",
                ShortName = "Kesco_Persons_MDL_606",
                Description = "Kesco_Persons_MDL_607",
                Prompt = "Kesco_Persons_MDL_608"
            )]
        public int Parameter { get; set; }

    }
}
