using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Collections;

namespace Kesco.Web.Mvc
{
	/// <summary>
	/// Конфигурация для ETag
	/// </summary>
	public static class ETagConfig
	{
		/// <summary>
		/// Возвращает или устанавливает строку штампа времени (ticks).
		/// </summary>
		/// <value>
		/// Штамп времени (ticks). Устанавливается обработчиком Application_Start
		/// </value>
		public static string Timestamp { get; set; }

		/// <summary>
		/// Возвращает или уставливает дополнительный пользовательский тэг.
		/// </summary>
		/// <value>
		/// Дополнительный пользовательский тэг.
		/// (На усмотрение разработчика)
		/// </value>
		public static string CustomTag { get; set; }

	}

	/// <summary>
	/// Определяет флаги зависимостей, на основе которых 
	/// следует строить значение ETag
	/// </summary>
	[Flags]
	public enum ETagDependencies: int {

		/// <summary>
		/// Флаг, указывающий, что контент зависит
		/// от изменения конфигурации приложения
		/// </summary>
		ConfigurationDependency		= 1,
		
		/// <summary>
		/// Флаг, указывающий, что контент зависит
		/// от времени запуска/перезапуска приложения.
		/// </summary>
		ApplicationStartDependency	= 2,

		/// <summary>
		/// Флаг, указывающий, что контент зависит
		/// от изменения текущего языка культуры пользователя
		/// </summary>
		CultureDependency			= 4,

		/// <summary>
		/// Флаг, указывающий, что контент зависит
		/// от изменения версии сборки, в которой 
		/// определён контроллер
		/// </summary>
		AssemblyDependency			= 64,

		/// <summary>
		/// Флаг, указывающий, что контент зависит
		/// от изменения параметра конфигурации ETagConfig.CustomTag
		/// </summary>
		CustomDependency			= 128,

		/// <summary>
		/// Флаг, указывающий, что контент зависит
		/// от любой определённой для ETag зависимости
		/// </summary>
		AllDependecies				= CustomDependency | AssemblyDependency
			| ConfigurationDependency | ApplicationStartDependency | CultureDependency
	}

	/// <summary>
	/// Фильтр для проверки на изменения условно-статического содержимого,
	/// динамически генерируемого на стороне сервера.
	/// Например, скрипт, в котором есть строки из ресурсов или 
	/// настройки из конфигурационного файла.
	/// </summary>
	public class ETagBasedCacheFilterAttribute : ActionFilterAttribute
	{

		/// <summary>
		/// Содержит etag-часть для зависимости от сборки
		/// </summary>
		private string assemblyDependencyETag;

		/// <summary>
		/// Содержит etag-часть для зависимости от конфигурации
		/// </summary>
		private string сonfigurationDependencyETag;

		/// <summary>
		/// Возвращает или устанавливает тип содержимого HTTP-ответа.
		/// </summary>
		/// <value>
		/// Тип содержимого HTTP-ответа
		/// </value>
		public string ContentType { get; set; }

		/// <summary>
		/// Возвращает или устанавливает зависимости содержимого, 
		/// при изменении которых содержимое необходимо обновить
		/// на стороне клиента.
		/// </summary>
		/// <value>
		/// Зависимости содержимого
		/// </value>
		public ETagDependencies Dependencies { get; set; }

		#region Конструкторы

		/// <summary>
		/// Initializes a new instance of the <see cref="ETagBasedCacheFilterAttribute" /> class.
		/// </summary>
		/// <param name="contentType">Type of the content.</param>
		/// <param name="dependencies">The dependencies.</param>
		public ETagBasedCacheFilterAttribute(string contentType, ETagDependencies dependencies)
			: base()
		{
			ContentType = contentType ?? "text/plain";
			Dependencies = dependencies;
		}

		public ETagBasedCacheFilterAttribute()
			: this(null, ETagDependencies.AllDependecies) {}

		public ETagBasedCacheFilterAttribute(string contentType)
			: this(null, ETagDependencies.AllDependecies) {}

		public ETagBasedCacheFilterAttribute(ETagDependencies dependencies)
			: this(null, dependencies) { }

		
		#endregion

