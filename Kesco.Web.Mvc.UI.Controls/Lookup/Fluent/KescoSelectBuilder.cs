using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc.UI.Fluent
{

	/// <summary>
	/// Класс, реализующий построитель для элемента управления KescoSelect.
	/// </summary>
	public class KescoSelectBuilder : ControlBuilderBase<KescoSelect, KescoSelectBuilder>
	{

		public KescoSelectBuilder(KescoSelect control) : base(control) { }

		/// <summary>
		/// Устанавливает значение для элемента управления, включая отображаемое значение
		/// </summary>
		/// <param name="value">Значение, которое нужно установить для элемента управления</param>
		/// <param name="displayValue">Отображаемое значение</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoSelectBuilder Value(string value, string displayValue)
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
		public KescoSelectBuilder Value(string value)
		{
			return Value(value, value);
		}

		/// <summary>
		/// Устанавливает отображаемое значение элемента управления.
		/// </summary>
		/// <param name="displayValue">Отображаемое значение элемента управления.</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoSelectBuilder DisplayValue(string displayValue)
		{
			this.control.DisplayValue = displayValue;
			return this;
		}

		/// <summary>
		/// Устанавливает поле для структуры данных элемента списка, чьё значение является значением поля.
		/// </summary>
		/// <param name="keyField">Название поля, чьё значение является отображаемым.</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoSelectBuilder KeyField(string keyField)
		{
			this.control.KeyField = keyField;
			return this;
		}

		/// <summary>
		/// Устанавливает поле для структуры данных элемента списка, чьё значение является отображаемым значением поля.
		/// </summary>
		/// <param name="displayField">Название поля, чьё значение является отображаемым.</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoSelectBuilder DisplayField(string displayField)
		{
			this.control.DisplayField = displayField;
			return this;
		}

		/// <summary>
		/// Устанавливает CSS стиль для кнопки поиска.
		/// </summary>
		/// <param name="cssStyle">CSS стиль.</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoSelectBuilder SearchButtonCssStyle(string cssStyle)
		{
			this.control.SearchButtonCssStyle = cssStyle;
			return this;
		}

		/// <summary>
		/// Устанавливает URI запроса для обновления.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <param name="keyField">The key field.</param>
		/// <param name="labelField">The label field.</param>
		/// <returns>
		/// Построитель элемента управления
		/// </returns>
		public KescoSelectBuilder RefreshUri(string uri, string keyField, string labelField)
		{
			this.control.RefreshUri = uri;
			this.control.RefreshUriKeyField = keyField;
			this.control.RefreshUriLabelField = labelField;
			return this;
		}

		/// <summary>
		/// Указывает показать кнопку просмотра выбранного элемента.
		/// </summary>
		/// <returns>Построитель элемента управления</returns>
		public KescoSelectBuilder ShowViewButton()
		{
			return ShowViewButton(true);
		}

		/// <summary>
		/// Указывает показать или нет кнопку просмотра выбранного элемента.
		/// </summary>
		/// <remarks>
		/// Данный метод позволяет указать необходимо ли показывать кнопку просмотра
		/// выбранного элемента или нет. Если указано показать, то при нажатии на данную
		/// кнопку элементу управления посылается команда <b>view</b>. Пользователь вправе
		/// сам определить необходимые действия в обработчики события <see
		/// cref="P:Kesco.Web.Mvc.UI.KescoLookupTextBoxClientEvents.OnCommand">KescoLookupTextBoxClientEvents.OnCommand</see>
		/// </remarks>
		/// <param name="show">Если установлено в <c>true</c>, то кнопка поиска будет показана.</param>
		/// <returns>
		/// Построитель элемента управления
		/// </returns>
		/// <seealso
		/// cref="P:Kesco.Web.Mvc.UI.KescoLookupTextBoxClientEvents.OnCommand">KescoLookupTextBoxClientEvents.OnCommand</seealso>
		public KescoSelectBuilder ShowViewButton(bool show = true)
		{
			if (show) {
				this.control.Buttons |= KescoSelectButtons.ViewButton;
			} else
				this.control.Buttons &= ~KescoSelectButtons.ViewButton;
			return this;
		}

		/// <summary>
		/// Указывает показать или нет кнопку поиска.
		/// </summary>
		/// <param name="show">Если установлено в <c>true</c>, то кнопка поиска будет показана.</param>
		/// <returns>
		/// Построитель элемента управления
		/// </returns>
		public KescoSelectBuilder ShowSearchButton(bool show = true)
		{
			if (show) {
				this.control.Buttons |= KescoSelectButtons.SearchButton;
			} else
				this.control.Buttons &= ~KescoSelectButtons.SearchButton;
			return this;
		}

        /// <summary>
        /// Указывает, что элемент требует указать значение.
        /// </summary>
        /// <returns>Построитель элемента управления</returns>
		public KescoSelectBuilder Required()
		{
			return Required(true);
		}

		/// <summary>
		/// Указывает является ли элемент обязательным или нет.
		/// </summary>
        /// <param name="isRequired">Если установлено в <c>true</c>, то требуется указать значение.</param>
		/// <returns>
		/// Построитель элемента управления
		/// </returns>
		public KescoSelectBuilder Required(bool isRequired)
		{
			this.control.Required = isRequired;
			return this;
		}


		/// <summary>
		/// Autocompletes the specified URI.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoSelectBuilder Autocomplete(string uri)
		{
			this.control.AutocompleteUri = uri;
			return this;
		}

		/// <summary>
		/// Устанавливает настройки для получения списка автозаполнения 
		/// </summary>
		/// <param name="uri">URL-адрес, возвращаюший список автозаполнения</param>
		/// <param name="maxItems">Максимальное кол-во результатов поиска</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoSelectBuilder Autocomplete(string uri, int maxItems) {
			this.control.AutocompleteUri = uri;
			this.control.AutocompleteMaxItems = maxItems;
			return this;
		}

		/// <summary>
		/// Позволяет настроить обрабочитки клиентских событий
		/// </summary>
		/// <param name="customize">Действие, устанавливающее обработчики клиентских событий</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoSelectBuilder ClientEvents(Action<KescoSelectClientEvents> customize)
		{
			if (customize != null) {
				customize(this.control.ClientEvents);
			}
			return this;
		}


        /// <summary>
        /// Добавляет ссылку к выпадающему меню автозавершения
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="text">Текст пункта меню</param>
        /// <param name="icon">CSS класс иконки.</param>
        /// <param name="showCondition">Условия показа.</param>
        /// <returns>
        /// Построитель элемента управления
        /// </returns>
		public KescoSelectBuilder AddLink(string command, string text, string icon, KescoSelectLinkShowCondition showCondition = KescoSelectLinkShowCondition.Always)
		{
            this.control.Links.Add(new KescoSelectLink { 
                Command = command, Text = text, Icon = icon, ShowCondition = showCondition 
            });
			return this;

		}

		/// <summary>
		/// Устанавливает контекст для клиентского элемента управления
		/// </summary>
		/// <param name="clientContext">Контекст для клиентского элемента управления.</param>
		/// <returns>Построитель элемента управления</returns>
		public KescoSelectBuilder ClientContext(object clientContext)
		{
			this.control.ClientSideControlContext = clientContext;
			return this;

		}
	}
}
