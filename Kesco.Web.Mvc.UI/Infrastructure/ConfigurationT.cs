using System.Collections;
using System.Collections.Specialized;
using System.Web.Configuration;
using BLToolkit.Mapping;
using BLToolkit.Reflection;

namespace Kesco.Web.Mvc
{
	/// <summary>
	/// Реализует конфигурацию приложения
	/// </summary>
	/// <typeparam name="TApplicationSettings">The type of the application settings.</typeparam>
	public class Configuration<TApplicationSettings>
		where TApplicationSettings : ApplicationSettings
	{
		/// <summary>
		/// Возвращает коллекцию с поддерживаемыми темами.
		/// </summary>
		public static StringDictionary Themes { get { return _themes; } }
		protected static readonly StringDictionary _themes = new StringDictionary();

		protected Configuration() { }

		/// <summary>
		/// Возвращает строго-типизированные настройки приложения.
		/// </summary>
		/// <value>
		/// Строго-типизированные настройки приложения.
		/// </value>
		public static TApplicationSettings AppSettings { get; protected set; }

		/// <summary>
		/// Инициализирует строго-типизированные настройки приложения.
		/// </summary>
		public static void Init()
		{
			AppSettings = TypeAccessor<TApplicationSettings>.CreateInstanceEx();

			Hashtable ht = new Hashtable();
			NameValueCollection settings = WebConfigurationManager.AppSettings;
			foreach (string key in settings.AllKeys) {
				if (!ht.ContainsKey(key)) 
					ht.Add(key, settings[key]);
			}

			Map.DictionaryToObject(ht, AppSettings);


		}

		static Configuration()
		{

			_themes.Add("classic", "");
			_themes.Add("humanity", "themes/ui-humanity/");
			_themes.Add("lightness", "themes/ui-lightness/");
			_themes.Add("redmond", "themes/ui-redmond/");
			_themes.Add("sunny", "themes/ui-sunny/");
			_themes.Add("lufthansa", "themes/ui-lufthansa/");
			_themes.Add("rose", "themes/ui-rose/");
			_themes.Add("bluesky", "themes/ui-bluesky/");
			_themes.Add("green", "themes/ui-green/");
			_themes.Add("oldstyle", "themes/ui-oldstyle/");
		}

	}
}
