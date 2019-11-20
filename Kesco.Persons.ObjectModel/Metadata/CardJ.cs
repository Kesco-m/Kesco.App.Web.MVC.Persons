using System;
using System.ComponentModel.DataAnnotations;
using Kesco.Persons.ObjectModel.Localization;

namespace Kesco.Persons.ObjectModel.Metadata
{
    /// <summary>
    /// vwКарточкиЮрЛиц
    /// </summary>
    internal class CardJ
    {
        /// <summary>
        /// КодКарточкиЮрЛица
        /// </summary>
        [UIHint("UniqueID")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_309",
                ShortName = "Kesco_Persons_MDL_310",
                Description = "Kesco_Persons_MDL_311",
                Prompt = "Kesco_Persons_MDL_312"
            )]
        public int ID { get; set; }

        [UIHint("DateTimeField")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_177",
                ShortName = "Kesco_Persons_MDL_178",
                Description = "Kesco_Persons_MDL_179",
                Prompt = "Kesco_Persons_MDL_180"
            )]
        public DateTime From { get; set; }

        [UIHint("DateTimeField")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_181",
                ShortName = "Kesco_Persons_MDL_182",
                Description = "Kesco_Persons_MDL_183",
                Prompt = "Kesco_Persons_MDL_184"
            )]        
        public DateTime To { get; set; }

        /// <summary>
        /// КраткоеНазваниеРус
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_189",
                ShortName = "Kesco_Persons_MDL_190",
                Description = "Kesco_Persons_MDL_191",
                Prompt = "Kesco_Persons_MDL_192"
            )]
        public string ShortNameRus { get; set; }

        /// <summary>
        /// КраткоеНазваниеРусРП
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_193",
                ShortName = "Kesco_Persons_MDL_194",
                Description = "Kesco_Persons_MDL_195",
                Prompt = "Kesco_Persons_MDL_196"
            )]
        public string ShortNameRusGen { get; set; }

        /// <summary>
        /// КраткоеНазваниеЛат
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_197",
                ShortName = "Kesco_Persons_MDL_198",
                Description = "Kesco_Persons_MDL_199",
                Prompt = "Kesco_Persons_MDL_200"
            )]
        public string ShortNameLat { get; set; }

        /// <summary>
        /// ПолноеНазвание
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_201",
                ShortName = "Kesco_Persons_MDL_202",
                Description = "Kesco_Persons_MDL_203",
                Prompt = "Kesco_Persons_MDL_204"
            )]
        public string FullName { get; set; }

        /// <summary>
        /// ОКОНХ
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_205",
                ShortName = "Kesco_Persons_MDL_206",
                Description = "Kesco_Persons_MDL_207",
                Prompt = "Kesco_Persons_MDL_208"
            )]
        public string OKONH { get; set; }

        /// <summary>
        /// ОКВЭД
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_209",
                ShortName = "Kesco_Persons_MDL_210",
                Description = "Kesco_Persons_MDL_211",
                Prompt = "Kesco_Persons_MDL_212"
            )]
        public string OKVED { get; set; }

        /// <summary>
        /// КПП
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_213",
                ShortName = "Kesco_Persons_MDL_214",
                Description = "Kesco_Persons_MDL_215",
                Prompt = "Kesco_Persons_MDL_216"
            )]
        public string KPP { get; set; }

        /// <summary>
        /// КодЖД
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_217",
                ShortName = "Kesco_Persons_MDL_218",
                Description = "Kesco_Persons_MDL_219",
                Prompt = "Kesco_Persons_MDL_220"
            )]
        public string RwID { get; set; }

        /// <summary>
        /// АдресЮридический
        /// </summary>
        [UIHint("TwoLinesTextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_221",
                ShortName = "Kesco_Persons_MDL_222",
                Description = "Kesco_Persons_MDL_223",
                Prompt = "Kesco_Persons_MDL_224"
            )]
        public string AddressLegal { get; set; }

        /// <summary>
        /// АдресЮридическийЛат
        /// </summary>
        [UIHint("TwoLinesTextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_225",
                ShortName = "Kesco_Persons_MDL_226",
                Description = "Kesco_Persons_MDL_227",
                Prompt = "Kesco_Persons_MDL_228"
            )]
        public string AddressLegalLat { get; set; }
    }
}
