using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kesco.Web.Mvc
{

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TModel">The type of the model.</typeparam>
	public class DataLinkBuilder<TModel>
	{
		public string FormID { get; set; }
		public string ModelName { get; set; }

		/// <summary>
		/// Возвращает значение является ли элемент управления на клиентской странице самоинициализируемым.
		/// </summary>
		/// <value>Признак самоинициализации элемента управления </value>
		public bool IsSelfInitialized { get; protected set; }

		public ViewDataDictionary<TModel> ViewData { get; protected set; }

		public DataLinkBuilder(ViewDataDictionary<TModel> viewData)
		{
			ViewData = viewData;
			IsSelfInitialized = true;
		}

		protected virtual void ExtractPropertyList(ModelMetadata metadata, Dictionary<string, Type> properties, string prefix)
		{
			if (prefix == null) 
				prefix = String.Empty;

			foreach (ModelMetadata prop in metadata.Properties.Where(pm => pm.ShowForEdit && !ViewData.TemplateInfo.Visited(pm))) {
				if (prop.IsComplexType) {
					ExtractPropertyList(prop, properties, prefix+prop.PropertyName+".");
				} else {
					properties.Add(prefix+prop.PropertyName, prop.ModelType);
				}
			}
		}

		public Dictionary<string, Type> BuildPropertyList(string prefix)
		{
			if (prefix == null)
				prefix = String.Empty;

			Dictionary<string, Type> properties = new Dictionary<string, Type>();

			if (ViewData.ModelMetadata.IsComplexType)
				ExtractPropertyList(ViewData.ModelMetadata, properties, prefix);
			else
				properties.Add(prefix + ViewData.ModelMetadata.PropertyName, ViewData.ModelMetadata.ModelType);

			return properties;
		}

		protected virtual void ExtractPropertyListEx(ModelMetadata metadata, Dictionary<string, ModelMetadata> properties, string prefix)
		{
			if (prefix == null)
				prefix = String.Empty;

			foreach (ModelMetadata prop in metadata.Properties.Where(pm => pm.ShowForEdit && !ViewData.TemplateInfo.Visited(pm))) {
				if (prop.IsComplexType) {
					ExtractPropertyListEx(prop, properties, prefix + prop.PropertyName + ".");
				} else {
					properties.Add(prefix + prop.PropertyName, prop);
				}
			}
		}

		public Dictionary<string, ModelMetadata> BuildPropertyListEx(string prefix)
		{
			if (prefix == null)
				prefix = String.Empty;

			Dictionary<string, ModelMetadata> properties = new Dictionary<string, ModelMetadata>();

			if (ViewData.ModelMetadata.IsComplexType)
				ExtractPropertyListEx(ViewData.ModelMetadata, properties, prefix);
			else
				properties.Add(prefix + ViewData.ModelMetadata.PropertyName, ViewData.ModelMetadata);

			return properties;
		}

		private string FormatLinkProperty(string property, ModelMetadata metadata)
		{
			string convert = "";
			string convertBack = "";
			if (metadata.DataTypeName == "Date" 
				|| metadata.ModelType.AssemblyQualifiedName == typeof(DateTime).AssemblyQualifiedName
				|| metadata.ModelType.AssemblyQualifiedName == typeof(DateTime?).AssemblyQualifiedName) {
					convert = "getDateFromDatePicker";
					convertBack = "setDateToDatePicker";
			}

			if (metadata.DataTypeName == "Numeric") {
				convert = "getNumeric";
				convertBack = "setNumeric";
			}

			return String.Format(@"
						'{0}': {{
							name: '{0}',
							convert: '{1}',
							convertBack: '{2}'
							}},"
				, property, convert, convertBack
				, metadata.ModelType);
		}

		/// <summary>
		/// Пишет скрипт инициализаии элемента управления.
		/// </summary>
		/// <param name="writer">Экземпляр класса <see cref="TextWriter"/></param>
		public virtual void WriteInitializationScript(TextWriter writer) {
			if (String.IsNullOrEmpty(ModelName))
				return;

			Dictionary<string, ModelMetadata> properties = BuildPropertyListEx(String.Empty);

			StringBuilder sb = new StringBuilder();
			properties.ToList().ForEach( pair => {
				sb.Append(FormatLinkProperty(pair.Key, pair.Value));
			});
			sb.AppendFormat("		'__stub__':'__stub__'\n");


			Dictionary<string, object> dictionary = new Dictionary<string, object>();

			properties.ToList().ForEach(pair => {
				dictionary.Add(pair.Key, pair.Value.Model);
			});

			Dictionary<string, dynamic> dictionary2 = new Dictionary<string, object>();

			properties.ToList().ForEach(pair => {
				dictionary2.Add(pair.Key, pair.Value.DataTypeName);
			});

			string sb1 = JsonConvert.SerializeObject(dictionary, new JavaScriptDateTimeConverter());
			sb1 = sb1.Replace("\'", "\\u0027");
			string sb2 = JsonConvert.SerializeObject(dictionary2, Formatting.Indented);
			writer.WriteLine(@"
				/* 
					{4}
				*/
				// create instance for model object
				//var {0} = $.parseJSON('');
				var {0} = {3};
				var {0}___DataLinkOptions = {{{2}}};

				// link instance to the form.
				$(document).ready(function() {{
					
					$('#{1}').link({0}, {0}___DataLinkOptions).trigger('change');

				}});
			", ModelName, FormID, sb.ToString(), sb1, sb2);
		}
		/// <summary>
		/// Возвращает HTML код, представляющий элемент управления на клиентской веб-странице.
		/// </summary>
		/// <returns>HTML код для клиентской веб-страницы.</returns>
		public string ToHtmlString()
		{
			using (StringWriter writer = new StringWriter()) {
				this.WriteHtml(new HtmlTextWriter(writer));
				return writer.ToString();
			}
		}
		/// <summary>
		/// Пишет HTML HTML код, представляющий элемент управления.
		/// </summary>
		/// <param name="writer">Экземпляр класса <see cref="HtmlTextWriter"/></param>
		protected virtual void WriteHtml(HtmlTextWriter writer)
		{
			if (this.IsSelfInitialized) {
				writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
				writer.RenderBeginTag(HtmlTextWriterTag.Script);
				this.WriteInitializationScript(writer);
				writer.RenderEndTag();
			}
		}
		/// <summary>
		/// Выводит HTML код в текущий поток HTTP-ответа.
		/// </summary>
		public void Render()
		{
			using (HtmlTextWriter writer = new HtmlTextWriter(HttpContext.Current.Response.Output)) {
				this.WriteHtml(writer);
			}

		}
	}

}
