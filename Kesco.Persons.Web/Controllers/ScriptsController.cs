using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kesco.Persons.Web.Controllers
{
	public class ScriptsController : Kesco.Web.Mvc.SharedViews.Controllers.ScriptController
    {

        public ActionResult Search()
        {
            return View();
        }

    }
}
