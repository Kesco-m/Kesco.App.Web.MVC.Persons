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
	/// Описывает доступные действия с выбранным значением контакта
	/// </summary>
	public enum PersonContactAction : int
	{
        /// <summary>
        /// Ничего не делать
        /// </summary>
		None		= 0,

        /// <summary>
        /// Сделать звонок
        /// </summary>
		MakeCall	= 1,
        
        /// <summary>
        /// Написать электронное письмо
        /// </summary>
		SendEmail	= 2,

        /// <summary>
        /// Открыть MSN Messenger
        /// </summary>
		MSN			= 3 
	}

	/// <summary>
	/// Класс реализует атрибут, описывающий элемент управления выбора контакта
	/// </summary>
	public class PersonContactControlAttribute : UIHintAttribute, IMetadataAware
	{
		/// <summary>
		/// Возвращает текущий HTTP-контекст
		/// </summary>
		internal HttpContextBase Context {
			get { return new HttpContextWrapper(HttpContext.Current); }
		}

		public const string ScriptTemplate = @"
			window.PersonContact_HandleFormatItem = function (item) {{
				var comment = (item.data.Comment) ? item.data.Comment : '';
				var s = '';
				s += "" <span style='width: 300px; overflow: hidden; text-overflow: ellipsis; white-space: nowrap; display:inline-block;'>"" + item.data.ContactText + ""</span>"";
				s += "" <span style='width: 120px; overflow: hidden; text-overflow: ellipsis; white-space: nowrap; display:inline-block;'>"" + item.data.ContactTypeDesc + ""</span>"";
				s += "" <span style='overflow: hidden; text-overflow: ellipsis; white-space: nowrap; display:inline-block;'>"" + comment + ""</span>"";
				return s;
			}}
		";

		/// <summary>
		/// Возвращает или устанавливает массив кодов типов контактов, разрешённых для хранения.
		/// Если массив пустой, то поле может хранить контакт любого типа.
		/// </summary>
		/// <value>
		/// Массив кодов типов контактов, разрешённых для хранения
		/// </value>
		public int[] AvailableContactTypes { get; set; }

		/// <summary>
		/// Возвращает или устанавливает имя свойства модели, хранящее код лица.
		/// </summary>
		/// <value>
		/// Имя свойства модели, хранящее код лица.
		/// </value>
		public string PersonIDProperty { get; set; }

		/// <summary>
		/// Возвращает идентификатор скрипта в элементах контекста (Context.Items["Scripts"])
		/// </summary>
		/// <value>
		/// The script ID.
		/// </value>
		public virtual string ScriptID {
			get { return "PersonContact_HandleFormatItem"; }
		}

		/// <summary>
		/// Возращает или устанавливает тип действия, доступный с выбранным значением.
		/// </summary>
		/// <value>
		/// тип действия, доступный с выбранным значение.
		/// </value>
		public PersonContactAction Action { get; set; }

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="PersonContactControlAttribute"/> атрибута.
		/// </summary>
		public PersonContactControlAttribute()
			: base("PersonContact")
		{
			Action = PersonContactAction.None;
		}

		/// <summary>
		/// Возвращает скрипт для элемента управления PersonContact
		/// </summary>
		/// <param name="metadata">Метаданные модели.</param>
		/// <returns>скрипт для элемента управления PersonContact</returns>
		public virtual string GetScript(ModelMetadata metadata)
		{
			return string.Format(CultureInfo.InvariantCulture, ScriptTemplate, ScriptID);
		}


		#region Члены IMetadataAware

		/// <summary>
		/// При реализации в классе предоставляет метаданные для процесса создания метаданных модели.
		/// </summary>
		/// <param name="metadata">Метаданные модели.</param>
		public virtual void OnMetadataCreated(ModelMetadata metadata)
		{
			// Добавляем скрипт форматирования элемента
			Context.RegisterCommonScript(ScriptID, GetScript(metadata));

			// Добавляем дополнительные значения для элемента управления PersonContact
			metadata.AdditionalValues["PersonContactControl.PersonIDProperty"] = (AvailableContactTypes != null) ? String.Join(",", AvailableContactTypes) : "";
			metadata.AdditionalValues["PersonContactControl.AvailableContactTypes"] = (AvailableContactTypes != null) ? String.Join(",", AvailableContactTypes) : "";
			metadata.AdditionalValues["PersonContactControl.ScriptID"] = ScriptID;
			metadata.AdditionalValues["PersonContactControl.Action"] = ((int) Action).ToString();

		}

		#endregion

	}
}