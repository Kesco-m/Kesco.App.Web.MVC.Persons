using System.ComponentModel.DataAnnotations;
using Kesco.ApplicationServices.Localization;

namespace Kesco.ApplicationServices
{
	[MetadataType(typeof(UserSetting_Metadata))]
	//[Display(ResourceType=typeof(Resources), Name="UserSetting_Display_Name")]
	public abstract partial class UserSetting 
	{
		public class UserSetting_Metadata
		{
					
			[Display(ResourceType=typeof(Resources), Name="UserSetting_ClientAplicationSettingID_Display_Name")]
			[UIHint("int")]
			public int ClientApplicationID { get; set; } 
			
			[Display(ResourceType=typeof(Resources), Name="UserSetting_Parameter_Display_Name")]
			[UIHint("string")]
			[StringLength(20)]
			public string Parameter { get; set; } 
			
			
			[Display(ResourceType=typeof(Resources), Name="UserSetting_Value_Display_Name")]
			[UIHint("string")]
			[StringLength(1000)]
			public string Value { get; set; } 
    
		}
	}

}
