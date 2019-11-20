using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Reflection;
using System.Globalization;
using System.Threading;

namespace Kesco.Web.Mvc.SharedViews.Controllers
{
	/// <summary>
	/// Контроллер, действия которого возвращают кешируемые скрипты.
	/// </summary>
	public class ScriptController : CorporateCultureController
    {

		public ScriptController()
			: base()
		{
			UseCompressHtml = true;
			MimeTypesToCompress = new string[] { "text/javascript", "text/html" };
		}

		/// <summary>
		/// Возвращает скрипт, который определяет переменные окружения с локализованными строками для приложения.
		/// </summary>
		/// <param name="hash">The hash.</param>
		/// <returns>скрипт, который определяет переменные окружения с локализованными строками для приложения</returns>
		[ETagBasedCacheFilter("text/javascript", 
			ETagDependencies.ConfigurationDependency 
			| ETagDependencies.AssemblyDependency 
			| ETagDependencies.CultureDependency)]
		public ActionResult AppEnv(string hash)
		{
			return View("AppEnv");
		}

		/// <summary>
		/// Возвращает общий скрипт, который используется всеми приложениями.
		/// </summary>
		/// <param name="hash">The hash.</param>
		/// <returns>общий скрипт, который используется всеми приложениями</returns>
		[ETagBasedCacheFilter("text/javascript", ETagDependencies.AssemblyDependency)]
		public ActionResult AppCommon(string hash)
        {
			return View();
        }

		/// <summary>
		/// Возвращает скрипт, который обслуживает кнопку выбора темы приложения.
		/// </summary>
		/// <param name="hash">The hash.</param>
		/// <returns>Скрипт, который обслуживает кнопку выбора темы приложения</returns>
		[ETagBasedCacheFilter("text/javascript", ETagDependencies.AssemblyDependency)]
		public ActionResult AppThemeRoller(string hash)
		{
			return View();
		}

		/// <summary>
		/// Возвращает настройки корпоративной культуры.
		/// </summary>
		/// <returns>
		/// Настройки корпоративной культуры
		/// </returns>
		protected override CultureSettings GetCorporateCultureSettings()
		{
			return Configuration.AppSettings.Culture;
		}


    }
}
