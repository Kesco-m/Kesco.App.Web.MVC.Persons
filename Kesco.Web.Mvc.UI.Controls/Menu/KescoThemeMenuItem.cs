using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Kesco.Web.Mvc.UI
{

	using Localization;

	public class KescoThemeMenuItem : KescoMenuItem
	{

		public KescoThemeMenuItem(string theme, string clientChangeThemeFuncName)
			: base()
		{
			Caption = Kesco.Web.Mvc.UI.Controls.Localization.Resources.GUI_Menu_View_Theme;
			CreateMenuItems(theme, clientChangeThemeFuncName);
		}

		public KescoThemeMenuItem(string theme) : this(theme, null) { }

		public void CreateMenuItems(string theme, string clientChangeThemeFuncName)
		{
			EnsureMenuCreated();
			Menu.Clear();
			clientChangeThemeFuncName = clientChangeThemeFuncName ?? "changeTheme";

			this.AddMenuItem(Kesco.Web.Mvc.UI.Controls.Localization.Resources.GUI_Menu_View_Theme_Default, (cmi) => {
				cmi.CallbackFunc = String.Format("{0}('{1}')", "changeTheme", "oldstyle");
				cmi.IsCurrent = theme == "oldstyle";
			});
			this.AddMenuItem(Kesco.Web.Mvc.UI.Controls.Localization.Resources.GUI_Menu_View_Theme_Lightness, (cmi) => {
				cmi.CallbackFunc = String.Format("{0}('{1}')", "changeTheme", "lightness"); 
				cmi.IsCurrent = theme == "lightness";
			});
			this.AddMenuItem(Kesco.Web.Mvc.UI.Controls.Localization.Resources.GUI_Menu_View_Theme_Redmond, (cmi) => {
				cmi.CallbackFunc = String.Format("{0}('{1}')", "changeTheme", "redmond");
				cmi.IsCurrent = theme == "redmond";
			});
	
		}

	}

}