		/// <summary>
		/// Builds the assembly dependency.
		/// </summary>
		/// <param name="filterContext">The filter context.</param>
		/// <returns></returns>
		protected virtual string BuildAssemblyDependencyPart(ActionExecutingContext filterContext)
		{

            if (String.IsNullOrWhiteSpace(assemblyDependencyETag)) {
                var v = filterContext.Controller.GetType().Assembly.GetName(false).Version;
                assemblyDependencyETag = "A"+System.Configuration.ConfigurationManager.AppSettings["URI_styles_cache"];// string.Format("A{0}{1}{2}{3}", v.Major, v.Minor, v.Build, v.Revision);
			}
			return assemblyDependencyETag;
		}

		/// <summary>
		/// Builds the assembly dependency.
		/// </summary>
		/// <param name="filterContext">The filter context.</param>
		/// <returns></returns>
		protected virtual string BuildConfigurationDependencyPart(ActionExecutingContext filterContext)
		{

			if (String.IsNullOrWhiteSpace(сonfigurationDependencyETag)) {
				var v = filterContext.Controller.GetType().Assembly.GetName(false).Version;
				сonfigurationDependencyETag = String.Format("G{0}/{1}/{2}",
						System.Configuration.ConfigurationManager.AppSettings["configLevel1_Generation"] ?? "1",
						System.Configuration.ConfigurationManager.AppSettings["configLevel2_Generation"] ?? "2",
						System.Configuration.ConfigurationManager.AppSettings["configLevel3_Generation"] ?? "3"
					);
			}
			return сonfigurationDependencyETag;
		}

		/// <summary>
		/// Строит строку ETag, основываясь на установленных зависимостях
		/// </summary>
		/// <returns></returns>
		protected virtual string BuildETag(ActionExecutingContext filterContext) {
			List<string> keys = new List<string>();
			
			if ((Dependencies & ETagDependencies.ConfigurationDependency) == ETagDependencies.ConfigurationDependency) {
				keys.Add(BuildConfigurationDependencyPart(filterContext));
			}

			if ((Dependencies & ETagDependencies.AssemblyDependency) == ETagDependencies.AssemblyDependency) {
				keys.Add(BuildAssemblyDependencyPart(filterContext));
			}

			if ((Dependencies & ETagDependencies.ApplicationStartDependency) == ETagDependencies.ApplicationStartDependency) {
				keys.Add(String.Format("S{0}", ETagConfig.Timestamp ?? String.Empty));
			}

			if ((Dependencies & ETagDependencies.CultureDependency) == ETagDependencies.CultureDependency) {
				keys.Add(String.Format("C{0}", System.Threading.Thread.CurrentThread.CurrentCulture.IetfLanguageTag));
			}

			if ((Dependencies & ETagDependencies.CustomDependency) == ETagDependencies.CustomDependency) {
				keys.Add(String.Format("CUS{0}", ETagConfig.CustomTag ?? String.Empty));
			}
			return String.Join(":", keys);
		}

		/// <summary>
		/// Вызывается инфраструктурой MVC до выполнения метода действия.
		/// </summary>
		/// <param name="filterContext">Контекст фильтра.</param>
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			filterContext.HttpContext.Trace.Write("(ETagBasedCache Filter) Action Executing: " +
				filterContext.ActionDescriptor.ActionName);

			var request = filterContext.HttpContext.Request;
			var response = filterContext.HttpContext.Response;

			// eTag основан на времени последнего запуска приложения 
			// и текущем языке культуры
			string eTag = BuildETag(filterContext);

			// проверяем заголовок If-None-Match (он содержит eTag от клиента), 
			// если указан и совпадает с текущим etag, то считаем, что
			// контент не менялся. меняется сборка <=> меняется контент.
			if (!String.IsNullOrEmpty(request.Headers["If-None-Match"])
				&& request.Headers["If-None-Match"] == eTag) {
				response.StatusCode = 304;
				response.StatusDescription = "Not Modified";
				filterContext.Result = new EmptyResult();
			} else {
				if (String.IsNullOrEmpty(ContentType))
					ContentType = "text/plain";

				response.ContentType = ContentType;
				response.Cache.SetCacheability(HttpCacheability.ServerAndPrivate);
				response.Cache.SetETag(eTag);

				base.OnActionExecuting(filterContext);
			}
		}
	}
}
