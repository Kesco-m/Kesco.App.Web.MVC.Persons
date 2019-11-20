using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc
{
	/// <summary>
	///     Представляет класс, используемый для отправки об ошибке в формате JSON.
	/// </summary>
	public class JsonErrorResult : JsonResultEx
	{
		class ErrorResultData
		{
			public string status = "ok";
			public string error { get; set; }
			public object error_details { get; set; }
		}

		public JsonErrorResult()
			: base()
		{
			jsonResult.Data = new ErrorResultData { status = "error", error = null, error_details = null };
		}

		public string Error
		{
			get { return ((ErrorResultData)jsonResult.Data).error; }
			set { ((ErrorResultData)jsonResult.Data).error = value; }
		}

		public object ErrorDetails
		{
			get { return ((ErrorResultData)jsonResult.Data).error_details; }
			set { ((ErrorResultData)jsonResult.Data).error_details = value; }
		}

	}
}
