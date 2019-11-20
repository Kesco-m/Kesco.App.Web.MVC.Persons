using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Kesco.Lib.Log;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.Compression.Configuration;
using Kesco.Web.Mvc.Compression.Routing;
using Kesco.Web.Mvc.SharedViews.Controllers;
using Kesco.Web.Mvc.SharedViews.App_Start;
using System.Web.Optimization;
using Kesco.Web.Mvc.Validation;
using Kesco.Web.Mvc.SharedViews.Localization;

using System.Diagnostics;
using System.Security.Policy;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Configuration<Kesco.Web.Mvc.SharedViews.SharedApplicationSettings>), "Init")]
//[assembly: WebActivator.PreApplicationStartMethod(typeof(Configuration<Kesco.Web.Mvc.ApplicationSettings>), "Init")]

namespace Kesco.Web.Mvc.SharedViews
{

	/// <summary>
	/// Класс, хранящий настройки для приложения SharedViews
	/// </summary>
	public class Configuration : Configuration<SharedApplicationSettings> {
		/// <summary>
		/// Initializes a new instance of the <see cref="Configuration" /> class.
		/// </summary>
		protected Configuration() : base() { }
	}

	/// <summary>
	/// Задаёт базовую мастер-страницу для всех приложений, 
	/// с типом настроек <see cref="Kesco.Web.Mvc.SharedViews.SharedApplicationSettings"/>
	/// и динамическим типом модели
	/// </summary>
	public abstract class SharedViewPage : SharedViewPage<dynamic> { }

	/// <summary>
	/// Задаёт базовую мастер-страницу для всех приложений
	/// с типом настроек <see cref="Kesco.Web.Mvc.SharedViews.SharedApplicationSettings"/>
	/// </summary>
	/// <typeparam name="TModel">Тип модели.</typeparam>
	public abstract class SharedViewPage<TModel> : SharedViewPage<SharedApplicationSettings, TModel> {}

	/// <summary>
	/// Задаёт базовую мастер-страницу для всех приложений.
	/// </summary>
	/// <typeparam name="S">Тип настроек приложения</typeparam>
	/// <typeparam name="TModel">Тип модели.</typeparam>
	public abstract class SharedViewPage<S, TModel> : WebViewPage<S, TModel> where S : SharedApplicationSettings
	{
		/// <summary>
		/// Возвращает имя контрола для отбражения контакта
		/// </summary>
		/// <param name="contactTypeID">Код типа контакта</param>
		/// <returns>Название шаблона для отображения контакта</returns>
		public string GetControlNameByContactType(int contactTypeID)
		{
		
			string ControlName = null;
			if ((contactTypeID == -1) || (contactTypeID >= 20 && contactTypeID <= 39)) // Телефон/Факс
				ControlName = "Phone";
			if (contactTypeID == -2 || contactTypeID == 40) // Электр. адрес
				ControlName = "Email";
			if (contactTypeID == 41) // Yahoo IM
				ControlName = "YahooIM";
			if (contactTypeID == -3) // MSN
				ControlName = "MSN";
			if (contactTypeID == 50) // Web
				ControlName = "Hyperlink";

			return ControlName;
		}

		/// <summary>
		/// Возвращает имя иконки для отбражения контакта
		/// </summary>
		/// <param name="contactTypeID">Код типа контакта</param>
		/// <returns>Имя файла картинки</returns>
		protected string GetControlIconByContactType(int contactTypeID)
		{
			string result = String.Empty;

			if ((contactTypeID == -1) || (contactTypeID >= 20 && contactTypeID <= 39)) // Телефон/Факс
				result = "PhoneStandard.gif";
			if (contactTypeID == -2 || contactTypeID == 40) // Электр. адрес
				result = "Email.gif";
			if (contactTypeID == 41) // Yahoo IM
				result = "Yim.gif";
			if (contactTypeID == -3) // MSN
				result = "Messenger.gif";
			if (contactTypeID == 50) // Web IM
				result = "Web.gif";

			return result;
		}

