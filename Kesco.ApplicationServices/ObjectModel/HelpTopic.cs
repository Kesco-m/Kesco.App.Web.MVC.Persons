using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using Kesco.ComponentModel.DataAnnotations;
using Kesco.Localization;
using Kesco.ObjectModel;

namespace Kesco.Common
{
	
	
	public struct HelpTopicID
	{
		public string ID { get; set; }
		public string Culture { get; set; }

		public override string ToString()
		{
			return String.Format("{0}-{1}", ID, Culture);
		}
	}

	public abstract class HelpTopic : Entity<HelpTopic, HelpTopicID>
	{
		[MapField("КодТемыСправки"), PrimaryKey, NonUpdatable]
		[Display(ResourceType = typeof(Resources), Name="HelpTopic_ID_Display_Name")]
		[UIHint("UniqueID")]
		public override HelpTopicID ID { get; set; }

		[MapField("ТемаСправки")]
		[Display(ResourceType = typeof(Resources), Name="HelpTopic_Subject_Display_Name")]
		public string Subject { get; set; }

		[MapField("СодержаниеСправки")]
		[Display(ResourceType = typeof(Resources), Name="HelpTopic_Content_Display_Name")]
		public string Content { get; set; }

	}
}
