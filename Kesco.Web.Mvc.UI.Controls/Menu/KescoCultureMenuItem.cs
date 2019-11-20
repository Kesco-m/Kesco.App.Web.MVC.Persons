using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Kesco.Web.Mvc.UI
{
	public class KescoCultureMenuItem : KescoMenuItem
	{

		public KescoCultureMenuItem(string culture, string clientChangeCultureFuncName)
			: base()
		{
			Caption = Controls.Localization.Resources.GUI_Menu_View_Culture;
			CreateMenuItems(culture, clientChangeCultureFuncName);
		}

		public KescoCultureMenuItem(string culture) : this(culture, null) { }

		public KescoCultureMenuItem() : this(CultureInfo.CurrentUICulture.Name, null) { }

		public void CreateMenuItems(string culture, string clientChangeCultureFuncName)
		{
			EnsureMenuCreated();
			Menu.Clear();
			clientChangeCultureFuncName = clientChangeCultureFuncName ?? "changeCulture";
			this.AddMenuItem("English (United States)", (cmi) => {
				cmi.CallbackFunc = "changeCulture('en-US')";
				cmi.PrimaryIcon = "memu-icon flags-us";
				cmi.IsCurrent = culture == "en-US";
			});
			this.AddMenuItem("Русский (Россия)", (cmi) => {
				cmi.CallbackFunc = "changeCulture('ru-RU')";
				cmi.PrimaryIcon = "memu-icon flags-ru";
				cmi.IsCurrent = culture == "ru-RU";
			});
		}

	}

}
