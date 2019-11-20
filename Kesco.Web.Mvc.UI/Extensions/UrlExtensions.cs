using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Kesco.Web.Mvc
{
	/// <summary>
	/// Класс расширяет MVC UrlHelper класс, добавляя новый FullPathAction метод.
	/// </summary>
	public static class UrlExtensions
	{

		/// <summary>
		/// Возвращает абсолютный путь, включая домен и протокол.
		/// </summary>
		/// <param name="urlHelper">Экземпляр URL helper.</param>
		/// <param name="actionName">Имя действия.</param>
		/// <param name="controllerName">Имя контроллера.</param>
		/// <param name="routeValues">Значения роутинга.</param>
		/// <returns>Абсолютный путь к ресурсу</returns>
		public static string AbsoluteAction(this UrlHelper urlHelper, string actionName, string controllerName, object routeValues = null)
		{
			string scheme = urlHelper.RequestContext.HttpContext.Request.Url.Scheme;

			return urlHelper.Action(actionName, controllerName, routeValues, scheme);
		}

		/// <summary>
		/// Возвращает абсолютный путь, включая домен и протокол.
		/// </summary>
		/// <param name="urlHelper">Экземпляр URL helper.</param>
		/// <param name="actionName">Имя действия.</param>
		/// <param name="controllerName">Имя контроллера.</param>
		/// <param name="routeValues">Значения роутинга.</param>
		/// <returns>Абсолютный путь к ресурсу</returns>
		public static string FullPathAction(this UrlHelper urlHelper
			, string actionName, string controllerName, object routeValues ) {
				return AbsoluteAction(urlHelper, actionName, controllerName, routeValues);
		}

		/// <summary>
		/// Возвращает полный путь, включая домен и протокол.
		/// </summary>
		/// <param name="urlHelper">Экземпляр URL helper.</param>
		/// <param name="actionName">Имя действия.</param>
		/// <param name="controllerName">Имя контроллера.</param>
		/// <returns>Полный путь к ресурсу</returns>
		public static string FullPathAction(this UrlHelper urlHelper
			, string actionName, string controllerName)
		{
			return FullPathAction(urlHelper, actionName, controllerName, null);
		}

		/// <summary>
		/// Возвращает полный путь, включая домен и протокол.
		/// </summary>
		/// <param name="urlHelper">Экземпляр URL helper.</param>
		/// <param name="actionName">Имя действия.</param>
		/// <returns>Полный путь к ресурсу</returns>
		public static string FullPathAction(this UrlHelper urlHelper, string actionName)
		{
			return FullPathAction(urlHelper, actionName, null, null);
		}

		/// <summary>
		/// Converts the provided app-relative path into an absolute Url containing the full host name
		/// </summary>
		/// <param name="relativeUrl">App-Relative path</param>
		/// <returns>Provided relativeUrl parameter as fully qualified Url</returns>
		/// <example>~/path/to/foo to http://www.web.com/path/to/foo</example>
		public static string ToAbsoluteUrl(this string relativeUrl)
		{
			if (string.IsNullOrEmpty(relativeUrl))
				return relativeUrl;

			if (HttpContext.Current == null)
				return relativeUrl;

			if (relativeUrl.StartsWith("/"))
				relativeUrl = relativeUrl.Insert(0, "~");
			if (!relativeUrl.StartsWith("~/"))
				relativeUrl = relativeUrl.Insert(0, "~/");

			var url = HttpContext.Current.Request.Url;
			var port = url.Port != 80 ? (":" + url.Port) : String.Empty;

			return String.Format("{0}://{1}{2}{3}", url.Scheme, url.Host, port, VirtualPathUtility.ToAbsolute(relativeUrl));
		}
	}
}
