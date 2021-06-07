using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Mapping;

namespace Kesco.Employees.ObjectModel
{
	public class EmployeeWorkPlace
	{
		[MapField("КодРасположения")]
		public int WorkPlaceID { get; set; }

		[MapField("РасположениеPath")]
		public string Path { get; set; }

        [MapField("РабочееМесто")]
        public string WorkPlacePar { get; set; }

        [MapField("ОрганизованноеРабочееМесто")]
        public bool OrgWorkPlace { get; set; }
        
        [MapIgnore]
		public List<EmployeeCoWorker> CoWorkers { get; set; }
	}
}
