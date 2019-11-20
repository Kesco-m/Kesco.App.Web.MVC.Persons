using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.WebPages;
using Kesco.Web.Mvc.UI.Grid;
using Kesco.Web.Mvc.UI;
using Kesco.Web.Mvc.UI.Fluent;

namespace Kesco.Web.Mvc
{


	/// <summary>
	/// Класс реализует методы расширения класса HtmlHelper,
	/// которые позволяют создать элемент управления KescoSelect.
	/// </summary>
	/// <remarks></remarks>
	public static class HtmlHelperExtensions
	{
		static MvcHtmlString EmptyMvcHtmlString = new MvcHtmlString(string.Empty);

		#region Методы-расширения для формирования HTML-атрибутов

		/// <summary>
		/// Merges the HTML attributes.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="mergeWithHtmlAttributes">HTML attributes to merge with.</param>
		/// <returns></returns>
		public static IDictionary<string, object> MergeHtmlAttributes(
					this HtmlHelper htmlHelper,
					object mergeWithHtmlAttributes
			)
		{
			return MergeHtmlAttributes(htmlHelper, null, mergeWithHtmlAttributes);
		}
		/// <summary>
		/// Объединяет HTML-аттрибуты .
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="htmlAttributes">The HTML attributes.</param>
		/// <param name="mergeWithHtmlAttributes">The merge with HTML attributes.</param>
		/// <returns></returns>
		public static IDictionary<string, object> MergeHtmlAttributes(
					this HtmlHelper htmlHelper, 
					IDictionary<string, object> htmlAttributes,
					object mergeWithHtmlAttributes
			)
		{

			htmlAttributes = htmlAttributes ?? new Dictionary<string, object>();

			if (mergeWithHtmlAttributes != null) {
				if (mergeWithHtmlAttributes is IDictionary<string, object>) {
					htmlAttributes.Merge((IDictionary<string, object>)mergeWithHtmlAttributes);
				} else {
					htmlAttributes.Merge(mergeWithHtmlAttributes);
				}
			}

			return htmlAttributes;
		}

		#endregion

		#region Методы-расширения для регистрации/вывода скриптов
		/// <summary>
		/// Регистрирует внешний скрипт с заданным ключом для последующего вывода на странице
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="key">The key.</param>
		/// <param name="script">Javascript код.</param>
		public static void RegisterExternalScript(this HtmlHelper htmlHelper, string key, string hrefOrScriptTag)
		{
			var dictionary = htmlHelper.ViewContext.HttpContext.Items["ExternalScripts"] as OrderedDictionary
					?? new OrderedDictionary();
			
			if (!dictionary.Contains(key)) {
				hrefOrScriptTag = hrefOrScriptTag.Trim();
				if (hrefOrScriptTag.StartsWith("<script")) // <script..>...</script> или несколько скрипт-блоков
					dictionary.Add(key, hrefOrScriptTag);
				else // предполагаем, что hrefOrScriptTag - ссылка
					dictionary.Add(key, "\t\t<script type='text/javascript' src='{0}'></script>\n".FormatWith(hrefOrScriptTag));
			}

			htmlHelper.ViewContext.HttpContext.Items["ExternalScripts"] = dictionary;
		}

		public static MvcHtmlString ExternalScript(this HtmlHelper htmlHelper, string key, Func<dynamic, HelperResult> template)
		{
			if (!htmlHelper.ViewContext.HttpContext.Request.IsAjaxRequest()) {
				RegisterExternalScript(htmlHelper, key, template(null).ToHtmlString());
			}
			return EmptyMvcHtmlString;
		}

		/// <summary>
		/// Регистрирует общий код скрипта с заданным ключом для последующего вывода на странице
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="key">The key.</param>
		/// <param name="script">Javascript код.</param>
		public static void RegisterCommonScriptCode(this HtmlHelper htmlHelper, string key, string script)
		{
			htmlHelper.ViewContext.HttpContext.RegisterCommonScript(key, script);
		}

		/// <summary>
		/// Регистрирует общий код скрипта с заданным ключом для последующего вывода на странице
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="key">The key.</param>
		/// <param name="script">Javascript код.</param>
		public static void RegisterCommonScriptCode(this HtmlHelper htmlHelper, string key, Func<string> scriptWriter)
		{
			htmlHelper.ViewContext.HttpContext.RegisterCommonScript(key, scriptWriter);
		}

