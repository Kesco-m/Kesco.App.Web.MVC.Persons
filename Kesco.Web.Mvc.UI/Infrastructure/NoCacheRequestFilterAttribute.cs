using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace Kesco.Web.Mvc
{
	/// <summary>
	/// Данный фильтр предотвращает кэширование ответа для текущего HTTP-запроса
	/// </summary>
	public class NoCacheActionFilterAttribute : ActionFilterAttribute
	{
		/// <summary>
		/// Вызывается инфраструктурой MVC до выполнения результата действия.
		/// </summary>
		/// <param name="filterContext">Контекст фильтра.</param>
		public override void OnResultExecuting(ResultExecutingContext filterContext)
		{
			filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
			filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
			filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
			filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
			filterContext.HttpContext.Response.Cache.SetNoStore();

			base.OnResultExecuting(filterContext);
		}
	}
}
