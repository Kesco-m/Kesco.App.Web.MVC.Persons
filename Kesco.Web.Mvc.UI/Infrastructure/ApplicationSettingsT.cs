using BLToolkit.Mapping;
using BLToolkit.Validation;

namespace Kesco.Web.Mvc
{
    /// <summary>
    /// Стили приложения
    /// </summary>
    public class AppStyles
    {

        /// <summary>
        /// Возвращает или устанавливает URI расположения папки с клиентскими скриптами.
        /// </summary>
        /// <value>
        /// URI расположения папки с клиентскими скриптами.
        /// </value>        
        public static string URI_Styles_Scripts {  get { return "~/styles/scripts/"; } }

        /// <summary>
        /// Возвращает или устанавливает URI расположения папки со 
        /// стиливыми CSS-файлами.
        /// </summary>
        /// <value>
        /// URI папки со стиливыми CSS-файлами.
        /// </value>
        
        public static string URI_Styles_Css { get { return "~/styles/css/"; } }

        /// <summary>
        /// Возвращает или устанавливает URI расположения папки с медиа-файлами.
        /// </summary>
        /// <value>
        /// URI папки с медиа-файлами.
        /// </value>

        public static string URI_Styles { get { return "/styles/"; } } 
    }
	/// <summary>
	/// Данный класс описывает настройки локализации дат/времени
	/// на стороне клиента (браузер).
	/// </summary>
	public abstract class DateTimeSettings : Settings
	{
		/// <summary>
		/// Устанавливает или возвращает разделитель частей даты.
		/// </summary>
		/// <value>
		/// Разделитель частей даты.
		/// </value>
		[DefaultValue(".")]
		public abstract string DatePartsSeparator { get; protected internal set; }

		/// <summary>
		/// Устанавливает или возвращает разделитель частей времени.
		/// </summary>
		/// <value>
		/// Разделитель частей времени.
		/// </value>
		[DefaultValue(":")]
		public abstract string TimePartsSeparator { get; protected internal set; }

		[DefaultValue("")]
		public abstract string AM { get; protected internal set; }

		[DefaultValue("")]
		public abstract string PM { get; protected internal set; }

		[DefaultValue("M/d/yyyy")]
		public abstract string ShortDatePattern { get; protected internal set; }

		[DefaultValue("dddd, MMMM dd, yyyy")]
		public abstract string LongDatePattern { get; protected internal set; }

		[DefaultValue("h:mm tt")]
		public abstract string ShortTimePattern { get; protected internal set; }

		[DefaultValue("h:mm:ss tt")]
		public abstract string LongTimePattern { get; protected internal set; }

		[DefaultValue("dddd, MMMM dd, yyyy h:mm tt")]
		public abstract string LongDateShortTimePattern { get; protected internal set; }

		[DefaultValue("dddd, MMMM dd, yyyy h:mm:ss tt")]
		public abstract string FullDateTimePattern { get; protected internal set; }

		[DefaultValue("MMMM dd")]
		public abstract string MonthDayPattern { get; protected internal set; }

		[DefaultValue("yyyy MMMM")]
		public abstract string MonthYearPattern { get; protected internal set; }

	}

	/// <summary>
	/// Данный класс описывает настройки локализации числовой информации (целые, вещественные, проценты, валюта)
	/// на стороне клиента (браузер).
	/// </summary>
	/// <remarks>
	/// Для работы с числовой информацией, требующей форматирование, 
	/// которое зависит от культуры, используется JS-библиотека Globalize.
	/// </remarks>
	public abstract class NumberSettings : Settings
	{
		/// <summary>
		/// Устанавливает или возвращает шаблон форматирования положительного числа.
		/// </summary>
		/// <value>
		/// Шаблон форматирования положительного числа.
		/// </value>
		[DefaultValue("n")]
		public abstract string PositivePattern { get; protected internal set; }

		/// <summary>
		/// Устанавливает или возвращает символ положительного числа.
		/// </summary>
		/// <value>
		/// Символ положительного числа.
		/// </value>
		[DefaultValue("+")]
		public abstract string PositiveNumberSymbol { get; protected internal set; }

		/// <summary>
		/// Устанавливает или возвращает шаблон форматирования отрицательного числа.
		/// </summary>
		/// <value>
		/// Шаблон форматирования отрицательного числа.
		/// Один из "(n)|-n|- n|n-|n -"
		/// </value>
		[DefaultValue("-n")]
		public abstract string NegativePattern { get; protected internal set; }

