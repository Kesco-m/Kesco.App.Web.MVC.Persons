using System.ComponentModel.DataAnnotations;
using Kesco.Persons.ObjectModel.Localization;

namespace Kesco.Persons.ObjectModel.Metadata
{
    /// <summary>
    /// ОргПравФормы
    /// </summary>
    public class IncorporationForm
    {
        /// <summary>
        /// КодОргПравФормы
        /// </summary>
        [UIHint("UniqueID")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_529",
                ShortName = "Kesco_Persons_MDL_530",
                Description = "Kesco_Persons_MDL_531",
                Prompt = "Kesco_Persons_MDL_532"
            )]
        public int ID { get; set; }

        /// <summary>
        /// ОргПравФорма
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_533",
                ShortName = "Kesco_Persons_MDL_534",
                Description = "Kesco_Persons_MDL_535",
                Prompt = "Kesco_Persons_MDL_536"
            )]
        public string Name { get; set; }

        /// <summary>
        /// КраткоеНазвание
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_537",
                ShortName = "Kesco_Persons_MDL_538",
                Description = "Kesco_Persons_MDL_539",
                Prompt = "Kesco_Persons_MDL_540"
            )]
        public string ShortName { get; set; }

        /// <summary>
        /// ТипЛица
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_541",
                ShortName = "Kesco_Persons_MDL_542",
                Description = "Kesco_Persons_MDL_543",
                Prompt = "Kesco_Persons_MDL_544"
            )]
        public byte PersonType { get; set; }
    }
}
