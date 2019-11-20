using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Mvc;
using Kesco.Web.Mvc.SharedViews.Localization;
using Kesco.Web.Mvc.SharedViews.Models;

using System.Web;
using Kesco.Web.Mvc.Infrastructure;
using FluentValidation;
using Kesco.Web.Mvc.SharedViews.Models.Test;
using Kesco.DataAccess;
using System.Data;

namespace Kesco.Web.Mvc.SharedViews.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	public class DefaultController : SharedValidationSupportedController<Models.Test.DataModel>
	{

		/// <summary>
		/// Indexes this instance.
		/// </summary>
		/// <returns></returns>
		public ActionResult ZvonilkaScript()
		{
			Response.Clear();
			Response.ContentType = "text/javascript";
			Response.Cache.SetAllowResponseInBrowserHistory(true);
			Response.Cache.SetExpires(DateTime.UtcNow.AddDays(365));
			Response.Cache.SetValidUntilExpires(true);
			Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
			Response.Cache.SetCacheability(HttpCacheability.Private);
			
			
			return View();
		}

		/// <summary>
		/// Возвращает настройки культуры для приложения.
		/// </summary>
		/// <returns>Настройки культуры для приложения</returns>
		protected override CultureSettings GetCorporateCultureSettings()
		{
			return Configuration.AppSettings.Culture;
		}

		/// <summary>
		/// Indexes this instance.
		/// </summary>
		/// <returns></returns>
		public ActionResult Index()
		{
			IndexViewModel model = new IndexViewModel();
			return View(model);
		}

		/// <summary>
		/// Edits this instance.
		/// </summary>
		/// <returns></returns>
		public ActionResult Edit()
		{
			IndexViewModel model = new IndexViewModel();
			return View(model);
		}

		/// <summary>
		/// Выполняет диспетчеризацию команд.
		/// </summary>
		/// <param name="command">Команда</param>
		/// <param name="control">Идентификатор элемента управления на стороне клиента</param>
		/// <param name="model">Модель</param>
		/// <param name="result">Результат действия, если команда обработана, иначе null.</param>
		/// <returns>
		/// Возвращает истину, если команда обработана, иначе false
		/// </returns>
		protected override bool DoDispatch(string command, string control, Models.Test.DataModel model, out ActionResult result)
		{
			result = null;
			switch (command) {
				case "save":
					result = Save(model);
					break;	
			}
			return base.DoDispatch(command, control, model, out result);
		}

		/// <summary>
		/// Saves the specified dummy.
		/// </summary>
		/// <param name="dummy">The dummy.</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Save(Models.Test.DataModel dummy)
		{
			try {
				// Проверяем состояние модели (валидность данных)
				if (ModelState.IsValid) {
					/*dummy.ChangedBy = UserContext.EmployeeInfo.ID;
					dummy.ChangedDate = DateTime.Now;
					Session["Dummy"] = dummy;*/
					return JsonModel(dummy, JsonRequestBehavior.AllowGet);
				} else {
					return JsonError("Ошибка в переданных данных", GetModelErrors(), JsonRequestBehavior.AllowGet);
				}
			} catch (Exception ex) {
				Kesco.Logging.Logger.WriteEx(ex);
				return JsonError("Ошибка сохранения документа"
						, new object[] { 
							new { ErrorMessage = ex.Message} }
						, JsonRequestBehavior.AllowGet);
			}
		}
        

		/// <summary>
		/// Makes the call.
		/// </summary>
		/// <param name="clientName">Name of the client.</param>
		/// <returns></returns>
		public ActionResult GetAvailablePhones(string clientName)
		{
			List<AvailablePhone> phones = TapiClient.GetClientPhoneNumbers(clientName);
			return JsonModel(new {
				phones = phones.Where(p => p.Outcoming)
			}, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// Makes the call.
		/// </summary>
		/// <param name="phone">The phone.</param>
		/// <param name="clientName">Name of the client.</param>
		/// <param name="targetPhone">The target phone.</param>
		/// <returns></returns>
        //public ActionResult MakeCall(string phone, int equipmentID, string clientName, string targetPhone)
        //{
        //    try {
        //        targetPhone = (targetPhone ?? "").Trim();
        //        //Regex regex = new Regex(@"[^\d ]+", RegexOptions.IgnoreCase | RegexOptions.ECMAScript | RegexOptions.CultureInvariant);
        //        //targetPhone = regex.Replace(targetPhone, @"");
        //        if (String.IsNullOrEmpty(targetPhone))
        //            throw new Exception(Kesco.Web.Mvc.SharedViews.Localization.Resources.MakeCall_PhoneNotSpecified);

        //        List<AvailablePhone> phones = TapiClient.GetClientPhoneNumbers(clientName);
        //        var phoneEntry = phones.FirstOrDefault(p =>
        //                p.PhoneNumber == phone
        //                && p.EquipmentID == equipmentID
        //                && p.Outcoming
        //            );
        //        if (phoneEntry == null) {
        //            throw new Exception(String.Format(Kesco.Web.Mvc.SharedViews.Localization.Resources.MakeCall_PhoneNotFound, phone));
        //        }
        //        int callID = 0;

        //        // если контакт начинается с +, то это международный номер
        //        // получим номер набора, 
        //        if (targetPhone.StartsWith("+")) {
        //            string[] dialDigits = targetPhone.Split(new char[] { ' ' });
        //            dialDigits[0] = TapiClient.GetPhoneNumberDialDigits(dialDigits[0].Substring(1), phoneEntry.PhoneStationCode);
        //            targetPhone = String.Join(" ", dialDigits);
        //        }

        //        if (phoneEntry.PhoneStationCode == 0) {
        //            callID = Kesco.Tasks.RunTask(() => {
        //                return Kesco.Zvonilka.Zvonilka.MakeIpCall(
        //                    phoneEntry.CTI,
        //                    phoneEntry.PhoneNumber,
        //                    phoneEntry.NetName,
        //                    targetPhone);
        //            }, TimeSpan.FromSeconds(30));
        //        } else {
        //            callID = Kesco.Tasks.RunTask(() => {
        //                return Kesco.Zvonilka.Zvonilka.MakeDigitalCall(phoneEntry.CTI
        //                    , phoneEntry.PhoneNumber
        //                    , targetPhone, !String.IsNullOrEmpty(phoneEntry.Slave));
        //            }, TimeSpan.FromSeconds(30));
        //        }
        //        return JsonModel(new {
        //            established = callID > 0,
        //            callInfo = new {
        //                CallID = callID,
        //                CalledPhone = targetPhone,
        //                CallingPhone = (String.IsNullOrEmpty(phoneEntry.Slave)) ? phoneEntry.PhoneNumber : phoneEntry.Slave,
        //                ConnectionState = -1
        //            },
        //            message = String.Format(Resources.MakeCall_Call, phone, targetPhone)
        //        }, JsonRequestBehavior.AllowGet);
        //    } catch (Exception ex) {
        //        Kesco.Logging.Logger.WriteEx(ex);
        //        return JsonError(ex.Message, ex.ToString(), JsonRequestBehavior.AllowGet);
        //    }

        //}

		/// <summary>
		/// Checks the call.
		/// </summary>
		/// <param name="callID">The call ID.</param>
		/// <param name="phone">The phone.</param>
		/// <param name="clientName">Name of the client.</param>
		/// <param name="targetPhone">The target phone.</param>
		/// <returns></returns>
        //public ActionResult CheckCall(int callID, string phone, int equipmentID, string clientName, string targetPhone)
        //{

        //    try {
        //        bool active = false;
        //        string msg = String.Empty;
        //        List<AvailablePhone> phones = TapiClient.GetClientPhoneNumbers(clientName);

        //        var phoneEntry = phones.FirstOrDefault(p =>
        //                p.PhoneNumber == phone
        //                && p.EquipmentID == equipmentID
        //                && p.Outcoming
        //            );
        //        if (phoneEntry == null) {
        //            throw new Exception(String.Format(Kesco.Web.Mvc.SharedViews.Localization.Resources.MakeCall_PhoneNotFound, phone));
        //        }

        //        Kesco.Zvonilka.CallState state;
        //        if (phoneEntry.PhoneStationCode == 0) {
        //            state = Kesco.Tasks.RunTask(() => {
        //                return Kesco.Zvonilka.Zvonilka.GetIPCallState(callID);
        //            }, TimeSpan.FromSeconds(30));
        //            active = state.ID > 0 && state.State >= 0;
        //            msg = GetCallStateDescription(state.State);
        //        } else {
        //            state = Kesco.Tasks.RunTask(() => {
        //                return Kesco.Zvonilka.Zvonilka.GetDigitalCallState(callID);
        //            }, TimeSpan.FromSeconds(30));
        //            active = state.ID > 0 && state.State >= 0;
        //            msg = GetCallStateDescription(state.State);
        //        }
        //        return JsonModel(
        //            new {
        //                callInfo = new {
        //                    CallID = state.ID,
        //                    CalledPhone = state.AnsweredNumber,
        //                    CallingPhone = (String.IsNullOrEmpty(phoneEntry.Slave)) ? phoneEntry.PhoneNumber : phoneEntry.Slave,
        //                    ConnectionState = state.State
        //                },
        //                active = active,
        //                message = msg
        //            }, JsonRequestBehavior.AllowGet);
        //    } catch (Exception ex) {
        //        Kesco.Logging.Logger.WriteEx(ex);
        //        return JsonError(ex.Message, ex.ToString(), JsonRequestBehavior.AllowGet);
        //    }
        //}

		/// <summary>
		/// Cancel the call.
		/// </summary>
		/// <param name="callID">The call ID.</param>
		/// <param name="phone">The phone.</param>
		/// <param name="clientName">Name of the client.</param>
		/// <param name="targetPhone">The target phone.</param>
		/// <returns></returns>
        //public ActionResult CancelCall(int callID, string phone, int equipmentID, string clientName, string targetPhone)
        //{
        //    AutoResetEvent are = new AutoResetEvent(false);
        //    try {
        //        string msg = String.Empty;
        //        List<AvailablePhone> phones = TapiClient.GetClientPhoneNumbers(clientName);

        //        var phoneEntry = phones.FirstOrDefault(p =>
        //                p.PhoneNumber == phone
        //                && p.EquipmentID == equipmentID
        //                && p.Outcoming
        //            );
        //        if (phoneEntry == null) {
        //            throw new Exception(String.Format(Kesco.Web.Mvc.SharedViews.Localization.Resources.MakeCall_PhoneNotFound, phone));
        //        }

        //        int fake = 0;
        //        if (phoneEntry.PhoneStationCode == 0) {
        //            fake = Kesco.Tasks.RunTask(() => {
        //                Kesco.Zvonilka.Zvonilka.CancelIPCall(callID);
        //                return 1;
        //            }, TimeSpan.FromSeconds(30));
        //        } else {
        //            fake = Kesco.Tasks.RunTask(() => {
        //                Kesco.Zvonilka.Zvonilka.CancelDigitalCall(callID);
        //                return 1;
        //            }, TimeSpan.FromSeconds(30));
        //        }
        //        return JsonModel(new { message = msg }, JsonRequestBehavior.AllowGet);
        //    } catch (Exception ex) {
        //        Kesco.Logging.Logger.WriteEx(ex);
        //        return JsonError(ex.Message, ex.ToString(), JsonRequestBehavior.AllowGet);
        //    }
        //}

		protected string GetCallStateDescription(int state)
		{
			string description = String.Empty;
			switch (state) {
				case -1:
					description = Resources.LINECALLSTATE_IDLE;
					break;
				case 0:
					description = Resources.LINECALLSTATE_DIALING;
					break;
				case 1:
					description = Resources.LINECALLSTATE_CONNECTED;
					break;
				default:
					description = state.ToString();
					break;
			}
			return description;
		}

		public ActionResult Test()
		{
			using (var db = new Database("DS_User"))
			{
				var idbCommand = db.Command;//Connection.CreateCommand();
				int result = 0;
				idbCommand.CommandTimeout = 0;
				idbCommand.CommandText = "[Инвентаризация].[dbo].[sp_ВыполнениеУказанийIT]";
				idbCommand.CommandType = CommandType.StoredProcedure;
				idbCommand.Parameters.Add(db.Parameter("@кодДокумента", 15403));
				var rez = db.Parameter("@RETURN_VALUE", result);
				rez.Direction = ParameterDirection.ReturnValue;
				idbCommand.Parameters.Add(rez);
				db.ExecuteScalar();
			}
			var model = new Models.Test.ViewModel();
			model.UpdateValidationState(ModelState, null);
			return View(model);
		}

		public ActionResult DumpAssemblies()
		{
			//Retrieve the loaded assemblies from the current AppDomain.
			var model = new Models.Dump.ViewModel();

			return View(model);
		}

		public ActionResult SearchEntity()
		{
			var model = new Models.SearchEntity.ViewModel();

			return View(model);
		}
		protected override ViewModel<Models.Test.DataModel> GetViewModel(Models.Test.DataModel dataModel)
		{
			var vm = new Models.Test.ViewModel();
			vm.SetDataModel(dataModel);
			return vm;
		}
	}
}
