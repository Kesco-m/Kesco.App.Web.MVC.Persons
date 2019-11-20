using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kesco.Web.Mvc;

namespace Kesco.Support.Web.Controllers
{
	[Authorize]
    public class MailServiceController : Controller
    {
        //
        // GET: /MailService/

        public ActionResult Index()
        {
            return View();
        }

		public ActionResult SendMail(string subject, string body)
		{
			return View();
		}

	}
}
