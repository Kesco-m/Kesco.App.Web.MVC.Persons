using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kesco.Web.Mvc.Test.Models.SelectBox;
using Kesco.Web.Mvc.UI.Infrastructure;

namespace Kesco.Web.Mvc.Test.Controllers
{
    public class SelectBoxController : KescoSelectBaseController<PersonSelectBoxAccessor, PersonSimple, PersonSelectBoxAccessor.SearchParameters, int>
    {

		public SelectBoxController()
			: base()
		{
			UseCompressHtml = true;
			MimeTypesToCompress = new string[] { "text/javascript", "text/html" };
		}

        public ActionResult Index()
        {
			var vm = new ViewModel();
			
			vm.Model.SupplierID = 506;

            return View(vm);
        }

		/// <summary>
		/// Gets the corporate culture settings.
		/// </summary>
		/// <returns></returns>
		protected override CultureSettings GetCorporateCultureSettings()
		{
			return Configuration.AppSettings.Culture;
		}

		protected override void AdjustSearchParameters(PersonSimpleAccessor.SearchParameters parameters)
		{
			base.AdjustSearchParameters(parameters);
			if (parameters != null) {
				parameters.MaxEntries = 9;
			}
		}

		protected override string GetEntryLabel(PersonSimple entry)
		{
			return entry.GetInstanceFriendlyName();
		}

		protected override string GetAdvancedSearchUrl(PersonSimpleAccessor.SearchParameters parameters)
		{
			throw new NotImplementedException();
		}

		protected override string GetDetailsUrl()
		{
			throw new NotImplementedException();
		}
	}
}
