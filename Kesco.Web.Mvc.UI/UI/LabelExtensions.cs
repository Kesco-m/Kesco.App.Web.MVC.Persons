using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq.Expressions;

namespace Kesco.Web.Mvc
{
	/// <summary>
	/// Представляет поддержку для HTML-элемента label в представлении MVC ASP.NET.
	/// </summary>
	public static class LabelExtensions
	{
		#region Label/LabelFor Html helpers

		/// <summary>
		/// Возвращает HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </summary>
		/// <param name="html">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
		/// <param name="expression">Строка, указывающая свойство модели, для которой вернуть заголовок.</param>
		/// <returns>
		/// HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </returns>
		public static MvcHtmlString LabelEx(this HtmlHelper html, string expression)
		{
			return LabelHelper(html, ModelMetadata.FromStringExpression(expression, html.ViewData), expression, null, new RouteValueDictionary());
		}

		/// <summary>
		/// Возвращает HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </summary>
		/// <param name="html">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
		/// <param name="expression">Строка, указывающая свойство модели, для которой вернуть заголовок.</param>
		/// <param name="htmlAttributes">HTML аттрибуты.</param>
		/// <returns>
		/// HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </returns>
		public static MvcHtmlString Label(this HtmlHelper html, string expression, object htmlAttributes)
		{
			return LabelHelper(html, ModelMetadata.FromStringExpression(expression, html.ViewData), expression, null,  new RouteValueDictionary(htmlAttributes));
		}

		/// <summary>
		/// Возвращает HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </summary>
		/// <param name="html">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
		/// <param name="expression">Строка, указывающая свойство модели, для которой вернуть заголовок.</param>
		/// <param name="htmlAttributes">HTML аттрибуты.</param>
		/// <returns>
		/// HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </returns>
		public static MvcHtmlString Label(this HtmlHelper html, string expression, IDictionary<string, object> htmlAttributes)
		{
			return LabelHelper(html, ModelMetadata.FromStringExpression(expression, html.ViewData), expression, null, htmlAttributes);
		}

		/// <summary>
		/// Возвращает HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </summary>
		/// <param name="html">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
		/// <param name="expression">Строка, указывающая свойство модели, для которой вернуть заголовок.</param>
		/// <param name="labelText">Текст заголовка поля.</param>
		/// <param name="htmlAttributes">HTML аттрибуты.</param>
		/// <returns>
		/// HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </returns>
		public static MvcHtmlString Label(this HtmlHelper html, string expression, string labelText, object htmlAttributes)
		{
			return LabelHelper(html, ModelMetadata.FromStringExpression(expression, html.ViewData), expression, labelText, new RouteValueDictionary(htmlAttributes));
		}

		/// <summary>
		/// Возвращает HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </summary>
		/// <param name="html">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
		/// <param name="expression">Строка, указывающая свойство модели, для которой вернуть заголовок.</param>
		/// <param name="labelText">Текст заголовка поля.</param>
		/// <param name="htmlAttributes">HTML аттрибуты.</param>
		/// <returns>
		/// HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </returns>
		public static MvcHtmlString Label(this HtmlHelper html, string expression, string labelText, IDictionary<string, object> htmlAttributes)
		{
			return LabelHelper(html, ModelMetadata.FromStringExpression(expression, html.ViewData), expression, labelText, htmlAttributes);
		}

		/// <summary>
		/// Возвращает HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="html">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
		/// <param name="expression">Выражение, возвращающая модель, для которой вернуть заголовок.</param>
		/// <returns>
		/// HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </returns>
		public static MvcHtmlString LabelForEx<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
		{
			return LabelHelper(html, ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, html.ViewData)
				, ExpressionHelper.GetExpressionText((LambdaExpression)expression)
				, null
				, new RouteValueDictionary());
		}

		/// <summary>
		/// Возвращает HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="html">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
		/// <param name="expression">Выражение, возвращающая модель, для которой вернуть заголовок.</param>
		/// <param name="htmlAttributes">HTML аттрибуты.</param>
		/// <returns>
		/// HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </returns>
		//public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
		//{
		//    return LabelHelper(html, ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, html.ViewData)
		//        , ExpressionHelper.GetExpressionText((LambdaExpression)expression)
		//        , null
		//        , new RouteValueDictionary(htmlAttributes));
		//}

		/// <summary>
		/// Возвращает HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="html">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
		/// <param name="expression">Выражение, возвращающая модель, для которой вернуть заголовок.</param>
		/// <param name="htmlAttributes">HTML аттрибуты.</param>
		/// <returns>
		/// HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </returns>
		//public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
		//{
		//    return LabelHelper(html, ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, html.ViewData)
		//        , ExpressionHelper.GetExpressionText((LambdaExpression)expression)
		//        , null
		//        , htmlAttributes);
		//}

		/// <summary>
		/// Возвращает HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="html">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="labelText">Текст заголовка поля.</param>
		/// <param name="htmlAttributes">HTML аттрибуты.</param>
		/// <returns>
		/// HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </returns>
		//public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string labelText, object htmlAttributes)
		//{
		//    return LabelHelper(html, ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, html.ViewData)
		//            , ExpressionHelper.GetExpressionText((LambdaExpression)expression)
		//            , labelText
		//            , new RouteValueDictionary(htmlAttributes)
		//        );
		//}

