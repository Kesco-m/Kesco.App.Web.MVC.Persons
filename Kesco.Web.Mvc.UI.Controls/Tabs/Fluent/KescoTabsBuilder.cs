using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc.UI.Fluent
{

	/// <summary>
	/// Класс, реализующий построитель для элемента управления KescoDatePicker.
	/// </summary>
	public class KescoTabsBuilder : ControlBuilderBase<KescoTabs, KescoTabsBuilder>
	{

		public KescoTabsBuilder(KescoTabs control) : base(control) { }

		public KescoTabsBuilder Tabs(Action<IList<KescoTabItem>> tabsAction)
		{
			tabsAction(control.Tabs);
			return this;
		}

		public KescoTabsBuilder AutoResize(bool autoResize)
		{
			control.AutoResize = autoResize;
			return this;
		}

		public KescoTabsBuilder AutoResize()
		{
			return AutoResize(true);
		}

	}
}
