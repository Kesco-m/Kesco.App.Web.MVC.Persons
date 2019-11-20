using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Mapping;

namespace Kesco.Employees.ObjectModel
{
    public class EmployeeReplacement
    {
        [MapField("КодЗамещенияСотрудников")]
        public int ReplacementId { get; set; }

        [MapField("До")]
        public DateTime ForDate { get; set; }

        [MapField("КодСотрудникаЗамещаемого")]
        public int EmployeeId { get; set; }

        [MapField("Замещённый")]
        public string ReplacedEmployeeName { get; set; }

        [MapField("КодСотрудникаЗамещающего")]
        public int VicariousId { get; set; }

        [MapField("ИспОбязанности")]
        public string VicariousName { get; set; }

        [MapField("Примечания")]
        public string Comment { get; set; }

        [MapField("Изменил")]
        public string EmployeeWhoChange { get; set; }

        [MapField("Изменено")]
        public DateTime ChangeDate { get; set; }
    }
}
