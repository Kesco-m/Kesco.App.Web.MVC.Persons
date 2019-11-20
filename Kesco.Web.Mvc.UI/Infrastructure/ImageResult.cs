using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Net.Mime;

namespace Kesco.Web.Mvc
{
	/// <summary>
	/// Инкапсулирует результат метода действия и возвращает картинку в качестве результата.
	/// </summary>
	public class ImageResult : ActionResult
	{
		public ImageResult() { }

		/// <summary>
		/// Gets or sets the image.
		/// </summary>
		/// <value>
		/// The image.
		/// </value>
		public Image Image { get; set; }
		/// <summary>
		/// Gets or sets the image format.
		/// </summary>
		/// <value>
		/// The image format.
		/// </value>
		public ImageFormat ImageFormat { get; set; }

		public bool Cacheable { get; set; }

		public DateTime? LastModified { get; set; }

		/// <summary>
		/// Gets or sets the ETag (http://en.wikipedia.org/wiki/HTTP_ETag).
		/// </summary>
		/// <value>
		/// The ETag.
		/// </value>
		public string ETag { get; set; }

		/// <summary>
		/// Разрешает обработку результата выполнения метода действия пользовательским типом, наследуемым от класса <see cref="T:System.Web.Mvc.ActionResult" />.
		/// </summary>
		/// <param name="context">Контекст, в котором выполняется результат. Сведения о контексте включают информацию о контроллере, HTTP-содержимом, контексте запроса и данных маршрута.</param>
		/// <exception cref="System.ArgumentNullException">Image</exception>
		public override void ExecuteResult(ControllerContext context)
		{
			// verify properties 
			if (Image == null) {
				throw new ArgumentNullException("Image");
			}
			if (ImageFormat == null) {
				throw new ArgumentNullException("ImageFormat");
			}
			var response = context.HttpContext.Response;
			// output 
			response.Clear();

			if (Cacheable) {
				response.Cache.SetCacheability(HttpCacheability.ServerAndPrivate);
				//response.Cache.SetExpires(DateTime.Now.AddDays(1));
				if (!String.IsNullOrEmpty(ETag))
					response.Cache.SetETag(ETag);
				else if (LastModified.HasValue)
					response.Cache.SetLastModified(LastModified.Value);
			} else response.Cache.SetCacheability(HttpCacheability.NoCache);

			if (ImageFormat.Equals(ImageFormat.Bmp)) context.HttpContext.Response.ContentType = "image/bmp";
			if (ImageFormat.Equals(ImageFormat.Gif)) context.HttpContext.Response.ContentType = MediaTypeNames.Image.Gif;
			if (ImageFormat.Equals(ImageFormat.Icon)) context.HttpContext.Response.ContentType = "image/vnd.microsoft.icon";
			if (ImageFormat.Equals(ImageFormat.Jpeg)) context.HttpContext.Response.ContentType = MediaTypeNames.Image.Jpeg;
			if (ImageFormat.Equals(ImageFormat.Png)) context.HttpContext.Response.ContentType = "image/png";
			if (ImageFormat.Equals(ImageFormat.Tiff)) context.HttpContext.Response.ContentType = MediaTypeNames.Image.Tiff;
			if (ImageFormat.Equals(ImageFormat.Wmf)) context.HttpContext.Response.ContentType = "image/wmf";
			Image.Save(context.HttpContext.Response.OutputStream, ImageFormat);
		}
	}
}
