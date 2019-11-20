using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Globalization;
using System.Threading;

namespace Kesco.Web.Mvc
{
	/// <summary>
	///     Класс расширяет класс <see cref="System.Web.Mvc.Controller">Controller</see>, 
	///     определяя языковый стандарт в соответствии с настройками корпоративной культуры.
	/// </summary>
	[CompressFilter(Order = 1000)]
	[ValidateInput(false)]
	public abstract class CorporateCultureController : Controller
	{
		public bool UseCompressHtml { get; set; }

		public string[] MimeTypesToCompress { get; set; }

		public CorporateCultureController()
			: base()
		{
			MimeTypesToCompress = new string[] { "text/html" };
		}

		/// <summary>
		///  Объект для блокировки доступа к словарю культур, 
		///  когда процесс пытается добавить новый элемент.
		/// </summary>
		protected static object culturesWithCorporateSettingsCacheLock = new object();

		/// <summary>
		/// Словарь-кеш культур.
		/// </summary>
		protected static volatile Dictionary<string, CultureInfo> culturesWithCorporateSettingsCache = new Dictionary<string, CultureInfo>();

		/// <summary>
		/// Возвращает контекст пользователя.
		/// </summary>
		/// <value>
		/// Контекст пользователя.
		/// </value>
		public UserContext UserContext
		{
			get { return Session.GetUserContext(); }
		}

		/// <summary>
		/// Инициализирует данные, которые могут быть недоступны на момент вызова конструктора.
		/// </summary>
		/// <param name="requestContext">Контекст HTTP и данные маршрута.</param>
		protected override void Initialize(System.Web.Routing.RequestContext requestContext)
		{

			base.Initialize(requestContext);

			InitializeUserContext(requestContext);

		}

		/// <summary>
		/// Инициализирует пользовательский контекст.
		/// </summary>
		/// <param name="requestContext">Контекст HTTP и данные маршрута.</param>
		protected virtual void InitializeUserContext(System.Web.Routing.RequestContext requestContext)
		{
			UserContext ctx = UserContext;

			// Определим клиентское смещение временной зоны
			var tz = requestContext.HttpContext.Request.Cookies["tz"];
			if (tz != null) {
				ctx.ClientTimeZoneOffset = Int32.Parse(tz.Value);
			}

			// Определим язык пользователя
			if (string.IsNullOrEmpty(ctx.Culture))
			{
				ctx.Culture = Kesco.ApplicationServices.Manager.GetUserLanguage();
			}

			string culture = ctx.Culture;

			// Устанавливаем культуру для пользовательского запроса
			CultureInfo ci = GetCorporateCulture(culture);
			Thread.CurrentThread.CurrentCulture = ci;
			Thread.CurrentThread.CurrentUICulture = ci;

			string theme = ctx.Theme;
			if (String.IsNullOrEmpty(theme)) { // Тема интерфейса пользователя в сессии не установлена.
				ctx.Theme = "classic";
			}

		}

		/// <summary>
		/// Инициализирует языковой стандарт (сведения об языке и региональных параметрах) 
		/// в соотвествии с корпоративными стандартами.
		/// </summary>
		/// <param name="culture">The culture.</param>
		protected virtual void InitCultureWithCorporateSettings(string culture)
		{
			if (!culturesWithCorporateSettingsCache.ContainsKey(culture)) {
				lock (culturesWithCorporateSettingsCacheLock) { // двойная проверка
					if (!culturesWithCorporateSettingsCache.ContainsKey(culture)) {

						CultureSettings settings = GetCorporateCultureSettings();
						CultureInfo ci = CultureInfo.GetCultureInfo(culture).ToCorporateCulture(settings);
						culturesWithCorporateSettingsCache.Add(culture, ci);
					}
				}
			}
		}

		/// <summary>
		/// Вовзвращает языковой стандарт с корпоративными настройками
		/// </summary>
		/// <param name="culture">Идентификатор культуры.</param>
		/// <returns>Сведения об языке и региональных параметрах</returns>
		protected virtual CultureInfo GetCorporateCulture(string culture)
		{
			InitCultureWithCorporateSettings(culture);
			return culturesWithCorporateSettingsCache[culture];
		}

		/// <summary>
		/// Возвращает настройки корпоративной культуры.
		/// </summary>
		/// <returns>Настройки корпоративной культуры</returns>
		protected abstract CultureSettings GetCorporateCultureSettings();

	}
}
