using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Kesco.BusinessProjects.Controls.ComponentModel;
using Kesco.Web.Mvc;

namespace Kesco.BusinessProjects.Controls.Controllers
{
	public class BusinessProjectSelectUsageController : ModelController<BusinessProjectSelectUsageController.DataModel>
    {
		public class DataModel
		{

			[Display(Name = "Бизнес-проект")]
			[BusinessProjectSelect]
			[BusinessProjectSelectSearchParameters(CLID = 62)]
			public int BusinessProjectID { get; set; }

		}

		public class ViewModel : ViewModel<DataModel>
		{
			protected override void CreateSettings() { }
		}

		public BusinessProjectSelectUsageController() : base()
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
			viewModel.Model.BusinessProjectID = 18;
			return View(viewModel);
		}

    }
}
