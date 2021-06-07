using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Kesco.Web.Mvc;
using Kesco.Lib.Log;
using Kesco.Persons.BusinessLogic;

namespace Kesco.Persons.Web.Controllers
{
    public class AssetsController : Controller
    {
        //
        // GET: /Assets/

		public ActionResult Logo(int? id, int? phId, int? w, int? h)
		{

			try {
				if (id.HasValue || phId.HasValue) {

					var photo = Repository.Logotypes.GetPersonLogotype(id, phId);

					if (photo == null)
						return Redirect(AppStyles.URI_Styles + "AlfNoPhoto.jpg");

					var eTag = String.Format("logo-{0}-{1}-{2}",
							phId.HasValue ? "phid" : "pid",
							phId.HasValue ? phId.Value : id.Value,
							photo.ChangedDate.Value.Ticks
						);

					if (w.HasValue) eTag += "-w-" + w.Value.ToString();

					if (Request.Headers["If-None-Match"] != null && Request.Headers["If-None-Match"] == eTag) {
						Response.StatusCode = 304;
						Response.StatusDescription = "Not Modified";
						return new EmptyResult();
					}

					using (MemoryStream ms = new MemoryStream(photo.Логотип)) {
						Image img;
						int width, height;
						img = Image.FromStream(ms);
						width = img.Width;
						height = img.Height;

						if (h.HasValue) {
							height = h.Value;
							img = new Bitmap(img, (height * width) / img.Height, height);
						} else {
							if (w.HasValue) width = w.Value;
							img = new Bitmap(img, width, (width * img.Height) / img.Width);
						}

						return this.Image(img, ImageFormat.Gif,
								cacheable: true, lastModified: photo.ChangedDate, eTag: eTag
							);
					}

				} else {
					return Redirect(AppStyles.URI_Styles + "AlfNoPhoto.jpg");
				}
			} catch (Exception ex) {
				Logger.WriteEx(new DetailedException("Возникла ошибка во время получения логотипа сотрудника.", ex));
				return Redirect(AppStyles.URI_Styles + "AlfNoPhoto.jpg");
			}
		}

    }
}
