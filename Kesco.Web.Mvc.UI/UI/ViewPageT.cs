using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc
{
	/// <summary>
	/// Задаёт базовую страницу представления для страниц, основанных на <see cref="System.Web.Mvc.ViewPage"/>.
	/// </summary>
	/// <typeparam name="TSettings">Тип установок приложения.</typeparam>
	public class ViewPage<TSettings> : ViewPage<TSettings, dynamic>
		where TSettings : ApplicationSettings
	{ }

	/// <summary>
	/// Задаёт базовую страницу представления для страниц, основанных на <see cref="System.Web.Mvc.ViewPage&lt;TModel&gt;"/>.
	/// </summary>
	/// <typeparam name="TSettings">Тип установок приложения.</typeparam>
	/// <typeparam name="TModel">Тип модели.</typeparam>
	public class ViewPage<TSettings, TModel> : System.Web.Mvc.ViewPage<TModel>
		where TSettings : ApplicationSettings
	{
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
