using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Kesco.Web.Mvc.ComponentModel.DataAnnotations;
using Kesco.Web.Mvc.Validation;
using System.Web.WebPages;
using System.Web;
using System.Collections.Specialized;

namespace Kesco.Web.Mvc
{
	public static class HtmlHelperExtensions
	{
		private const string SCRIPTBLOCK_BUILDER = "ScriptBlockBuilder";
		private const string ENUM_BUILDER = "EnumBuilder";
		private const string EXTERNAL_HUBS = "ExternalHubs";

		public static MvcHtmlString ScriptBlock<T>(
			this HtmlHelper<T> htmlHelper,
			Func<dynamic, HelperResult> template)
		{
			if (!htmlHelper.ViewContext.HttpContext.Request.IsAjaxRequest())
			{
				var scriptBuilder = htmlHelper.ViewContext.HttpContext.Items[SCRIPTBLOCK_BUILDER]
					as StringBuilder ?? new StringBuilder();

				scriptBuilder.Append(template(null).ToHtmlString().Replace("<script>", "").Replace("</script>", ""));

				htmlHelper.ViewContext.HttpContext.Items[SCRIPTBLOCK_BUILDER] = scriptBuilder;
			}
			return new MvcHtmlString(string.Empty);
		}

		public static void RegisterEnum<T>(this HtmlHelper htmlHelper) where T : struct
		{
			if (!htmlHelper.ViewContext.HttpContext.Request.IsAjaxRequest())
			{
				var scriptBuilder = htmlHelper.ViewContext.HttpContext.Items[ENUM_BUILDER]
				                    as StringBuilder ?? new StringBuilder();
				scriptBuilder.AppendLine("if(!ViewModel.Enum) ViewModel.Enum = {};");
				var enumeration = Activator.CreateInstance(typeof (T));
				var enums = typeof (T).GetFields().Where(t => t.IsLiteral).ToDictionary(x => x.Name, x => x.GetValue(enumeration));
				scriptBuilder.AppendLine("ViewModel.Enum." + typeof(T).Name + " = " + System.Web.Helpers.Json.Encode(enums) + " ;");
				htmlHelper.ViewContext.HttpContext.Items[ENUM_BUILDER] = scriptBuilder;
			}
		}

		public static void RegisterHub(this HtmlHelper htmlHelper, string key, string href)
		{
			var dictionary = htmlHelper.ViewContext.HttpContext.Items[EXTERNAL_HUBS] as OrderedDictionary
					?? new OrderedDictionary();

			if (!dictionary.Contains(key))
			{
				href = href.Trim();
				dictionary.Add(key, "\t\t<script type='text/javascript' src='{0}'></script>\n".FormatWith(href));
			}

			htmlHelper.ViewContext.HttpContext.Items[EXTERNAL_HUBS] = dictionary;
		}

		public static MvcHtmlString WriteHubs<T>(this HtmlHelper<T> htmlHelper)
		{
			var builder = new StringBuilder();
			var externalScripts = htmlHelper.ViewContext.HttpContext.Items[EXTERNAL_HUBS] as OrderedDictionary;

			if (externalScripts != null)
			{
				builder.AppendLine();
				foreach (var script in externalScripts.Values)
				{
					builder.Append(script);
				}
			}

			return new MvcHtmlString(builder.ToString());
		}

		public static MvcHtmlString WriteEnums<T>(this HtmlHelper<T> htmlHelper)
		{
			var scriptBuilder = htmlHelper.ViewContext.HttpContext.Items[ENUM_BUILDER]
					as StringBuilder ?? new StringBuilder();

			return new MvcHtmlString("<script type='text/javascript'>\r\n" + scriptBuilder.ToString() + "\r\n</script>");
		}

		public static MvcHtmlString WriteScriptBlocks<T>(this HtmlHelper<T> htmlHelper)
		{
			var scriptBuilder = htmlHelper.ViewContext.HttpContext.Items[SCRIPTBLOCK_BUILDER]
								as StringBuilder ?? new StringBuilder();

			return new MvcHtmlString("<script type='text/javascript'>\r\n" + scriptBuilder.ToString() + "\r\n</script>");
		}

		/// <summary>
		/// Подготавливает валидацию для конкретной ViewModel
		/// </summary>
		/// <param name="htmlHelper">Экземпляр объекта <see cref="System.Web.Mvc">HtmlHelper</see>.</param>
		/// <param name="viewModel">Экземпляр объекта <see cref="T">viewModel</see>.</param>
		public static void PrepareValidationFor<T>(this HtmlHelper<T> htmlHelper)
		where T: class, new()
		{
			var type = htmlHelper.ViewData.Model.GetType();
			var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			foreach (var property in properties)
			{
				var attr = property.GetCustomAttributes(typeof (ValidationResultsAttribute), false);
				if (attr.Any())
				{
					htmlHelper.RegisterValidationMessages(property.Name, htmlHelper.Display(property.Name));
					//if (htmlHelper.ViewData[property.Name] == null)
					//{
					//	htmlHelper.ViewData[property.Name] = htmlHelper.Display(property.Name);
					//}
				}
			}
		}

		/// <summary>
		/// Регистрирует представление валидации для последующего вывода на странице
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="output">HTML-форматированная строка представления</param>
		public static void RegisterValidationMessages(this HtmlHelper htmlHelper, string propertyName, MvcHtmlString output)
		{
			var dictionary = htmlHelper.ViewContext.HttpContext.Items["RegisterValidationMessages"] as IDictionary<string, MvcHtmlString> ?? new Dictionary<string, MvcHtmlString>();
			if (!dictionary.ContainsKey(propertyName))
				dictionary.Add(propertyName, output);
			htmlHelper.ViewContext.HttpContext.Items["RegisterValidationMessages"] = dictionary;
		}

		/// <summary>
		/// Возвращает представление по значению свойства
		/// </summary>
		/// <param name="htmlHelper">Экземпляр объекта <see cref="System.Web.Mvc">HtmlHelper</see>.</param>
		/// <param name="propertyName">Значение свойства <see cref="propertyName">propertyName</see>.</param>
		/// <returns>HTML-кодированная строка</returns>
		public static MvcHtmlString ValidationMessagesFor<T>(this HtmlHelper<T> htmlHelper,
			string propertyName)
		{
			var dictionary =
				htmlHelper.ViewContext.HttpContext.Items["RegisterValidationMessages"] as IDictionary<string, MvcHtmlString>;

			if (dictionary != null && dictionary.ContainsKey(propertyName))
				return (MvcHtmlString) dictionary[propertyName];

			return MvcHtmlString.Empty;
		}
	}
}
