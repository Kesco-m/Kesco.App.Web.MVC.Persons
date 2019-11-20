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
	/// Описывает доступные контакты лица
	/// </summary>
	public enum EmployeeContactType : int
	{
		Unknown = 0,
		Phone = -1,
		Email = -2,
		MSN = -3
	}

	/// <summary>
	/// Класс реализует атрибут, описывающий элемент управления выбора контакта
	/// </summary>
	public class EmployeeContactControlAttribute : UIHintAttribute, IMetadataAware
	{
		/// <summary>
		/// Возвращает текущий HTTP-контекст
		/// </summary>
		internal HttpContextBase Context {
			get { return new HttpContextWrapper(HttpContext.Current); }
		}

		/// <summary>
		/// Определяет клиентский скрипт форматирования
		/// контакта сотрудника в выпадающем списке
		/// </summary>
		public const string ScriptTemplate = @"
			window.EmployeeContact_HandleFormatItem = function (item) {{
				var comment = (item.data.Note) ? item.data.Note : '';
				var s = '';
				s += "" <span style='width: 120px; overflow: hidden; text-overflow: ellipsis; white-space: nowrap; display:inline-block;'>"" + item.data.Contact + ""</span>"";
				s += "" <span style='width: 150px; overflow: hidden; text-overflow: ellipsis; white-space: nowrap; display:inline-block;'>"" + item.data.ContactTypeDesc + ""</span>"";
				s += "" <span style='overflow: hidden; text-overflow: ellipsis; white-space: nowrap; display:inline-block;'>"" + comment + ""</span>"";
				return s;
			}}
		";

		/// <summary>
		/// Возвращает или устанавливает тип контакта, разрешённых для хранения.
		/// </summary>
		/// <value>
		/// Тип контактов, разрешённых для хранения
		/// </value>
		public EmployeeContactType ContactType { get; set; }

		/// <summary>
		/// Возвращает идентификатор скрипта в элементах контекста (Context.Items["Scripts"])
		/// </summary>
		/// <value>
		/// The script ID.
		/// </value>
		public virtual string ScriptID {
			get { return "EmployeeContact_HandleFormatItem"; }
		}

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="PersonContactControlAttribute"/> атрибута.
		/// </summary>
		public EmployeeContactControlAttribute()
			: base("EmployeeContact")
		{
			ContactType = EmployeeContactType.Unknown;
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
			metadata.AdditionalValues["EmployeeContactControl.ContactType"] = ((int) ContactType).ToString();
			metadata.AdditionalValues["EmployeeContactControl.ScriptID"] = ScriptID;

		}

		#endregion

	}
}