using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Kesco.Web.Mvc
{
	/// <summary>
	/// Данный фильтр
	/// </summary>
	public class AjaxRequestFilterAttribute : ActionFilterAttribute
	{
		/// <summary>
		/// Вызывается инфраструктурой MVC до выполнения метода действия.
		/// </summary>
		/// <param name="filterContext">Контекст фильтра.</param>
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			// Verify if a XMLHttpRequest is fired.  
			// This can be done by checking the X-Requested-With  
			// HTTP header.  
			ControllerEx controller = filterContext.Controller as ControllerEx;
			if (controller != null) {
				string ajaxRequestHeader = filterContext.HttpContext.Request.Headers["X-Requested-With"];
				if (!String.IsNullOrEmpty(ajaxRequestHeader) && ajaxRequestHeader == "XMLHttpRequest") {
					controller.IsAjaxRequest = true;
				} else {
					controller.IsAjaxRequest = false;
				}
			}
		}
	}
}
