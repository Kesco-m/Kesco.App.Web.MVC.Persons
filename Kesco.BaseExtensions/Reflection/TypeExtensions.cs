using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Reflection
{
	/// <summary>
	/// Статический класс-расширение для определения реального типа объекта, если объект является Nullable&lt;T&gt;.
	/// </summary>
	public static class TypeExtensions
	{
		/// <summary>
		/// Данный метод возвращает реальный тип объекта, если объект является Nullable&lt;T&gt;.
		/// </summary>
		/// <param name="type">Тип объекта</param>
		/// <returns>Реальный тип объекта</returns>
		public static Type GetObjectTypeIfNullable(this Type type)
		{
			Type real = type;
			// We need to check whether the type is NULLABLE
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)) {
				// If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
				real = type.GetGenericArguments()[0];
			}

			return real;
		}
	}

}
