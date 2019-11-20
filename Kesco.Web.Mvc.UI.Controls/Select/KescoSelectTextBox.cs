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
	public class KescoSelectTextBoxClientEvents
	{
		public string OnSelect { get; set; }
		public string FormatItem { get; set; }
	}

	/// <summary>
	/// Класс представлет элемент управления KescoSelect. 
	/// Данный элемент упраления реализует расширенную функциональность текстового поля 
	/// с поддержкой автозавершения выбора из списка поиска.
	/// </summary>
	[Obsolete("Неиспользовать!!!. Использовать KescoSelect. ")]
	public class KescoSelectTextBox : ControlBase
	{
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

		public string SearchButtonImage { get; set; }
		public string SearchButtonCssStyle { get; set; }

		public KescoSelectTextBoxClientEvents ClientEvents { get; internal set; }

		/// <summary>
		/// Возвращает или устанавливает ссылку (<see cref="DialogLinkItem"/>) 
		/// на страницу с подробной информацией об выбранном элементе.
		/// </summary>
		/// <value>
		/// Cсылку на страницу с подробной информацией
		/// </value>
		/// <remarks>
		/// Ссылка открывается в диалоговом окне.
		/// </remarks>
		public DialogLinkItem DetailsLink { get; set; }

		/// <summary>
		/// Возвращает или устанавливает CSS стиль для кнопки просмотра, реализующего элемент управления.
		/// </summary>
		/// <value>
		/// CSS стиль
		/// </value>
		/// <remarks>
		/// Аналогичен HTML атрибуту style. 
		/// <c>&lt;input type="text" style="" /%gt;</c>
		/// </remarks>
		public string DetailsLinkCssStyle { get; set; }

		/// <summary>
		/// Возвращает или устанавливает текст, отображаемый в нижнем колонтитуле 
		/// окна автозавершения выбора
		/// </summary>
		/// <value>
		/// Текст, отображаемый в нижнем колонтитуле 
		/// </value>
		public string FooterText { get; set; }

		/// <summary>
		/// Возвращает или устанавливает список ссылок, отображаемый в нижнем колонтитуле 
		/// окна автозавершения выбора
		/// </summary>
		/// <value>
		/// Список ссылок, отображаемых в нижнем колонтитуле 
		/// </value>
		public List<DialogLinkItem> FooterLinks { get; set; }

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
		/// Создаёт экземпляр элемента управления <see cref="KescoSelectTextBox"/>.
		/// </summary>
		/// <param name="viewContext">Представляет контекст представления <see cref="ViewContext"/> для элемента управления.</param>
		public KescoSelectTextBox(ViewContext viewContext)
			: base(viewContext)
		{
			ClientEvents = new KescoSelectTextBoxClientEvents();
			FooterLinks = new List<DialogLinkItem>();
			DetailsLink = new DialogLinkItem();
		}

		/// <summary>
		/// Пишет HTML код, представляющий элемент управления.
		/// </summary>
		/// <param name="writer">Экземпляр класса <see cref="HtmlTextWriter"/></param>
		protected override void WriteHtml(HtmlTextWriter writer)
		{
			KescoSelectTextBoxHtmlBuilder builder = new KescoSelectTextBoxHtmlBuilder(this);
			writer.Write(builder.TextBoxTag());
			writer.Write(builder.SearchButtonTag());
			writer.Write(builder.DetailsButtonTag());
			writer.Write(builder.HiddenTag());
			base.WriteHtml(writer);
		}

		/// <summary>
		/// Пишет скрипт инициализаии элемента управления.
		/// </summary>
		/// <param name="writer">Экземпляр класса <see cref="TextWriter"/></param>
		public override void WriteInitializationScript(TextWriter writer)
		{
			base.WriteInitializationScript(writer);
			List<object> links = new List<object>();
			dynamic detailsLink = null;
			if (DetailsLink != null)
			{
				detailsLink = new { 
					caption = DetailsLink.Caption, 
					uri = DetailsLink.Uri, 
					dialogTitle = DetailsLink.DialogTitle, 
					dialogWidth = DetailsLink.DialogWidth, 
					dialogHeight = DetailsLink.DialogHeight };
			}

			string onselectHandler = String.IsNullOrEmpty(ClientEvents.OnSelect) ? "null" : ClientEvents.OnSelect;
			string formatItemHandler = String.IsNullOrEmpty(ClientEvents.FormatItem) ? "null" : ClientEvents.FormatItem;

			var options = new {
				onselect = "$$$OnSelect$$$",
				formatItemFunc = "$$$formatItemFunc$$$",
				value = Value,
				displayValue = DisplayValue,
				uri = AutocompleteUri,
				limit = AutocompleteMaxItems,
				keyField = KeyField,
				displayField = DisplayField,
				searchButtonImage = SearchButtonImage,
				refreshUri = RefreshUri,
				refreshUriKeyField = RefreshUriKeyField,
				refreshUriLabelField = RefreshUriLabelField,
				detailsLink = detailsLink,
				footerLinks = new List<dynamic>(),
				resources = new {
					searchTitle = Kesco.Web.Mvc.UI.Controls.Localization.Resources.KescoSelectControl_SearchResult,
					NoSourceString = Kesco.Web.Mvc.UI.Controls.Localization.Resources.KescoSelectControl_NoSourceString,
					foundMore = Kesco.Web.Mvc.UI.Controls.Localization.Resources.KescoSelectControl_foundMore,
					foundRecordsMessage = Kesco.Web.Mvc.UI.Controls.Localization.Resources.KescoSelectControl_foundRecordsMessage
				}
			};


			FooterLinks.ForEach((item) =>
			{
				options.footerLinks.Add(new { caption = item.Caption, uri = item.Uri, dialogTitle = item.DialogTitle, dialogWidth = item.DialogWidth, dialogHeight = item.DialogHeight });
			});


			string jsonizedOptions = new JavaScriptSerializer().Serialize(options);
			jsonizedOptions = jsonizedOptions
				.Replace("\"$$$OnSelect$$$\"", onselectHandler)
				.Replace("\"$$$formatItemFunc$$$\"", formatItemHandler);

			writer.WriteLine(@"
					$(document).ready(function() {{
						InitLookupTextBox('#{0}___AUTOCOMPLETE', '#{0}', '#{0}___SEARCH_BUTTON', {1});
					}});
					"
				, ID, jsonizedOptions);

		}
	}

}
