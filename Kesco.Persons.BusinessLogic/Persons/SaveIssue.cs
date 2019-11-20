using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Mapping;
using Kesco.Persons.BusinessLogic.Localization;

namespace Kesco.Persons.BusinessLogic.Persons
{
	/// <summary>
	/// "Похожее" лицо, уже существующее в бд
	/// </summary>
	public class SaveIssue
	{
		public int R { get; set; }

		public string Equality { get { return R == 0 ? Resources.Kesco_Persons_MDL_678 : Resources.Kesco_Persons_MDL_677; } }

		[MapField("КодЛица")]
		public int PersonID { get; set; }

		[MapField("Кличка")]
		public string Nickname { get; set; }

		[MapField("Поле")]
		public string Field { get; set; }

		[MapField("Значение")]
		public string Value { get; set; }

		[MapField("Доступ")]
		public bool Granted { get; set; }
	}

}
