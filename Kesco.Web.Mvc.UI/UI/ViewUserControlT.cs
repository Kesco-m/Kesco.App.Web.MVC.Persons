using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc
{
	public class ViewUserControl<TSettings> : ViewUserControl<TSettings, dynamic>
		where TSettings : ApplicationSettings
	{	}

	public class ViewUserControl<TSettings, TModel> : System.Web.Mvc.ViewUserControl<TModel>
		where TSettings : ApplicationSettings
	{

		public string GetUserThemeFolder()
		{
			string theme = Session.GetUserContext().Theme;
			theme = theme ?? "classic";
			return Configuration<TSettings>.Themes[theme];
		}

		public string WebAssetScript(string filePath)
		{
			return ResolveUrl(AppStyles.URI_Styles_Scripts + filePath);
		}

		public string WebAssetCssStyle(string filePath)
		{
			return ResolveUrl(AppStyles.URI_Styles_Css + filePath);
		}

		public string WebAssetImage(string filePath)
		{
			return ResolveUrl(AppStyles.URI_Styles + filePath);
		}

	}
}
