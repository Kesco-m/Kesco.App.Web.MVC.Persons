using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kesco;
using Kesco.Web.Mvc;

namespace Kesco.Persons.Controls
{
	/// <summary>
	/// Html helpers для элементов управления
	/// </summary>
	public static class HtmlHelperExtensions
	{
		const string personControlsEnvironment = @"
						!(function() {{ 
							var env = window.Env || {{}}; window.Env = env; env.URI_person_info = env.URI_person_info || '{0}'; 
						}})();
					";

		public static void RegisterPersonControlsEnvironment(this HtmlHelper htmlHelper)
		{
			htmlHelper.RegisterCommonScriptCode("PersonControlsEnvironment", () => personControlsEnvironment.FormatWith(
							Kesco.Persons.Controls.Configuration.AppSettings.URI_person_info
						)
				);
		}
	}
}