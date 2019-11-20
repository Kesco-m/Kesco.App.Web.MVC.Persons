using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Linq.Expressions;

namespace Kesco.Web.Mvc
{
	public static class DropDownListExtensions
	{
		public static MvcHtmlString DropDownList(this HtmlHelper helper,
			string name, Dictionary<int, string> dictionary)
		{
			var selectListItems = new SelectList(dictionary, "Key", "Value");
			return helper.DropDownList(name, selectListItems);
		}

		public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
			Expression<Func<TModel, TProperty>> expression, Dictionary<int, string> dictionary)
		{
			var selectListItems = new SelectList(dictionary, "Key", "Value");
			return helper.DropDownListFor(expression, selectListItems);
		}
	}
}
