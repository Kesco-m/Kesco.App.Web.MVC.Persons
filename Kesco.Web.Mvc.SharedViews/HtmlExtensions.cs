using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Kesco.Web.Mvc.SharedViews.Models;
using Kesco.Web.Mvc.SharedViews.Views.Shared;
using Kesco.Web.Mvc.SharedViews.Controls;

namespace Kesco.Web.Mvc.SharedViews
{
	/// <summary>
	/// 
	/// </summary>
	public static class HtmlExtensions
	{
		public static IHtmlString ToJson(this HtmlHelper helper, object source)
		{
			return helper.Raw(Json.Serialize(source));
		}
		///// <summary>
		///// Displays a model as employee.
		///// </summary>
		///// <typeparam name="TModel">The type of the model.</typeparam>
		///// <typeparam name="TProperty">The type of the property.</typeparam>
		///// <param name="helper">The helper.</param>
		///// <param name="expression">The expression.</param>
		///// <param name="infoType">Type of the info.</param>
		///// <returns></returns>
		//public static HelperResult DisplayAsEmployeeFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression
		//    , EmployeeInfoTypes infoType)
		//{
		//    return helper.DisplayAsEmployee(ExpressionHelper.GetExpressionText(expression), infoType);
		//}

		///// <summary>
		///// Displays as person for.
		///// </summary>
		///// <typeparam name="TModel">The type of the model.</typeparam>
		///// <typeparam name="TProperty">The type of the property.</typeparam>
		///// <param name="helper">The helper.</param>
		///// <param name="expression">The expression.</param>
		///// <param name="infoType">Type of the info.</param>
		///// <returns></returns>
		//public static HelperResult DisplayAsPersonFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, 
		//    PersonInfoTypes infoType)
		//{
		//    return helper.DisplayAsPerson(ExpressionHelper.GetExpressionText(expression), infoType);
		//}

		///// <summary>
		///// Editors as person for.
		///// </summary>
		///// <typeparam name="TModel">The type of the model.</typeparam>
		///// <typeparam name="TProperty">The type of the property.</typeparam>
		///// <param name="helper">The helper.</param>
		///// <param name="expression">The expression.</param>
		///// <param name="infoType">Type of the info.</param>
		///// <returns></returns>
		//public static HelperResult EditorAsPersonFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, PersonInfoTypes infoType)
		//{
		//    return helper.EditorAsPerson(ExpressionHelper.GetExpressionText(expression), infoType);
		//}

		///// <summary>
		///// Editors as employee for.
		///// </summary>
		///// <typeparam name="TModel">The type of the model.</typeparam>
		///// <typeparam name="TProperty">The type of the property.</typeparam>
		///// <param name="helper">The helper.</param>
		///// <param name="expression">The expression.</param>
		///// <param name="infoType">Type of the info.</param>
		///// <returns></returns>
		//public static HelperResult EditorAsEmployeeFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, EmployeeInfoTypes infoType)
		//{
		//    return helper.EditorAsEmployee(ExpressionHelper.GetExpressionText(expression), infoType);
		//}
    }
}