using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace Kesco.Web.Mvc.SharedViews.App_Start
{
	/// <summary>
	/// Статический класс для инициализации оптимизации скриптов и файлов разметок.
	/// </summary>
    public static class BundleConfig
    {

		/// <summary>
		/// Регистрирует набор скпритов и css файлов.
		/// </summary>
		/// <param name="bundles">The bundles.</param>
		public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new StyleBundle("~/css/Scripts/Kesco.V4/CSS/kesco.confirm").Include("~/styles/css/Scripts/Kesco.V4/CSS/*"));
            bundles.Add(new StyleBundle("~/css/jquery.ui").Include("~/styles/css/jquery-ui.css"));
			bundles.Add(new StyleBundle("~/css/jquery.ui/themes/humanity").Include("~/styles/css/themes/ui-humanity/jquery-ui.css"));
			bundles.Add(new StyleBundle("~/css/jquery.ui/themes/lightness").Include("~/styles/css/themes/ui-lightness/jquery-ui.css"));
			bundles.Add(new StyleBundle("~/css/jquery.ui/themes/redmond").Include("~/styles/css/themes/ui-redmond/jquery-ui.css"));
			bundles.Add(new StyleBundle("~/css/jquery.ui/themes/sunny").Include("~/styles/css/themes/sunny/jquery-ui.css"));

			bundles.Add(new StyleBundle("~/css/plugins")
				.Include(
						"~/styles/css/jquery-ui.css",
						"~/styles/css/jquery-ui-jqgrid.css",
						"~/styles/css/jquery.qtip.min.css",
						"~/styles/css/jquery.tooltip.css",
						"~/styles/css/jquery-ui-chosen.css",
						"~/styles/css/jquery-ui-selectmenu.css",
						"~/styles/css/jquery-ui-jqgrid.css",
                        "~/styles/css/kesco-ui.css"
					)
				);
            
			bundles.Add(new ScriptBundle("~/scripts/globalize").Include(
					"~/styles/scripts/globalize.js",
					"~/styles/scripts/cultures/globalize.culture.*"
				));

			bundles.Add(new ScriptBundle("~/scripts/knockout").Include(
					"~/styles/scripts/knockout.js",
					"~/styles/scripts/knockout.mapping.js"
				));

			bundles.Add(new ScriptBundle("~/scripts/jquery").Include(
					"~/styles/scripts/knockout.js",
					"~/styles/scripts/jquery.js",
					"~/styles/scripts/knockout.bindingHandlers.js",
					"~/styles/scripts/jquery-ui.js",
					"~/styles/scripts/json2.min.js",
					"~/styles/scripts/jquery.busy.min.js",
					"~/styles/scripts/jquery-ui-i18n.min.js",
					"~/styles/scripts/jquery.validate.min.js",
					"~/styles/scripts/jquery.validate.unobtrusive.min.js"
				));

			bundles.Add(new ScriptBundle("~/scripts/jqueryPlugins").Include(
					"~/styles/scripts/jquery.mousewheel.min.js",
					"~/styles/scripts/jquery.ui.selectmenu.min.js",
					"~/styles/scripts/jquery.ui.chosen.min.js",
					"~/styles/scripts/jquery.datalink.js",
					"~/styles/scripts/jquery.cookie.js",
					"~/styles/scripts/jquery.hotkeys.min.js",
					"~/styles/scripts/jquery.jstree.js",
					"~/styles/scripts/jquery.form.js",
					"~/styles/scripts/jquery.qtip.min.js",
					"~/styles/scripts/jquery.tooltip.js",
					"~/styles/scripts/jquery.meio.mask.min.js",
					"~/styles/scripts/jquery-ui-timepicker-addon.js"
				));

			bundles.Add(new ScriptBundle("~/scripts/jqGrid").Include(
					"~/styles/scripts/jquery.jqGrid.src.js",
					"~/styles/scripts/i18n/grid.locale-*"
				));

			bundles.Add(new ScriptBundle("~/scripts/silverlight").Include(
				"~/styles/scripts/silverlight.js"
			));

			bundles.Add(new ScriptBundle("~/scripts/kesco").Include(
					"~/styles/scripts/kesco.common.js",
					"~/styles/scripts/kesco.srv4js.js",
					"~/styles/scripts/kesco.select.js",
					"~/styles/scripts/kesco.decor.js",
					"~/styles/scripts/kesco.spinner.js",
					"~/styles/scripts/kesco.dialog.js",
					"~/styles/scripts/kesco.daterange.js",
					"~/styles/scripts/kesco.datalink.js",
                    "~/styles/scripts/Kesco.SetActualContactWindow.js"
				));

			BundleTable.EnableOptimizations = true;
		}

    }
}
