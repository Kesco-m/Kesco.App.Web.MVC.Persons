using System;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.SharedViews;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Configuration<Kesco.Territories.Controls.SiteSettings>), "Init")]

namespace Kesco.Territories.Controls
{
	public abstract class SiteSettings : Kesco.Web.Mvc.SharedViews.SharedApplicationSettings {
		public abstract string URI_area_search { get; protected set; }
		public abstract string URI_area_form { get; protected set; }
	}
	 
	public class Configuration : Configuration<SiteSettings> { }

	public abstract class SiteViewPage<TSettings, TModel> : SharedViewPage<TSettings, TModel> where TSettings : SiteSettings { }
	public abstract class SiteViewPage<TModel> : SiteViewPage<SiteSettings, TModel> { }
	public abstract class SiteViewPage : SiteViewPage<dynamic> { }

	public class MvcApplication : Kesco.Web.Mvc.SharedViews.SharedApplication<SiteSettings>
	{

	}
}