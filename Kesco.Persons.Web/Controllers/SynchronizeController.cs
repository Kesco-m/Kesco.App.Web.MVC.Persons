using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kesco.Persons.Web.Models.Synchronize;
using Kesco.Web.Mvc.SharedViews;

namespace Kesco.Persons.Web.Controllers
{
    public class SynchronizeController : SharedModelController<DataModel>
    {

		public ActionResult Index(int idPerson, int idEmployee)
        {
			
			var vm = new ViewModel();

			vm.Init(idPerson, idEmployee);

			vm.Compare();

            return View(vm);

        }

    }
}
