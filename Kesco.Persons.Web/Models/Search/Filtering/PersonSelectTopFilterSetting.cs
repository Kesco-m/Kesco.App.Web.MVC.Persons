using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kesco.Web.Mvc.Filtering;

namespace Kesco.Persons.Web.Models.Search.Filtering
{
	public class PersonSelectTopFilterSetting : IntQSFilterSetting<PersonSelectTopFilterSetting>
	{
		public PersonSelectTopFilterSetting() : base(0, Int32.MaxValue) { }
	}
}