namespace Kesco.Web.Mvc.UI.Grid
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public sealed class GroupSettings
    {

        public GroupSettings()
        {
            this.CollapseGroups = false;
            this.GroupFields = new List<GroupField>();
        }

		// Properties
		public bool CollapseGroups { get; set; }
		public List<GroupField> GroupFields { get; set; }

	}
}
