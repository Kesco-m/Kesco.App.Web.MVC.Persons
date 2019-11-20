using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Mapping;

namespace Kesco.Employees.ObjectModel
{
	public class EmployeeCustomer
	{
		[MapField("Кличка")]
		public string Nickname { get; set; }

		[MapField("КраткоеНазваниеРус")]
		public string ShortNameRus { get; set; }

		[MapField("КраткоеНазваниеЛат")]
		public string ShortNameLat { get; set; }


	}
}
