using System.ComponentModel.DataAnnotations;
using Kesco.Persons.ObjectModel.Localization;

namespace Kesco.Persons.ObjectModel.Metadata
{
    /// <summary>
    /// ТипыКонтактов
    /// </summary>
    internal class Department
    {
        /// <summary>
        /// КодПодразделенияЛица
        /// </summary>
        [UIHint("UniqueID")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_449",
                ShortName = "Kesco_Persons_MDL_450",
                Description = "Kesco_Persons_MDL_451",
                Prompt = "Kesco_Persons_MDL_452"
            )]
        public int ID { get; set; }

        /// <summary>
        /// Подразделение
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_457",
                ShortName = "Kesco_Persons_MDL_458",
                Description = "Kesco_Persons_MDL_459",
                Prompt = "Kesco_Persons_MDL_460"
            )]
        public string DepartmentName { get; set; }

        /// <summary>
        /// ПодразделениеЛат
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_461",
                ShortName = "Kesco_Persons_MDL_462",
                Description = "Kesco_Persons_MDL_463",
                Prompt = "Kesco_Persons_MDL_464"
            )]
        public string DepartmentNameLat { get; set; }

        [ScaffoldColumn(false)]
        public int L { get; set; }


        [ScaffoldColumn(false)]
        public int R { get; set; }
    }
}
