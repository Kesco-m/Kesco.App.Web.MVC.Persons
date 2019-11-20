using System.ComponentModel.DataAnnotations;
using Kesco.Persons.ObjectModel.Localization;

namespace Kesco.Persons.ObjectModel.Metadata
{
    /// <summary>
    /// vwЛица_Сотрудники
    /// </summary>
    internal class ResponsibleEmployee
    {
        /// <summary>
        /// КодЛица_Сотрудники
        /// </summary>
        [UIHint("UniqueID")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_665",
                ShortName = "Kesco_Persons_MDL_666",
                Description = "Kesco_Persons_MDL_667",
                Prompt = "Kesco_Persons_MDL_668"
            )]
        public int ID { get; set; }

    }
}
