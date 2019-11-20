using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Kesco.Web.Mvc
{
	/// <summary>
	/// Задаёт базовую мастер-страницу представления, основанную на <see cref="System.Web.Mvc.ViewMasterPage"/>.
	/// </summary>
	/// <typeparam name="TSettings">Тип установок приложения.</typeparam>
	public class ViewMasterPage<TSettings> : System.Web.Mvc.ViewMasterPage
		where TSettings : ApplicationSettings
	{

		protected Dictionary<string, string> _userSettings = null;

		public Dictionary<string, string> UserSettings
		{
			get
			{
				if (_userSettings == null) {
					int clid = 0;
					if (Context.Request["clid"] != null) {
						Int32.TryParse(Context.Request["clid"], out clid);
					}
					//if (clid != 0)
						_userSettings = Kesco.ApplicationServices.Manager.SelectClientApplicationUserSettingDictionary(clid);
					//else
					//	_userSettings = new Dictionary<string,string>();
				}
				return _userSettings;
			}
		}

		/// <summary>
		/// Возвращает путь к директории с темой для текущего пользователя.
		/// </summary>
		/// <returns>Путь к директории с темой для текущего пользователя</returns>
		public string GetUserThemeFolder()
		{
			string theme = Session.GetUserContext().Theme;
			theme = theme ?? "classic";

			return Configuration<TSettings>.Themes[theme];
		}

		/// <summary>
		/// Возвращает URL к скрипту, расположенный в общих веб-ресурсах (скрипты, файлы со стилями, медиа-файлы, такие как картинки, видео, аудио).
		/// </summary>
		/// <param name="filePath">Относительный путь к файлу, относительно папки со скриптами из общими.</param>
		/// <returns>URL веб-скрипта.</returns>
		public string WebAssetScript(string filePath)
		{
			return ResolveUrl(Configuration<TSettings>.AppSettings.URI_Styles_Scripts + filePath);
		}

		/// <summary>
		/// Возвращает URL к файлу со стилями, расположенный в общих веб-ресурсах (скрипты, файлы со стилями, медиа-файлы, такие как картинки, видео, аудио).
		/// </summary>
		/// <param name="filePath">Относительный путь к файлу, относительно папки со стилями из общими.</param>
		/// <returns>URL веб-скрипта.</returns>
		public string WebAssetCssStyle(string filePath)
		{
			return ResolveUrl(Configuration<TSettings>.AppSettings.URI_Styles_Css + filePath);
		}

		/// <summary>
		/// Возвращает URL к файлу со стилями, расположенный в общих веб-ресурсах (скрипты, файлы со стилями, медиа-файлы, такие как картинки, видео, аудио).
		/// </summary>
		/// <param name="filePath">Относительный путь к файлу, относительно папки с медиа-файлами из общих ресурсов.</param>
		/// <returns>URL веб-скрипта.</returns>
		public string WebAssetImage(string filePath)
		{
			return ResolveUrl(Configuration<TSettings>.AppSettings.URI_Styles + filePath);
		}

	}
}
