using System;
using System.ComponentModel.DataAnnotations;
using Kesco.Persons.ObjectModel.Localization;

namespace Kesco.Persons.ObjectModel.Metadata
{
    /// <summary>
    /// vwКарточкиФизЛиц
    /// </summary>
    internal class CardN
    {
        /// <summary>
        /// КодКарточкиФизЛица
        /// </summary>
        [UIHint("UniqueID")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_305",
                ShortName = "Kesco_Persons_MDL_306",
                Description = "Kesco_Persons_MDL_307",
                Prompt = "Kesco_Persons_MDL_308"
            )]
        public int ID { get; set; }

        [UIHint("DateTimeField")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_233",
                ShortName = "Kesco_Persons_MDL_234",
                Description = "Kesco_Persons_MDL_235",
                Prompt = "Kesco_Persons_MDL_236"
            )]
        public DateTime From { get; set; }

        [UIHint("DateTimeField")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_237",
                ShortName = "Kesco_Persons_MDL_238",
                Description = "Kesco_Persons_MDL_239",
                Prompt = "Kesco_Persons_MDL_240"
            )]
        public DateTime To { get; set; }

		/// <summary>
		/// Связь КарточкиФизЛиц -> ОргПравФормы
		/// </summary>
		[UIHint("IncorporationFormField")]					// ???
		[Display(ResourceType = typeof(Resources),
				Name = "Kesco_Persons_MDL_241",
				ShortName = "Kesco_Persons_MDL_242",
				Description = "Kesco_Persons_MDL_243",
				Prompt = "Kesco_Persons_MDL_244"
			)]
		public int IncorporationFormID { get; set; }
		
        /// <summary>
        /// ФамилияРус
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_245",
                ShortName = "Kesco_Persons_MDL_246",
                Description = "Kesco_Persons_MDL_247",
                Prompt = "Kesco_Persons_MDL_248"
            )]
		[StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Kesco_Persons_MDL_2002")]
		public string LastNameRus { get; set; }

        /// <summary>
        /// ИмяРус
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_249",
                ShortName = "Kesco_Persons_MDL_250",
                Description = "Kesco_Persons_MDL_251",
                Prompt = "Kesco_Persons_MDL_252"
            )]
		[StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Kesco_Persons_MDL_2000")]
        public string FirstNameRus { get; set; }

        /// <summary>
        /// ОтчествоРус
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_253",
                ShortName = "Kesco_Persons_MDL_254",
                Description = "Kesco_Persons_MDL_255",
                Prompt = "Kesco_Persons_MDL_256"
            )]
		[StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Kesco_Persons_MDL_2001")]
		public string MiddleNameRus { get; set; }

        /// <summary>
        /// ФИОРус
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_257",
                ShortName = "Kesco_Persons_MDL_258",
                Description = "Kesco_Persons_MDL_259",
                Prompt = "Kesco_Persons_MDL_260"
            )]
        public string FIORus { get; set; }

        /// <summary>
        /// ИОФРус
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_261",
                ShortName = "Kesco_Persons_MDL_262",
                Description = "Kesco_Persons_MDL_263",
                Prompt = "Kesco_Persons_MDL_264"
            )]
        public string IOFRus { get; set; }

        /// <summary>
        /// ФамилияЛат
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_265",
                ShortName = "Kesco_Persons_MDL_266",
                Description = "Kesco_Persons_MDL_267",
                Prompt = "Kesco_Persons_MDL_268"
            )]
        public string LastNameLat { get; set; }

        /// <summary>
        /// ИмяЛат
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_269",
                ShortName = "Kesco_Persons_MDL_270",
                Description = "Kesco_Persons_MDL_271",
                Prompt = "Kesco_Persons_MDL_272"
            )]
		public string FirstNameLat { get; set; }

        /// <summary>
        /// ОтчествоЛат
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_273",
                ShortName = "Kesco_Persons_MDL_274",
                Description = "Kesco_Persons_MDL_275",
                Prompt = "Kesco_Persons_MDL_276"
            )]
        public string MiddleNameLat { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        [UIHint("Sex")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_277",
                ShortName = "Kesco_Persons_MDL_278",
                Description = "Kesco_Persons_MDL_279",
                Prompt = "Kesco_Persons_MDL_280"
            )]
        public char Sex { get; set; }

        /// <summary>
        /// ОКОНХ
        /// </summary>
		[UIHint("FixedLengthTextBox")]
		[Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_281",
                ShortName = "Kesco_Persons_MDL_282",
                Description = "Kesco_Persons_MDL_283",
                Prompt = "Kesco_Persons_MDL_284"
            )]
        public string OKONH { get; set; }

        /// <summary>
        /// ОКВЭД
        /// </summary>
		[UIHint("FixedLengthTextBox")]
		[Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_285",
                ShortName = "Kesco_Persons_MDL_286",
                Description = "Kesco_Persons_MDL_287",
                Prompt = "Kesco_Persons_MDL_288"
            )]
        public string OKVED { get; set; }

        /// <summary>
        /// КПП
        /// </summary>
        [UIHint("FixedLengthTextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_289",
                ShortName = "Kesco_Persons_MDL_290",
                Description = "Kesco_Persons_MDL_291",
                Prompt = "Kesco_Persons_MDL_292"
            )]
        public string KPP { get; set; }

        /// <summary>
        /// КодЖД
        /// </summary>
		[UIHint("FixedLengthTextBox")]
		[Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_293",
                ShortName = "Kesco_Persons_MDL_294",
                Description = "Kesco_Persons_MDL_295",
                Prompt = "Kesco_Persons_MDL_296"
            )]
        public string RwID { get; set; }

        /// <summary>
        /// АдресЮридический
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_297",
                ShortName = "Kesco_Persons_MDL_298",
                Description = "Kesco_Persons_MDL_299",
                Prompt = "Kesco_Persons_MDL_300"
            )]
        public string AddressLegal { get; set; }

        /// <summary>
        /// АдресЮридическийЛат
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_Persons_MDL_301",
                ShortName = "Kesco_Persons_MDL_302",
                Description = "Kesco_Persons_MDL_303",
                Prompt = "Kesco_Persons_MDL_304"
            )]
        public string AddressLegalLat { get; set; }
	}
}
