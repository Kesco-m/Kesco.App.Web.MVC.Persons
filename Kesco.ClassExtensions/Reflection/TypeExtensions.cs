using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Kesco
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
			if (type.IsNullableType()) {
				// If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
				real = type.GetGenericArguments()[0];
			}

			return real;
		}

		/// <summary>
		/// Возвращает является ли тип Nullablе или нет.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>
		///   <c>true</c> если тип Nullablе; иначе, <c>false</c>.
		/// </returns>
		public static bool IsNullableType(this Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		/// <summary>
		/// Gets the metadata attribute.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="type">The type.</param>
		/// <param name="propertyName">Name of the property.</param>
		/// <returns></returns>
		public static T GetMetadataAttribute<T>(this Type type, string propertyName)
			where T : Attribute
		{
			Type attributeType = typeof(T);

			// First look into attributes on a type and it's parents
			T attr = type.GetProperty(propertyName).GetCustomAttributes(attributeType, true).SingleOrDefault() as T;

			// Look for [MetadataType] attribute in type hierarchy
			// http://stackoverflow.com/questions/1910532/attribute-isdefined-doesnt-see-attributes-applied-with-metadatatype-class
			if (attr == null) {
				MetadataTypeAttribute metadataType = (MetadataTypeAttribute) type.GetCustomAttributes(typeof(MetadataTypeAttribute), true).FirstOrDefault();
				if (metadataType != null) {
					var property = metadataType.MetadataClassType.GetProperty(propertyName);
					if (property != null) {
						attr = property.GetCustomAttributes(attributeType, true).SingleOrDefault() as T;
					}
				}
			}
			return attr;
		}
	}

}
