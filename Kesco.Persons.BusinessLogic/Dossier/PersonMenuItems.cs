using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Mapping;
using System.Text.RegularExpressions;
using Kesco.Persons.ObjectModel;

namespace Kesco.Persons.BusinessLogic.Dossier
{
	/// <summary>
	/// Пункт меню лица
	/// </summary>
	public class PersonMenuItem
	{
		[MapField("КодПунктаМенюЛиц")]
		public int ID { get; set; }

		[MapField("Название")]
		public string Caption { get; set; }

		[MapField("ПунктМенюKey")]
		public string CaptionKey { get; set; }

		[MapField("Рисунок")]
		public string Image { get; set; }

		[MapField("dialogForm")]
		public string DialogForm { get; set; }

		[MapField("title")]
		public string Title { get; set; }

		[MapField("paramEdit")]
		public byte ParamEdit { get; set; }

		[MapField("КодТипаСвязейЛиц")]
		public int PersonLinkID { get; set; }

		[MapField("ТипURL")]
		public byte URLType { get; set; }

		[MapField("Разделитель")]
		public byte Separator { get; set; }

		[MapField("ТипЛицаРодителя")]
		public byte ParentPersonType { get; set; }

		[MapField("ТипЛицаПотомка")]
		public byte ChildPersonType { get; set; }

		[MapField("URLФормы")]
		public string FormURL { get; set; }

		[MapField("URLПараметры")]
		public string URLParameters { get; set; }

		[MapField("Доступ_Роли")]
		public string RolesAccess { get; set; }

	}
}
