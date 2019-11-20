using System;
using System.ComponentModel.DataAnnotations;
using Kesco.Persons.ObjectModel.Localization;

namespace Kesco.Persons.ObjectModel.Metadata
{
    /// <summary>
    /// КонтактыАктуальность
    /// </summary>
    internal class ContactActual
    {
        /// <summary>
        /// Связь КонтактыАктуальность -> Лица
        /// </summary>
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_417",
                ShortName = "Kesco_Persons_MDL_418",
                Description = "Kesco_Persons_MDL_419",
                Prompt = "Kesco_Persons_MDL_420"
            )]
        public int ID { get; set; }

    }
}
