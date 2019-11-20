using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Mapping;

namespace Kesco.Employees.ObjectModel
{
	public class EmployeeChangedBy
	{
		[MapField("КодСотрудника")]
		public int WorkPlaceID { get; set; }

		[MapField("Кличка")]
		public string Path { get; set; }

		[MapIgnore]
		public List<EmployeeCoWorker> CoWorkers { get; set; }
	}
}
