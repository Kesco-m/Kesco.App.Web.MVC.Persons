using System.ComponentModel.DataAnnotations;
using Kesco.Persons.ObjectModel.Localization;

namespace Kesco.Persons.ObjectModel.Metadata
{
    /// <summary>
    /// ФорматНомеровРегистрацииЛиц
    /// </summary>
    internal class FormatRegistration
    {

        /// <summary>
        /// НазваниеОГРН
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_497",
                ShortName = "Kesco_Persons_MDL_498",
                Description = "Kesco_Persons_MDL_499",
                Prompt = "Kesco_Persons_MDL_500"
            )]
        public string OGRNName { get; set; }

        /// <summary>
        /// ФорматОГРН
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_501",
                ShortName = "Kesco_Persons_MDL_502",
                Description = "Kesco_Persons_MDL_503",
                Prompt = "Kesco_Persons_MDL_504"
            )]
        public string OGRNFormat { get; set; }


        /// <summary>
        /// НазваниеИНН
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_505",
                ShortName = "Kesco_Persons_MDL_506",
                Description = "Kesco_Persons_MDL_507",
                Prompt = "Kesco_Persons_MDL_508"
            )]
        public string INNName { get; set; }

        /// <summary>
        /// ДлинаИНН1
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_509",
                ShortName = "Kesco_Persons_MDL_510",
                Description = "Kesco_Persons_MDL_511",
                Prompt = "Kesco_Persons_MDL_512"
            )]
        public int INNLength1 { get; set; }

        /// <summary>
        /// ДлинаИНН2
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_513",
                ShortName = "Kesco_Persons_MDL_514",
                Description = "Kesco_Persons_MDL_515",
                Prompt = "Kesco_Persons_MDL_516"
            )]
        public int INNLength2 { get; set; }

        /// <summary>
        /// НазваниеОКПО
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_517",
                ShortName = "Kesco_Persons_MDL_518",
                Description = "Kesco_Persons_MDL_519",
                Prompt = "Kesco_Persons_MDL_520"
            )]
        public string OKPOName { get; set; }

        /// <summary>
        /// ДлинаОКПО1
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_521",
                ShortName = "Kesco_Persons_MDL_522",
                Description = "Kesco_Persons_MDL_523",
                Prompt = "Kesco_Persons_MDL_524"
            )]
        public int OKPOLength1 { get; set; }

        /// <summary>
        /// ДлинаОКПО2
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_525",
                ShortName = "Kesco_Persons_MDL_526",
                Description = "Kesco_Persons_MDL_527",
                Prompt = "Kesco_Persons_MDL_528"
            )]
        public int OKPOLength2 { get; set; }
    }
}
