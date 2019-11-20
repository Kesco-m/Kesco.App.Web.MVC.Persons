using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using System.Web;
using System.Web.UI;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;


namespace Kesco.Web.Mvc.UI
{

	/// <summary>
	/// Данный класс представляет базовый класс для всех Kesco MVC элементов управления
	/// </summary>
	public abstract class ControlBase
	{
		protected readonly ViewContext _viewContext;

		/// <summary>
		/// Возвращает идентификатор элемента управления.
		/// </summary>
		/// <value>Идентификатор элемента управления</value>
		/// <remarks>
		/// Если элемент управления содержит идентификатор в Html аттрибутах, то возвращает значение из соответсвующего аттрибута. 
		/// Иначе формируется на основе названия <see cref="Name"/> элемента управления.
		/// </remarks>
		public string ID
		{
			get
			{
				if (this.HtmlAttributes.ContainsKey("id"))
				{
					return this.HtmlAttributes["id"].ToString();
				}
				if (string.IsNullOrEmpty(this.Name))
				{
					return null;
				}
				return this.Name.Replace(".", HtmlHelper.IdAttributeDotReplacement);
			}
		}

		/// <summary>
		/// Возвращает название элемента управления.
		/// </summary>
		/// <value>Название элемента управления</value>
		public string Name { get; set; }

		/// <summary>
		/// Возвращает HTML аттрибуты элемента управления.
		/// </summary>
		/// <value>HTML аттрибуты</value>
		public IDictionary<string, object> HtmlAttributes { get; private set; }

		/// <summary>
		/// Возвращает или устанавливает значение является ли 
        /// элемент управления на клиентской странице самоинициализируемым.
		/// </summary>
		/// <value>Признак самоинициализации элемента управления </value>
		public bool IsSelfInitialized { get; set; }

		/// <summary>
		/// Создаёт экземпляр элемента управления.
		/// </summary>
		/// <param name="viewContext">Представляет контекст представления <see cref="ViewContext"/> для элемента управления.</param>
		protected ControlBase(ViewContext viewContext)
		{
			this._viewContext = viewContext;
			this.HtmlAttributes = new RouteValueDictionary();
			this.IsSelfInitialized = true;
		}

        /// <summary>
        /// Регистрирует скрипт для последующего вывода на странице
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="script">Javascript код.</param>
        protected virtual void RegisterScript(string script)
        {
            var list = this._viewContext.HttpContext.Items["Scripts"] as IList<string> ?? new List<string>();
            list.Add(script);
            this._viewContext.HttpContext.Items["Scripts"] = list;
        }

        /// <summary>
		/// Пишет скрипт инициализаии элемента управления.
		/// </summary>
		/// <param name="writer">Экземпляр класса <see cref="TextWriter"/></param>
		public virtual void WriteInitializationScript(TextWriter writer) { }

		/// <summary>
		/// Возвращает HTML код, представляющий элемент управления на клиентской веб-странице.
		/// </summary>
		/// <returns>HTML код для клиентской веб-страницы.</returns>
		public string ToHtmlString()
		{
			using (StringWriter writer = new StringWriter())
			{
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
            if (this.IsSelfInitialized)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
                writer.RenderBeginTag(HtmlTextWriterTag.Script);
                this.WriteInitializationScript(writer);
                writer.RenderEndTag();
            }
            else
            {
                using (StringWriter scriptWriter = new StringWriter())
                {
                    this.WriteInitializationScript(new HtmlTextWriter(scriptWriter));
                    RegisterScript(scriptWriter.ToString());
                }
            }
		}

		/// <summary>
		/// Выводит HTML код в текущий поток HTTP-ответа.
		/// </summary>
		public void Render()
		{
			using (HtmlTextWriter writer = new HtmlTextWriter(HttpContext.Current.Response.Output))
			{
				this.WriteHtml(writer);
			}

		}



	}


}
