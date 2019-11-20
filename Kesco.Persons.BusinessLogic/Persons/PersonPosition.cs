using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Mapping;

namespace Kesco.Persons.BusinessLogic.Persons
{

	public class PersonPosition
	{
		[MapField("КодЛица")]
		public int PersonID { get; set; }

		[MapField("Организация")]
		public string Organization { get; set; }

		[MapField("Должность")]
		public string Position { get; set; }

		[MapField("КодСвязиЛиц")]
		public int? PersonLinkID { get; set; }

	}

}
