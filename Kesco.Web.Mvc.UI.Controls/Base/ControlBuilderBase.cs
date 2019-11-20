using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc.UI
{

	/// <summary>
	/// Класс реализует базовый класс построителя для элемента управления
	/// </summary>
	/// <typeparam name="TControl">Конечный класс элемента управления.</typeparam>
	/// <typeparam name="TBuilder">Конечный класс построителя элемента управления.</typeparam>
	public class ControlBuilderBase<TControl, TBuilder>
		where TControl : ControlBase
		where TBuilder : ControlBuilderBase<TControl, TBuilder>
	{
		/// <summary>
		/// Возвращает или устанавливает элемент управления для построителя.
		/// </summary>
		/// <value>
		/// The control.
		/// </value>
		protected TControl control { get; set; }

		/// <summary>
		/// Создаёт новый экземпляр класса <see cref="ControlBuilderBase&lt;TControl, TBuilder&gt;"/> class.
		/// </summary>
		/// <param name="control">Экземпляр элемента управления.</param>
		protected ControlBuilderBase(TControl control)
		{
			this.control = control;
		}

		/// <summary>
		/// Устанавливает название для элемента управления
		/// </summary>
		/// <param name="controlName">Название элемента управления</param>
		/// <returns>Построитель элемента управления</returns>
		/// <example>
		/// <code>
		/// &lt;% Html.KescoSelect().Name("Name1").Render() %&gt;
		/// </code>
		/// </example>
		public virtual TBuilder Name(string controlName)
		{
			this.control.Name = controlName;
			return (this as TBuilder);
		}

        /// <summary>
        /// Устанавливает должен ли элемент управления самоинициализироваться.
        /// </summary>
        /// <remarks>
        /// Самоинициализирование означает, что вместе с разметкой элемента управления
        /// выводится скрипт инициализации. Иначе скрипт инициализации добавляется в
        /// в HttContext.Items["Scripts"], приложени
        /// </remarks>
        /// <param name="isSelfInitialized">если установлено в <c>true</c>, то элемент управления самоинициализируется.</param>
        /// <returns>Построитель элемента управления</returns>
        /// <example>
        /// <code>
        /// &lt;% Html.KescoSelect().SelfInitialized(false).Render() %&gt;
        /// </code>
        /// </example>
        public virtual TBuilder SelfInitialized(bool isSelfInitialized)
        {
            this.control.IsSelfInitialized = isSelfInitialized;
            return (this as TBuilder);
        }

        /// <summary>
        /// Устанавливает, что элемент управления должен самоинициализироваться.
        /// </summary>
        /// <remarks>
        /// Самоинициализирование означает, что вместе с разметкой элемента управления
        /// выводится скрипт инициализации. Иначе скрипт инициализации добавляется в
        /// в HttContext.Items["Scripts"], приложение обязанно вывести скрипт инициализации
        /// </remarks>
        /// <param name="isSelfInitialized">если установлено в <c>true</c>, 
        /// то элемент управления самоинициализируется.</param>
        /// <returns>Построитель элемента управления</returns>
        /// <example>
        /// <code>
        /// &lt;% Html.KescoSelect().SelfInitialized(false).Render() %&gt;
        /// </code>
        /// </example>
        public virtual TBuilder SelfInitialized()
        {
            this.control.IsSelfInitialized = true;
            return (this as TBuilder);
        }

        /// <summary>
		/// Устанавливает HTML атрибуты.
		/// </summary>
		/// <param name="attributes">Экземпляр объекта, представляющий HTML атрибуты.</param>
		/// <returns>Построитель элемента управления</returns>
		/// <example>
		/// <code>
		/// &lt;% Html.KescoSelect().HtmlAttributes(new { size = 40, @readonly = 1}).Render() %&gt;
		/// </code>
		/// </example>
		public virtual TBuilder HtmlAttributes(object attributes)
		{
			this.control.HtmlAttributes.Clear();
			this.control.HtmlAttributes.Merge(attributes);
			return (this as TBuilder);
		}

		/// <summary>
		/// Устанавливает HTML атрибуты.
		/// </summary>
		/// <param name="attributes">Экземпляр словаря, содержащщий HTML атрибуты.</param>
		/// <returns>Построитель элемента управления</returns>
		/// <example>
		/// <code>
		/// &lt;% Html.KescoSelect().HtmlAttributes(new Dictionary%lt;string, object&gt; { 
		///		new KeyValuePair&lt;string,object&gt; { Key = "size", Value = 40 } 
		///		}).Render() %&gt;
		/// </code>
		/// </example>
		public virtual TBuilder HtmlAttributes(IDictionary<string, object> attributes)
		{
			this.control.HtmlAttributes.Merge(attributes, true);
			return (this as TBuilder);
		}

		/// <summary>
		/// Устанавливает CSS class для элемента управления.
		/// </summary>
		/// <param name="cssClass">CSS класс.</param>
		/// <returns>Построитель элемента управления</returns>
		public TBuilder CssClass(string cssClass)
		{
			this.control.HtmlAttributes.Add("class", cssClass);
			return this as TBuilder;
		}

		/// <summary>
		/// Устанавливает CSS стиль для элемента управления..
		/// </summary>
		/// <param name="cssStyle">CSS стиль.</param>
		/// <returns>Построитель элемента управления</returns>
		public TBuilder CssStyle(string cssStyle)
		{
			this.control.HtmlAttributes.Add("style", cssStyle);
			return this as TBuilder;
		}

		/// <summary>
		/// Выводит HTML код элемента управления 
		/// </summary>
		/// <returns>Построитель элемента управления</returns>
		/// <example>
		/// <code>
		/// &lt;% Html.KescoSelect(ЭЭ).HtmlAttributes(new { size = 40, @readonly = 1}).Render() %&gt;
		/// </code>
		/// </example>
		public virtual void Render()
		{
			this.control.Render();
		}

		/// <summary>
		/// Возвращает экземпляр элемента управления, связанного с данным построителем.
		/// </summary>
		/// <returns>Экземпляр элемента управления, связанного с построителем</returns>
		public ControlBase ToControl()
		{
			return this.control;
		}

		/// <summary>
		/// Returns the HTML string that represents a control.
		/// </summary>
		/// <returns></returns>
		public string ToHtmlString()
		{
			return this.ToControl().ToHtmlString();
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return this.ToHtmlString();
		}

	}

}
