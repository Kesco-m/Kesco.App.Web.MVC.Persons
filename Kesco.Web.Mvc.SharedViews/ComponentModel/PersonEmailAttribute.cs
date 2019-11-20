using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Globalization;

namespace Kesco.Web.Mvc.SharedViews.ComponentModel
{
	/// <summary>
	/// Класс реализует атрибут, описывающий элемент управления 
	/// выбора электронного адреса из контактов лица
	/// </summary>
	public class PersonEmailAttribute : PersonContactControlAttribute
	{

		/// <summary>
		/// Представляет скрипт форматирования контакта, 
		/// представляющий телефон, как элемента списка
		/// </summary>
		public const string PersonEmailScriptTemplate = @"
			window.PersonPhone_HandleFormatItem = function (item) {{
				var comment = (item.data.Comment) ? item.data.Comment : '';
				var s = '';
				s += "" <span style='width: 150px; overflow: hidden; text-overflow: ellipsis; white-space: nowrap; display:inline-block;'>"" + item.data.ContactText + ""</span>"";
				s += "" <span style='width: 150px; overflow: hidden; text-overflow: ellipsis; white-space: nowrap; display:inline-block;'>"" + item.data.ContactTypeDesc + ""</span>"";
				s += "" <span style='overflow: hidden; text-overflow: ellipsis; white-space: nowrap; display:inline-block;'>"" + comment + ""</span>"";
				return s;
			}}
		";

		/// <summary>
		/// Возвращает идентификатор скрипта в элементах контекста (Context.Items["Scripts"])
		/// </summary>
		/// <value>
		/// The script ID.
		/// </value>
		public override string ScriptID {
			get { return "PersonEmail_HandleFormatItem"; }
		}

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="PersonAddressControlAttribute"/> атрибута.
		/// </summary>
		public PersonEmailAttribute()
			: base()
		{
			AvailableContactTypes = new int[] { 40 };
			Action = PersonContactAction.SendEmail;
		}

		/// <summary>
		/// Возвращает скрипт для элемента управления PersonContact
		/// </summary>
		/// <param name="metadata">Метаданные модели.</param>
		/// <returns>скрипт для элемента управления PersonContact</returns>
		public override string GetScript(ModelMetadata metadata)
		{
			return string.Format(CultureInfo.InvariantCulture, PersonEmailScriptTemplate, ScriptID);
		}

	}
}