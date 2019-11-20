using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Kesco.Web.Mvc
{
	/// <summary>
	/// Абстрактный класс, который инкапсулирует объект <see cref="JsonResult"/> типа.
	/// </summary>
	public abstract class JsonResultEx : ActionResult
	{

		//protected JsonResult jsonResult;
		protected JsonNetResult jsonResult;

		public Encoding ContentEncoding { 
			get { return jsonResult.ContentEncoding; }
			set { jsonResult.ContentEncoding = value; }
		}

		public string ContentType {
			get { return jsonResult.ContentType; }
			set { jsonResult.ContentType = value; }
		}

		/*public JsonRequestBehavior JsonRequestBehavior {
			get { return jsonResult.JsonRequestBehavior; }
			set { jsonResult.JsonRequestBehavior = value; }
		}
		*/
		protected JsonResultEx()
		{
			jsonResult = new JsonNetResult();
			jsonResult.Formatting = Formatting.Indented;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			jsonResult.ExecuteResult(context);
		}


	}


}