		protected string GetContactGroupLabel(int contactID)
		{
			string label = "";
			switch (contactID) {
				case -1: return Resources.DossierEmployeeInfo_Contacts_WorkPhone;
				case -2: return Resources.DossierEmployeeInfo_Contacts_Email;
				case -3: return Resources.DossierEmployeeInfo_Contacts_Messanger;
				default:
					return label;
			}
		}

	}

	/// <summary>
	/// Настройки приложения, используемые приложением SharedViews
	/// </summary>
	public abstract class SharedApplicationSettings : ApplicationSettings
	{
		public abstract int ResourceHowSearch { get; protected internal set; }
		public abstract string URI_user_form { get; protected internal set; }
		public abstract string URI_user_form_simple { get; protected internal set; }
		public abstract string URI_user_search { get; protected internal set; }
		public abstract string URI_user_photo { get; protected internal set; }
		public abstract string URI_user_info { get; protected set; }

		public abstract string URI_person_form { get; protected internal set; }
		public abstract string URI_person_search { get; protected internal set; }
		public abstract string URI_person_info { get; protected set; } 
		public abstract string URI_person_create_juridical { get; protected internal set; }
		public abstract string URI_person_create_natural { get; protected internal set; }

		public abstract string URI_resource_search { get; protected internal set; }
		public abstract string URI_resource_form { get; protected internal set; }
        public abstract string URI_phones { get; protected internal set; }

		/// <summary>
		/// Возвращает или устанавливает URI логотипа лица.
		/// </summary>
		/// <value>
		/// URI логотипа лица
		/// </value>
		public abstract string URI_person_logo { get; protected internal set; }
		public abstract string URI_resources_export1S { get; protected internal set; }
	}

	public class MvcApplication : SharedApplication<SharedApplicationSettings> {}

	/*public class ErrorHandlingPipelineModule : HubPipelineModule
	{
		protected override void OnIncomingError(Exception ex, IHubIncomingInvokerContext context)
		{
			Debug.WriteLine("=> Exception " + ex.Message);
			if (ex.InnerException != null)
			{
				Debug.WriteLine("=> Inner Exception " + ex.InnerException.Message);
			}
			base.OnIncomingError(ex, context);

		}
	}*/

	public class SharedApplication<S> : Application<S>
		where S : SharedApplicationSettings
	{

		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("");

			routes.MapRoute(
				"Default", // Имя маршрута
				"{controller}.aspx/{action}/{id}", // URL-адрес с параметрами
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Параметры по умолчанию

			);

			//script compression route handler
			Route compositeScriptRoute = new Route("CompositeScriptResource.aspx/getresource/{name}/{type}", new CompositeResourceRouteHandler());
			RouteTable.Routes.Add(compositeScriptRoute);
		}

