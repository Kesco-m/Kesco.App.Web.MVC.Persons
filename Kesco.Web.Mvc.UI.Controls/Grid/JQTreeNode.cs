namespace Kesco.Web.Mvc.UI.Grid
{
	using System.Collections;
	using System.Collections.Generic;
	using System.Web.Script.Serialization;

    public class JQTreeNode
    {

		public string Text
		{
			get;
			set;
		}
		public string Value
		{
			get;
			set;
		}
		public List<JQTreeNode> Nodes
		{
			get;
			set;
		}
		public bool Expanded
		{
			get;
			set;
		}
		public bool Enabled
		{
			get;
			set;
		}
		public bool Selected
		{
			get;
			set;
		}
		public string Url
		{
			get;
			set;
		}
		public string ImageUrl
		{
			get;
			set;
		}
		public JQTreeNode()
		{
			this.Text = "";
			this.Value = "";
			this.Nodes = new List<JQTreeNode>();
			this.Selected = false;
			this.Expanded = false;
			this.Enabled = true;
			this.Url = "";
			this.ImageUrl = "";
		}

		public string ToJSON()
		{
			return new JavaScriptSerializer().Serialize(this.ToHashtable());
		}

		internal Hashtable ToHashtable()
		{
			Hashtable hashtable = new Hashtable();
			if (!string.IsNullOrEmpty(this.Text)) {
				hashtable.Add("text", this.Text);
			}
			if (!string.IsNullOrEmpty(this.Value)) {
				hashtable.Add("value", this.Text);
			}
			if (this.Expanded) {
				hashtable.Add("expanded", true);
			}
			if (!this.Enabled) {
				hashtable.Add("enabled", false);
			}
			if (this.Selected) {
				hashtable.Add("selected", true);
			}
			if (!string.IsNullOrEmpty(this.Url)) {
				hashtable.Add("url", this.Url);
			}
			if (!string.IsNullOrEmpty(this.ImageUrl)) {
				hashtable.Add("imageUrl", this.ImageUrl);
			}
			List<Hashtable> list = new List<Hashtable>();
			foreach (JQTreeNode current in this.Nodes) {
				list.Add(current.ToHashtable());
			}
			if (list.Count > 0) {
				hashtable.Add("nodes", list);
			}
			return hashtable;
		}

	}
}
