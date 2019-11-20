using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace Kesco.Web.Mvc
{
	/// <summary>
	/// Данный фильтр
	/// </summary>
	public class ClientContextRequestFilterAttribute : ActionFilterAttribute
	{
		/// <summary>
		/// Вызывается инфраструктурой MVC до выполнения метода действия.
		/// </summary>
		/// <param name="filterContext">Контекст фильтра.</param>
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			ControllerEx controller = filterContext.Controller as ControllerEx;
			if (controller != null) {
				string clientContextData = filterContext.HttpContext.Request["clientContext"];
				if (!String.IsNullOrEmpty(clientContextData)) {
					controller.ClientContext = Json.Deserialize<dynamic>(clientContextData);
					Debug.WriteLine("Клиентский контекст: {0}", clientContextData);
				} 
			}
		}
	}
}
