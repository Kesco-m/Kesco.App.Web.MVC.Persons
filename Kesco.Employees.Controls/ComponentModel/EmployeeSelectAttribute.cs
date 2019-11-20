using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kesco.Employees.Controls.DataAccess;
using Kesco.Web.Mvc.UI;
using Kesco.Web.Mvc.UI.ComponentModel.DataAnnotations;
using Kesco.Employees.Controls.Controllers;

namespace Kesco.Employees.Controls.ComponentModel
{
	/// <summary>
	/// Атрибут определяет элемент управления выбора сотрудника
	/// и свойства его отбражения
	/// </summary>
	public class EmployeeSelectAttribute : KescoSelectAttribute
	{
		/// <summary>
		/// Создаёт новый экземпляр <see cref="EmployeeSelectAttribute"/> класса.
		/// </summary>
		public EmployeeSelectAttribute()
			: base("EmployeeSelect")
		{
			EntityAccessorType = typeof(EmployeeSelectAccessor);
			KeyField = "ID";
			DisplayField = "LastNameWithInitials";
			AutocompleteController = "EmployeeSelect";
			AutocompleteAction = "Search";
			AutocompleteLimit = 8;
			SelectControllerType = typeof(EmployeeSelectController);
			ShowSearchButton = true;
			ShowViewButton = true;
		}

		#region Члены IMetadataAware

		/// <summary>
		/// Добавляет стандартные команды для элемента управления выбор сотрудника,
		/// основанного на элементе управления <see cref="Kesco.Web.Mvc.UI.KescoSelect"/>.
		/// </summary>
		/// <param name="metadata">Метаданные модели.</param>
		public override void OnMetadataCreated(ModelMetadata metadata)
		{
			base.OnMetadataCreated(metadata);

			/// Добавляем стандартные команды для элемента управления выбор сотрудника
			AddCommands(metadata,
					// расширенный поиск
					new KescoSelectAdvancedSearchAttribute(),
					// просмотр деталей
					new KescoSelectDetailsAttribute()
				);
		}

		#endregion
	}
}