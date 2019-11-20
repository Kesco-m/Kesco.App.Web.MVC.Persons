using System;
using System.ComponentModel.DataAnnotations;
using Kesco.Persons.ObjectModel.Localization;

namespace Kesco.Persons.ObjectModel.Metadata
{
    /// <summary>
    /// ЛоготипыЛиц
    /// </summary>
    public class Logotype
    {
        /// <summary>
        /// КодЛоготипаЛица
        /// </summary>
        [UIHint("UniqueID")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_545",
                ShortName = "Kesco_Persons_MDL_546",
                Description = "Kesco_Persons_MDL_547",
                Prompt = "Kesco_Persons_MDL_548"
            )]
        public int ID { get; set; }

        /// <summary>
        /// ДатаСохранения
        /// </summary>
        [UIHint("Date")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_553",
                ShortName = "Kesco_Persons_MDL_554",
                Description = "Kesco_Persons_MDL_555",
                Prompt = "Kesco_Persons_MDL_556"
            )]
        public DateTime SaveDate { get; set; }

        /// <summary>
        /// Логотип
        /// </summary>
        [UIHint("Image")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_557",
                ShortName = "Kesco_Persons_MDL_558",
                Description = "Kesco_Persons_MDL_559",
                Prompt = "Kesco_Persons_MDL_560"
            )]
        public byte[] Logo { get; set; }

        /// <summary>
        /// ВерхнийКолонтитул
        /// </summary>
        [UIHint("Image")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_561",
                ShortName = "Kesco_Persons_MDL_562",
                Description = "Kesco_Persons_MDL_563",
                Prompt = "Kesco_Persons_MDL_564"
            )]
        public byte[] Header { get; set; }

        /// <summary>
        /// НижнийКолонтитул
        /// </summary>
        [UIHint("Image")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_565",
                ShortName = "Kesco_Persons_MDL_566",
                Description = "Kesco_Persons_MDL_567",
                Prompt = "Kesco_Persons_MDL_568"
            )]
        public byte[] Footer { get; set; }
    }
}
