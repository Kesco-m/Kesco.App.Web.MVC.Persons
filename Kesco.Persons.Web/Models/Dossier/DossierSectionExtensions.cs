using System.Text.RegularExpressions;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.ObjectModel;
using System.Security.Policy;
using System.Web;
using System;
using Kesco.Web.Mvc;

namespace Kesco.Persons.Web.Models.Dossier
{
	public static class DossierSectionExtensions
	{
		public static string GetURL(this Kesco.Persons.BusinessLogic.DataAccess.PersonAccessor.PersonDossierSection section, int PersonID)
		{
			string url = "", urlParams = section.URLParameters;

			if (section.FormURL != null)
			{
				if (section.FormURL.StartsWith("@"))
					url = System.Configuration.ConfigurationManager.AppSettings["URI_" + section.FormURL.Replace("@", "")];
				else
					url = section.FormURL.ToAbsoluteUrl();
				}
			else
			{
				Person person = Repository.Persons.GetInstance(PersonID);
				url = "";
                urlParams = "idClient=" + PersonID + "&type=" + person.PersonType;
            }

            if (string.IsNullOrEmpty(url)) return "";

			if (section.URLParameters != null)
			{
				if (urlParams.IndexOf("@") != -1) urlParams = System.Configuration.ConfigurationManager.AppSettings[urlParams.Replace("@", "")];
                
				urlParams = Regex.Replace(urlParams, "@id", PersonID.ToString(), RegexOptions.IgnoreCase);
			}
			else
			{
				urlParams = "idclient=" + PersonID;
			}

			if (!urlParams.StartsWith("?")) urlParams = "?" + urlParams;
			url += urlParams;

			// убираем Id-ники чтобы подставить их в конец
			url = Regex.Replace( Regex.Replace(url, "[?]id=\\d*(&|$)", "?", RegexOptions.IgnoreCase), "&id=\\d*(&|$)", "&", RegexOptions.IgnoreCase);

			url = url.Replace("Счет", HttpUtility.UrlEncode("Счет"));
			url = url.Replace("Предоставление банковских услуг", HttpUtility.UrlEncode("Предоставление банковских услуг"));
			url = url.Replace("Склад", HttpUtility.UrlEncode("Склад"));
			url = url.Replace("Предоставление услуг хранения", HttpUtility.UrlEncode("Предоставление услуг хранения"));

			return url;
		}
	}
}