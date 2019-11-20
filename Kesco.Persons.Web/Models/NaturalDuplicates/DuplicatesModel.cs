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
	/// Модель данных для формы дубликатов лица
	/// </summary>
	public class DuplicatesModel
	{
		public List<Duplicate> Duplicates { get; protected set; }

		public int PersonID { get; set; }
		public bool Confirmed { get; set; }

		public DuplicatesModel()
		{
			Duplicates = new List<Duplicate>();
		}

	}

}