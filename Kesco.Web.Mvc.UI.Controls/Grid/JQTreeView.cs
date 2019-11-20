using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace Kesco.Web.Mvc.UI.Grid
{
	public class JQTreeView
	{
		public string ID { get; set; }

		public string DataUrl { get; set; }

		public Unit Height { get; set; }

		public Unit Width { get; set; }

		public JQTreeView()
		{
			this.ID = "";
			this.DataUrl = "";
			this.Width = Unit.Empty;
			this.Height = Unit.Empty;
		}

		public JsonResult DataBind(List<JQTreeNode> nodes)
		{
			return new JsonResult() {
				JsonRequestBehavior = JsonRequestBehavior.AllowGet,
				Data = (object)new JavaScriptSerializer().Serialize((object)this.SerializeNodes(nodes))
			};
		}

		private List<Hashtable> SerializeNodes(List<JQTreeNode> nodes)
		{
			List<Hashtable> list = new List<Hashtable>();
			foreach (JQTreeNode jqTreeNode in nodes)
				list.Add(jqTreeNode.ToHashtable());
			return list;
		}
	}
}
