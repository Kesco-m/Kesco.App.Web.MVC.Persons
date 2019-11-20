using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kesco.ApplicationServices;
using System.Web;

namespace Kesco.Web.Mvc
{
	/// <summary>
	/// 
	/// </summary>
	public class UserContext : Settings
	{
		protected const string KEY = "___User.Context___";

		private static object lockObj = new object();

		internal UserContext() {
			ClientTimeZoneOffset = 0;
		}

		/// <summary>
		/// Gets the instance.
		/// </summary>
		public static UserContext Current
		{
			get {
				return HttpContext.Current.Session.GetUserContext();
			}
		}

		private static UserContext GetUserContext()
		{
			lock (lockObj) {
				UserContext ctx = HttpContext.Current.Items[KEY] as UserContext;
				if (ctx == null) {
					ctx = new UserContext();
					HttpContext.Current.Items.Add(KEY, ctx);
				}
				return ctx;
			}
		}

		public dynamic Bag { get; protected set; }

		/// <summary>
		/// Gets the employee info.
		/// </summary>
		public EmployeeInfo EmployeeInfo
		{
			get {
				if (_employeeInfo == null) {
					_employeeInfo = Kesco.ApplicationServices.Manager.GetCurrentEmployee(Culture == "en");
				}
				return _employeeInfo;
			}
		}
		private EmployeeInfo _employeeInfo = null;

		/// <summary>
		/// Gets or sets the culture.
		/// </summary>
		/// <value>
		/// The culture.
		/// </value>
		public string Culture
		{
			get {
				/*if (String.IsNullOrEmpty(_culture)) { 
					string language = Kesco.ApplicationServices.Manager.GetUserLanguage();
					if (language == "en") _culture = "en-US";
					else if (language == "ru") _culture = "ru-RU";
					else if (language == "et") _culture = "et-EE";
				}*/
				return _culture;
			}
			set {
				if (_culture != value) {
					_culture = value;
					// force reloading employee info
					_employeeInfo = null;
				}
			}
		}

		/// <summary>
		/// Текущий язык пользователя
		/// </summary>
		public string Lang
		{
			get { return (Culture ?? "en").Substring(0,2); }
		}

		private string _culture = String.Empty;

		/// <summary>
		/// Проверяет является ли указанная культура установленной в данный момент.
		/// </summary>
		/// <param name="culture">Культура.</param>
		/// <returns>
		///   <c>true</c> если указанная культура установлена; иначе, <c>false</c>.
		/// </returns>
		public bool IsCulture(string culture)
		{
			return Culture == culture;
		}

		public bool IsEnCulture
		{
			get
			{
				return IsCulture("en-US");
			}
		}

		/// <summary>
		/// Gets or sets the theme.
		/// </summary>
		/// <value>
		/// The theme.
		/// </value>
		public string Theme
		{
			get {
				try
				{
					if (_theme == null)
					{
						// Тема интерфейса пользователя не установлена.
						_theme = Kesco.ApplicationServices.Manager.GetUserSettingValue(0, "UITheme");
					}
				}
				catch
				{
					_theme = null;
				}
				return _theme;
			}
			set {
				try
				{
					if (value != string.Empty && _theme != value) {
						_theme = value;
						Kesco.ApplicationServices.Manager.SaveUserSetting(0, "UITheme", _theme);
					}
				}
				catch
				{
					
				}
			}
		}

		private string _theme = null;

		public int ClientTimeZoneOffset { get; set; }
	}
}
