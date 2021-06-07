using System.Text.RegularExpressions;
using Kesco.Persons.BusinessLogic.Dossier;
using Kesco.Persons.ObjectModel;
using Kesco.Persons.BusinessLogic;
using Kesco.Web.Mvc;
using System.Web.Mvc;


namespace Kesco.Persons.Web.Models.Dossier
{
	public static class MenuItemExtensions
	{
		public static string GetURL(this UrlHelper urlHelper, PersonMenuItem menuItem, int ID)
		{
			string url = "", urlParams = menuItem.URLParameters;

			if (menuItem.FormURL != null && menuItem.FormURL != "aspect.aspx")
			{
				if (menuItem.FormURL.StartsWith("@"))
					url = System.Configuration.ConfigurationManager.AppSettings["URI_" + menuItem.FormURL.Replace("@", "")];
				else
					url = menuItem.FormURL.ToAbsoluteUrl();
				}
			else if( menuItem.ID == 9 || menuItem.ID == 10 )
			{
				Person person = Repository.Persons.GetInstance(ID);
				if(person.PersonType == Kesco.Persons.ObjectModel.PersonCardType.Juridical)
					url = urlHelper.FullPathAction("Index", "Requisites", new { id = 0, PersonID = person.ID, type = 1 });
				else
					url = urlHelper.FullPathAction("Index", "Natural", new { id = 0, PersonID = person.ID, type = 2, act = 1 });
			}
			else
			{
				Person person = Repository.Persons.GetInstance(ID);
				url = "";
			}

            if (string.IsNullOrEmpty(url)) return "";

			if (menuItem.URLParameters != null)
			{
				if (urlParams.IndexOf("@") != -1) urlParams = System.Configuration.ConfigurationManager.AppSettings[urlParams.Replace("@", "")];

				urlParams = Regex.Replace(urlParams, "@id", ID.ToString(), RegexOptions.IgnoreCase);

				if (!urlParams.StartsWith("?")) urlParams = "?" + urlParams;

				url += urlParams;
			}
			else if (url.IndexOf("?") == -1)
			{
				url += "?idclient=" + ID;
			}

			return url;
		}
	}
}