		/// <summary>
		/// Устанавливает или возвращает символ отрицательного числа.
		/// </summary>
		/// <value>
		/// Символ отрицательного числа.
		/// </value>
		[DefaultValue("-")]
		public abstract string NegativeNumberSymbol { get; protected internal set; }

		/// <summary>
		/// Устанавливает или возвращает количество дробных знаков.
		/// </summary>
		/// <value>
		/// Количество дробных знаков.
		/// </value>
		[DefaultValue(2)]
		public abstract byte Decimals { get; protected internal set; }

		/// <summary>
		/// Устанавливает или возвращает разделитель целой и дробной части числа.
		/// </summary>
		/// <value>
		/// Разделитель целой и дробной части числа.
		/// </value>
		[DefaultValue(",")]
		public abstract string DecimalSeparator { get; protected internal set; }

		/// <summary>
		/// Устанавливает или возвращает разделитель групп разрядов.
		/// </summary>
		/// <value>
		/// Разделитель групп разрядов.
		/// </value>
		[DefaultValue(" ")]
		public abstract string GroupSeparator { get; protected internal set; }

		/// <summary>
		/// Устанавливает или возвращает число цифр в каждой из групп целой части 
		/// десятичной дроби в числовых значениях, разделённых запятой.
		/// </summary>
		/// <value>
		/// Число цифр в каждой из групп целой части десятичной дроби в числовых значениях.
		/// </value>
		[DefaultValue("3")]
		public abstract string GroupSizes { get; protected internal set; }

	}

	/// <summary>
	/// Данный класс описывает настройки локализации числовой информации, 
	/// имеющих символ, такие как - проценты, валюта.
	/// </summary>
	public abstract class NumberWithSymbolSettings : NumberSettings
	{
		/// <summary>
		/// Устанавливает или возвращает символ для обозначение информации, 
		/// представленной числом - процент, валюта.
		/// </summary>
		/// <value>
		/// Символ для обозначение числовой информации
		/// </value>
		[DefaultValue("")]
		public abstract string Symbol { get; protected internal set; }
	}

	/// <summary>
	/// Данный класс описывает настройки локализации приложения на стороне клиента.
	/// </summary>
	/// <remarks>
	/// Для работы с данными, требующие форматирование, которое зависит 
	/// от культуры (числа, проценты, валюты, дата, время),
	/// используется JS-библиотека Globalize.
	/// </remarks>
	[System.ComponentModel.Description("Данный класс описывает настройки локализации приложения на стороне клиента.")]
	public abstract class CultureSettings : Settings
	{
		/// <summary>
		/// Устанавливает или возвращает международное название локализации.
		/// </summary>
		/// <value>
		/// Международное название локализации.
		/// </value>
		[DefaultValue("Application Culture Settings")]
		public abstract string EnglishName { get; protected internal set; }

		/// <summary>
		/// Устанавливает или возвращает русское название локализации.
		/// </summary>
		/// <value>
		/// Русское название локализации.
		/// </value>
		[DefaultValue("Корпоративные настройки локализации")]
		public abstract string NativeName { get; protected internal set; }

		/// <summary>
		/// Устанавливает или возвращает настройки локализации для чисел.
		/// </summary>
		/// <value>
		/// Настройки локализации для чисел.
		/// </value>
		public abstract NumberSettings Number { get; protected internal set; }

		/// <summary>
		/// Устанавливает или возвращает настройки локализации для чисел, представляющий проценты.
		/// </summary>
		/// <value>
		/// Настройки локализации для чисел, представляющий проценты.
		/// </value>
		public abstract NumberWithSymbolSettings Percent { get; protected internal set; }

		/// <summary>
		/// Устанавливает или возвращает настройки локализации для чисел, представляющих валюту.
		/// </summary>
		/// <value>
		/// Настройки локализации для чисел, представляющих валюту.
		/// </value>
		public abstract NumberWithSymbolSettings Currency { get; protected internal set; }

		/// <summary>
		/// Устанавливает или возвращает настройки локализации для дат/времени.
		/// </summary>
		/// <value>
		/// Настройки локализации для дат/времени.
		/// </value>
		public abstract DateTimeSettings DateTime { get; protected internal set; }
	}

