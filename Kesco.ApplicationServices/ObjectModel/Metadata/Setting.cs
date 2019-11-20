using System.ComponentModel.DataAnnotations;
using Kesco.ApplicationServices.Localization;

namespace Kesco.ApplicationServices
{
	[MetadataType(typeof(Setting_Metadata))]
	//[Display(ResourceType = typeof(Resources), Name="Setting_Display_Name")]
	public abstract partial class Setting 
	{
		public class Setting_Metadata
		{

			[Display(ResourceType = typeof(Resources), Name="Setting_ID_Display_Name")]
			[UIHint("UniqueID")]
			public string  ID { get; set; }

			[Display(ResourceType = typeof(Resources), Name="Setting_ParameterType_Display_Name")]
			public int  ParameterType { get; set; }

			[Display(ResourceType = typeof(Resources), Name="Setting_ValidValues_Display_Name")]
			public string  ValidValues { get; set; }

			[Display(ResourceType = typeof(Resources), Name="Setting_DefaultValue_Display_Name")]
			public string  DefaultValue { get; set; }

			[Display(ResourceType = typeof(Resources), Name = "Setting_Description_Display_Name")]
			public string  Description { get; set; } 
    
		}
	}
}