		/// <summary>
		/// Регистрирует общий javascript-код для текущего запроса
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="key">Уникальный ключ для текущего кода</param>
		/// <param name="template">Шаблон. Делегат для формирования шаблона.</param>
		/// <returns></returns>
		public static MvcHtmlString CommonScriptCode(
				this HtmlHelper htmlHelper,
				string key,
				Func<dynamic, HelperResult> template
			)
		{
			if (!htmlHelper.ViewContext.HttpContext.Request.IsAjaxRequest()) {
				RegisterCommonScriptCode(
						htmlHelper, 
						key, 
						() => template(null).ToHtmlString().Replace("<script>", "").Replace("</script>", "")
					);
			}
			return EmptyMvcHtmlString;
		}



		/// <summary>
		/// Регистрирует скрипт для последующего вывода на странице
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="script">Javascript код.</param>
		public static void RegisterScript(this HtmlHelper htmlHelper, string script)
		{
			var list = htmlHelper.ViewContext.HttpContext.Items["Scripts"] as IList<string> ?? new List<string>();
			list.Add(script);
			htmlHelper.ViewContext.HttpContext.Items["Scripts"] = list;
		}

		/// <summary>
		/// Регистрирует javascript-код, который должен быть выполнен после загрузки страницы.
		/// Оборачивается в $(document).ready(function() { ... })
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="template">Шаблон. Делегат, возвращающий javascript-код</param>
		/// <returns></returns>
		public static MvcHtmlString ScriptCode(
				this HtmlHelper htmlHelper,
				Func<dynamic, HelperResult> template
			)
		{
			if (!htmlHelper.ViewContext.HttpContext.Request.IsAjaxRequest()) {
				RegisterScript(
						htmlHelper,
						template(null).ToHtmlString().Replace("<script>", "").Replace("</script>", "")
					);
			}
			return EmptyMvcHtmlString;
		}


		/// <summary>
		/// Выводит скрипты, созданные и сохраннёные в элементах контекста
		/// </summary>
		/// <param name="htmlHelper">HTML помощник.</param>
		/// <returns>Html со скиптами</returns>
		public static IHtmlString RenderScripts(this HtmlHelper htmlHelper)
		{
			var scripts = htmlHelper.ViewContext.HttpContext.Items["Scripts"] as IList<string>;
			var commonScripts = htmlHelper.ViewContext.HttpContext.Items["CommonScripts"] as OrderedDictionary;
			var externalScripts = htmlHelper.ViewContext.HttpContext.Items["ExternalScripts"] as OrderedDictionary;

			if (scripts != null || commonScripts != null || externalScripts != null) {
				var builder = new StringBuilder();

				if (externalScripts != null) {
					builder.AppendLine();
					foreach (var script in externalScripts.Values) {
						builder.Append(script);
					}
				}

				builder.AppendLine("<script type='text/javascript'>");
				builder.AppendLine("// Вывод скриптов сохраненных в контексте.");
				if (commonScripts != null) {
					foreach (var script in commonScripts.Values) {
						builder.AppendLine((string) script);
					}
				}
				if (scripts != null) {
					builder.AppendLine("\t$(document).ready(function() {");
					foreach (var script in scripts) {
						builder.AppendLine(script);
					}
					builder.AppendLine("\t});");
				}
				builder.AppendLine("</script>");

				return new MvcHtmlString(builder.ToString());
			}
			return null;
		}

		#endregion

		/// <summary>
		/// Создаёт связанную модель с HTML формой
		/// </summary>
		/// <param name="htmlHelper">Экземпляр объекта <see cref="System.Web.Mvc">HtmlHelper</see>.</param>
		/// <param name="name">Имя модели</param>
		/// <param name="controlID">Задаёт идентификатор элемента управления на клиентской странице. Опционален.</param>
		/// <returns>
		/// Возвращает построитель модели связи <see cref="KescoDatePickerBuilder">KescoDataLinkBuilder</see> для формы
		/// </returns>
		public static DataLinkBuilder<TModel> KescoDataLink<TModel>(this HtmlHelper htmlHelper,
				ViewDataDictionary<TModel> viewData,
				string modelName, string formID)
		{

			DataLinkBuilder<TModel> builder = new DataLinkBuilder<TModel>(viewData);
			
			builder.ModelName = modelName;

			if (String.IsNullOrEmpty(formID))
				builder.FormID = htmlHelper.ViewContext.FormContext.FormId;
			else
				builder.FormID = formID;
			
			return builder;
		}

