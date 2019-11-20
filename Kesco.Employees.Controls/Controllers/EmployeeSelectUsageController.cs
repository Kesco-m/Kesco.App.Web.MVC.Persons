using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Kesco.Employees.Controls.ComponentModel;
using Kesco.Web.Mvc;

namespace Kesco.Employees.Controls.Controllers
{
	public class EmployeeSelectUsageController : ModelController<EmployeeSelectUsageController.DataModel>
    {
		public class DataModel
		{

			[Display(Name = "Разработчик")]
			[EmployeeSelect(CLID = 62)]
			public int DeveloperID { get; set; }

			[Display(Name = "Тестировщик")]
			[EmployeeSelect(CLID = 62)]
			public int TesterID { get; set; }

			[Display(Name = "Ответственный")]
			[EmployeeSelect(CLID = 62)]
			public int? ResponsibleID { get; set; }

		}

		public class ViewModel : ViewModel<DataModel>
		{
			protected override void CreateSettings() { }
		}

		public EmployeeSelectUsageController()
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

		//
		// GET: /PersonSelectUsage/

		public virtual ActionResult Index()
		{
			var viewModel = new ViewModel();
			viewModel.Model.DeveloperID = 3279;
			viewModel.Model.TesterID = 1684;
			viewModel.Model.ResponsibleID = 284;
			return View(viewModel);
		}

    }
}
