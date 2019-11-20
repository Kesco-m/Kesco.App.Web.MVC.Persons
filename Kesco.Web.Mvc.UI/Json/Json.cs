using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kesco.Web.Mvc
{
	public static class Json
	{
		/// <summary>
		/// Сериализует данные в формат JSON.
		/// </summary>
		/// <param name="Data">>Данные, которые необходимо сериализовать.</param>
		/// <param name="useJavaScriptDateTimeConverter">если установлено в <c>true</c>, то будет использован JavaScript Date объект].</param>
		/// <returns>
		/// JSON
		/// </returns>
		public static string Serialize(object data, bool useJavaScriptDateTimeConverter)
		{
			if (data != null) {
				return useJavaScriptDateTimeConverter
					? JsonConvert.SerializeObject(data, new JavaScriptDateTimeConverter())
					: JsonConvert.SerializeObject(data);
			}
			return "null";
		}

		/// <summary>
		/// Сериализует данные в формат JSON.
		/// </summary>
		/// <param name="data">Данные, которые необходимо.</param>
		/// <returns>JSON</returns>
		public static string Serialize(object data)
		{
			return Serialize(data, false);
		}

		///// <summary>
		///// Десериализует JSON в объект.
		///// </summary>
		///// <param name="json">The json.</param>
		///// <returns></returns>
		//public static dynamic Deserialize(string json)
		//{
		//    var settings = new JsonSerializerSettings();
		//    settings.Converters.Add(new JavaScriptDateTimeConverter());
		//    return JsonConvert.DeserializeObject(json, settings);
		//}

		/// <summary>
		/// Десериализует JSON в объект, определённого типа.
		/// </summary>
		/// <param name="json">The json.</param>
		/// <returns></returns>
		public static T Deserialize<T>(string json)
			where T : class, new()
		{
			return JsonConvert.DeserializeObject<T>(json, new JavaScriptDateTimeConverter());
		}
	}


	//string str = JsonConvert.SerializeObject(new DateTimeClass(), new MyDateTimeConvertor()); 
	
}
