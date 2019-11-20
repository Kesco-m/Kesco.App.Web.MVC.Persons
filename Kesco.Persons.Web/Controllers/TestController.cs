using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kesco.Persons.Web.Models.Naturals;
using Kesco.Persons.Web.Models.Test;
using Kesco.Persons.BusinessLogic;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.SharedViews;

namespace Kesco.Persons.Web.Controllers
{
    public class TestController : SharedModelController<IndexViewModel>
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
			IndexViewModel model = new IndexViewModel();
            return View(model);
        }

        protected override bool DoDispatch(string command, string control, IndexViewModel model, out ActionResult result)
        {
            result = null;
            switch (command)
            {
                case "choosepersontypes":
                    result = ChoosePersonTypes(control, model);
                    return true;
                default:
                    return false;
            }
        }

        public ActionResult ChoosePersonTypes(string clientID, IndexViewModel model)
        {
            //model.DocumentInstance.PersonThemeID.Add("99");
            string script = String.Format(@"(function() {{
					var selected = $.cookie('personThemes');
					var callbackUrl = encodeURIComponent('{0}');
					var title = 'test--comment';
					var url;
					url = '{1}?{2}&selectedid=' + selected +'&clientId={3}';
					url = $.validator.format(url, callbackUrl, title);

					openPopupWindow(url, {{
							type: 'GET'
						}}, function (result) {{
							if ($.isArray(result)) {{
								var resultAr = [];

								for (var i = 0; i < result.length; i++) resultAr.push(result[i].value);
                                if (window.console) console.log(resultAr.join(','));
								window.ViewModel.Model.PersonThemes(resultAr.join(','));
								
								if (result.length > 0)
									window.ViewModel.checkPersonThemes();
                               if (result.length == 0){{
                                    window.ViewModel.Model.PersonTypes('');
				                    window.ViewModel.setPersonTypes(); }}
                                    
								
							}}
						}}, 'wnd_ChooseThemes', 800, 600);
				}})()",
                Url.FullPathAction("DialogResult", "Default"),
                Controls.Configuration.AppSettings.URI_theme_search,
                Controls.Configuration.URI_theme_search_QS,
                clientID
                );

            return JavaScript(script);
        }

    }
}
