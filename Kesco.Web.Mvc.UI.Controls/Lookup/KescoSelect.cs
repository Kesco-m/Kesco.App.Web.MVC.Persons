using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.Script.Serialization;

namespace Kesco.Web.Mvc.UI
{

	public enum KescoSelectLinkShowCondition : int
    {
        Always = 0,
        OnlyIfLessThenLimit = 1,
        OnlyIfGreaterThenLimit = 2
    }

	/// <summary>
	/// Данный класс представляет дополнительную ссылку в выпадающем меню элемента управления <see cref="KescoSelect"/>
	/// </summary>
	public class KescoSelectLink
	{
		public string Command { get; set; }
		public string Text { get; set; }
		public string Icon { get; set; }
		public KescoSelectLinkShowCondition ShowCondition { get; set; }
	}

	/// <summary>
	/// Данный класс определяет клиентские события (js функции) для элемента управления <see cref="KescoSelect"/>
	/// </summary>
	public class KescoSelectClientEvents
	{
		/// <summary>
		/// Возвращает или устанавливает клиентскую функции 
        /// для обработки команды KescoSelect.
		/// </summary>
		/// <value>
		/// Обработчик ОnСommand.
		/// </value>
		public string OnCommand { get; set; }

        /// <summary>
        /// Возвращает или устанавливает клиентскую функции 
        /// для обработки события получения фокуса из меню.
        /// </summary>
        /// <value>
        /// Клиентскую функции обработчик OnFocusItem.
        /// </value>
		public string OnFocusItem { get; set; }
		public string OnSelectItem { get; set; }
		public string OnFormatItem { get; set; }
		public string OnRequest { get; set; }
	}

	/// <summary>
	/// 
	/// </summary>
	[Flags]
	public enum KescoSelectButtons
	{
		None = 0,
		ViewButton = 1, 
		SearchButton = 2
		
	}

	/// <summary>
	/// Класс представлет элемент управления KescoLookup. 
	/// Данный элемент упраления реализует расширенную функциональность текстового поля 
	/// с поддержкой автозавершения выбора из списка поиска.
	/// </summary>
	public class KescoSelect : ControlBase
	{
		/// <summary>
		/// Возвращает или устанавливает контекст для клиентского элемента управления.
		/// Может содержать любой объект. В дальнейшем он сериализуется в формат JSON.
		/// </summary>
		/// <value>
		/// Контекст для клиентского элемента управления
		/// </value>
		public dynamic ClientSideControlContext { get; set; }

		/// <summary>
		/// Возвращает или устанавливает заголовок элемента управления.
		/// </summary>
		/// <value>
		/// Заголовок элемента управления.
		/// </value>
		public string Caption { get; set; }

		/// <summary>
		/// Возвращает или устанавливает CSS класс для текстового поля, реализующего элемент управления.
		/// </summary>
		/// <value>
		/// CSS класс.
		/// </value>
		public string CssClass { get; set; }

		/// <summary>
		/// Возвращает или устанавливает CSS стиль для текстового поля, реализующего элемент управления.
		/// </summary>
		/// <value>
		/// CSS стиль
		/// </value>
		/// <remarks>
		/// Аналогичен HTML атрибуту style. 
		/// <c>&lt;input type="text" style="" /%gt;</c>
		/// </remarks>
		public string CssStyle { get; set; }

		/// <summary>
		/// Возвращает или устанавливает имя поля, которое является ключом, 
		/// из списка, возвращаемого запросом, указанным в <see cref="AutocompleteUri"/>
		/// </summary>
		/// <value>
		/// Имя поля, которое является ключом.
		/// </value>
		public string KeyField { get; set; }

		/// <summary>
		/// Возвращает или устанавливает имя поля, которое является отображаемым значением, 
		/// из списка, возвращаемого запросом, указанным в <see cref="AutocompleteUri"/>
		/// </summary>
		/// <value>
		/// Имя поля, которое является отображаемым значением.
		/// </value>
		public string DisplayField { get; set; }

		/// <summary>
		/// Возвращает или устанавливает адрес запроса (Uri), возвращающего список автозавершения.
		/// </summary>
		/// <value>
		/// Адрес запроса (Uri), возвращающего список автозавершения
		/// </value>
		/// <remarks>
		/// </remarks>
		public string AutocompleteUri { get; set; }
		public int AutocompleteMaxItems { get; set; }

		/// <summary>
		/// Возвращает или устанавливает адрес запроса (Uri), возвращающего обновлённое значение элемента.
		/// </summary>
		/// <value>
		/// Адрес запроса (Uri), возвращающего обновлённое значение элемента
		/// </value>
		/// <remarks>
		/// </remarks>
		public string RefreshUri { get; set; }
		public string RefreshUriKeyField { get; set; }
		public string RefreshUriLabelField { get; set; }

		/// <summary>
		/// Gets or sets the search button image.
		/// </summary>
		/// <value>
		/// The search button image.
		/// </value>
		public string SearchButtonImage { get; set; }

