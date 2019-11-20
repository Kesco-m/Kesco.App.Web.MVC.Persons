using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Mapping;
using System.Threading;

namespace Kesco.Employees.ObjectModel
{
	public class EmployeeCoWorker
	{
		[MapField("КодСотрудника")]
		public int CoWorkerID { get; set; }
		
		public string CoWorker { 
			get
			{
				string coWorkerName = String.Empty;

				if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "ru")
					coWorkerName = Kesco.StringExtensions.Coalesco(CoWorkerRU, CoWorkerEN);
				else
					coWorkerName = Kesco.StringExtensions.Coalesco(CoWorkerEN, CoWorkerRU);

				return coWorkerName ?? ("#" + CoWorkerID);
			}
		}

		[MapField("Сотрудник")]
		public string CoWorkerRU { get; set; }

		[MapField("Employee")]
		public string CoWorkerEN { get; set; }

        [MapField("КодЛица")]
        public int? PersonID { get; set; }

        [MapField("Состояние")]
        public int Status { get; set; }

        [MapField("РабочееМесто")]
        public int WorkPlace { get; set; }

	}
}