		protected void Application_Start()
		{		
			// Проставим timestamp для ETagCultureBased
			// Если приложение перегружалось, необходимо очистить кеш клиента
			ETagConfig.Timestamp = DateTime.Now.Ticks.ToString();

			BLToolkit.Common.Configuration.NullableValues.String = null;
			BLToolkit.Common.Configuration.TrimOnMapping = false;

			// Set our model metadata provider to support Description attribute
			ModelMetadataProviders.Current = new KescoDataAnnotationsModelMetadataProvider();

			LogModule logModule = new LogModule(Configuration.AppSettings.AppName);
			logModule.Init(Configuration.AppSettings.SmtpServer, Configuration.AppSettings.Email_Support);
			Logger.Init(logModule);
			Logging.Logger.Init(logModule);

			// Инициализируем TAPI
			//Kesco.Zvonilka.Zvonilka.InitTapi(Configuration.AppSettings.AppName, false, false);
            

			AreaRegistration.RegisterAllAreas();

			ModelBinding.RegisterBinders();

			RegisterGlobalFilters(GlobalFilters.Filters);

			RegisterRoutes(RouteTable.Routes);

			BundleConfig.RegisterBundles(BundleTable.Bundles);

			//set the build version, so all composite resource requests include build
			//will invalite cache when no build is deployed
			Version v = Assembly.GetExecutingAssembly().GetName(false).Version;
			string version = string.Format("{0}{1}{2}{3}", v.Major, v.Minor, v.Build, v.Revision);
			CompositeResourceSettings.Version = version;

			// Устанавливаем собственный провайдер метаданных
			ModelMetadataProviders.Current = new KescoDataAnnotationsModelMetadataProvider();

			// Определяем собственный JsonValue Provider : Kesco.Web.Mvc.JsonValueProviderFactory
			ValueProviderFactories.Factories.Remove(ValueProviderFactories.Factories.OfType<System.Web.Mvc.JsonValueProviderFactory>().FirstOrDefault());
			ValueProviderFactories.Factories.Add(new Kesco.Web.Mvc.JsonValueProviderFactory());

			// Регистрируем адаптеры для клиентской валидации
			ModelValidatorRegistration.RegisterAdapters();

			// Регистрируем собсвенный связыватель данных.
			ModelBinders.Binders.DefaultBinder = new DefaultModelBinderEx();
		}

		/// <summary>
		/// Handles the Error event of the Application control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void Application_Error(object sender, EventArgs e)
		{
			try {
				HttpContext ctx = HttpContext.Current;
				Exception ex = ctx.Server.GetLastError();
				ctx.Response.Clear();

				Logger.WriteEx(ex);
				if (ctx.CurrentHandler is MvcHandler)
				{
					RequestContext rc = ((MvcHandler)ctx.CurrentHandler).RequestContext;
					DefaultController controller = new DefaultController();
						// Тут можно использовать любой контроллер, например тот что используется в качестве базового типа
					var context = new ControllerContext(rc, (ControllerBase) controller);

					ActionResult result = null;
					string ajaxRequestHeader = ctx.Request.Headers["X-Requested-With"];
					if (!String.IsNullOrEmpty(ajaxRequestHeader)
					    && ajaxRequestHeader == "XMLHttpRequest")
					{
						// Ajax запрос
						result = controller.JavaScriptAlert(
							Kesco.Web.Mvc.SharedViews.Localization.Resources.Kesco_Web_Mvc_SharedApp_LBL_13112,
							ex.Message
							);
					}
					else
					{
						// обычный запрос
						var viewResult = new ViewResult();
						result = viewResult;
						viewResult.ViewName = "Error";
						viewResult.ViewData.Model = new HandleErrorInfo(ex, context.RouteData.GetRequiredString("controller"),
						                                                context.RouteData.GetRequiredString("action"));
					}

					result.ExecuteResult(context);
				}
				ctx.Server.ClearError();
			} catch (Exception cex) {
				Logger.WriteEx(new DetailedException("Возникла критическая ошибка в процессе обработки Application_Error", cex));
			}

		}

		/// <summary>
		/// Handles the End event of the Application control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void Application_End(object sender, EventArgs e)
		{
		}

		/// <summary>
		/// Handles the AuthenticateRequest event of the Application control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{
			if (Context.Request.UserHostAddress.Equals(Request.ServerVariables["LOCAL_ADDR"])) return;

			string auth = Context.Request.ServerVariables["HTTP_AUTHORIZATION"];

			if (auth != null && auth.Length > 0 && !(auth.Substring(0, 6).Equals("Basic ") || auth.Substring(0, 11).Equals("Negotiate Y"))) {

				if (Request.Url.Scheme.Equals("http")) {
	
					string url = Request.Url.ToString().Replace("http", "https");
					Response.Status = "302 Redirect";
					Response.AddHeader("Location", url);
					Response.End();

				} else {

					Response.Redirect("/errorPage.htm");

				}

			}


		} 
	}
}