		/// <summary>
		/// Создаёт связанную модель с HTML формой
		/// </summary>
		/// <param name="htmlHelper">Экземпляр объекта <see cref="System.Web.Mvc">HtmlHelper</see>.</param>
		/// <param name="name">Имя модели</param>
		/// <param name="controlID">Задаёт идентификатор элемента управления на клиентской странице. Опционален.</param>
		/// <returns>
		/// Возвращает построитель модели связи <see cref="KescoDatePickerBuilder">KescoDataLinkBuilder</see> для формы
		/// </returns>
		public static DataLinkBuilder<TModel> KescoDataLink<TModel>(this HtmlHelper htmlHelper,
				ViewDataDictionary<TModel> viewData,
				string modelName)
		{
			return KescoDataLink<TModel>(htmlHelper, viewData, modelName, null);
		}

		/*
		public static TBuilder CreateBuilder<TControl, TBuilder>(this HtmlHelper htmlHelper,
				string name, string controlID)
			where TControl : ControlBase
			where TBuilder: ControlBuilderBase<TControl, TBuilder>
		{
			TControl control = new TControl(htmlHelper.ViewContext);
			control.Name = name;

			if (!String.IsNullOrWhiteSpace(controlID)) {
				control.HtmlAttributes.Add("id", controlID);
			}

			TBuilder builder = new TBuilder(control);
			return builder;
		}
		*/

		/// <summary>
		/// Выводит текстовое поле.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="htmlAttributes">The HTML attributes.</param>
		/// <returns></returns>
		public static MvcHtmlString KescoTextBoxFor<TModel, TProperty>(
			this HtmlHelper<TModel> htmlHelper,
			Expression<Func<TModel, TProperty>> expression,
			object htmlAttributes
		)
		{
			var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
			var prop = metadata.ContainerType.GetMember(metadata.PropertyName).FirstOrDefault();
			StringLengthAttribute stringLength = null;
			stringLength = prop
				.GetCustomAttributes(typeof(StringLengthAttribute), true)
				.FirstOrDefault() as StringLengthAttribute;

			var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
			if (stringLength != null) {
				attributes.Add("maxlength", stringLength.MaximumLength);
			}
			return htmlHelper.TextBoxFor(expression, attributes);
		}

		#region KescoTabs

		/// <summary>
		/// Создаёт элемент управления <see
		/// cref="KescoTabs">KescoTabs</see>
		/// </summary>
		/// <param name="htmlHelper">Экземпляр объекта <see cref="System.Web.Mvc">HtmlHelper</see>.</param>
		/// <param name="name">Имя для элемента управления</param>
		/// <param name="controlID">Задаёт идентификатор элемента управления на клиентской странице. Опционален.</param>
		/// <returns>
		/// Возвращает построитель <see cref="KescoTabsBuilder">KescoTabsBuilder</see> для элемента
		/// управления <see cref="KescoTabs">KescoTabs</see>
		/// </returns>
		public static KescoTabsBuilder KescoTabs(this HtmlHelper htmlHelper,
				string name, string controlID)
		{
			KescoTabs control = new KescoTabs(htmlHelper.ViewContext);
			control.Name = name;

			if (!String.IsNullOrWhiteSpace(controlID)) {
				control.HtmlAttributes.Add("id", controlID);
			}

			KescoTabsBuilder builder = new KescoTabsBuilder(control);
			return builder;
		}

		/// <summary>
		/// Создаёт элемент управления <see
		/// cref="KescoDatePicker">KescoTabs</see>
		/// </summary>
		/// <param name="htmlHelper">Экземпляр объекта 
		/// <see cref="System.Web.Mvc">HtmlHelper</see>.</param>
		/// <param name="name">Имя для элемента управления</param>
		/// <param name="controlID">Задаёт идентификатор элемента управления на клиентской странице. Опционален.</param>
		/// <returns>
		/// Возвращает построитель <see cref="KescoTabsBuilder" />KescoTabsBuilder</see> для элемента
		/// управления <see cref="KescoTabs">KescoTabs</see>
		/// </returns>
		public static KescoTabsBuilder KescoTabs(this HtmlHelper htmlHelper,
				string name)
		{
			return KescoTabs(htmlHelper, name, null);
		}

		#endregion

		#region KescoDateRange

