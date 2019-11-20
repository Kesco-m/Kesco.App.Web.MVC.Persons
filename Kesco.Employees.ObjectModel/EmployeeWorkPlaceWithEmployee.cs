using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Mapping;

namespace Kesco.Employees.ObjectModel
{
    public class EmployeeWorkPlaceWithEmployee
    {
        [MapField("КодСотрудника")]
        public string EmployeeID { get; set; }

        [MapField("КодРасположения")]
        public int WorkPlaceID { get; set; }
    }

}
