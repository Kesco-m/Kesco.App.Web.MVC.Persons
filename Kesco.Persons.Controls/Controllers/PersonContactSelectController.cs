using System;
using Kesco.Persons.Controls.DataAccess;
using Kesco.Persons.ObjectModel;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.UI.Infrastructure;

namespace Kesco.Persons.Controls.Controllers
{
	public class PersonContactSelectController : KescoSelectBaseController<PersonContactSelectAccessor, PersonContact, PersonContactSelectAccessor.SearchParameters, int>
    {
		/// <summary>
		/// Gets the corporate culture settings.
		/// </summary>
		/// <returns></returns>
		protected override CultureSettings GetCorporateCultureSettings()
		{
			return Configuration.AppSettings.Culture;
		}


		protected override string GetAdvancedSearchUrl(PersonContactSelectAccessor.SearchParameters parameters)
		{
			throw new NotImplementedException();
		}

		protected override string GetDetailsUrl()
		{
			throw new NotImplementedException();
		}

		protected override string GetEntryLabel(PersonContact entry)
		{
			return entry.GetInstanceFriendlyName();
		}
	}
}
