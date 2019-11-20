using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Kesco.Web.Mvc.ComponentModel.DataAnnotations;
using Kesco.Web.Mvc.UI.ComponentModel.DataAnnotations;
using Kesco.Web.Mvc.Test.Controllers;

namespace Kesco.Web.Mvc.Test.Models.SelectBox
{

	public class DerivedSelectAttribute : KescoSelectAttribute
	{
		public DerivedSelectAttribute() : base("DerivedSelectBox")
		{
			AutocompleteAction = "Search";
			AutocompleteController = "SelectBox";
			AutocompleteLimit = 8;
			EntityAccessorType = typeof(PersonSimpleAccessor);
			KeyField = "ID"; 
			DisplayField = "Nickname";
			ShowSearchButton = false;
			ShowViewButton = false;
			SelectControllerType = typeof(SelectBoxController);
		}
	}

	public class DataModel
	{
		[Display(Name="Поставщик")]
		[UIHint("DerivedSelectBox")]
		[Required]
		public int? SupplierID { get; set; }

	}


	public class ViewModel : ViewModel<DataModel>
	{

		public ViewModel()
			: base()
		{

		}

		protected override void CreateSettings()
		{
			settings = new object();
		}


	}


}