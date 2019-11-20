using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kesco.Web.Mvc
{
	public class JsonNetResult : ActionResult
	{
		public Encoding ContentEncoding { get; set; }
		public string ContentType { get; set; }
		public object Data { get; set; }

		public JsonSerializerSettings SerializerSettings { get; set; }
		public Formatting Formatting { get; set; }

		public JsonNetResult()
		{
			SerializerSettings = new JsonSerializerSettings();
			//IsoDateTimeConverter converter = new IsoDateTimeConverter();
			//converter.DateTimeStyles = System.Globalization.DateTimeStyles.AssumeUniversal;
			//converter.Culture = CultureInfo.GetCultureInfoByIetfLanguageTag("en-GB");
			//SerializerSettings.Converters.Add(new IsoDateTimeConverter());
			//SerializerSettings.Converters.Add(new KeyValuePairConverter());
		}

		public override void ExecuteResult(ControllerContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");

			HttpResponseBase response = context.HttpContext.Response;

			response.ContentType = !string.IsNullOrEmpty(ContentType)
			  ? ContentType
			  : "application/json";

			if (ContentEncoding != null)
				response.ContentEncoding = ContentEncoding;

			if (Data != null) {
				JsonTextWriter writer = new JsonTextWriter(response.Output) { Formatting = Formatting };

				JsonSerializer serializer = JsonSerializer.Create(SerializerSettings);
				serializer.Serialize(writer, Data);

				writer.Flush();
			}
		}
	}
}
