using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel;

namespace Kesco.Web.Mvc
{
	/// <summary>
	///	Отображает запрос браузера в объект данных. Этот класс расширяет 
	/// реализацию связывателя модели по умолчанию. Добавлена связывание
	/// целочисленного значения на свойство с типом перечисления.
	/// см. http://stackoverflow.com/questions/6051756/model-binding-to-enums-in-asp-net-mvc-3
	/// </summary>
    public class DefaultModelBinderEx : DefaultModelBinder
    {
		/// <summary>
		/// Возвращает значение свойства, используя заданные контекст контроллера, контекст привязки, дескриптор свойства и связыватель свойства.
		/// </summary>
		/// <param name="controllerContext">Контекст, в котором функционирует контроллер. Сведения о контексте включают информацию о контроллере, HTTP-содержимом, контексте запроса и данных маршрута.</param>
		/// <param name="bindingContext">Контекст, в котором привязана модель. Контекст содержит такие сведения, как объект модели, имя модели, тип модели, фильтр свойств и поставщик значений.</param>
		/// <param name="propertyDescriptor">Дескриптор свойства, к которому выполняется доступ. Дескриптор предоставляет информацию, такую как тип компонента, тип свойства и значение свойства. Также предоставляет методы для получения или задания значения свойства.</param>
		/// <param name="propertyBinder">Объект, который обеспечивает способ привязки свойства.</param>
		/// <returns>
		/// Объект, представляющий значение свойства.
		/// </returns>
        protected override object GetPropertyValue(ControllerContext controllerContext, ModelBindingContext bindingContext,
            PropertyDescriptor propertyDescriptor, IModelBinder propertyBinder)
        {
            var propertyType = propertyDescriptor.PropertyType;
            if (propertyType.IsEnum)
            {
                var providerValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
                if (null != providerValue)
                {
                    var value = providerValue.RawValue;
                    if (null != value)
                    {
                        var valueType = value.GetType();
                        if (!valueType.IsEnum)
                        {

							if (valueType.IsArray)
								value = Array.Find<string>((string[])value, item => { return true; });

							return Enum.ToObject(propertyType, Convert.ChangeType(value, propertyType.GetEnumUnderlyingType()));
                        }
                    }
                }
            }
            return base.GetPropertyValue(controllerContext, bindingContext, propertyDescriptor, propertyBinder);
        }
    }
}
