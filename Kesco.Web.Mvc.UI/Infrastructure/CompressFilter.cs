using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Kesco.Web.Mvc
{
	/// <summary>
	/// http://stackoverflow.com/questions/855526/removing-extra-whitespace-from-generated-html-in-mvc
	/// </summary>
	public class CompressFilter : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			HttpRequestBase request = filterContext.HttpContext.Request;
			string acceptEncoding = request.Headers["Accept-Encoding"];
			if (string.IsNullOrEmpty(acceptEncoding)) return;

			var controller = filterContext.Controller as CorporateCultureController;

			if (controller == null || !controller.UseCompressHtml) 
				return;

			var response = filterContext.HttpContext.Response;

			if (controller.MimeTypesToCompress == null || controller.MimeTypesToCompress.Length == 0 
				|| controller.MimeTypesToCompress.Any(mime => response.ContentType.Contains(mime))) {

				acceptEncoding = acceptEncoding.ToUpperInvariant();

				if (acceptEncoding.Contains("GZIP")) {
					response.AppendHeader("Content-encoding", "gzip");
					response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
				} else if (acceptEncoding.Contains("DEFLATE")) {
					response.AppendHeader("Content-encoding", "deflate");
					response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
				}

			}
		}
	}
}