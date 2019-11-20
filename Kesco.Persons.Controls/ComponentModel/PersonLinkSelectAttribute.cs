using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kesco.Persons.Controls.DataAccess;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.UI;
using Kesco.Web.Mvc.UI.ComponentModel.DataAnnotations;

namespace Kesco.Persons.Controls.ComponentModel
{

	/// <summary>
	/// Атрибут определяет элемент управления выбора связи лица
	/// </summary>
	public class PersonLinkSelectAttribute : KescoSelectAttribute
	{

		/// <summary>
		/// Идентификатор скрипта форматирования элемента списка для связей лиц
		/// </summary>
		public const string PersonLinkSelect_HandleFormatItem_ScriptID = "PersonLinkSelect_HandleFormatItem";

		/// <summary>
		/// Cкрипта форматирования элемента списка для связей лица
		/// </summary>
		public const string PersonLinkSelect_HandleFormatItem_ScriptTemplate = @"
			window.PersonLink_HandleFormatItem = function (item) {{
				var comment = (item.data.Description) ? item.data.Description : '';
				var s = '';
				s += "" <span style='width: 200px; overflow: hidden; text-overflow: ellipsis; white-space: nowrap; display:inline-block;'>"" + item.data.FriendlyName + ""</span>"";
				s += "" <span style='overflow: hidden; text-overflow: ellipsis; white-space: nowrap; display:inline-block;'><small><i>"" + comment + ""</i></small></span>"";
				return s;
			}}
		";

		/// <summary>
		/// Создаёт новый экземпляр <see cref="PersonLinkSelectAttribute"/> класса.
		/// </summary>
		public PersonLinkSelectAttribute()
			: base("PersonLinkSelect")
		{
			EntityAccessorType = typeof(PersonLinkSelectAccessor);
			KeyField = "ID";
			DisplayField = "FriendlyName";
			AutocompleteController = "PersonLinkSelect";
			AutocompleteAction = "SearchEx";
			AutocompleteLimit = 8;
			ShowSearchButton = true;
			ShowViewButton = true;
			OnFormatItemClientFunction = "PersonLink_HandleFormatItem";
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
					PersonLinkSelect_HandleFormatItem_ScriptID,
					String.Format(CultureInfo.InvariantCulture,
							PersonLinkSelect_HandleFormatItem_ScriptTemplate,
							PersonLinkSelect_HandleFormatItem_ScriptID
						)
				);
		}

	}
}