		/// <summary>
		/// Gets or sets the search button CSS style.
		/// </summary>
		/// <value>
		/// The search button CSS style.
		/// </value>
		public string SearchButtonCssStyle { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="KescoSelect"/> is required.
		/// </summary>
		/// <value>
		///   <c>true</c> if required; otherwise, <c>false</c>.
		/// </value>
		public bool Required { get; set; }

		/// <summary>
		/// Gets the client events.
		/// </summary>
		public KescoSelectClientEvents ClientEvents { get; internal set; }

		/// <summary>
		/// Возвращает или устанавливает список ссылок, отображаемый в нижнем колонтитуле 
		/// окна автозавершения выбора
		/// </summary>
		/// <value>
		/// Список ссылок, отображаемых в нижнем колонтитуле 
		/// </value>
		public List<KescoSelectLink> Links { get; set; }

		/// <summary>
		/// Возвращает или устанавливает опции показа кнопок.
		/// </summary>
		/// <value>
		/// Опции показа кнопок.
		/// </value>
		public KescoSelectButtons Buttons { get; set; }

		/// <summary>
		/// Возвращает или устанавливает значение для элемента управления
		/// </summary>
		/// <value>
		/// Значение для элемента управления
		/// </value>
		public string Value { get; set; }

		/// <summary>
		/// Возвращает или устанавливает отображаемое значение для элемента управления
		/// </summary>
		/// <value>
		/// Отображаемое значение для элемента управления
		/// </value>
		public string DisplayValue { get; set; }

		/// <summary>
		/// Создаёт экземпляр элемента управления <see cref="KescoSelect"/>.
		/// </summary>
		/// <param name="viewContext">Представляет контекст представления <see cref="ViewContext"/> для элемента управления.</param>
		public KescoSelect(ViewContext viewContext)
			: base(viewContext)
		{
			ClientEvents = new KescoSelectClientEvents();
			Links = new List<KescoSelectLink>();
		}

		/// <summary>
		/// Пишет HTML код, представляющий элемент управления.
		/// </summary>
		/// <param name="writer">Экземпляр класса <see cref="HtmlTextWriter"/></param>
		protected override void WriteHtml(HtmlTextWriter writer)
		{
			KescoSelectHtmlBuilder builder = new KescoSelectHtmlBuilder(this);
			writer.Write(builder.TextBoxTag());
			base.WriteHtml(writer);
		}

		/// <summary>
		/// Пишет скрипт инициализаии элемента управления.
		/// </summary>
		/// <param name="writer">Экземпляр класса <see cref="TextWriter"/></param>
		public override void WriteInitializationScript(TextWriter writer)
		{
			base.WriteInitializationScript(writer);
			//return;
			List<object> links = new List<object>();

			string onCommandHandler = String.IsNullOrEmpty(ClientEvents.OnCommand) ? "null" : ClientEvents.OnCommand;
			string onFocusItemHandler = String.IsNullOrEmpty(ClientEvents.OnFocusItem) ? "null" : ClientEvents.OnFocusItem;
			string onSelectItemHandler = String.IsNullOrEmpty(ClientEvents.OnSelectItem) ? "null" : ClientEvents.OnSelectItem;
			string onFormatItemHandler = String.IsNullOrEmpty(ClientEvents.OnFormatItem) ? "null" : ClientEvents.OnFormatItem;
			string onRequestHandler = String.IsNullOrEmpty(ClientEvents.OnRequest) ? "null" : ClientEvents.OnRequest;
			
			var options = new {
				required = Required,
				value = Value,
				displayValue = DisplayValue,
				source = AutocompleteUri,
				limit = AutocompleteMaxItems,
				keyField = KeyField,
				displayField = DisplayField,
				context = ClientSideControlContext,
				buttons = new {
					search = (Buttons & KescoSelectButtons.SearchButton) == KescoSelectButtons.SearchButton,
					view = (Buttons & KescoSelectButtons.ViewButton) == KescoSelectButtons.ViewButton
				},
				//searchButtonImage = SearchButtonImage,
				//refreshUri = RefreshUri,
				//refreshUriKeyField = RefreshUriKeyField,
				//refreshUriLabelField = RefreshUriLabelField,
				focus = "$$$OnFocusItem$$$",
				select = "$$$OnSelectItem$$$",
				formatItem = "$$$OnFormatItem$$$",
				command = "$$$OnCommand$$$",
				request = "$$$OnRequest$$$",
				links = new List<dynamic>(),
				localization = new {
					found= Kesco.Web.Mvc.UI.Controls.Localization.Resources.KescoLookUpControl_Found,
					notFound = Kesco.Web.Mvc.UI.Controls.Localization.Resources.KescoLookUpControl_NotFound,
					search = Kesco.Web.Mvc.UI.Controls.Localization.Resources.KescoLookUpControl_Search,
					view = Kesco.Web.Mvc.UI.Controls.Localization.Resources.KescoLookUpControl_View
				}
			};


			Links.ForEach((item) =>
			{
				options.links.Add(new { command = item.Command, text = item.Text, icon = item.Icon, show = (int) item.ShowCondition });
			});


			string jsonizedOptions = new JavaScriptSerializer().Serialize(options);
			jsonizedOptions = jsonizedOptions
				.Replace("\"$$$OnRequest$$$\"", onRequestHandler)
				.Replace("\"$$$OnCommand$$$\"", onCommandHandler)
				.Replace("\"$$$OnFocusItem$$$\"", onFocusItemHandler)
				.Replace("\"$$$OnSelectItem$$$\"", onSelectItemHandler)
				.Replace("\"$$$OnFormatItem$$$\"", onFormatItemHandler);
            if (IsSelfInitialized)
			    writer.WriteLine(@"
					$(document).ready(function() {{
					");
            writer.WriteLine(@"
                        $('#{0}').selectBox({1});
					    "
                , ID, jsonizedOptions);
            if (IsSelfInitialized)
                writer.WriteLine(@"
					}});
					");

		}
	}

}
