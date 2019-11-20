using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Kesco.Territories.Controls.ComponentModel;
using Kesco.Web.Mvc;

namespace Kesco.Territories.Controls.Controllers
{
	public class TerritorySelectUsageController : ModelController<TerritorySelectUsageController.DataModel>
    {
		public class DataModel
		{

			[Display(Name = "Страна")]
			[TerritorySelect(CLID = 62)]
			[TerritorySelectSearchParameters(TAreaID=2)]
			public int CountryID { get; set; }

		}

		public class ViewModel : ViewModel<DataModel>
		{
			protected override void CreateSettings() { }
		}

		public TerritorySelectUsageController() : base()
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

		//
		// GET: /PersonSelectUsage/

		public virtual ActionResult Index()
		{
			var viewModel = new ViewModel();
			viewModel.Model.CountryID = 180;
			return View(viewModel);
		}

    }
}