		/// <summary>
		/// Создаёт элемент управления <see
		/// cref="KescoDateRange">KescoDateRange</see>
		/// </summary>
		/// <param name="htmlHelper">Экземпляр объекта <see cref="System.Web.Mvc">HtmlHelper</see>.</param>
		/// <param name="name">Имя для элемента управления</param>
		/// <param name="controlID">Задаёт идентификатор элемента управления на клиентской странице. Опционален.</param>
		/// <returns>
		/// Возвращает построитель <see cref="KescoDatePickerBuilder">KescoDateRangeBuilder</see> для элемента
		/// управления <see cref="KescoDateRange">KescoDateRange</see>
		/// </returns>
		public static KescoDateRangeBuilder KescoDateRange(this HtmlHelper htmlHelper,
				string name, string controlID)
		{
			KescoDateRange control = new KescoDateRange(htmlHelper.ViewContext);
			control.Name = name;

			if (!String.IsNullOrWhiteSpace(controlID)) {
				control.HtmlAttributes.Add("id", controlID);
			}
			
			KescoDateRangeBuilder builder = new KescoDateRangeBuilder(control);
			return builder;
		}

		/// <summary>
		/// Создаёт элемент управления <see
		/// cref="KescoDatePicker">KescoDatePicker</see>
		/// </summary>
		/// <param name="htmlHelper">Экземпляр объекта 
		/// <see cref="System.Web.Mvc">HtmlHelper</see>.</param>
		/// <param name="name">Имя для элемента управления</param>
		/// <param name="controlID">Задаёт идентификатор элемента 
		/// управления на клиентской странице. Опционален.</param>
		/// <returns>
		/// Возвращает построитель <see cref="KescoTreeViewBuilder"
		/// >KescoTreeViewBuilder</see> для элемента
		/// управления <see cref="KescoTreeView">KescoTreeView</see>
		/// </returns>
		public static KescoDateRangeBuilder KescoDateRange(this HtmlHelper htmlHelper,
				string name)
		{
			return KescoDateRange(htmlHelper, name, null);
		}

		#endregion

		#region KescoDatePicker

		/// <summary>
		/// Создаёт элемент управления <see
		/// cref="KescoDatePicker">KescoDatePicker</see>
		/// </summary>
		/// <param name="htmlHelper">Экземпляр объекта <see cref="System.Web.Mvc">HtmlHelper</see>.</param>
		/// <param name="name">Имя для элемента управления</param>
		/// <param name="controlID">Задаёт идентификатор элемента управления на клиентской странице. Опционален.</param>
		/// <returns>
		/// Возвращает построитель <see cref="KescoDatePickerBuilder">KescoDatePickerBuilder</see> для элемента
		/// управления <see cref="KescoTreeView">KescoTreeView</see>
		/// </returns>
		public static KescoDatePickerBuilder KescoDatePicker(this HtmlHelper htmlHelper,
				string name, string controlID)
		{
			KescoDatePicker control = new KescoDatePicker(htmlHelper.ViewContext);
			control.Name = name;

			if (!String.IsNullOrWhiteSpace(controlID)) {
				control.HtmlAttributes.Add("id", controlID);
			}

			KescoDatePickerBuilder builder = new KescoDatePickerBuilder(control);
			return builder;
		}

		/// <summary>
		/// Создаёт элемент управления <see
		/// cref="KescoDatePicker">KescoDatePicker</see>
		/// </summary>
		/// <param name="htmlHelper">Экземпляр объекта 
		/// <see cref="System.Web.Mvc">HtmlHelper</see>.</param>
		/// <param name="name">Имя для элемента управления</param>
		/// <param name="controlID">Задаёт идентификатор элемента 
		/// управления на клиентской странице. Опционален.</param>
		/// <returns>
		/// Возвращает построитель <see cref="KescoTreeViewBuilder"
		/// >KescoTreeViewBuilder</see> для элемента
		/// управления <see cref="KescoTreeView">KescoTreeView</see>
		/// </returns>
		public static KescoDatePickerBuilder KescoDatePicker(this HtmlHelper htmlHelper,
				string name)
		{
			return KescoDatePicker(htmlHelper, name, null);
		}

		#endregion

		#region KescoTreeView

