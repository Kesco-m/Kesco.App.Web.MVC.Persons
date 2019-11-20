using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.SharedViews;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Configuration<Kesco.Persons.Controls.SiteSettings>), "Init")]

namespace Kesco.Persons.Controls
{

    public abstract class SiteSettings : Kesco.Web.Mvc.SharedViews.SharedApplicationSettings
    {
        public abstract string URI_person_jp_add { get; set; }
        public abstract string URI_person_np_add { get; set; }
        public abstract string URI_theme_search { get; protected set; }
        public abstract string URI_contacts { get; protected set; }
    }

    public class Configuration : Configuration<SiteSettings>
    {
        public const string URI_theme_search_QS = "clid=8&return=2&mvc=1&callbackKey=c1&callbackUrl={0}&title={1}";
        public static string URI_theme_search = System.Configuration.ConfigurationManager.AppSettings["URI_theme_search"];
        public static string URI_contacts = System.Configuration.ConfigurationManager.AppSettings["URI_contacts"];
        public static string URI_person_jp_add = System.Configuration.ConfigurationManager.AppSettings["URI_person_jp_add"];
        public static string URI_person_np_add = System.Configuration.ConfigurationManager.AppSettings["URI_person_np_add"];
    }

    public abstract class SiteViewPage<TSettings, TModel> : SharedViewPage<TSettings, TModel> where TSettings : SiteSettings { }
    public abstract class SiteViewPage<TModel> : SiteViewPage<SiteSettings, TModel> { }
    public abstract class SiteViewPage : SiteViewPage<dynamic> { }

    public class MvcApplication : Kesco.Web.Mvc.SharedViews.SharedApplication<SiteSettings>
    {
    }

}