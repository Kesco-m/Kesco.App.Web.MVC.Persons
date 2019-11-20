using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kesco.Web.Mvc.UI.ComponentModel.DataAnnotations;
using BLToolkit.Mapping;

namespace Kesco.Persons.Controls.ComponentModel
{
	[MapField("СтрокаПоиска", "Search")]
	public class PersonContactSearchParameters : KescoSelectSearchParametersAttribute
	{

		[MapField("КодЛица")]
		public int PersonID { get; set; }

		[MapField("СписокКодовТиповКонтактов")]
		public string ContactTypeIDList { get; set; }
	}

}