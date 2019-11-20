using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kesco.Persons.Controls.DataAccess;
using Kesco.Web.Mvc.UI;
using Kesco.Web.Mvc.UI.ComponentModel.DataAnnotations;
using Kesco.Persons.Controls.Controllers;

namespace Kesco.Persons.Controls.ComponentModel
{

	/// <summary>
	/// Атрибут определяет элемент управления выбора лица
	/// и свойства его отбражения
	/// </summary>
	public class PersonSelectAttribute : KescoSelectAttribute
	{

		/// <summary>
		/// Создаёт новый экземпляр <see cref="PersonSelectAttribute"/> класса.
		/// </summary>
		public PersonSelectAttribute()
			: base("PersonSelect")
		{
			EntityAccessorType = typeof(PersonSelectAccessor);
			KeyField = "ID";
			DisplayField = "Nickname";
			AutocompleteController = "PersonSelect";
			AutocompleteAction = "SearchEx";
			AutocompleteLimit = 8;
			SelectControllerType = typeof(PersonSelectController);
			ShowSearchButton = true;
			ShowViewButton = true;
			HideCommands = false;
		}

		/// <summary>
		/// Возвращает или устанавливает признак, указывающий не выводить команды или вывести.
		/// </summary>
		/// <value>
		///   <c>true</c> если не выводить команды, <c>false</c>.
		/// </value>
		public bool HideCommands { get; set; }

		#region Члены IMetadataAware

		/// <summary>
		/// Добавляет стандартные команды для элемента управления выбор лица,
		/// основанного на элементе управления <see cref="Kesco.Web.Mvc.UI.KescoSelect"/>.
		/// </summary>
		/// <param name="metadata">Метаданные модели.</param>
		public override void OnMetadataCreated(ModelMetadata metadata)
		{
			base.OnMetadataCreated(metadata);

			AddCommands(metadata,
				// расширенный поиск
					new KescoSelectAdvancedSearchAttribute(),
				// просмотр деталей
					new KescoSelectDetailsAttribute()
				);
			if (!HideCommands) {
				// Добавляем стандартные команды для элемента управления выбор лица
				AddCommands(metadata,
					// создание юридического лица
						new KescoSelectLinkAttribute(
								"createJuridical",
								"PersonControl_CreateJuridalPerson",
								"ui-icon-home",
								typeof(Localization.Resources),
								KescoSelectLinkShowCondition.OnlyIfLessThenLimit) { SortOrder = 1 },
					// создание физического лица
						new KescoSelectLinkAttribute(
								"createNatural",
								"PersonControl_CreateNaturalPerson",
								"ui-icon-person",
								typeof(Localization.Resources),
								KescoSelectLinkShowCondition.OnlyIfLessThenLimit) { SortOrder = 2 }
					);
			}
		}

		#endregion

	}
}