		/// <summary>
		/// Создаёт элемент управления <see
		/// cref="KescoTreeView">KescoTreeView</see>
		/// </summary>
		/// <param name="htmlHelper">Экземпляр объекта <see cref="System.Web.Mvc">HtmlHelper</see>.</param>
		/// <param name="name">Имя для элемента управления</param>
		/// <param name="controlID">Задаёт идентификатор элемента управления на клиентской странице. Опционален.</param>
		/// <returns>
		/// Возвращает построитель <see
		/// cref="KescoTreeViewBuilder">KescoTreeViewBuilder</see> для элемента
		/// управления <see cref="KescoTreeView">KescoTreeView</see>
		/// </returns>
		public static KescoTreeViewBuilder KescoTreeView(this HtmlHelper htmlHelper,
				string name, string controlID)
		{
			KescoTreeView control = new KescoTreeView(htmlHelper.ViewContext);
			control.Name = name;

			if (!String.IsNullOrWhiteSpace(controlID)) {
				control.HtmlAttributes.Add("id", controlID);
			}

			KescoTreeViewBuilder builder = new KescoTreeViewBuilder(control);
			return builder;
		}

		/// <summary>
		/// Создаёт элемент управления <see
		/// cref="KescoTreeView">KescoTreeView</see>
		/// </summary>
		/// <param name="htmlHelper">Экземпляр объекта 
		/// <see cref="System.Web.Mvc">HtmlHelper</see>.</param>
		/// <param name="name">Имя для элемента управления</param>
		/// <param name="controlID">Задаёт идентификатор элемента 
		/// управления на клиентской странице. Опционален.</param>
		/// <returns>
		/// Возвращает построитель <see cref="KescoTreeViewBuilder"
		/// >KescoTreeViewBuilder</see> для элемента
		/// управления <see cref="KescoTreeView">KescoTreeView</see>
		/// </returns>
		public static KescoTreeViewBuilder KescoTreeView(this HtmlHelper htmlHelper,
				string name)
		{
			return KescoTreeView(htmlHelper, name, null);
		}

		#endregion

		#region Kesco Menu Html Helpers

		/// <summary>
		/// Создаёт элемент управления <see
		/// cref="KescoMenu">KescoMenu</see>
		/// </summary>
		/// <param name="htmlHelper">Экземпляр объекта <see cref="System.Web.Mvc">HtmlHelper</see>.</param>
		/// <param name="name">Имя для элемента управления</param>
		/// <param name="controlID">Задаёт идентификатор элемента управления на клиентской странице. Опционален.</param>
		/// <returns>
		/// Возвращает построитель <see
		/// cref="KescoMenuBuilder">KescoMenuBuilder</see> для элемента
		/// управления <see cref="KescoMenu">KescoMenu</see>
		/// </returns>
		public static KescoMenuBuilder KescoMenu(this HtmlHelper htmlHelper,
				string name, string controlID)
		{
			KescoMenu control = new KescoMenu(htmlHelper.ViewContext);
			control.Name = name;

			if (!String.IsNullOrWhiteSpace(controlID)) {
				control.HtmlAttributes.Add("id", controlID);
			}
			KescoMenuBuilder builder = new KescoMenuBuilder(control);
			return builder;
		}

		/// <summary>
		/// Создаёт элемент управления <see
		/// cref="KescoMenu">KescoMenu</see>
		/// </summary>
		/// <param name="htmlHelper">Экземпляр объекта <see cref="System.Web.Mvc">HtmlHelper</see>.</param>
		/// <param name="name">Имя для элемента управления</param>
		/// <returns>
		/// Возвращает построитель <see
		/// cref="KescoMenuBuilder">KescoMenuBuilder</see> для элемента
		/// управления <see cref="KescoMenu">KescoMenu</see>
		/// </returns>
		public static KescoMenuBuilder KescoMenu(this HtmlHelper htmlHelper,
				string name)
		{
			return KescoMenu(htmlHelper, name, null);
		}

		#endregion

		#region KescoButtonBar Html Helpers

		/// <summary>
		/// Создаёт элемент управления <see
		/// cref="KescoButtonBar">KescoButtonBar</see>
		/// </summary>
		/// <param name="htmlHelper">Экземпляр объекта <see cref="System.Web.Mvc">HtmlHelper</see>.</param>
		/// <param name="name">Имя для элемента управления</param>
		/// <param name="form">Reference to KescoForm instance. Optional</param>
		/// <param name="controlID">Задаёт идентификатор элемента управления на клиентской странице. Опционален.</param>
		/// <returns>
		/// Возвращает построитель <see
		/// cref="KescoButtonBarBuilder">KescoButtonBarBuilder</see> для элемента
		/// управления <see cref="KescoSelectTextBox">KescoButtonBar</see>
		/// </returns>
		public static KescoButtonBarBuilder KescoButtonBar(this HtmlHelper htmlHelper,
				string name, string controlID)
		{
			KescoButtonBar buttonBar = new KescoButtonBar(htmlHelper.ViewContext);
			buttonBar.Name = name;

			if (!String.IsNullOrWhiteSpace(controlID))
			{
				buttonBar.HtmlAttributes.Add("id", controlID);
			}
			KescoButtonBarBuilder builder = new KescoButtonBarBuilder(buttonBar);
			return builder;
		}

