using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Kesco.Employees.Controls.DataAccess;
using Kesco.Employees.ObjectModel;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.UI.Infrastructure;
using System.Linq;
using Kesco.Employees.Controls.Localization;
using Kesco.Employees.BusinessLogic;
using System.Text.RegularExpressions;
using System.IO;

namespace Kesco.Employees.Controls.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	public class EmployeeSelectController : KescoSelectBaseController<EmployeeSelectAccessor, EmployeePartial, EmployeeSelectAccessor.SearchParameters, int>
	{
		public EmployeeSelectController()
			: base()
		{
			UseCompressHtml = true;
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
		/// Gets the advanced search URL.
		/// </summary>
		/// <param name="clid">The clid.</param>
		/// <param name="parameters">The parameters.</param>
		/// <returns></returns>
		protected override string GetAdvancedSearchUrl(EmployeeSelectAccessor.SearchParameters parameters)
		{
			return Configuration.AppSettings.URI_user_search
				+ String.Format("?return=1&clid={0}&mvc=1&control=c&callbackKey=c1&callbackUrl={{0}}&search={{1}}", parameters.CLID);
		}

		/// <summary>
		/// Gets the details URL.
		/// </summary>
		/// <returns></returns>
		protected override string GetDetailsUrl()
		{
			return Configuration.AppSettings.URI_user_form;
		}

		/// <summary>
		/// Adjusts the search parameters.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		protected override void AdjustSearchParameters(EmployeeSelectAccessor.SearchParameters parameters)
		{
			base.AdjustSearchParameters(parameters);
			if (parameters != null) {
				parameters.MaxEntries = 9;
			}
		}

		/// <summary>
		/// Gets the entry label.
		/// </summary>
		/// <param name="entry">The entry.</param>
		/// <returns></returns>
		protected override string GetEntryLabel(EmployeePartial entry)
		{
			return entry.GetInstanceFriendlyName();
		}

        public string Proxy(string url)
        {
            var rq = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            rq.Method = "GET";

            var authHeader = HttpContext.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                rq.Headers.Add("Authorization", authHeader);
            }
            else
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
		/// Возвращает HTML-представление об сотруднике
		/// </summary>
		/// <param name="id">Код сотрудника</param>
		/// <returns>HTML-представление об сотруднике</returns>
		public virtual string EmployeeInfo(int id)
		{
             var sb = new System.Text.StringBuilder();

            sb.Append(Configuration.URI_contacts);            
            sb.Append($"?cid={id}");
            sb.Append("&ctype=2");
            //sb.Append("&jq=1");

            try
            {
                string[] computerName = null;
                var hostName = Request.ServerVariables["remote_addr"];

                if (string.IsNullOrEmpty(hostName))
                {
                    hostName = Request.UserHostName;
                    if (string.IsNullOrEmpty(hostName))
                        sb.Append($"&computerName=");
                    else
                        computerName = hostName.Split(new Char[] { '.' }); ;
                }
                else
                    computerName = System.Net.Dns.GetHostEntry(hostName).HostName.Split(new Char[] { '.' });

                if (computerName != null && computerName.Length > 0)
                    sb.Append($"&computerName={computerName[0]}");

            }
            catch {
                sb.Append($"&computerName=");
            }
                       
            //string url = String.Format(@"{0}?lang={3}&id={1}&callerType=2&computerName={2}#", Configuration.URI_contacts, id, computerName[0], System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.ToLower());

            var url = sb.ToString();

            string result = Proxy(url);
            string htmlResult = new Regex("<!--CSSBLOCK.+([^;])+ENDCSSBLOCK-->").Replace(result, "");
            htmlResult += String.Format(@"<script>$('a.phoneLink').click(function(event) {{ViewModel.showPhoneList($(this)[0].href);event.preventDefault();}});</script>");
            return htmlResult;
            //string[] computer_name = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.Split(new Char[] { '.' });
            //return Redirect(String.Format(@"{0}?lang=ru&personId={1}&computerName={2}#", Configuration.URI_contacts, id, computer_name[0]));
            //string viewName = "EmployeeInfo";
            //if (IsAjaxRequest)
            //{
            //    viewName += "Ajax";
            //}
            //return View(viewName, new Models.EmployeeInfo.ViewModel(id));
		}

		/// <summary>
		/// Возвращает HTML-представление об сотруднике
		/// </summary>
		/// <param name="id">Код сотрудника</param>
		/// <returns>HTML-представление об сотруднике</returns>
		public virtual ActionResult DossierEmployeeInfo(int id)
		{
			string viewName = "DossierEmployeeInfo";
			return View(viewName, new Models.Dossier.EmployeeInfo.ViewModel(id));
		}

		public class EmployeeWorkplaceForSave
		{
			public int ID { get; set; }
			public int WorkPlaceID { get; set; }
		}

        public class ListReplacmentIds
        {
            public string ids { get; set; }
        }

		public virtual ActionResult SaveEmployeeWorkplace(EmployeeWorkplaceForSave model)
		{

            Kesco.Employees.BusinessLogic.Repository.Employees
                   .SaveEmployeeWorkPlaces(model.ID, model.WorkPlaceID);
            return Json("success", JsonRequestBehavior.AllowGet);
            
		}

        public virtual ActionResult DeleteAllReplacement(ListReplacmentIds model)
        {
            if (model != null && !String.IsNullOrEmpty(model.ids))
            {
                string[] ids = model.ids.Split(',');
                foreach(var item in ids)
                {
                    if (!String.IsNullOrEmpty(item))
                    {
                        Kesco.Employees.BusinessLogic.Repository.Employees.DeleteAllEmployeeReplacements(item);
                    }
                        
                }
            }
                

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult OpenReplace(string id)
        {
            string script = String.Format(@"
				(function() {{
                    	var url = '{0}';
			            var value = url.substring(0, url.lastIndexOf('/') + 1);
			            value += 'userFilling.aspx?id={1}';
			            DialogPageOpen(value, 'dialogHeight:250px;dialogWidth:570px;center:yes;status:no;help:no;resizable:yes;', function(){{ window.location.href = location.href;}});
				}})();",
             Configuration.AppSettings.URI_user_form,
            id
            );
            return JavaScript(script);
        }

        public virtual ActionResult AddReplace(string uid)
        {
            string script = String.Format(@"
				(function() {{
                    	var url = '{0}';
			            var value = url.substring(0, url.lastIndexOf('/') + 1);
			            value += 'userFilling.aspx?uid={1}';
			            DialogPageOpen(value, 'dialogHeight:250px;dialogWidth:570px;center:yes;status:no;help:no;resizable:yes;', function(){{ window.location.href = location.href;}});
				}})();",
             Configuration.AppSettings.URI_user_form,
            uid
            );
            return JavaScript(script);
        }

        public virtual ActionResult CheckCoworkersAndCommonEmployees(EmployeeWorkplaceForSave model)
        {
            string msg = "";
            int? oldCoworkerID = null;
            int? newCoworkerID = null;

            var employee = Repository.Employees.GetInstance(model.ID);
            var coworkers = Kesco.Employees.BusinessLogic.Repository.Employees.GetEmployeeCoWorkersByOfficeWorkPlace(model.ID, model.WorkPlaceID);

            //Текущий общий сотрудник
            oldCoworkerID = employee.CommonEmployeeID;
            //Новый общий сотрудник
            newCoworkerID = coworkers.Where(c => c.Status == 0 && c.PersonID == null).Select(k => k.CoWorkerID).FirstOrDefault();

            if (oldCoworkerID == 0) oldCoworkerID = null;
            if (newCoworkerID == 0) newCoworkerID = null;

            if (employee.Status == 0 && employee.PersonID == null)
            {
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            else
            {
                //Ошибка
                if (oldCoworkerID != null && newCoworkerID != null && oldCoworkerID != newCoworkerID)
                {
                    var commonEmployee = Repository.Employees.GetInstance(Convert.ToInt32(employee.CommonEmployeeID));
                    msg = String.Format(@"{0} является членом группы {1}, но вы назначаете его на рабочее место группы {2}. Сотрудник не может одновременно быть членом двух групп посменной работы.", employee.LastNameWithInitials, commonEmployee.LastNameWithInitials, coworkers.Where(m => m.PersonID == null).Select(k => k.CoWorker).FirstOrDefault());
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return Content(msg, System.Net.Mime.MediaTypeNames.Text.Plain);    
                }
                //Новое место в группе
                else if (newCoworkerID != null && oldCoworkerID != newCoworkerID)
                {
                    msg = String.Format(@"Вы назначаете {0} на рабочее место группы {1}. Вы подтверждаете, что {0} является членом группы {1}?", employee.LastNameWithInitials, coworkers.Where(m => m.PersonID == null).Select(k => k.CoWorker).FirstOrDefault());
                }
                //Новое место в совместной работе
                else if (coworkers.Any(m => m.PersonID != null && m.WorkPlace == 1))
                {
                    msg = String.Format("Вы назначаете {0} на рабочее место, на котором посменно работают {1}. Вы подтверждаете, что {0} работает посменно {1}?", employee.LastNameWithInitials, String.Join(" ,", coworkers.Where(m => m.PersonID != null).Select(t => t.CoWorker)));
                }
            }

            if (!String.IsNullOrEmpty(msg))
            {
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            return Json("true", JsonRequestBehavior.AllowGet);
        }

//        private void Save(string idLoc)
//        {
//            DataTable dtLoc = new DataTable();
//            Kesco.Env.Corporate.Find(String.Format("SELECT * FROM vwРасположения WHERE КодРасположения = {0}", idLoc.ToString()), dtLoc);

//            if (dtLoc.Rows.Count == 0) {
//                string err = "Указанное расположение в базе данных отсутствует";
//                Response.Write(String.Format("<script language='javascript'>alert(\"{0}\");</script>", err));

//                return;
//            }

//            string sql = @"IF NOT EXISTS(SELECT * FROM РабочиеМеста WHERE КодСотрудника={0} AND КодРасположения={1})
//				INSERT INTO РабочиеМеста (КодСотрудника, КодРасположения) VALUES ({0},{1})";

//            sql = string.Format(sql, id, idLoc);

//            SqlConnection cn = new SqlConnection(Global.DS_user);
//            SqlCommand cm = new SqlCommand(sql, cn);

//            try {
//                cn.Open();
//                cm.ExecuteNonQuery();
//            } catch (SqlException sex) {
//                Kesco.Logger.WriteEx(new Kesco.Log.DetailedException(sex.Message, sex, cm));
//                string err = sex.Message.Replace("'", "").Replace("\"", "").Replace("\r\n", "");
//                Response.Write("<script language='javascript'>alert(\"" + err + "\");</script>");
//            } catch (Exception ex) {
//                Kesco.Logger.WriteEx(new DetailedException(ex.Message, ex));
//                string err = ex.Message.Replace("'", "").Replace("\"", "").Replace("\r\n", "");
//                Response.Write("<script language='javascript'>alert(\"" + err + "\");</script>");
//            } finally {
//                cn.Close();
//            }

//            DataTable dt = new DataTable();
//            SqlDataAdapter da = new SqlDataAdapter("SELECT КодОборудования, ТипОборудования, МодельОборудования, ФИО FROM vwОборудованиеСписок WHERE КодРасположения=" + idLoc + " AND (КодСотрудника IS NULL OR КодСотрудника<>" + id + ")", Global.DS_user);
//            da.Fill(dt);

//            if (dt.Rows.Count > 0)
//                Response.Write("<script language='javascript'>var x=0; if(x==0) {x=1;window.showModalDialog('LocationType.aspx?loc1=&loc2=" + idLoc + "&user=" + id + "','_blank', 'dialogHeight: 600px; dialogWidth: 650px;resizable: Yes;status: No;help: No;');}</script>");
//        }


	}
}