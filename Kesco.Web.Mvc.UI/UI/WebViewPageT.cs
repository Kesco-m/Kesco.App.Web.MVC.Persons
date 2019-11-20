using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace Kesco.Web.Mvc
{
	/// <summary>
	/// Задаёт базовую мастер-страницу представления, основанную на <see cref="System.Web.Mvc.WebViewPage"/>.
	/// </summary>
	/// <typeparam name="TSettings">Тип установок приложения.</typeparam>
	/// <typeparam name="TModel">Тип модели.</typeparam>
	public abstract class WebViewPage<TSettings, TModel> : System.Web.Mvc.WebViewPage<TModel>
		where TSettings : ApplicationSettings
	{

		public string UserTheme
		{
			get {
				return UserContext.Current.Theme ?? "classic";
			}
		}

		/// <summary>
		/// Возвращает путь к директории с темой для текущего пользователя.
		/// </summary>
		/// <returns>Путь к директории с темой для текущего пользователя</returns>
		public string GetUserThemeFolder()
		{
			return Configuration<TSettings>.Themes[UserTheme];
		}

		/// <summary>
		/// Возвращает URL к скрипту, расположенный в общих веб-ресурсах (скрипты, файлы со стилями, медиа-файлы, такие как картинки, видео, аудио).
		/// </summary>
		/// <param name="filePath">Относительный путь к файлу, относительно папки со скриптами из общими.</param>
		/// <returns>URL веб-скрипта.</returns>
		public string WebAssetScript(string filePath)
		{
			return Url.Content(Configuration<TSettings>.AppSettings.URI_Styles_Scripts + filePath);
		}

		/// <summary>
		/// Возвращает URL к файлу со стилями, расположенный в общих веб-ресурсах (скрипты, файлы со стилями, медиа-файлы, такие как картинки, видео, аудио).
		/// </summary>
		/// <param name="filePath">Относительный путь к файлу, относительно папки со стилями из общими.</param>
		/// <returns>URL веб-скрипта.</returns>
		public string WebAssetCssStyle(string filePath)
		{
			return Url.Content(Configuration<TSettings>.AppSettings.URI_Styles_Css + filePath);
		}

		/// <summary>
		/// Возвращает URL к файлу со стилями, расположенный в общих веб-ресурсах (скрипты, файлы со стилями, медиа-файлы, такие как картинки, видео, аудио).
		/// </summary>
		/// <param name="filePath">Относительный путь к файлу, относительно папки с медиа-файлами из общих ресурсов.</param>
		/// <returns>URL веб-скрипта.</returns>
		public string WebAssetImage(string filePath)
		{
			return Url.Content(Configuration<TSettings>.AppSettings.URI_Styles + filePath);
		}

		public string WebAssetClientHubProxy(string filePath)
		{
			return string.IsNullOrEmpty(Configuration<TSettings>.AppSettings.URI_StateHubProxy) ? (Url.FullPathAction("", "connectionState") + filePath) : (Configuration<TSettings>.AppSettings.URI_StateHubProxy + filePath);
		}

		/// <summary>
		/// Находит вхождение строки в тексте и посдвечивает его с использованием стиля 'ui-state-highlight'
		/// </summary>
		/// <param name="text">Исходный текст.</param>
		/// <param name="highlight">Строка для поиска.</param>
		/// <returns>Исходная строка с подсвечиванием строки</returns>
		public string Highlight(string text, string highlight)
		{
			string str = text ?? String.Empty;
			if (!String.IsNullOrWhiteSpace(highlight)) {

				Regex myRegex = new Regex(
						String.Format(@"({0})", highlight),
						RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant
					);

				str = myRegex.Replace(str, @"<span class=""ui-state-highlight"">$1</span>");
			}
			return str;
		}

		/// <summary>
		/// Находит вхождение в тексте строки, заданной в Request["text"] 
		/// и посдвечивает его с использованием стиля 'ui-state-highlight'
		/// </summary>
		/// <param name="text">Исходный текст.</param>
		/// <returns>Исходная строка с подсвечиванием строки</returns>
		public string HL(string text)
		{
			return Highlight(text, Request["text"]);
		}
	}
}
