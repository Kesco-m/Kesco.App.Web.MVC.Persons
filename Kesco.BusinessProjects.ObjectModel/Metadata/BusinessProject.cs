using System.ComponentModel.DataAnnotations;
using Kesco.BusinessProjects.ObjectModel.Localization;

namespace Kesco.BusinessProjects.ObjectModel.Metadata
{
    /// <summary>
    /// vwБизнесПроекты
    /// </summary>
    internal class BusinessProject
	{
        /// <summary>
        /// КодБизнесПроекта
        /// </summary>
		[UIHint("UniqueID")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_BProjects_MDL_101",
                ShortName = "Kesco_BProjects_MDL_102",
                Description = "Kesco_BProjects_MDL_103",
                Prompt = "Kesco_BProjects_MDL_104"
            )] 
		public int ID { get; set; }

        /// <summary>
        /// БизнесПроект
        /// </summary>
        [UIHint("TextBox")]
        [Display(ResourceType = typeof(Resources),
                Name = "Kesco_BProjects_MDL_105",
                ShortName = "Kesco_BProjects_MDL_106",
                Description = "Kesco_BProjects_MDL_107",
                Prompt = "Kesco_BProjects_MDL_108"
            )]    
        public string Name { get; set; }

	}

	
}