using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kesco.Persons.Controls.DataAccess;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.UI.ComponentModel.DataAnnotations;

namespace Kesco.Persons.Controls.ComponentModel
{
	/// <summary>
	/// Атрибут определяет для свойства модели использование
	/// элемента управления "Выбор Контакта Лица"
	/// </summary>
	public class PersonContactSelectAttribute : KescoSelectAttribute
	{

		/// <summary>
		/// The person contact select_ handle format item_ script template
		/// </summary>
		public const string PersonContactSelect_HandleFormatItem_ScriptTemplate = @"
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
		/// Идентификатор скрипта форматирования элемента списка для контакта лица
		/// </summary>
		public const string PersonContactSelect_HandleFormatItem_ScriptID = "PersonContactSelect_HandleFormatItem";

		/// <summary>
		/// Возращает или устанавливает тип действия, доступный с выбранным значением.
		/// </summary>
		/// <remarks>
		/// Контакты могут быть разных типов, включая 
		/// номера телефонов, электронный адрес, адрес в MSN.
		/// Данное свойство задаёт действие, которое может быть выполнено
		/// с введённым значением.
		/// </remarks>
		/// <value>
		/// Тип действия, доступный с выбранным значение.
		/// </value>
		public PersonContactAction Action { get; set; }

		/// <summary>
		/// Возвращает или устанавливает массив кодов типов контактов, разрешённых для хранения.
		/// Если массив пустой, то поле может хранить контакт любого типа.
		/// </summary>
		/// <value>
		/// Массив кодов типов контактов, разрешённых для хранения
		/// </value>
		public int[] AvailableContactTypes { get; set; }

		/// <summary>
		/// Создаёт новый экземпляр <see cref="PersonContactSelectAttribute"/> класса.
		/// </summary>
		public PersonContactSelectAttribute()
			: base("PersonContactSelect")
		{
			EntityAccessorType = typeof(PersonContactSelectAccessor);
			KeyField = "ID";
			DisplayField = "ContactText";
			AutocompleteController = "PersonContactSelect";
			AutocompleteAction = "Search";
			AutocompleteLimit = 8;
			ShowSearchButton = true;
			ShowViewButton = false;
			OnFormatItemClientFunction = "PersonContact_HandleFormatItem";
			Action = PersonContactAction.None;
		}

		/// <summary>
		/// Добавляет скрипт форматирования элемента списка из результатов поиска.
		/// </summary>
		/// <param name="metadata">Метаданные модели.</param>
		public override void OnMetadataCreated(ModelMetadata metadata)
		{
			base.OnMetadataCreated(metadata);

			// Добавляем скрипт форматирования элемента
			var context = new HttpContextWrapper(HttpContext.Current);

			context.RegisterCommonScript(
						PersonContactSelect_HandleFormatItem_ScriptID,
						String.Format(CultureInfo.InvariantCulture,
								PersonContactSelect_HandleFormatItem_ScriptTemplate,
								PersonContactSelect_HandleFormatItem_ScriptID
							)
					);
		}

	}
}