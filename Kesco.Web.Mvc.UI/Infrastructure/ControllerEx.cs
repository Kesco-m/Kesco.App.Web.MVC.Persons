using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Kesco.ApplicationServices;
using Kesco.Lib.Log;
using System.Reflection;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kesco.Web.Mvc
{
	/// <summary>
	///     Класс расширяет класс <see cref="System.Web.Mvc.Controller">Controller</see>, 
	///     добавляя методы для отправки модели представления и ошибок в формате JSON.
	/// </summary>
	[NoCacheActionFilter]
	[AjaxRequestFilter]
	//[HandleError]
	[ClientContextRequestFilter]
	public class ControllerEx : CorporateCultureController
	{
		public dynamic ClientContext = null;

		public bool IsAjaxRequest { get; set; }

		/// <summary>
		/// Возвращает настройки корпоративной культуры.
		/// </summary>
		/// <returns>
		/// Настройки корпоративной культуры
		/// </returns>
		/// <exception cref="System.NotImplementedException"></exception>
		protected override CultureSettings GetCorporateCultureSettings()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the model dictionary.
		/// </summary>
		/// <returns></returns>
		public Dictionary<string, object> GetModelDictionary()
		{

			Dictionary<string, ModelMetadata> properties = BuildPropertyListEx(String.Empty);
			Dictionary<string, object> dictionary = new Dictionary<string, object>();

			properties.ToList().ForEach(pair => {
				dictionary.Add(pair.Key, pair.Value.Model);
			});

			return dictionary;
		}
		
		protected virtual void ExtractPropertyListEx(ModelMetadata metadata, Dictionary<string, ModelMetadata> properties, string prefix)
		{
			if (prefix == null)
				prefix = String.Empty;

			foreach (ModelMetadata prop in metadata.Properties.Where(pm => pm.ShowForEdit && !ViewData.TemplateInfo.Visited(pm))) {
				if (prop.IsComplexType) {
					ExtractPropertyListEx(prop, properties, prefix + prop.PropertyName + ".");
				} else {
					properties.Add(prefix + prop.PropertyName, prop);
				}
			}
		}

		protected Dictionary<string, ModelMetadata> BuildPropertyListEx(string prefix)
		{
			if (prefix == null)
				prefix = String.Empty;

			Dictionary<string, ModelMetadata> properties = new Dictionary<string, ModelMetadata>();

			if (ViewData.ModelMetadata.IsComplexType)
				ExtractPropertyListEx(ViewData.ModelMetadata, properties, prefix);
			else
				properties.Add(prefix + ViewData.ModelMetadata.PropertyName, ViewData.ModelMetadata);

			return properties;
		}

		#region JsonModel methods

		/// <summary>
		///     Создает объект <see cref="JsonModelResult"/>, который сериализует указанную модель,
		///     ожидаемую клиентом в нотацию объектов JavaScript (JSON).
		/// </summary>
		/// <typeparam name="TModel">Тип модели.</typeparam>
		/// <param name="model">Модель данных.</param>
		/// <param name="contentType">Тип содержимого (тип MIME).</param>
		/// <param name="contentEncoding">Кодировка содержимого.</param>
		/// <param name="behavior">Поведение JSON-запроса.</param>
		/// <returns>Результирующий объект JSON, который сериализует указанный объект в формате Json.</returns>
		protected virtual JsonModelResult<TModel> JsonModel<TModel>(TModel model, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior) 
			where TModel : class
		{
			JsonModelResult<TModel> result = new JsonModelResult<TModel>(model);
			result.ContentEncoding = contentEncoding;
			result.ContentType = contentType;
			//result.JsonRequestBehavior = behavior;
			return result;
		}

		protected JsonModelResult<TModel> JsonModel<TModel>(TModel model, string contentType, JsonRequestBehavior behavior)
			where TModel : class
		{
			return JsonModel<TModel>(model, contentType, null, behavior);
		}

		protected JsonModelResult<TModel> JsonModel<TModel>(TModel model, JsonRequestBehavior behavior)
			where TModel : class
		{
			return JsonModel<TModel>(model, null, null, behavior);
		}

		protected JsonModelResult<TModel> JsonModel<TModel>(TModel model, string contentType)
			where TModel : class
		{
			return JsonModel<TModel>(model, contentType, null, JsonRequestBehavior.DenyGet);
		}

		protected JsonModelResult<TModel> JsonModel<TModel>(TModel model, string contentType, Encoding contentEncoding)
			where TModel : class
		{
			return JsonModel<TModel>(model, contentType, contentEncoding, JsonRequestBehavior.DenyGet);
		}

		#endregion

		#region JsonError methods

		/// <summary>
		///     Создает объект <see cref="JsonModelResult"/>, который сериализует данные об ошибке,
		///     ожидаемые клиентом в нотацию объектов JavaScript (JSON).
		/// </summary>
		/// <param name="error">Сообщение об ошибке.</param>
		/// <param name="errorDetails">Объект, представляющий детали ошибки.</param>
		/// <param name="contentType">Тип содержимого (тип MIME).</param>
		/// <param name="contentEncoding">Кодировка содержимого.</param>
		/// <param name="behavior">Поведение JSON-запроса.</param>
		/// <returns>Результирующий объект JSON, который сериализует указанный объект в формате Json.</returns>
		protected virtual JsonErrorResult JsonError(string error, object errorDetails, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
		{
			JsonErrorResult result = new JsonErrorResult();
			result.Error = error;
			result.ErrorDetails = errorDetails;
			result.ContentEncoding = contentEncoding;
			result.ContentType = contentType;
			//result.JsonRequestBehavior = behavior;
			return result;
		}


		/// <summary>
		/// Jsons the error.
		/// </summary>
		/// <param name="error">The error.</param>
		/// <param name="errorDetails">The error details.</param>
		/// <param name="behavior">The behavior.</param>
		/// <returns></returns>
		protected JsonErrorResult JsonError(string error, object errorDetails, JsonRequestBehavior behavior)
		{
			return JsonError(error, errorDetails, null, null, behavior);
		}

		protected JsonErrorResult JsonError(string error, object errorDetails, string contentType, Encoding contentEncoding)
		{
			return JsonError(error, errorDetails, contentType, contentEncoding, JsonRequestBehavior.DenyGet);
		}

		protected JsonErrorResult JsonError(string error, object errorDetails, string contentType)
		{
			return JsonError(error, errorDetails, contentType, null, JsonRequestBehavior.DenyGet);
		}

		protected JsonErrorResult JsonError(string error, object errorDetails)
		{
			return JsonError(error, errorDetails, null, null, JsonRequestBehavior.DenyGet);
		}

		protected JsonErrorResult JsonError(string error, JsonRequestBehavior behavior)
		{
			return JsonError(error, null, null, null, behavior);
		}

		#endregion

		/// <summary>
		/// Saves the application setting.
		/// </summary>
		/// <param name="clid">The clid.</param>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public ActionResult SaveApplicationSetting(int? clid, string key, string value)
		{
			try {

				if (String.IsNullOrEmpty(key))
					throw new Exception("Параметр не задан"); 

				Kesco.ApplicationServices.Manager.SaveUserSetting(clid ?? 0, key, value);

				return JsonModel("saved", JsonRequestBehavior.AllowGet);

			} catch (Exception ex) {
				Kesco.Logging.Logger.WriteEx(ex);
				return JsonError(ex.Message, ex.ToString(), JsonRequestBehavior.AllowGet);
			}
		}

		/// <summary>
		/// Saves the application settings.
		/// </summary>
		/// <param name="clid">The clid.</param>
		/// <param name="settings">The settings.</param>
		/// <returns></returns>
		public ActionResult SaveApplicationSettings(int? clid, IDictionary<string, string> settings)
		{

			clid = clid ?? 0;

			try {

				if (settings != null) {
					foreach (KeyValuePair<string, string> setting in settings) {
						Kesco.ApplicationServices.Manager.SaveUserSetting(clid.Value, setting.Key, setting.Value);
					}
				}

				return JsonModel("saved", JsonRequestBehavior.AllowGet);

			} catch (Exception ex) {
				Kesco.Logging.Logger.WriteEx(ex);
				return JsonError(ex.Message, ex.ToString(), JsonRequestBehavior.AllowGet);
			}
		}

		/// <summary>
		/// Saves the application settings from values.
		/// </summary>
		/// <param name="clid">The clid.</param>
		/// <param name="values">The values.</param>
		/// <returns></returns>
		/// <exception cref="System.FormatException"></exception>
		public ActionResult SaveApplicationSettingsFromValues(int? clid, string values)
		{
			string _parameterPattern = "([^" + Regex.Escape("|") + "]{1,})=([^" + Regex.Escape("|") + "]{0,})";

			try {

				Regex r = new Regex(_parameterPattern);
				Match m = r.Match(values);

				if (!m.Success)
					throw new FormatException(String.Format(
							Kesco.Web.Mvc.Localization.Resources.ControllerEx_SaveApplicationSettingsFromValues, values
						));

				while(m.Success) {
					
					Kesco.ApplicationServices.Manager.SaveUserSetting(clid ?? 0, m.Groups[1].Value, m.Groups[2].Value);

					m = m.NextMatch();
				}

				return JsonModel("saved", JsonRequestBehavior.AllowGet);

			} catch (Exception ex) {
				Kesco.Logging.Logger.WriteEx(ex);
				return JsonError(ex.Message, ex.ToString(), JsonRequestBehavior.AllowGet);
			}
		}

		/// <summary>
		/// Registers the client error.
		/// </summary>
		/// <param name="details">The details.</param>
		/// <returns></returns>
		/// <exception cref="System.Exception">Ошибка клиентского скрипта</exception>
		public ActionResult RegisterClientError(string details)
		{
			try {
				throw new Exception("Ошибка клиентского скрипта");
			} catch(Exception ex) {
				Kesco.Logging.Logger.WriteEx(ex, details);
			}
			return JsonModel("Registered", JsonRequestBehavior.AllowGet);
		}

		public ActionResult RegisterSilverlightError(Priority type, string msg)
		{
			try
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = new SqlConnection("");
				throw new DetailedException(msg, null, cmd, type);
				
			}
			catch (Exception ex)
			{
				Kesco.Logging.Logger.WriteEx(ex, msg);
			}
			return JsonModel("Registered", JsonRequestBehavior.AllowGet);
		}

		public ActionResult ChangeTheme(string theme)
		{
			if (String.IsNullOrEmpty(theme)) {
				return JsonError("Тема не указана", JsonRequestBehavior.AllowGet);
			}
			
			IThemeable themable = HttpContext.ApplicationInstance as IThemeable;
			
			if (themable == null) {
				return JsonError("Приложение не поддерживает темы.", JsonRequestBehavior.AllowGet);
			}

			if (!themable.SetTheme(theme)) {
				return JsonError("Тема не найдена", JsonRequestBehavior.AllowGet);
			}

			UserContext.Theme = theme;

			IAppConfiguration config = HttpContext.ApplicationInstance as IAppConfiguration;

			return JsonModel(
				new { 
					theme = theme, 
					css = Url.Content(config.GetWebAssetThemeCssStylePath(theme))
				}
				, JsonRequestBehavior.AllowGet);
		}

		public ActionResult ChangeCulture(string culture)
		{
			if (String.IsNullOrEmpty(culture)) {
				return JsonError("Культура не указана", JsonRequestBehavior.AllowGet);
			}

			ILocalized localized = HttpContext.ApplicationInstance as ILocalized;

			if (localized == null) {
				return JsonError("Приложение не поддерживает смену языка/культуры.", JsonRequestBehavior.AllowGet);
			}

			if (!localized.SetCulture(culture)) {
				return JsonError("Культура не найдена", JsonRequestBehavior.AllowGet);
			}
			UserContext.Culture = culture;
			//Kesco.ApplicationServices.Manager.SaveUserLanguage(culture.Substring(0, 2));

			return JsonModel(culture, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// Реализует метод действия для запуска серверного приложения.
		/// Имеется ввиду, что приложение-клиент (страница) вызывает
		/// приложение-сервер (с)
		/// </summary>
		/// <returns>Результат метода действия</returns>
		public ActionResult Start()
		{
			return View();
		}

		public ActionResult Launch()
		{
			return View("Start");
		}

		/// <summary>
		/// Отправляет JavaScript код, который показывает сообщение клиенту.
		/// </summary>
		/// <param name="title">Заголовок информационного окна сообщения.</param>
		/// <param name="message">Сообщение</param>
		/// <param name="actionFunc">The action func.</param>
		/// <returns>
		/// JavaScript код, который показыет сообщение клиенту
		/// </returns>
		public ActionResult JavaScriptAlert(string title, string message, string actionFunc = "")
		{
			string alert = String.Format(@"
					(function() {{                        
						var err = {0};
						var okButtonTitle = '{2}';
						alertMessage(err.title, err.message, okButtonTitle, {3})
					}})();
					",
					Kesco.Web.Mvc.Json.Serialize(new { title = title, message = message }, true),
					message,
					Kesco.Web.Mvc.Localization.Resources.GUI_Button_OK,
					String.IsNullOrEmpty(actionFunc)?"''":actionFunc
				);
			var result = JavaScript(alert);
			return result;
		}

		[ValidateInput(false)]
		public ActionResult DialogResult(string control, string value)
		{
			return View(new DialogResultModel(String.Empty, control, value));
		}

		protected List<string> GetModelErrorMessages()
		{
			return GetModelErrors()
				.SelectMany(propErrors =>
					propErrors.Value.Errors.Select(er =>
						String.IsNullOrEmpty(er.ErrorMessage) 
							? er.Exception.Message 
							: er.ErrorMessage)
				).ToList();
		}

		protected Dictionary<string, ModelState> GetModelErrors()
		{
			return GetModelErrors(null);
		}
		
		protected Dictionary<string, ModelState> GetModelErrors(string prefix)
		{
			prefix = prefix ?? "Item.";
			Dictionary<string, ModelState> errors = new Dictionary<string, ModelState>();
			ModelState.ToList().ForEach(pair => {
				if (pair.Value.Errors.Count > 0)
					errors.Add((pair.Key.StartsWith(prefix) ? pair.Key.Substring(prefix.Length) : pair.Key), pair.Value);
			});

			return errors;
		}

		/// <summary>
		/// Вызывается, когда в действии происходит необработанное исключение.
		/// </summary>
		/// <param name="filterContext">Сведения о текущем запросе и действии.</param>
		/*protected override void OnException(ExceptionContext filterContext)
		{
			// Логируем ошибку
			Kesco.Logger.WriteEx(filterContext.Exception, true);
			
			// Указываем для инфраструктуры ASP.NET, что ошибка обработана
			filterContext.ExceptionHandled = true;

			// Если, запрос Ajax, 
			if (this.IsAjaxRequest) {
				// то вернём диалог с ошибкой
				JavaScriptAlert(
						Kesco.Localization.Resources.Ajax_Alert_Title_ApplicationError,
						filterContext.Exception.Message
					).ExecuteResult(ControllerContext);
			} else {
				// иначе страницу с информацией об ошибке
				this
					.View("Error", new HandleErrorInfo(
							filterContext.Exception,
							ControllerContext.RouteData.GetRequiredString("controller"),
							ControllerContext.RouteData.GetRequiredString("action")
						))
					.ExecuteResult(ControllerContext);
			}
		}*/

		/// <summary>
		/// Возвращает JS код закрытия диалоговой страницы с результатом
		/// </summary>
		/// <param name="resultValue">Результат</param>
		/// <returns></returns>
		protected ActionResult ReturnDialogValue(object resultValue)
		{
            var parametrs = Mvc.Json.Serialize(resultValue, true);
            string sectionID = HttpContext.Request["sectionId"] ??
                               HttpUtility.ParseQueryString(HttpContext.Request.UrlReferrer.ToString()).Get("sectionId");
		    if (sectionID != null)
		    {
		        JObject values = new JObject();
		        try
		        {
                    values = JsonConvert.DeserializeObject<JObject>(parametrs);

		        }
		        catch (Exception)
		        {
		            values.Add("something", JsonConvert.DeserializeObject<JValue>(parametrs));
		        }
                
                values.Add("sectionId", sectionID);
                parametrs = Mvc.Json.Serialize(values, true);
            }
			string script = String.Format(@"
				(function() {{
					var dialogResult = [{0}];
					dialogResult = JSON.stringify(dialogResult);
					closeDialogAndReturnValue(dialogResult);
				}})();"
                , parametrs);

			return JavaScript(script);
		}

		/// <summary>
		/// Возвращает JS код закрытия диалоговой страницы 
		/// и возврата результата через куки
		/// </summary>
		/// <param name="resultValue">Результат</param>
		/// <returns></returns>
		protected ActionResult DialogPageReturn(object resultValue)
		{
			string script = String.Format(@"
				(function() {{
					var retVal = {0};
					DialogPageReturn(retVal, 0);
				}})();"
				, Kesco.Web.Mvc.Json.Serialize(resultValue, true));

			return JavaScript(script);
		}

	}
}
