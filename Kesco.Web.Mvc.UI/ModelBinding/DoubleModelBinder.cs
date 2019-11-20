using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Kesco.Lib.Log;

namespace Kesco.Web.Mvc
{
	/// <summary>
	/// Определяет связывателя, который требуются для типа Double.
	/// Данный связыватель учитывает культуру, установленную для выполняемого потока.
	/// </summary>
	public class DoubleModelBinder : IModelBinder
	{
		/// <summary>
		/// Привязывает модель к значению, используя указанный контекст контроллера и контекст привязки.
		/// </summary>
		/// <param name="controllerContext">Контекст контроллера.</param>
		/// <param name="bindingContext">Контекст привязки.</param>
		/// <returns>
		/// Значение привязки.
		/// </returns>
		public object BindModel(ControllerContext controllerContext,
			ModelBindingContext bindingContext)
		{
			ValueProviderResult valueResult = bindingContext.ValueProvider
				.GetValue(bindingContext.ModelName);
			ModelState modelState = new ModelState { Value = valueResult };
			object actualValue = null;
			string attemptedValue = (valueResult.AttemptedValue ?? String.Empty).Replace("\xA0", " ");
			try {
				actualValue = Double.Parse(attemptedValue, NumberStyles.Any, System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat);
			} catch (FormatException e) {
				modelState.Errors.Add(e);
				//Kesco.Logger.WriteEx(new Exception(String.Format("valueResult: {0} / {1}; actualValue: {2}",
				//        valueResult.AttemptedValue,
				//        valueResult.RawValue,
				//        actualValue
				//    ), e));
			}

			bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
			return actualValue;
		}
	}
}
