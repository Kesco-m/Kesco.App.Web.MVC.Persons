using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using BLToolkit.Mapping;
using BLToolkit.Reflection;

namespace Kesco.Web.Mvc
{
	/// <summary>
	/// Определяет интерфейс, поддерживающий кастомизацию локализации для пользователя
	/// </summary>
	public interface ILocalized
	{
		/// <summary>
		/// Возвращает идентификатор культуры, установленной для текущего пользователя.
		/// </summary>
		string Culture { get; }

		/// <summary>
		/// Устанавливает идентификатор культуры, установленной для текущего пользователя.
		/// </summary>
		/// <param name="culture">Идентификатор культуры.</param>
		/// <returns>Результат операции установки культуры</returns>
		bool SetCulture(string culture);
	}

	/// <summary>
	/// Определяет интерфейс, поддерживающий кастомизацию тем для пользователя
	/// </summary>
	public interface IThemeable
	{
		/// <summary>
		/// Возвращает коллекцию с поддерживаемыми темами.
		/// </summary>
		StringDictionary Themes { get; }

		/// <summary>
		/// Устанавливает тему для текущего пользователя.
		/// </summary>
		/// <param name="theme">Тема.</param>
		/// <returns>Результат операции установки культуры</returns>
		bool SetTheme(string theme);
	}

	/// <summary>
	/// Определяет интерфейс приложения для доступа к основным настройкам приложения
	/// </summary>
	public interface IAppConfiguration {

		/// <summary>
		/// Возвращает основные настройки приложения.
		/// </summary>
		/// <value>
		/// основные настройки приложения.
		/// </value>
		ApplicationSettings AppSettings { get; }

		/// <summary>
		/// Возвращает путь к CSS-файлу.
		/// </summary>
		/// <returns></returns>
		string GetWebAssetThemeCssStylePath(string theme);
	}

	/// <summary>
	/// Класс расширяет класс веб-приложения <see cref="System.Web.HttpApplication">HttpApplication</see> 
	/// , инициализируя строго типизированные установки веб-приложения из файла конфигурации.
	/// </summary>
	/// <typeparam name="T">Представляет класс веб-приложения <see cref="Sytem.Web.HttpApplication">HttpApplication</see></typeparam>
	/// <typeparam name="S">Представляет класс установок для веб приложения <see cref="Kesco.Web.Mvc.ApplicationSettings&lt;S&gt;">ApplicationSettings</see></typeparam>
	/// <example>
	/// <code>
	/// public abstract class CacheSettings : SiteSettings&lt;CacheSettings&gt;
	/// {
	/// 	public abstract bool Enabled { get; protected internal set; }
	/// 	public abstract int Expiration { get; protected internal set; }
	/// 
	/// }
	/// 
	/// public abstract class ApplicationSettings : SiteSettings&lt;ApplicationSettings&gt;
	/// {
	/// 	public abstract string AppName { get; protected internal set; }
	/// 	public abstract string CompanyName { get; protected internal set; }
	/// 	public abstract CacheSettings Cache { get; protected internal set; }
	/// }
	/// 
	/// public class SiteApplication : Application&lt;SiteApplication, ApplicationSettings&gt;
	/// {
	/// }
	/// 
	/// web.config
	/// &lt;configuration&gt; 
	/// &lt;appSettings>
	/// 	&lt;add key="AppName" value="Статьи движения денежных средств"/&gt;
	/// 	&lt;add key="Cache.Enabled" value="true"/&gt;
	/// 	&lt;add key="Cache.Expiration" value="1"/&gt;
	/// &lt;/appSettings&gt;
	/// &lt;/configuration&gt;
	/// </code>
	/// </example>
	public abstract class Application<S> : HttpApplication, IAppConfiguration, IThemeable, ILocalized
		where S : ApplicationSettings
	{

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="Application&lt;S&gt;"/>.
		/// </summary>
		public Application()
			: base()
		{
		}


		public ApplicationSettings AppSettings
		{
			get {
				return Configuration<S>.AppSettings;
			}
		}

		public string GetWebAssetThemeCssStylePath(string theme)
		{
			return AppStyles.URI_Styles_Css + Configuration<S>.Themes[theme] + "jquery-ui.css";
		}

		#region Члены Themable

		/// <summary>
		/// Возвращает коллекцию с поддерживаемыми темами.
		/// </summary>
		public StringDictionary Themes { get { return Configuration<S>.Themes; } }

		/// <summary>
		/// Устанавливает тему для текущего пользователя.
		/// </summary>
		/// <param name="theme">Тема.</param>
		/// <returns>
		/// Результат операции установки культуры
		/// </returns>
		public bool SetTheme(string theme)
		{
			if (Themes.ContainsKey(theme)) {
				UserContext.Current.Theme = theme;
				return true;
			}
			return false;
		}

		#endregion

		#region Члены ILocalized

		/// <summary>
		/// Возвращает идентификатор культуры, установленной для текущего пользователя.
		/// </summary>
		public string Culture
		{
			get {
				string culture = Session["User.Culture"] as string;
				if (String.IsNullOrEmpty(culture)) {
					Session["User.Culture"] = culture = "en-US";
				}
				return culture;
			}
		}

		/// <summary>
		/// Устанавливает идентификатор культуры, установленной для текущего пользователя.
		/// </summary>
		/// <param name="culture">Идентификатор культуры.</param>
		/// <returns>
		/// Результат операции установки культуры
		/// </returns>
		public bool SetCulture(string culture)
		{
			CultureInfo ci = null;
			try {
				ci = CultureInfo.GetCultureInfo(culture);
			} catch {}
			if (ci != null) {
				Session["User.Culture"] = culture;
				return true;
			}
			return false;
		}

		#endregion

	}
}
