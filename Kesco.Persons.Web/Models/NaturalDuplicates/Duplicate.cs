using System;
using System.Linq;
using System.Collections.Generic;
using BLToolkit.Reflection;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.ObjectModel;
using Kesco.Persons.Web.Models.Requisites;
using Kesco.Web.Mvc;

namespace Kesco.Persons.Web.Models.NaturalDuplicates
{
	/// <summary>
	/// Модель данных для возможного дубликата
	/// </summary>
	public class Duplicate
	{
        public int PersonID { get; set; }
        public string Nickname { get; set; }
		public List<Issue> Issues { get; set; }
	}

	public class Issue
	{
		public string Field { get; set; }
		public string Value { get; set; }
		public bool Granted { get; set; }
		public int R { get; set; }
		public string Equality { 
			get { 
				return R == 0
					? Kesco.Persons.BusinessLogic.Localization.Resources.Kesco_Persons_MDL_678
					: Kesco.Persons.BusinessLogic.Localization.Resources.Kesco_Persons_MDL_677;
			}
		}
	}

}