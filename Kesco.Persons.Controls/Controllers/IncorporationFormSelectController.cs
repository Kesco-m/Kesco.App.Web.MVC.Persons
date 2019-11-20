using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kesco.Persons.Controls.DataAccess;
using Kesco.Web.Mvc.UI.Infrastructure;
using Kesco.Persons.ObjectModel;
using Kesco.Web.Mvc;

namespace Kesco.Persons.Controls.Controllers
{
	/// <summary>
	/// Реализует контроллер для элементов управления "Выбор орг-правю формы"
	/// </summary>
	public class IncorporationFormSelectController: KescoSelectBaseController<IncorporationFormSelectAccessor,IncorporationForm, IncorporationFormSelectAccessor.SearchParameters, int>
    {

		/// <summary>
		/// Gets the corporate culture settings.
		/// </summary>
		/// <returns></returns>
		protected override CultureSettings GetCorporateCultureSettings()
		{
			return Configuration.AppSettings.Culture;
		}

		/// <summary>
		/// Gets the advanced search URL.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException"></exception>
		protected override string GetAdvancedSearchUrl(BusinessLogic.DataAccess.IncorporationFormAccessor.SearchParameters parameters)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Возвращает URL-адрес для просмотра досье на лицо
		/// </summary>
		/// <param name="clid">Идентификатор клиента.</param>
		/// <returns>URL-адрес для просмотра досье на лицо</returns>
		protected override string GetDetailsUrl()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the entry label.
		/// </summary>
		/// <param name="entry">The entry.</param>
		/// <returns></returns>
		protected override string GetEntryLabel(IncorporationForm entry)
		{
			return entry.Name;
		}
	}
}
