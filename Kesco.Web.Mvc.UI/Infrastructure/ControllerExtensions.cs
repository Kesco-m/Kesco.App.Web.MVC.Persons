using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;

namespace Kesco.Web.Mvc
{
	public static class ControllerExtensions
	{
		public static ImageResult Image(this Controller controller, 
			Image image, ImageFormat imageFormat, bool cacheable = false, 
			DateTime? lastModified = null, string eTag = null) 
		{
			return new ImageResult { 
				Image = image, 
				ImageFormat = imageFormat,
				Cacheable = cacheable,
				LastModified = lastModified,
				ETag = eTag
			};
		}
	}
}
