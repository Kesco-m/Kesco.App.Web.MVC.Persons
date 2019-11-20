using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BLToolkit.Mapping;
using Kesco.Lib.Log;
using Kesco.Web.Mvc;

namespace Kesco.ApplicationServices.Web
{

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
				new { controller = "Settings", action = "Index", id = UrlParameter.Optional } // Параметры по умолчанию
			);

		}

		protected void Application_Start()
		{

			// Set our model metadata provider to support Description attribute
			ModelMetadataProviders.Current = new KescoDataAnnotationsModelMetadataProvider();

			LogModule logModule = new LogModule(AppSettings.AppName);
			logModule.Init(AppSettings.SmtpServer, AppSettings.Email_Support);
			Logger.Init(logModule);

			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);

			ValueProviderFactories.Factories.Add(new Kesco.Web.Mvc.JsonValueProviderFactory());

		}

		protected void Session_Start()
		{

		}

	}
}