using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;

namespace Kesco.Web.Mvc
{
	/// <summary>
	/// Класс расширяет класс, реализущий интерфейс IDictionary&lt;string, object&gt;,
	/// для упрощения работы по созданию HTML-тэгов.
	/// </summary>
	public static class DictionaryExtensions
	{
		// Methods
		/// <summary>
		/// Добавляет HTML-атрибут style.
		/// </summary>
		/// <param name="instance">Экземпляр словаря.</param>
		/// <param name="key">Ключ.</param>
		/// <param name="value">Значение.</param>
		public static void AddStyleAttribute(this IDictionary<string, object> instance, string key, string value)
		{
			string str = string.Empty;
			if (instance.ContainsKey("style"))
			{
				str = (string)instance["style"];
			}
			StringBuilder builder = new StringBuilder(str);
			builder.Append(key);
			builder.Append(":");
			builder.Append(value);
			builder.Append(";");
			instance["style"] = builder.ToString();
		}

		/// <summary>
		/// Добавляет значение к ключу, используя разделитель.
		/// </summary>
		/// <param name="instance">Экземпляр.</param>
		/// <param name="key">Ключ.</param>
		/// <param name="separator">Разделитель.</param>
		/// <param name="value">Значение.</param>
		public static void AppendInValue(this IDictionary<string, object> instance, string key, string separator, object value)
		{
			instance[key] = instance.ContainsKey(key) ? (instance[key] + separator + value) : value.ToString();
		}

		/// <summary>
		/// Объединяет словари, заменяя значения существующих ключей.
		/// </summary>
		/// <param name="instance">Экземпляр.</param>
		/// <param name="from">Словарь с новыми значениями.</param>
		public static void Merge(this IDictionary<string, object> instance, IDictionary<string, object> from)
		{
			instance.Merge(from, true);
		}

		/// <summary>
		/// Объединяет словарь с новыми значениями из объекта, где свойства являются ключами в словаре.
		/// </summary>
		/// <param name="instance">Экземпляр</param>
		/// <param name="values">Значения.</param>
		public static void Merge(this IDictionary<string, object> instance, object values)
		{
			instance.Merge(values, true);
		}

		/// <summary>
		/// Объединяет словари, заменяя значения существующих ключей.
		/// </summary>
		/// <param name="instance">Экземпляр словаря.</param>
		/// <param name="from">Экземпляр словаря с новыми значениями.</param>
		/// <param name="replaceExisting">Если установлено в <c>true</c> , существущие значения заменяются новыми.</param>
		public static void Merge(this IDictionary<string, object> instance, IDictionary<string, object> from, bool replaceExisting)
		{
			foreach (KeyValuePair<string, object> pair in from)
			{
				if (replaceExisting || !instance.ContainsKey(pair.Key))
				{
					instance[pair.Key] = pair.Value;
				}
			}
		}

		/// <summary>
		/// Объединяет словарь с новыми значениями из объекта, где свойства являются ключами в словаре.
		/// </summary>
		/// <param name="instance">Экземпляр словаря.</param>
		/// <param name="values">Объект с новыми значениями.</param>
		/// <param name="replaceExisting">Если установлено в <c>true</c>, существущие значения заменяются новыми.</param>
		public static void Merge(this IDictionary<string, object> instance, object values, bool replaceExisting)
		{

			instance.Merge(HtmlHelper.AnonymousObjectToHtmlAttributes(values), replaceExisting);
		}

		/// <summary>
		/// Объединяет словарь с новым значениям ключа.
		/// </summary>
		/// <param name="instance">Экземпляр словаря.</param>
		/// <param name="key">Ключ.</param>
		/// <param name="value">Значение.</param>
		/// <param name="replaceExisting">Если установлено в <c>true</c> , существущие значения заменяются новыми.</param>
		public static void Merge(this IDictionary<string, object> instance, string key, object value, bool replaceExisting)
		{
			if (replaceExisting || !instance.ContainsKey(key))
			{
				instance[key] = value;
			}
		}

		/// <summary>
		/// Добавляет значение к существующему, используя указанный разделитель.
		/// </summary>
		/// <param name="instance">Экземпляр словаря.</param>
		/// <param name="key">Ключ.</param>
		/// <param name="separator">Разделитель.</param>
		/// <param name="value">Значение.</param>
		public static void PrependInValue(this IDictionary<string, object> instance, string key, string separator, object value)
		{
			instance[key] = instance.ContainsKey(key) ? (value + separator + instance[key]) : value.ToString();
		}

		/// <summary>
		/// Преобразует словарь в строку HTML-атрибутов.
		/// </summary>
		/// <param name="instance">Экземпляр словаря.</param>
		/// <returns>Строка</returns>
		public static string ToAttributeString(this IDictionary<string, object> instance)
		{
			StringBuilder builder = new StringBuilder();
			foreach (KeyValuePair<string, object> pair in instance)
			{
				builder.AppendFormat(" {0}=\"{1}\"", HttpUtility.HtmlAttributeEncode(pair.Key), HttpUtility.HtmlAttributeEncode(pair.Value.ToString()) );
			}
			return builder.ToString();
		}
	}

}
