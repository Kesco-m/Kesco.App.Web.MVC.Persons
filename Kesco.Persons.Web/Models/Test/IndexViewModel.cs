using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Kesco.Persons.Controls.Models;
using Kesco.Territories.Controls.ComponentModel;
using Kesco.Web.Mvc;
using Kesco.Persons.ObjectModel;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.Controls.ComponentModel;

namespace Kesco.Persons.Web.Models.Test
{
	public class IndexViewModel : DialogViewModel
	{
		public List<PersonSimple> PersonList { get; set; }

		public class Document
		{
			public int ID { get; set; }
			public int ResponsibleEmployeeID { get; set; }

			public int ShipperID { get; set; }
			public int ConsigneeID { get; set; }


            [UIHint("TerritoryField")]
            [TerritorySelect]
            [TerritorySelectSearchParameters(CLID = 66, TAreaID = 2, Limit = 9)]
            public int TerritoryID { get; set; }

		}


		public  Document DocumentInstance { get; set; }
        /// <summary>
        /// Типы лиц
        /// </summary>

       
        [Display(ResourceType = typeof(Kesco.Persons.Web.Localization.Resources),
        Name = "Models_JuridicalPersonCard_IncorporationForm_Name",
        ShortName = "Models_JuridicalPersonCard_IncorporationForm_ShortName",
        Description = "Models_JuridicalPersonCard_IncorporationForm_Description",
        Prompt = "Models_JuridicalPersonCard_IncorporationForm_Prompt"
        )]
        [UIHint("PersonTypesList")]
        public PersonTypesList PersonTypes { get; set; }

		public IndexViewModel() : base()
		{
            //this.PersonTypes = new PersonTypesList
            //                       {
                                       
            //                       };
            ////PersonThemeID = "501, 502, 505";
            //this.DocumentInstance = new Document
            //{
            //    ID = 1,
            //    ResponsibleEmployeeID = 3279,
            //    ShipperID = 506,
            //    ConsigneeID = 1603
            //};
            //PersonList = Repository.PersonPartials.Search(
            //        new BusinessLogic.DataAccess.PersonSimpleAccessor.SearchParameters {
            //            IDs = new List<int> { 506, 1603, 22670 },
            //            PersonTypes =  new List<int> { 2 },
            //            INNs = new List<string> { "12344564564" },
            //            Search = "Жданов"
            //    });
		}

		protected override void CreateSettings()
		{
			settings = new object();
		}


	}
}