		/// <summary>
		/// Возвращает HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="html">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
		/// <param name="expression">Выражение, возвращающая модель, для которой вернуть заголовок.</param>
		/// <param name="labelText">Текст заголовка поля.</param>
		/// <param name="htmlAttributes">HTML аттрибуты.</param>
		/// <returns>
		/// HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </returns>
		//public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string labelText, IDictionary<string, object> htmlAttributes)
		//{
		//    return LabelHelper(html, ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, html.ViewData), ExpressionHelper.GetExpressionText((LambdaExpression)expression), labelText, htmlAttributes);
		//}

		/// <summary>
		/// Возвращает HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </summary>
		/// <param name="html">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
		/// <returns>
		/// HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </returns>
		public static MvcHtmlString LabelForModelEx(this HtmlHelper html)
		{
			return LabelHelper(html, html.ViewData.ModelMetadata, string.Empty, null, new RouteValueDictionary());
		}

		/// <summary>
		/// Возвращает HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </summary>
		/// <param name="html">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
		/// <param name="htmlAttributes">HTML аттрибуты.</param>
		/// <returns>
		/// HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </returns>
		public static MvcHtmlString LabelForModel(this HtmlHelper html, object htmlAttributes)
		{
			return LabelHelper(html, html.ViewData.ModelMetadata, string.Empty, null, new RouteValueDictionary(htmlAttributes));
		}

		/// <summary>
		/// Возвращает HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </summary>
		/// <param name="html">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
		/// <param name="htmlAttributes">HTML аттрибуты.</param>
		/// <returns>
		/// HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </returns>
		public static MvcHtmlString LabelForModel(this HtmlHelper html, IDictionary<string, object> htmlAttributes)
		{
			return LabelHelper(html, html.ViewData.ModelMetadata, string.Empty, null, htmlAttributes);
		}

		/// <summary>
		/// Возвращает HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </summary>
		/// <param name="html">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
		/// <param name="labelText">Текст заголовка поля.</param>
		/// <param name="htmlAttributes">HTML аттрибуты.</param>
		/// <returns>HTML-элемент label и имя свойства для свойства, представленного моделью.</returns>
		public static MvcHtmlString LabelForModel(this HtmlHelper html, string labelText, object htmlAttributes)
		{
			return LabelHelper(html, html.ViewData.ModelMetadata, string.Empty, labelText, new RouteValueDictionary(htmlAttributes));
		}

		/// <summary>
		/// Возвращает HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </summary>
		/// <param name="html">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
		/// <param name="labelText">Текст заголовка поля.</param>
		/// <param name="htmlAttributes">HTML аттрибуты.</param>
		/// <returns>HTML-элемент label и имя свойства для свойства, представленного моделью.</returns>
		public static MvcHtmlString LabelForModel(this HtmlHelper html, string labelText, IDictionary<string, object> htmlAttributes)
		{
			return LabelHelper(html, html.ViewData.ModelMetadata, string.Empty, labelText, htmlAttributes);
		}

		/// <summary>
		///     Возвращает HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </summary>
		/// <param name="html">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
		/// <param name="metadata">Метаданные поля</param>
		/// <param name="htmlFieldName">Имя поля HTML.</param>
		/// <param name="labelText">Текст заголовка поля.</param>
		/// <param name="htmlAttributes">HTML аттрибуты.</param>
		/// <returns>HTML-элемент label и имя свойства для свойства, представленного моделью.</returns>
		internal static MvcHtmlString LabelHelper(HtmlHelper html, ModelMetadata metadata, string htmlFieldName, string labelText, IDictionary<string, object> htmlAttributes)
		{
			string str = labelText ?? (metadata.DisplayName ?? (metadata.PropertyName ?? htmlFieldName.Split(new char[] { '.' }).Last<string>()));
			if (string.IsNullOrEmpty(str)) {
				return MvcHtmlString.Empty;
			}
			TagBuilder builder = new TagBuilder("label");
			builder.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));
			if (!String.IsNullOrEmpty(metadata.Description)) {
				builder.Attributes.Add("alt", metadata.Description);
				builder.Attributes.Add("title", metadata.Description);
				builder.Attributes.Add("style", "BORDER-BOTTOM:1px dashed silver;");
			}
			builder.MergeAttributes(htmlAttributes, true);
			builder.SetInnerText(str);
			return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
		}

		/// <summary>
		/// Возвращает HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </summary>
		/// <param name="html">Экземпляр вспомогательного метода HTML, который расширяется данным методом.</param>
		/// <param name="controlID">The control ID.</param>
		/// <param name="metadata">Метаданные поля</param>
		/// <param name="labelText">Текст заголовка поля.</param>
		/// <param name="htmlAttributes">HTML аттрибуты.</param>
		/// <returns>
		/// HTML-элемент label и имя свойства для свойства, представленного моделью.
		/// </returns>
		public static MvcHtmlString LabelForControl(this HtmlHelper html, string controlID, ModelMetadata metadata, string labelText, IDictionary<string, object> htmlAttributes)
		{
			string str = labelText ?? (metadata.DisplayName ?? metadata.PropertyName);
			if (string.IsNullOrEmpty(str)) {
				return MvcHtmlString.Empty;
			}
			TagBuilder builder = new TagBuilder("label");
			builder.Attributes.Add("for", controlID);
			if (!String.IsNullOrEmpty(metadata.Description)) {
				builder.Attributes.Add("alt", metadata.Description);
				builder.Attributes.Add("title", metadata.Description);
				builder.Attributes.Add("style", "BORDER-BOTTOM:1px dashed silver;");
			}
			builder.MergeAttributes(htmlAttributes, true);
			builder.SetInnerText(str);
			return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
		}

		#endregion
	}
}
