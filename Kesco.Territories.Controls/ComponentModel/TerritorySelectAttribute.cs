using System.Web.Mvc;
using Kesco.Territories.Controls.Controllers;
using Kesco.Territories.Controls.DataAccess;
using Kesco.Web.Mvc.UI.ComponentModel.DataAnnotations;

namespace Kesco.Territories.Controls.ComponentModel
{
	/// <summary>
	/// Атрибут определяет элемент управления выбора сотрудника
	/// и свойства его отбражения
	/// </summary>
	public class TerritorySelectAttribute : KescoSelectAttribute
	{
		/// <summary>
		/// Создаёт новый экземпляр <see cref="EmployeeSelectAttribute"/> класса.
		/// </summary>
		public TerritorySelectAttribute()
			: base("TerritorySelect")
		{
			EntityAccessorType = typeof(TerritorySelectAccessor);
			KeyField = "ID";
			DisplayField = "Name";
			AutocompleteController = "TerritorySelect";
			AutocompleteAction = "Search";
			AutocompleteLimit = 8;
			SelectControllerType = typeof(TerritorySelectController);
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

			/// Добавляем стандартные команды для элемента управления выбор территории
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