using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Mapping;

namespace Kesco.Employees.ObjectModel
{
	public class EmployeeContact
	{
		[MapField("Icon")]
		public string Icon { get; set; }

		[MapField("КодТипаКонтакта")]
		public int ContactType { get; set; }

		[MapField("ТипКонтакта")]
		public string ContactTypeDesc { get; set; }

		[MapField("Контакт")]
		public string Contact { get; set; }

		[MapField("Примечание")]
		public string Note { get; set; }

		[MapField("ТелефонныйНомер")]
		public string PhoneNumber { get; set; }

		[MapField("БыстрыйНабор")]
		public string QuickCall { get; set; }

		[MapField("КодТелефоннойСтанции")]
		public int? PhoneCenterID { get; set; }

		[MapField("КодТипаТелефонныхНомеров")]
		public int PhoneNumberTypeID { get; set; }

		[MapField("ТипТелефонныхНомеров")]
		public string PhoneNumberType { get; set; }

		[MapField("ВСправочнике")]
		[MapValue(true, 1)]
		[MapValue(false, 0)]
		public bool InDictionary { get; set; }

		[MapField("Порядок")]
		public int Order { get; set; }

	}
}
