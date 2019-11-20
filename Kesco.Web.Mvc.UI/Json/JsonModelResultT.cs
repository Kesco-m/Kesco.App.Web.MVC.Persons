using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc
{
	/// <summary>
	///     Представляет класс, используемый для отправки модели представления 
	///     содержимого в формате JSON.
	/// </summary>
	/// <typeparam name="TModel">Тип модели.</typeparam>
	public class JsonModelResult<TModel> : JsonResultEx
		where TModel : class
	{
		class SuccessResultData
		{
			public string status = "ok";
			public TModel model { get; set; }
		}

		public JsonModelResult()
			: base()
		{
			jsonResult.Data = new SuccessResultData { status = "ok", model = null };
		}

		public JsonModelResult(TModel model)
			: this()
		{
			Model = model;
		}

		public TModel Model
		{
			get { return ((SuccessResultData)jsonResult.Data).model as TModel; }
			set { ((SuccessResultData)jsonResult.Data).model = value; }
		}
	}

}
