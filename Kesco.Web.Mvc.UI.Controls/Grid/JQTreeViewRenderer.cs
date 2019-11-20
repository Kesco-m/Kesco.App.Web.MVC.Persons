namespace Kesco.Web.Mvc.UI.Grid
{
    using System;
    using System.Text;
    using System.Web.Script.Serialization;
	using Kesco;

    internal class JQTreeViewRenderer
    {
		private JQTreeView _model;
		public JQTreeViewRenderer(JQTreeView model)
		{
			this._model = model;
		}
		public string RenderHtml()
		{
			Guard.IsNotNullOrEmpty(this._model.ID, "ID", "You need to set ID for this JQTree instance.");
			Guard.IsNotNullOrEmpty(this._model.DataUrl, "DataUrl", "You need to set DataUrl to the Action of the tree returning nodes.");
			return this.GetStandaloneJavascript();
		}
		private string GetStandaloneJavascript()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("<div id='{0}_wrapper'class='ui-jqtreeview-wrapper'>", this._model.ID);
			stringBuilder.AppendFormat("<ul id='{0}'></ul>", this._model.ID);
			stringBuilder.Append("</div>");
			stringBuilder.Append("<script type='text/javascript'>\n");
			stringBuilder.Append("jQuery(document).ready(function() {");
			stringBuilder.AppendFormat("jQuery('#{0}').jqTreeView({{", this._model.ID);
			stringBuilder.Append(this.GetStartupOptions());
			stringBuilder.Append("});");
			stringBuilder.Append("});");
			stringBuilder.Append("</script>");
			return stringBuilder.ToString();
		}
		private string GetStartupOptions()
		{
			new JavaScriptSerializer();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("id: '{0}'", this._model.ID);
			stringBuilder.AppendFormat(",dataUrl: '{0}'", this._model.DataUrl);
			return stringBuilder.ToString();
		}
	}
}
