using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Kesco.Common
{
	using Kesco.Localization;

	public static class HelpManager
	{
		public static HelpTopic GetHelpTopic(string id)
		{	
			HelpTopic topic = HelpTopic.CreateInstance();
			HelpTopicID topicID = new HelpTopicID();
			topicID.ID = id;
			topicID.Culture = CultureInfo.CurrentCulture.DisplayName;
			topic.ID = topicID;
			topic.Subject = Resources.HelpTopic_Subject;
			topic.Content = Resources.HelpTopic_Content;
			return topic;
		}

	}
}
