using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc.UI.Fluent
{

	/// <summary>
	/// Класс, реализующий построитель для элемента управления KescoSelectTextBox.
	/// </summary>
	public class KescoSelectTextBoxBuilder : ControlBuilderBase<KescoSelectTextBox, KescoSelectTextBoxBuilder>
	{

		public KescoSelectTextBoxBuilder(KescoSelectTextBox control) : base(control) { }

		/// <summary>
		/// Устанавливает значение для элемента управления, включая отображаемое значение
		/// </summary>
		/// <param name="value">Значение, которое нужно установить для элемента управления</param>
		/// <param name="displayValue">Отображаемое значение</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoSelectTextBoxBuilder Value(string value, string displayValue)
		{
			this.control.Value = value;
			this.control.DisplayValue = displayValue;
			return this;
		}

		/// <summary>
		/// Устанавливает значение для элемента управления, включая отображаемое значение
		/// </summary>
		/// <param name="value">Значение, которое нужно установить для элемента управления</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoSelectTextBoxBuilder Value(string value)
		{
			return Value(value, value);
		}

		/// <summary>
		/// Устанавливает отображаемое значение элемента управления.
		/// </summary>
		/// <param name="displayValue">Отображаемое значение элемента управления.</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoSelectTextBoxBuilder DisplayValue(string displayValue)
		{
			this.control.DisplayValue = displayValue;
			return this;
		}

		/// <summary>
		/// Keys the field.
		/// </summary>
		/// <param name="keyField">The key field.</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoSelectTextBoxBuilder KeyField(string keyField)
		{
			this.control.KeyField = keyField;
			return this;
		}

		/// <summary>
		/// Displays the field.
		/// </summary>
		/// <param name="displayField">The display field.</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoSelectTextBoxBuilder DisplayField(string displayField)
		{
			this.control.DisplayField = displayField;
			return this;
		}

		/// <summary>
		/// Устанавливает CSS стиль для кнопки поиска.
		/// </summary>
		/// <param name="cssStyle">CSS стиль.</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoSelectTextBoxBuilder SearchButtonCssStyle(string cssStyle)
		{
			this.control.SearchButtonCssStyle = cssStyle;
			return this;
		}

		// font-family: Verdana; font-size: 8pt; width: 20px; height: 17px;

		/// <summary>
		/// Clients the data bind.
		/// </summary>
		/// <param name="form">The form.</param>
		/// <param name="valueExpression">The value expression.</param>
		/// <param name="labelExpression">The label expression.</param>
		/// <returns>Построитель элемента управления</returns>
		//public KescoSelectTextBoxBuilder ClientDataBind(KescoForm form, string valueExpression, string labelExpression)
		//{
		//    form.AddDataBindClientScriptBlock("$('#" + this.control.ID + "', this).val(" + valueExpression + ");");
		//    form.AddDataBindClientScriptBlock("$('#" + this.control.ID + "___AUTOCOMPLETE', this).val(" + labelExpression + ");");
		//    return this;
		//}

		/// <summary>
		/// Устанавливает URI запроса для обновления.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <returns>
		/// Построитель элемента управления
		/// </returns>
		public KescoSelectTextBoxBuilder RefreshUri(string uri, string keyField, string labelField)
		{
			this.control.RefreshUri = uri;
			this.control.RefreshUriKeyField = keyField;
			this.control.RefreshUriLabelField = labelField;
			return this;
		}

		/// <summary>
		/// Autocompletes the specified URI.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoSelectTextBoxBuilder Autocomplete(string uri)
		{
			this.control.AutocompleteUri = uri;
			return this;
		}

		/// <summary>
		/// Autocompletes the specified URI.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <param name="maxItems">The max items.</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoSelectTextBoxBuilder Autocomplete(string uri, int maxItems) {
			this.control.AutocompleteUri = uri;
			this.control.AutocompleteMaxItems = maxItems;
			return this;
		}

		public KescoSelectTextBoxBuilder ClientEvents(Action<KescoSelectTextBoxClientEvents> customize)
		{
			if (customize != null) {
				customize(this.control.ClientEvents);
			}
			return this;
		}

		/// <summary>
		/// Detailses the link.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <param name="caption">The caption.</param>
		/// <param name="dialogTitle">The dialog title.</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoSelectTextBoxBuilder DetailsLink(string uri, string caption, string dialogTitle)
		{
			this.control.DetailsLink = new DialogLinkItem { Caption = caption, Uri = uri, DialogTitle = dialogTitle, OpenAsModal = true };
			return this;

		}

		/// <summary>
		/// Detailses the link.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <param name="caption">The caption.</param>
		/// <param name="dialogTitle">The dialog title.</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoSelectTextBoxBuilder DetailsLink(string uri, string caption, string dialogTitle, string cssStyle)
		{
			this.control.DetailsLinkCssStyle = cssStyle;
			this.control.DetailsLink = new DialogLinkItem { Caption = caption, Uri = uri, DialogTitle = dialogTitle, OpenAsModal = true };
			return this;

		}

		/// <summary>
		/// Создаёт ссылку для дополнительной информации.
		/// </summary>
		/// <param name="uri">Ссылка.</param>
		/// <param name="caption">Заголовок ссылки</param>
		/// <param name="dialogTitle">Заголовок диалогового окна</param>
		/// <param name="dialogWidth">Ширина диалогового окна.</param>
		/// <param name="dialogHeight">Высота диалогового окна.</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoSelectTextBoxBuilder DetailsLink(string uri, string caption, string dialogTitle, int? dialogWidth, int? dialogHeight, string cssStyle)
		{
			this.control.DetailsLinkCssStyle = cssStyle;
			this.control.DetailsLink = new DialogLinkItem { Caption = caption, Uri = uri, DialogTitle = dialogTitle, DialogWidth = dialogWidth, DialogHeight = dialogHeight, OpenAsModal = true };
			return this;

		}

		/// <summary>
		/// Adds the link.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <param name="caption">The caption.</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoSelectTextBoxBuilder AddLink(string uri, string caption)
		{
			this.control.FooterLinks.Add(new DialogLinkItem { Caption = caption, Uri = uri, OpenAsModal = false });
			return this;

		}

		/// <summary>
		/// Adds the dialog link.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <param name="caption">The caption.</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoSelectTextBoxBuilder AddDialogLink(string uri, string caption)
		{
			
			return AddDialogLink(uri, caption, null, null, null);

		}

		/// <summary>
		/// Adds the dialog link.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <param name="caption">The caption.</param>
		/// <param name="dialogTitle">The dialog title.</param>
		/// <param name="dialogWidth">Width of the dialog.</param>
		/// <param name="dialogHeight">Height of the dialog.</param>
		/// <returns></returns>
		public KescoSelectTextBoxBuilder AddDialogLink(string uri, string caption, string dialogTitle, int? dialogWidth, int? dialogHeight)
		{
			this.control.FooterLinks.Add(new DialogLinkItem { Caption = caption, Uri = uri, DialogTitle = dialogTitle, DialogWidth = dialogWidth, DialogHeight = dialogHeight, OpenAsModal = true });
			return this;

		}

	}
}
