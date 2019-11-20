using System.Web.Mvc;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.Controls.Controllers;
using Kesco.Persons.Controls.DataAccess;
using Kesco.Web.Mvc.UI.ComponentModel.DataAnnotations;

namespace Kesco.Persons.Controls.ComponentModel
{
    public class PersonThemeSelectAttribute : KescoSelectAttribute
	{

		public int PersonKind { get; set; }

        public PersonThemeSelectAttribute()
            : base("PersonThemeSelect")
		{
            EntityAccessorType = typeof(PersonThemeSelectAccessor);
			KeyField = "ID";
			DisplayField = "Name";
            AutocompleteController = "PersonThemeSelect";
            AutocompleteAction = "Search";
			AutocompleteLimit = 8;
            SelectControllerType = typeof(PersonThemeSelectController);
			ShowSearchButton = true;
			ShowViewButton = false;
            
                
		}
		public object GetOptions()
		{
            return Repository.PersonTheme.GetAll();
		}

		#region Члены IMetadataAware

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