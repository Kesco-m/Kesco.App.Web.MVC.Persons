using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Mapping;

namespace Kesco.Employees.ObjectModel
{
    public class EmployeeWorkPlaceChangeCheck
	{
        [MapField("КодСотрудника")]
		public int? EmployeeID { get; set; }

        [MapField("КодОбщегоСотрудника")]
        public int? CommonEmployeeID { get; set; }

        [MapField("КодРасположения")]
        public int? WorkPlaceID { get; set; }

        [MapField("Расположение")]
        public string WorkPlace { get; set; }

        [MapField("КодОбщегоСотрудникаНаРабочемМесте")]
        public int? CommonEmployeeIDOnWorkPlace { get; set; }

        [MapField("ФИООбщегоСотрудника")]
        public string FIOCommonEMployee { get; set; }


	}
}
