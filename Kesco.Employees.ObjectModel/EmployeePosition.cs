using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Mapping;

namespace Kesco.Employees.ObjectModel
{
	public class EmployeePosition
	{
		[MapField("КодЛица")]
		public string PersonID { get; set; }

		[MapField("Организация")]
		public string Organization { get; set; }

		[MapField("Подразделение")]
		public string Department { get; set; }

		[MapField("Должность")]
		public string Position { get; set; }

		[MapField("Совместитель")]
		public bool IsCombining { get; set; }

		
	}
}
