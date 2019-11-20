using System;
using System.ComponentModel.DataAnnotations;
using Kesco.Persons.ObjectModel.Localization;

namespace Kesco.Persons.ObjectModel.Metadata
{
    /// <summary>
    /// vwСвидетельстваНП
    /// </summary>
    internal class DistributionCertificate
    {
        /// <summary>
        /// КодСвидетельстваНП
        /// </summary>
        [UIHint("UniqueID")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_469",
                ShortName = "Kesco_Persons_MDL_470",
                Description = "Kesco_Persons_MDL_471",
                Prompt = "Kesco_Persons_MDL_472"
            )]
        public int ID { get; set; }

        [UIHint("Date")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_477",
                ShortName = "Kesco_Persons_MDL_478",
                Description = "Kesco_Persons_MDL_479",
                Prompt = "Kesco_Persons_MDL_480"
            )]
        public DateTime From { get; set; }

        [UIHint("Date")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_481",
                ShortName = "Kesco_Persons_MDL_482",
                Description = "Kesco_Persons_MDL_483",
                Prompt = "Kesco_Persons_MDL_484"
            )]
        public DateTime To { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_485",
                ShortName = "Kesco_Persons_MDL_486",
                Description = "Kesco_Persons_MDL_487",
                Prompt = "Kesco_Persons_MDL_488"
            )]
        public string Number { get; set; }

        /// <summary>
        /// Примечание
        /// </summary>
        [UIHint("TwoLinesTextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_489",
                ShortName = "Kesco_Persons_MDL_490",
                Description = "Kesco_Persons_MDL_491",
                Prompt = "Kesco_Persons_MDL_492"
            )]
        public string Comment { get; set; }


    }
}