		/// <summary>
		/// Создаёт элемент управления <see
		/// cref="KescoButtonBar">KescoButtonBar</see>
		/// </summary>
		/// <param name="htmlHelper">Экземпляр объекта <see cref="System.Web.Mvc">HtmlHelper</see>.</param>
		/// <param name="name">Имя для элемента управления</param>
		/// <returns>
		/// Возвращает построитель <see
		/// cref="KescoButtonBarBuilder">KescoButtonBarBuilder</see> для элемента
		/// управления <see cref="KescoSelectTextBox">KescoButtonBar</see>
		/// </returns>
		public static KescoButtonBarBuilder KescoButtonBar(this HtmlHelper htmlHelper,
				string name)
		{
			return KescoButtonBar(htmlHelper, name, null);
		}

		#endregion

		#region KescoGrid Html Helpers

		public static MvcHtmlString KescoGridAutoCompleteEx(this HtmlHelper htmlHelper, JQAutoCompleteEx autoComplete, string id)
		{
			JQAutoCompleteExRenderer renderer = new JQAutoCompleteExRenderer(autoComplete);
			autoComplete.ID = id;
			return MvcHtmlString.Create(renderer.RenderHtml());
		}

		public static MvcHtmlString KescoGridAutoComplete(this HtmlHelper htmlHelper, JQAutoComplete autoComplete, string id)
		{
			JQAutoCompleteRenderer renderer = new JQAutoCompleteRenderer(autoComplete);
			autoComplete.ID = id;
			return MvcHtmlString.Create(renderer.RenderHtml());
		}

		/*public MvcHtmlString JQChart(Kesco.Web.Mvc.UI.JQChart chart, string id)
		{
			JQChartRenderer renderer = new JQChartRenderer(chart);
			chart.ID = id;
			return MvcHtmlString.Create(renderer.RenderHtml());
		}*/

		public static MvcHtmlString KescoGridDatePicker(this HtmlHelper htmlHelper, JQDatePicker datePicker, string id)
		{
			JQDatePickerRenderer renderer = new JQDatePickerRenderer(datePicker);
			datePicker.ID = id;
			return MvcHtmlString.Create(renderer.RenderHtml());
		}

		public static MvcHtmlString KescoGrid(this HtmlHelper htmlHelper, JQGrid grid)
		{
			JQGridRenderer renderer = new JQGridRenderer();
			RegisterCommonScriptCode(
					htmlHelper,
					htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(grid.ID),
					() => renderer.GetJavascriptCode(grid)
				);
			return MvcHtmlString.Create(renderer.RenderHtml(grid, false));
		}

		public static MvcHtmlString KescoGrid(this HtmlHelper htmlHelper, JQGrid grid, string id)
		{
			JQGridRenderer renderer = new JQGridRenderer();
			grid.ID = id;
			RegisterCommonScriptCode(
					htmlHelper,
					htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(grid.ID),
					() => renderer.GetJavascriptCode(grid)
				);
			return MvcHtmlString.Create(renderer.RenderHtml(grid, false));
		}

		public static MvcHtmlString KescoGridTree(this HtmlHelper htmlHelper, JQTreeView tree, string id)
		{
			JQTreeViewRenderer renderer = new JQTreeViewRenderer(tree);
			tree.ID = id;
			return MvcHtmlString.Create(renderer.RenderHtml());
		}

		#endregion

		#region KescoSelect html helper extensions

