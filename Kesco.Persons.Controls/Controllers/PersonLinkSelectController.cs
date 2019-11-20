using System;
using Kesco.Persons.Controls.DataAccess;
using Kesco.Persons.ObjectModel;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.UI.Infrastructure;

namespace Kesco.Persons.Controls.Controllers
{
	/// <summary>
	/// Реализует контроллер для элемента управления "Выбор лица"
	/// </summary>
	public partial class PersonLinkSelectController : KescoSelectBaseController<PersonLinkSelectAccessor, PersonLink, PersonLinkSelectAccessor.SearchParameters, int>
	{
		protected override string GetAdvancedSearchUrl(PersonLinkSelectAccessor.SearchParameters parameters)
		{
			throw new NotImplementedException();
		}

		protected override string GetDetailsUrl()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the corporate culture settings.
		/// </summary>
		/// <returns></returns>
		protected override CultureSettings GetCorporateCultureSettings()
		{
			return Configuration.AppSettings.Culture;
		}

		/// <summary>
		/// Возвращает наименованине для лица
		/// </summary>
		/// <param name="entry">Связь лица</param>
		/// <returns></returns>
		protected override string GetEntryLabel(PersonLink entry)
		{
			return entry.GetInstanceFriendlyName();
		}

		protected override void AdjustSearchParameters(BusinessLogic.DataAccess.PersonLinkAccessor.SearchParameters parameters)
		{
			base.AdjustSearchParameters(parameters);
			parameters.Limit = 9;
		}
	}
}
