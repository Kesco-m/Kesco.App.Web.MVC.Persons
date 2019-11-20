using Kesco.Web.Mvc;
using Kesco.Web.Mvc.SharedViews;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Configuration<Kesco.Persons.Web.SiteSettings>), "Init")]

namespace Kesco.Persons.Web {

	public class Configuration : Configuration<SiteSettings> {
		public const string URI_user_search_QS = "clid=1&return=2&mvc=1&UserOur=true&UserStaffMembers=true&callbackKey=c1&callbackUrl={0}&title={1}";
		public const string URI_theme_search_QS = "clid=8&return=2&mvc=1&callbackKey=c1&callbackUrl={0}&title={1}";
	}

	public abstract class SiteSettings : Kesco.Web.Mvc.SharedViews.SharedApplicationSettings {
		public static string RolesForBProject = "71,72,73,74,75,76";

		public abstract string URI_theme_search { get; protected set; }

		public abstract string URI_area_search { get; protected set; }
		public abstract string URI_area_form { get; protected set; }
		public abstract string URI_bank_search { get; protected set; }
		public abstract string URI_bproject_search { get; protected set; }
		public abstract string URI_person_synchronize { get; protected set; }

		public abstract string URI_person_catalogs { get; protected set; }
		public abstract string URI_person_search_old { get; protected set; }

		public abstract string URI_store_search { get; protected set; }
		public abstract string URI_store_form { get; protected set; }

		public abstract string URI_user_extFields { get; protected set; }

        public abstract string URI_store_person { get; protected set; }

		public abstract string URI_user_location_form { get; protected set; }
		public abstract string URI_location_search { get; protected set; }
        
	}

	public abstract class SiteViewPage<TSettings, TModel> : SharedViewPage<TSettings, TModel> where TSettings : SiteSettings { }
	public abstract class SiteViewPage<TModel> : SiteViewPage<SiteSettings, TModel> { }
	public abstract class SiteViewPage : SiteViewPage<dynamic> { }

	public class MvcApplication : Kesco.Web.Mvc.SharedViews.SharedApplication<SiteSettings> {
		public static string DS_person = System.Configuration.ConfigurationManager.AppSettings["DS_person"];
		public static string DS_doc = System.Configuration.ConfigurationManager.AppSettings["DS_doc"];
	    
	    //public static string URI_area_search = System.Configuration.ConfigurationManager.AppSettings["URI_area_search"];

	}
}