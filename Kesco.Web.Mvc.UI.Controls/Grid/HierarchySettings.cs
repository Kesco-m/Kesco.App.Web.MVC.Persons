namespace Kesco.Web.Mvc.UI.Grid
{
	using System.Collections;
	using System.Web.Script.Serialization;

    public class HierarchySettings
    {

		public HierarchyMode HierarchyMode { get; set; }

		public string PlusIcon { get; set; }

		public string MinusIcon { get; set; }

		public string OpenIcon { get; set; }

		public bool ExpandOnLoad { get; set; }

		public bool SelectOnExpand { get; set; }

		public bool ReloadOnExpand { get; set; }

		public HierarchySettings()
		{
			this.HierarchyMode = HierarchyMode.None;
			this.PlusIcon = "ui-icon-plus";
			this.MinusIcon = "ui-icon-minus";
			this.OpenIcon = "ui-icon-carat-1-sw";
			this.ExpandOnLoad = false;
			this.SelectOnExpand = false;
			this.ReloadOnExpand = true;
		}

		internal string ToJSON()
		{
			return new JavaScriptSerializer().Serialize((object)this.ToHashtable());
		}

		internal Hashtable ToHashtable()
		{
			Hashtable hashtable = new Hashtable();
			if (this.PlusIcon != null && this.PlusIcon != "ui-icon-plus")
				hashtable.Add((object)"plusicon", (object)this.PlusIcon);
			if (this.MinusIcon != null && this.MinusIcon != "ui-icon-minus")
				hashtable.Add((object)"minusicon", (object)this.MinusIcon);
			if (this.OpenIcon != null && this.OpenIcon != "ui-icon-carat-1-sw")
				hashtable.Add((object)"openicon", (object)this.OpenIcon);
			if (this.ExpandOnLoad)
				hashtable.Add((object)"expandOnLoad", (object)true);
			if (this.SelectOnExpand)
				hashtable.Add((object)"selectOnExpand", (object)true);
			if (!this.ReloadOnExpand)
				hashtable.Add((object)"reloadOnExpand", (object)false);
			return hashtable;
		}
	}
}
