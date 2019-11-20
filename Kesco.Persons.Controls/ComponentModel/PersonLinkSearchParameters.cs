using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kesco.Web.Mvc.UI.ComponentModel.DataAnnotations;
using BLToolkit.Mapping;
using Kesco.DataAccess;

namespace Kesco.Persons.Controls.ComponentModel
{
	public class PersonLinkSearchParameters : KescoSelectSearchParametersAttribute
	{
		public HowSearch HowSearch { get; set; }
		public int? StartFrom { get; set; }
		public string IDs { get; set; }
		public string LinkTypeIDs { get; set; }
		public string ParentIDs { get; set; }
		public string ChildIDs { get; set; }
		public int Limit { get; set; }
	}

}