using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Kesco.Web.Mvc
{
	/// <summary>
	/// Данный класс служит для регистрации связывателей данных, используемых в MVC приложениях Kesco
	/// </summary>
	public static class ModelBinding
	{
		/// <summary>
		/// Регистрирует связыватели данных.
		/// </summary>
		public static void RegisterBinders()
		{
			ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
			ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
			ModelBinders.Binders.Add(typeof(double?), new DoubleModelBinder());
			ModelBinders.Binders.Add(typeof(double), new DoubleModelBinder());

		}
	}
}
