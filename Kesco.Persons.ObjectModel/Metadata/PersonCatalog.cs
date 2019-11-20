using System.ComponentModel.DataAnnotations;
using Kesco.Persons.ObjectModel.Localization;

namespace Kesco.Persons.ObjectModel.Metadata
{
    /// <summary>
    /// КаталогиЛиц
    /// </summary>
    public class PersonCatalog
    {
        /// <summary>
        /// Связь КаталогиЛиц -> Лица
        /// </summary>
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_569",
                ShortName = "Kesco_Persons_MDL_570",
                Description = "Kesco_Persons_MDL_571",
                Prompt = "Kesco_Persons_MDL_572"
            )]
        public int PersonID { get; set; }

        /// <summary>
        /// Связь КаталогиЛиц -> Каталоги
        /// </summary>
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_573",
                ShortName = "Kesco_Persons_MDL_574",
                Description = "Kesco_Persons_MDL_575",
                Prompt = "Kesco_Persons_MDL_576"
            )]
        public int CatalogID { get; set; }

    }
}
