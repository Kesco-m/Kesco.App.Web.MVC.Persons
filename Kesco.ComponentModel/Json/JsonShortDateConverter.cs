using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kesco.ComponentModel.Json
{
	/// <summary>
	/// Класс сериализации объекта типа DateTime в формат Json
	/// в виде строки краткой даты
	/// </summary>
	public class JsonShortDateConverter : DateTimeConverterBase
	{

		/// <summary>
		/// Reads the JSON representation of the object.
		/// </summary>
		/// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
		/// <param name="objectType">Type of the object.</param>
		/// <param name="existingValue">The existing value of object being read.</param>
		/// <param name="serializer">The calling serializer.</param>
		/// <returns>
		/// The object value.
		/// </returns>
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			Guard.IsNotNull(objectType, "objectType");
			bool isNullable = objectType.IsNullableType();
			Type realType = isNullable ? objectType.GetGenericArguments()[0] : objectType;

			if (reader.TokenType == JsonToken.Null) {
				if (!isNullable) {
					throw new JsonSerializationException("Cannot convert null value to {0}.".FormatWith(objectType));
				}
				return null;
			} else {
				if (reader.TokenType == JsonToken.Date) {
					return reader.Value;
				} else {
					
					if (reader.TokenType != JsonToken.String) {
						throw new JsonSerializationException("Unexpected token parsing date. Expected String, got {0}.".FormatWith(reader.TokenType));
					}

					string text = reader.Value.ToString();
					if (string.IsNullOrEmpty(text) && isNullable) {
						return null;
					}
					
					return DateTime.Parse(text);
				}
			}

		}

		/// <summary>
		/// Writes the JSON representation of the object.
		/// </summary>
		/// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
		/// <param name="value">The value.</param>
		/// <param name="serializer">The calling serializer.</param>
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (!(value is DateTime))
				throw new JsonSerializationException("Expected date object value.");

			writer.WriteValue(((DateTime) value).ToString("d"));
		}
	}

}
