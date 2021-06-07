using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kesco.Employees.BusinessLogic;
using Kesco.Web.Mvc;
using Kesco.Lib.Log;

namespace Kesco.Employees.Controls.Controllers
{
	/// <summary>
	/// 
	/// </summary>
    public class EmployeeAssetsController : Controller
    {

		/// <summary>
		/// Возвращает фотографию сотрудника.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <param name="phId">The ph id.</param>
		/// <param name="w">The w.</param>
		/// <param name="mini">The mini.</param>
		/// <returns>Фотографию сотрудника</returns>
		[HttpGet]
        public ActionResult Photo(int? id, int? phId, int? w, int? mini)
        {
			mini = mini ?? 0;

			try {
				if (id.HasValue || phId.HasValue) {

					if (id.HasValue)
						phId = null;
					else if (phId.HasValue && mini == 1) {
						id = phId; phId = null;
					}

					var photo = Repository.Employees.GetEmployeePhoto(id, phId, mini == 1);

					if (photo == null)
						return Redirect(AppStyles.URI_Styles + ((mini == 1) ? "Empty.jpg" : "AlfNoPhoto.jpg"));

					var eTag = String.Format("{0}-{1}-{2}-{3}",
							mini == 1 ? "thumb" : "img",
							id.HasValue ? "uid" : "pid",
							id.HasValue ? id.Value : phId.Value,
							photo.ChangedDate.Value.Ticks
						);

					if (w.HasValue) eTag += "-w-"+w.Value.ToString();

					if (Request.Headers["If-None-Match"] != null && Request.Headers["If-None-Match"] == eTag) {
						Response.StatusCode = 304;
						Response.StatusDescription = "Not Modified";
						return new EmptyResult();
					}

					using (MemoryStream ms = new MemoryStream(photo.Photo)) {
						Image img;
						int width;
						img = Image.FromStream(ms);
						width = img.Width;

						if (w.HasValue) width = w.Value;

						img = new Bitmap(img, width, (width * img.Height) / img.Width);

						return this.Image(img, ImageFormat.Jpeg, 
								cacheable: true, lastModified: photo.ChangedDate, eTag: eTag
							);
					}

				} else {
					return Redirect(AppStyles.URI_Styles + "AlfNoPhoto.jpg");
				}
			} catch (Exception ex) {
				Logger.WriteEx(new DetailedException("Возникла ошибка во время получения фотографии сотрудника.", ex));
				return Redirect(AppStyles.URI_Styles + "AlfNoPhoto.jpg");
			}
        }

    }
}