		/// <summary>
		/// Создаёт элемент управления <see
		/// cref="KescoSelectTextBox">KescoSelectTextBox</see>
		/// </summary>
		/// <param name="htmlHelper">Экземпляр объекта <see cref="System.Web.Mvc">HtmlHelper</see>.</param>
		/// <param name="name">Имя для элемента управления</param>
		/// <param name="controlID">Задаёт идентификатор элемента управления на клиентской странице. Опционален.</param>
		/// <returns>
		/// Возвращает построитель <see
		/// cref="KescoSelectTextBoxBuilder">KescoSelectTextBoxBuilder</see> для элемента
		/// управления <see cref="KescoSelectTextBox">KescoSelectTextBox</see>
		/// </returns>
		public static KescoSelectTextBoxBuilder KescoSelect(this HtmlHelper htmlHelper,
				string name, string controlID)
		{
			KescoSelectTextBox autocomplete = new KescoSelectTextBox(htmlHelper.ViewContext);
			autocomplete.Name = name;

			if (!String.IsNullOrWhiteSpace(controlID)) {
				autocomplete.HtmlAttributes.Add("id", controlID);
			}
			KescoSelectTextBoxBuilder builder = new KescoSelectTextBoxBuilder(autocomplete);
			return builder;
		}

		/// <summary>
		/// Создаёт элемент управления <see
		/// cref="KescoSelectTextBox">KescoSelectTextBox</see>
		/// </summary>
		/// <param name="htmlHelper">Экземпляр объекта <see cref="System.Web.Mvc">HtmlHelper</see>.</param>
		/// <param name="name">Имя для элемента управления</param>
		/// <returns>
		/// Возвращает построитель <see
		/// cref="KescoSelectTextBoxBuilder">KescoSelectTextBoxBuilder</see> для элемента
		/// управления <see cref="KescoSelectTextBox">KescoSelectTextBox</see>
		/// </returns>
		public static KescoSelectTextBoxBuilder KescoSelect(this HtmlHelper htmlHelper,
				string name)
		{
			return KescoSelect(htmlHelper, name, null);
		}

		/// <summary>
		/// Создаёт элемент управления <see
		/// cref="KescoSelectTextBox">KescoSelectTextBox</see>
		/// </summary>
		/// <param name="htmlHelper">Экземпляр объекта <see cref="System.Web.Mvc">HtmlHelper</see>.</param>
		/// <returns>
		/// Возвращает построитель <see
		/// cref="KescoSelectTextBoxBuilder">KescoSelectTextBoxBuilder</see> для элемента
		/// управления <see cref="KescoSelectTextBox">KescoSelectTextBox</see>
		/// </returns>
		public static KescoSelectTextBoxBuilder KescoSelect(this HtmlHelper htmlHelper)
		{
			return KescoSelect(htmlHelper, null, null);
		}

		#endregion

		#region KescoLookup Html Helpers

		/// <summary>
		/// Создаёт элемент управления <see
		/// cref="KescoSelect">KescoSelect</see>
		/// </summary>
		/// <param name="htmlHelper">Экземпляр объекта <see cref="System.Web.Mvc">HtmlHelper</see>.</param>
		/// <param name="name">Имя для элемента управления</param>
		/// <param name="controlID">Задаёт идентификатор элемента управления на клиентской странице. Опционален.</param>
		/// <returns>
		/// Возвращает построитель <see
		/// cref="KescoSelectBuilder">KescoSelectBuilder</see> для элемента
		/// управления <see cref="KescoSelect">KescoSelect</see>
		/// </returns>
		public static KescoSelectBuilder KescoLookup(this HtmlHelper htmlHelper,
				string name, string controlID)
		{
			KescoSelect autocomplete = new KescoSelect(htmlHelper.ViewContext);
			autocomplete.Name = name;

			if (!String.IsNullOrWhiteSpace(controlID)) {
				autocomplete.HtmlAttributes.Add("id", controlID);
			}
			KescoSelectBuilder builder = new KescoSelectBuilder(autocomplete);
			return builder;
		}

		/// <summary>
		/// Создаёт элемент управления <see
		/// cref="KescoSelect">KescoSelect</see>
		/// </summary>
		/// <param name="htmlHelper">Экземпляр объекта <see cref="System.Web.Mvc">HtmlHelper</see>.</param>
		/// <param name="name">Имя для элемента управления</param>
		/// <returns>
		/// Возвращает построитель <see
		/// cref="KescoSelectBuilder">KescoSelectBuilder</see> для элемента
		/// управления <see cref="KescoSelect">KescoSelect</see>
		/// </returns>
		public static KescoSelectBuilder KescoLookup(this HtmlHelper htmlHelper,
				string name)
		{
			return KescoLookup(htmlHelper, name, name);
		}

		#endregion

	}
}
