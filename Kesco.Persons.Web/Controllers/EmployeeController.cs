using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kesco.Persons.BusinessLogic.DataAccess;
using Kesco.Persons.Web.Models.Test;

namespace Kesco.Persons.Web.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/

        public ActionResult Index(string employeeId)
        {
            ViewBag.EmployeeId = employeeId;
            ViewBag.titleInfo = employeeId + ' ' + PersonAccessor.Accessor.GetEmployeeFIOByID(employeeId);
            IndexViewModel model = new IndexViewModel();
            return View(model);
        }

    }
}
