using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Kesco.Web.Mvc;
using Kesco.Log;
using Kesco.Support.Web.Controllers;

namespace Kesco.Support.Web
{
	public class KescoViewMasterPage : ViewMasterPage<KescoViewMasterPage, MvcApplication, ApplicationSettings>
	{
	}
	public abstract class ApplicationSettings : ApplicationSettings<ApplicationSettings>
	{

	}

	public class MvcApplication : Application<MvcApplication, ApplicationSettings>
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Имя маршрута
				"{controller}.aspx/{action}/{id}", // URL-адрес с параметрами
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Параметры по умолчанию
			);

		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);

			ValueProviderFactories.Factories.Add(new Kesco.Web.Mvc.JsonValueProviderFactory());

			LogModule logModule = new LogModule(AppSettings.AppName);
			logModule.Init(AppSettings.SmtpServer, AppSettings.Email_Support);
			Kesco.Logging.Logger.Init(logModule);

		}

		protected void Application_Error(object sender, EventArgs e)
		{
			HttpContext ctx = HttpContext.Current;
			Exception ex = ctx.Server.GetLastError();
			ctx.Response.Clear();

			RequestContext rc = ((MvcHandler)ctx.CurrentHandler).RequestContext;
			IController controller = new ControllerEx(); // Тут можно использовать любой контроллер, например тот что используется в качестве базового типа
			var context = new ControllerContext(rc, (ControllerBase)controller);

			var viewResult = new ViewResult();

			var httpException = ex as HttpException;
			if (httpException != null) {
				switch (httpException.GetHttpCode()) {
					case 404:
						viewResult.ViewName = "Error";
						break;

					case 500:
						viewResult.ViewName = "Error";
						break;

					default:
						viewResult.ViewName = "Error";
						break;
				}
			} else {
				viewResult.ViewName = "Error";
			}

			Kesco.Logging.Logger.WriteEx(ex);

			viewResult.ViewData.Model = new HandleErrorInfo(ex, context.RouteData.GetRequiredString("controller"), context.RouteData.GetRequiredString("action"));
			viewResult.ExecuteResult(context);
			ctx.Server.ClearError();
		}

	}
}