	/// <summary>
	/// Данный класс описывает настройки кеширования для приложения.
	/// </summary>
	public abstract class CacheSettings : Settings
	{
		/// <summary>
		/// Возвращает или устанавливает включено ли кеширование для приложения или нет <see cref="CacheSettings"/>.
		/// </summary>
		/// <value>
		///   <c>true</c> если кеширование включено; или, <c>false</c>.
		/// </value>
		[DefaultValue(false)]
		public abstract bool Enabled { get; protected internal set; }

		/// <summary>
		/// Возвращает или устанавливает промежуток времении, 
		/// через который актуальность данных в кеше истекает.
		/// </summary>
		/// <value>
		/// Промежуток времении (в минутах), через который актуальность данных в кеше истекает.
		/// </value>
		[DefaultValue(5)]
		public abstract int Expiration { get; protected internal set; }

	}

	/// <summary>
	/// Данный класс определяет основные стандартные установки для приложения ASP.NET MVC.
	/// </summary>
	/// <typeparam name="T">Тип установок приложения</typeparam>
	public abstract class ApplicationSettings: Settings
	{

        /// <summary>
		/// Путь к скриптам
		/// </summary>
		/// <value>
		/// Путь к скриптам.
		/// </value>
		public abstract string URI_styles_js { get; protected internal set; }

        /// <summary>
        /// Сборос кэша
        /// </summary>
        /// <value>
        /// Сборос кэша.
        /// </value>
        public abstract string URI_styles_cache { get; protected internal set; }

        /// <summary>
        /// Возвращает или устанавливает название приложения.
        /// </summary>
        /// <value>
        /// Название приложения.
        /// </value>
        public abstract string AppName { get; protected internal set; }

		/// <summary>
		/// Возвращает или устанавливает URI справки.
		/// </summary>
		/// <value>
		/// URI справки.
		/// </value>
		public abstract string URI_WikiHelp { get; protected internal set; }

		/// <summary>
		/// Возвращает или устанавливает адрес SMTP-сервера.
		/// </summary>
		/// <value>
		/// Адрес SMTP-сервера.
		/// </value>
		[Required]
		public abstract string SmtpServer { get; protected internal set; }

		/// <summary>
		/// Возвращает или устанавливает домен кукисов веб-приложения.
		/// </summary>
		/// <value>
		/// Домен кукисов веб-приложения.
		/// </value>
		[Required]
		public abstract string Domain { get; protected internal set; }

		/// <summary>
		/// Возвращает или устанавливает URI службы поддержки.
		/// </summary>
		/// <value>
		/// URI службы поддержки.
		/// </value>
		[Required]
		public abstract string URI_Support { get; protected internal set; }

		/// <summary>
		/// Возвращает или устанавливает электронный адрес службы поддержки.
		/// </summary>
		/// <value>
		/// Электронный адрес службы поддержки.
		/// </value>
		[Required]
		public abstract string Email_Support { get; protected internal set; }

        

		/// <summary>
		/// Возвращает или устанавливает URI получения proxy объектов для взаимодействия с зарегистрированными хабами.
		/// </summary>
		/// <value>
		/// URI получения proxy объектов.
		/// </value>
		[DefaultValue("/connectionState")] 
		public abstract string URI_StateHubProxy { get; protected internal set; }

		/// <summary>
		/// Возвращает или устанавливает стандартную ширину диалогового окна
		/// при открытии, если не задано приложением.
		/// </summary>
		/// <value>
		/// Стандартная ширина диалогового окна
		/// </value>
		[DefaultValue(600)]
		public abstract int Width { get; protected internal set; }

		/// <summary>
		/// Возвращает или устанавливает стандартную высоту диалогового окна
		/// при открытии, если не задано приложением.
		/// </summary>
		/// <value>
		/// Стандартная высота диалогового окна
		/// </value>
		[DefaultValue(500)]
		public abstract int Height { get; protected internal set; }

		/// <summary>
		/// Возвращает или устанавливает, запущено ли приложение на рабочем сервере.
		/// </summary>
		/// <value>
		/// Указывает выполняется ли приложение на рабочем сервере
		/// </value>
		[DefaultValue(true)]
		public abstract bool IsProduction { get; protected internal set; }

		/// <summary>
		/// Возвращает или устанавливает настройки локализации приложения на стороне клиента.
		/// </summary>
		public abstract CultureSettings Culture { get; protected internal set; }

		/// <summary>
		/// Возвращает или устанавливает настройки локализации приложения на стороне клиента.
		/// </summary>
		public abstract CacheSettings Cache { get; protected internal set; }
	}
}
