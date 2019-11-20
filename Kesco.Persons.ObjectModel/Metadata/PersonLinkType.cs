using System.ComponentModel.DataAnnotations;
using Kesco.Persons.ObjectModel.Localization;

namespace Kesco.Persons.ObjectModel.Metadata
{
    /// <summary>
    /// ТипыСвязейЛиц
    /// </summary>
    internal class PersonLinkType
    {
        /// <summary>
        /// КодТипаСвязиЛиц
        /// </summary>
        [UIHint("UniqueID")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_609",
                ShortName = "Kesco_Persons_MDL_610",
                Description = "Kesco_Persons_MDL_611",
                Prompt = "Kesco_Persons_MDL_612"
            )]
        public int ID { get; set; }

        /// <summary>
        /// ТипЛицаРодителя
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_613",
                ShortName = "Kesco_Persons_MDL_614",
                Description = "Kesco_Persons_MDL_615",
                Prompt = "Kesco_Persons_MDL_616"
            )]
        public byte ParentPersonType { get; set; }

        /// <summary>
        /// ТипЛицаПотомка
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_617",
                ShortName = "Kesco_Persons_MDL_618",
                Description = "Kesco_Persons_MDL_619",
                Prompt = "Kesco_Persons_MDL_620"
            )]
        public byte ChildPersonType { get; set; }

        /// <summary>
        /// НазваниеРодителяЕЧИмП
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_621",
                ShortName = "Kesco_Persons_MDL_622",
                Description = "Kesco_Persons_MDL_623",
                Prompt = "Kesco_Persons_MDL_624"
            )]
        public string ParentNameSingularNominative { get; set; }

        /// <summary>
        /// НазваниеПотомкаЕЧИмП
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_625",
                ShortName = "Kesco_Persons_MDL_626",
                Description = "Kesco_Persons_MDL_627",
                Prompt = "Kesco_Persons_MDL_628"
            )]
        public string ChildNameSingularNominative { get; set; }

        /// <summary>
        /// НазваниеРодителяМЧИмП
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_629",
                ShortName = "Kesco_Persons_MDL_630",
                Description = "Kesco_Persons_MDL_631",
                Prompt = "Kesco_Persons_MDL_632"
            )]
        public string ParentNamePluralNominative { get; set; }

        /// <summary>
        /// НазваниеПотомкаМЧИмП
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_633",
                ShortName = "Kesco_Persons_MDL_634",
                Description = "Kesco_Persons_MDL_635",
                Prompt = "Kesco_Persons_MDL_636"
            )]
        public string ChildNamePluralNominative { get; set; }

        /// <summary>
        /// НазваниеРодителяЕЧВинП
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_637",
                ShortName = "Kesco_Persons_MDL_638",
                Description = "Kesco_Persons_MDL_639",
                Prompt = "Kesco_Persons_MDL_640"
            )]
        public string ParentNameSingularAccusative { get; set; }

        /// <summary>
        /// НазваниеПотомкаЕЧВинП
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_641",
                ShortName = "Kesco_Persons_MDL_642",
                Description = "Kesco_Persons_MDL_643",
                Prompt = "Kesco_Persons_MDL_644"
            )]
        public string ParentNamePluralAccusative { get; set; }

        /// <summary>
        /// НазваниеРодителяМЧЛат
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_645",
                ShortName = "Kesco_Persons_MDL_646",
                Description = "Kesco_Persons_MDL_647",
                Prompt = "Kesco_Persons_MDL_648"
            )]
        public string ParentNamePluralLat { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_649",
                ShortName = "Kesco_Persons_MDL_650",
                Description = "Kesco_Persons_MDL_651",
                Prompt = "Kesco_Persons_MDL_652"
            )]
        public string Description { get; set; }


    }
}
