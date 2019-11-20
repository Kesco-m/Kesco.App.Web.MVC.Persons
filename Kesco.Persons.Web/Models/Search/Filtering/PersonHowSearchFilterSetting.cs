using Kesco.Web.Mvc.Filtering;

namespace Kesco.Persons.Web.Models.Search.Filtering
{
	public class PersonHowSearchFilterSetting : IntQSFilterSetting<PersonHowSearchFilterSetting>
	{
		public PersonHowSearchFilterSetting() : base(1,2){}
	}
}