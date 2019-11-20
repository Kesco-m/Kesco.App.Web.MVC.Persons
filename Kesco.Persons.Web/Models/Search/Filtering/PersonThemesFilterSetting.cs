using Kesco.Web.Mvc;
using Kesco.Web.Mvc.Filtering;
using Kesco.Persons.ObjectModel;


namespace Kesco.Persons.Web.Models.Search.Filtering
{
	public class PersonThemesFilterSetting : ListQSFilterSetting<PersonThemesFilterSetting, PersonTheme> {

		public bool Subthemes { get { return (bool) subthemes.Value; } }

		private BooleanQSFilterSetting subthemes;

		public PersonThemesFilterSetting()
		{
			subthemes = BooleanQSFilterSetting.CreateInstance();
		}

		public PersonThemesFilterSetting InitFromQS(string qsParamName, object defaultValue, string qsSubthemesParamName, object subthemesDefaultValue)
		{
			InitFromQueryString(qsParamName, defaultValue);
			subthemes.InitFromQueryString(qsSubthemesParamName, subthemesDefaultValue);
			return this;
		}
	}
}