using System;
using System.ComponentModel.DataAnnotations;


namespace Kesco.Web.Mvc
{
	/// <summary>
	/// Определяет базовый тип модели представления
	/// </summary>
	public abstract class DialogViewModel
	{
		/// <summary>
		/// Возвращает или устанавливает заголовок страницы.
		/// </summary>
		/// <value>
		/// Заголовок страницы.
		/// </value>
		[ScaffoldColumn(false)]
		public string PageTitle { get; set; }
	
		/// <summary>
		/// Возвращает или устанавливает название раздела справки.
		/// </summary>
		/// <value>
		/// Название раздела справки
		/// </value>
		[ScaffoldColumn(false)]
		public string HelpTopic { get; set; }

		/// <summary>
		/// Возвращает или устанавливает идентификатор приложения-клиента (CLID).
		/// </summary>
		/// <value>
		/// идентификатор приложения-клиента 
		/// </value>
		[ScaffoldColumn(false)]
		public int CLID { get; protected set; }

		/// <summary>
		/// Возвращает или устанавливает URI возврата результата диалога.
		/// </summary>
		/// <value>
		/// URI возврата результата диалога
		/// </value>
		[ScaffoldColumn(false)]
		public string ReturnUri { get; protected set; }

		/// <summary>
		/// Возвращает или устанавливает значение, указывающее должна ли страница 
		/// вернуть результат.
		/// </summary>
		/// <value>
		///   <c>true</c> если страница должна вернуть результат (диалог); иначе, <c>false</c>.
		/// </value>
		[ScaffoldColumn(false)]
		public bool IsDialog { get; set; }

		/// <summary>
		/// Возвращает или устанавливает значение результата, которое диалог должен вернуть 
		/// </summary>
		/// <value>
		/// Значение результата, которое диалог должен вернуть
		/// </value>
		[ScaffoldColumn(false)]
		public string DialogResult { get; set; }

		/// <summary>
		/// Настройки пользователя, хранящиеся в БД
		/// </summary>
		protected object settings { get; set; }

		protected ScriptCapabilities ScriptCapabilities { get; set; }

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="DialogViewModel"/> класса.
		/// </summary>
		/// <param name="clid">идентификатор приложения-клиента.</param>
		/// <param name="returnUri">URI возврата результата диалога</param>
		/// <param name="isDialog">если установлено в <c>true</c> страница 
		/// работает в режиме диалога и  должна вернуть результат.</param>
		public DialogViewModel(int clid, string returnUri, bool isDialog) {
			// по-умолчанию загружаем все скрипты
			ScriptCapabilities = new ScriptCapabilities {
				LoadGridScript = true,
				LoadTreeScript = true,
				LoadHubScript = true
			};

			ScriptCapabilities = new ScriptCapabilities {
				LoadTreeScript = true,
				LoadGridScript = true,
				LoadHubScript = true
			};
			CLID = clid;
			ReturnUri = returnUri;
			IsDialog = isDialog;
			CreateSettings();
			if (settings != null) {
				LoadSettings(settings);
			}
		}

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="DialogViewModel"/> класса.
		/// </summary>
		public DialogViewModel() : this(0, String.Empty, false) { }

		public ScriptCapabilities GetScriptCapabilities()
		{
			return ScriptCapabilities;
		}

		/// <summary>
		/// Создаёт объект с настройками пользователя.
		/// </summary>
		/// <returns>Объект с настройками пользователя</returns>
		protected abstract void CreateSettings();

		/// <summary>
		/// Загружает настройки клиента для данного пользователя.
		/// </summary>
		/// <param name="settings">Настройки пользователя.</param>
		protected virtual void LoadSettings(object settings)
		{
			// Загружаем параметры из DB
			Kesco.ApplicationServices.Manager.SetClientApplicationUserSettings(CLID, settings);
		}

	}
}
