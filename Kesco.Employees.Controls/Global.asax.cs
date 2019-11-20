using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.SharedViews;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Configuration<Kesco.Employees.Controls.SiteSettings>), "Init")]

namespace Kesco.Employees.Controls
{
	public abstract class SiteSettings : Kesco.Web.Mvc.SharedViews.SharedApplicationSettings
	{
		public abstract string URI_user_search_QS { get; protected set; }
		public abstract string URI_user_extFields { get; protected set; }

        public abstract string URI_store_person { get; protected set; }
		public abstract string URI_user_photos { get; protected set; }
		public abstract string URI_user_workplace { get; protected set; }
		public abstract string URI_location_search { get; protected set; }
		public abstract string URI_role_form { get; protected set; }

        public abstract string URI_contacts { get; protected set; }
	}

	public class Configuration : Configuration<SiteSettings> {
        public static string URI_contacts = System.Configuration.ConfigurationManager.AppSettings["URI_contacts"];
    }

	public abstract class SiteViewPage<TSettings, TModel> : SharedViewPage<TSettings, TModel> where TSettings : SiteSettings { }
	public abstract class SiteViewPage<TModel> : SiteViewPage<SiteSettings, TModel> { }
	public abstract class SiteViewPage : SiteViewPage<dynamic> { }

	public class MvcApplication : Kesco.Web.Mvc.SharedViews.SharedApplication<SiteSettings>
	{

	}
}