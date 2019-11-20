using System.Web.Mvc;
using Kesco.BusinessProjects.Controls.Controllers;
using Kesco.BusinessProjects.Controls.DataAccess;
using Kesco.Web.Mvc.UI.ComponentModel.DataAnnotations;

namespace Kesco.BusinessProjects.Controls.ComponentModel
{
	/// <summary>
	/// Атрибут определяет элемент управления выбора бизнес-проекта
	/// и свойства его отбражения
	/// </summary>
	public class BusinessProjectSelectAttribute : KescoSelectAttribute
	{
		/// <summary>
		/// Создаёт новый экземпляр <see cref="BusinessProjectSelectAttribute"/> класса.
		/// </summary>
		public BusinessProjectSelectAttribute()
			: base("BusinessProjectSelect")
		{
			EntityAccessorType = typeof(BusinessProjectSelectAccessor);
			KeyField = "ID";
			DisplayField = "Name";
			AutocompleteController = "BusinessProjectSelect";
			AutocompleteAction = "Search";
			AutocompleteLimit = 8;
			SelectControllerType = typeof(BusinessProjectSelectController);
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
					new KescoSelectAdvancedSearchAttribute()
					// просмотр деталей
					//,new KescoSelectDetailsAttribute()
				);
		}

		#endregion
	}
}