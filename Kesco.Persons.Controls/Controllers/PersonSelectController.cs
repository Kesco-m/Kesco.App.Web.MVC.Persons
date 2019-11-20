using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using BLToolkit.Reflection;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.ObjectModel;
using Kesco.Persons.Controls.ComponentModel;
using Kesco.Persons.Controls.DataAccess;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.SharedViews.ComponentModel;
using Kesco.Web.Mvc.UI;
using Kesco.Web.Mvc.UI.ComponentModel.DataAnnotations;
using Kesco.Web.Mvc.UI.Infrastructure;
using System.Net;

namespace Kesco.Persons.Controls.Controllers
{
	/// <summary>
	/// Реализует контроллер для элемента управления "Выбор лица"
	/// </summary>
	public partial class PersonSelectController : KescoSelectBaseController<PersonSelectAccessor, Person, PersonSelectAccessor.SearchParameters, int>
	{
		static string[] mimeTypesToCompress = new string[] { "text/html", "text/javascript", "text/plain" };

		public PersonSelectController() : base() {
			UseCompressHtml = true;
			MimeTypesToCompress = mimeTypesToCompress;
		}


		/// <summary>
		/// Gets the corporate culture settings.
		/// </summary>
		/// <returns></returns>
		protected override CultureSettings GetCorporateCultureSettings()
		{
			return Configuration.AppSettings.Culture;
		}

		/// <summary>
		/// Возвращает URL-адрес для расширенного поиска лица
		/// </summary>
		/// <param name="clid">Идентификатор клиента.</param>
		/// <returns>URL-адрес для расширенного поиска лица</returns>
		protected override string GetAdvancedSearchUrl(PersonSelectAccessor.SearchParameters parameters)
		{
			return Configuration.AppSettings.URI_person_search
				+ String.Format("?return=1&clid={0}&mvc=1&control=c&callbackKey=c1&callbackUrl={{0}}&search={{1}}", parameters.CLID);
		}

		/// <summary>
		/// Возвращает URL-адрес для просмотра досье на лицо
		/// </summary>
		/// <param name="clid">Идентификатор клиента.</param>
		/// <returns>URL-адрес для просмотра досье на лицо</returns>
		protected override string GetDetailsUrl()
		{
			return Configuration.AppSettings.URI_person_form;
		}

		/// <summary>
		/// Adjusts the search parameters.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		protected override void AdjustSearchParameters(PersonSelectAccessor.SearchParameters parameters)
		{
			base.AdjustSearchParameters(parameters);
			if (parameters != null)
			{
				parameters.PersonWhereSearch = 1;
				parameters.PersonSelectTop = 9;
			}
		}

		/// <summary>
		/// Выполняет диспетчеризацию команд.
		/// </summary>
		/// <param name="command">Команда</param>
		/// <param name="control">Идентификатор элемента управления на стороне клиента</param>
		/// <param name="clid">Идентификатор клиента.</param>
		/// <param name="id">Идентификатор сущности</param>
		/// <param name="parameters">критерии поиска.</param>
		/// <returns></returns>
		public override ActionResult Dispatch(string command, string control, int mode, int? id, PersonSelectAccessor.SearchParameters parameters)
		{
			try
			{
				switch (command.ToLower())
				{
					case "createjuridical":
						return CreateJuridical(control);
					case "createnatural":
						return CreateNatural(control);
				}
				return base.Dispatch(command, control, mode, id, parameters);
			}
			catch (Exception ex)
			{
				Kesco.Logging.Logger.WriteEx(ex);
				return JavaScriptAlert(
						Kesco.Localization.Resources.Ajax_Alert_Title_ApplicationError,
						ex.Message
					);
			}
		}

		/// <summary>
		/// Creates the juridical.
		/// </summary>
		/// <param name="control">The control.</param>
		/// <returns></returns>
		public virtual ActionResult CreateJuridical(string control)
		{
			string script = String.Format(@"(function() {{ // closure/замыкание
				    var $lookup = $('#{0}');
				    var item = $lookup.selectBox('getValue');
				    if (window.console) console.log('{0}:CreateJuridical', item);
					openPopupWindow('{1}?text='+encodeURIComponent(item.label), null, null, 'wnd_PersonCreateJuridical_{0}', 800, 600);
				}})();
                ",
				 control,
				 Configuration.AppSettings.URI_person_jp_add
			);
			return JavaScript(script);
		}

		/// <summary>
		/// Creates the natural.
		/// </summary>
		/// <param name="control">The control.</param>
		/// <returns></returns>
		public virtual ActionResult CreateNatural(string control)
		{
			string script = String.Format(@"(function() {{ // closure/замыкание
				    var $lookup = $('#{0}');
				    var item = $lookup.selectBox('getValue');
				    if (window.console) console.log('{0}:CreateNatural', item);
					openPopupWindow('{1}?text='+encodeURIComponent(item.label), null, null, 'wnd_PersonCreateNatural_{0}', 800, 600);
				}})();
                ",
				 control,
				 Configuration.AppSettings.URI_person_np_add
			);
			return JavaScript(script);
		}

		/// <summary>
		/// Возвращает наименованине для лица
		/// </summary>
		/// <param name="entry">Лицо.</param>
		/// <returns></returns>
		protected override string GetEntryLabel(Person entry)
		{
			return entry.Nickname;
		}

        /// <summary>
        /// Возвращает данные для тултипа лица
        /// </summary>
        /// <param name="url">Адрес.</param>
        /// <returns></returns>
        public string Proxy(string url)
        {
            var rq = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            rq.Method = "GET";
            rq.Credentials = System.Net.CredentialCache.DefaultCredentials;
            System.Net.WebResponse rs = rq.GetResponse();
            System.IO.Stream stream = rs.GetResponseStream();
            if (stream != null)
            {
                var readStream = new StreamReader(stream, System.Text.Encoding.UTF8);
                string s = readStream.ReadToEnd();
                return s;
            }
            return "";
        }

        /// <summary>
		/// Возвращает HTML-представление об лице
		/// </summary>
		/// <param name="id">Код лица</param>
		/// <returns>HTML-представление об лица</returns>
		public virtual string PersonInfo(int id)
        {
            
            string[] computerName = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.Split(new Char[] { '.' });
            string url = String.Format(@"{0}?lang={3}&id={1}&computerName={2}#", Configuration.URI_contacts, id, computerName[0], System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.ToLower());
            string result = Proxy(url);
            string htmlResult = new Regex("<!--CSSBLOCK.+([^;])+ENDCSSBLOCK-->").Replace(result, "");
            htmlResult += String.Format(@"<script>$('a.phoneLink').click(function(event) {{ViewModel.showPhoneList($(this)[0].href);event.preventDefault();}});</script>");
            return htmlResult;
        }
	}
}
