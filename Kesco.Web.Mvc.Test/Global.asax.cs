using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.SharedViews;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Kesco.Web.Mvc.Test.Configuration), "Init")]

namespace Kesco.Web.Mvc.Test
{

	public abstract class SiteSettings : Kesco.Web.Mvc.SharedViews.SharedApplicationSettings
	{
		public abstract string URI_resource_info { get; set; }
	}

	public class Configuration : Configuration<SiteSettings> {
		public static void Init() { Configuration<SiteSettings>.Init(); }
	}

	public abstract class SiteViewPage<TSettings, TModel> : SharedViewPage<TSettings, TModel> where TSettings : SiteSettings { }
	public abstract class SiteViewPage<TModel> : SiteViewPage<SiteSettings, TModel> { }
	public abstract class SiteViewPage : SiteViewPage<dynamic> { }

	public class MvcApplication : Kesco.Web.Mvc.SharedViews.SharedApplication<SiteSettings>
	{
	}

}