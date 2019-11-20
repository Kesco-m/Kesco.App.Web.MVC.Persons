using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;

using Kesco.ApplicationServices.Localization;
using Kesco.ComponentModel.DataAnnotations;
using Kesco.ObjectModel;

namespace Kesco.Common
{
	///////////////////////////////////////////////////////////////////// 
	// ClientApplicationSetting class declaration
	//
	/// <summary>
	/// 
	/// </summary>
	[MetadataType(typeof(ClientApplicationSetting_Metadata))]
	//[Display(ResourceType = typeof(Resources), Name = "ClientApplicationSetting_Display_Name")]
	public abstract partial class ClientApplicationSetting
	{
		public class ClientApplicationSetting_Metadata
		{

			[Display(ResourceType = typeof(Resources), Name = "ClientApplicationSetting_ID_Display_Name")]
			[UIHint("UniqueID")]
			public int ID { get; set; }


			[Display(ResourceType = typeof(Resources), Name = "ClientApplicationSetting_DefaultValue_Display_Name")]
			[UIHint("TwoLinesTextBox")]
			[StringLength(100)]
			public string DefaultValue { get; set; }


			[Display(ResourceType = typeof(Resources), Name = "ClientApplicationSetting_Server_Display_Name")]
			[UIHint("TextBox")]
			[StringLength(20)]
			public string Server { get; set; }


			[Display(ResourceType = typeof(Resources), Name = "ClientApplicationSetting_Client_Display_Name")]
			[UIHint("TextBox")]
			[StringLength(20)]
			public string Client { get; set; }


			[Display(ResourceType = typeof(Resources), Name = "ClientApplicationSetting_Description_Display_Name")]
			[UIHint("TwoLinesTextBox")]
			[StringLength(300)]
			public string Description { get; set; }


			[Display(ResourceType = typeof(Resources), Name = "ClientApplicationSetting_Prefix_Display_Name")]
			[UIHint("TextBox")]
			[StringLength(50)]
			public string Prefix { get; set; }

		}
	}


}
