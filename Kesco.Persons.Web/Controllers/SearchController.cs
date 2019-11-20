using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.BusinessLogic.DataAccess;
using Kesco.Persons.ObjectModel;
using Kesco.Persons.Web.Models;
using Kesco.Web.Mvc;

namespace Kesco.Persons.Web.Controllers
{
	/// <summary>
	/// Контроллер обслуживает страницу поиска
	/// </summary>
	public partial class SearchController : ControllerEx
	{

		public SearchController()
			: base()
		{
			UseCompressHtml = true;
		}

		protected override CultureSettings GetCorporateCultureSettings()
		{
			return Configuration.AppSettings.Culture;
		}

		public virtual ActionResult Index()
		{
			return View(new SearchViewModel());
		}

		public virtual ActionResult Execute(PersonAccessor.SearchParameters parameters)
		{
			try {
				List<Person> persons = Repository.Persons.Search(parameters);

				return Json(new { Persons = persons }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				Logging.Logger.WriteEx(ex);

				return JsonError(ex.Message, ex.ToString(), JsonRequestBehavior.AllowGet);
			}
		}

		public virtual JsonResult SearchPersonGridDataRequested(PersonAccessor.SearchParameters parameters, int? execute)
		{
            if (parameters.PersonValidAtTicks > 0)
		        parameters.PersonValidAt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddTicks(parameters.PersonValidAtTicks);

			var gridModel = new SearchResultViewModel();
			gridModel.Grid.DataUrl = Url.Action("SearchPersonGridDataRequested");
			IQueryable<Person> result;

			if ((execute ?? 0) > 0 || !String.IsNullOrWhiteSpace(parameters.Search))
				result = Repository.Persons.Search(parameters).AsQueryable();
			else
				result = (new List<Person>()).AsQueryable();

			return gridModel.Grid.DataBind(result);
		}

	}
}
