using System;
using Kesco.BusinessProjects.BusinessLogic;
using Kesco.BusinessProjects.Controls.DataAccess;
using Kesco.BusinessProjects.ObjectModel;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.UI.Infrastructure;

namespace Kesco.BusinessProjects.Controls.Controllers
{
    public class BusinessProjectSelectController : KescoSelectBaseController<BusinessProjectSelectAccessor, BusinessProject, BusinessProjectSelectAccessor.SearchParameters, int>
    {

		public BusinessProjectSelectController()
			: base()
		{
			UseCompressHtml = true;
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
		/// Возвращает URL-адрес для расширенного поиска лица
		/// </summary>
		/// <param name="parameters">Параметры поиска/фильтрации.</param>
		/// <returns>
		/// URL-адрес для расширенного поиска лица
		/// </returns>
		protected override string GetAdvancedSearchUrl(BusinessProjectAccessor.SearchParameters parameters)
		{
			return Configuration.AppSettings.URI_bproject_search
				+ String.Format("?return=1&clid={0}&mvc=1&control=c&callbackKey=c1&callbackUrl={{0}}&search={{1}}", parameters.CLID);
		}

		/// <summary>
		/// Возвращает URL-адрес для просмотра досье на лицо
		/// </summary>
		/// <returns>
		/// URL-адрес для просмотра досье на лицо
		/// </returns>
		protected override string GetDetailsUrl()
		{
			return Configuration.AppSettings.URI_bproject_form;
		}

		protected override string GetEntryLabel(BusinessProject entry)
		{
			return entry.GetInstanceFriendlyName();
		}
	}
}
