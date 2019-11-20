using System.Text;
using System.Web.Script.Serialization;
using Kesco;

namespace Kesco.Web.Mvc.UI.Grid
{

	internal class JQAutoCompleteExRenderer
	{
		private JQAutoCompleteEx _model;

		public JQAutoCompleteExRenderer(JQAutoCompleteEx autoComplete)
		{
			this._model = autoComplete;
		}

		private string GetControlEditorJavascript()
		{
			return string.Format("<script type='text/javascript'>var {0}_acid = {{ {1} }};</script>", this._model.ID, this.GetStartupOptions());
		}

		private string GetStandaloneJavascript()
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat("<input type='text' id='{0}' name='{0}' />"
				, this._model.ID, this._model.DataValueField, this._model.DataLabelField);
			builder.Append("<script type='text/javascript'>\n");
			builder.Append("jQuery(document).ready(function() {");
			builder.AppendFormat("jQuery('#{0}').autocomplete({{", this._model.ID);
			builder.Append(this.GetStartupOptions());
			builder.Append("});");
			builder.Append("});");
			builder.Append("</script>");
			return builder.ToString();
		}

		private string GetStartupOptions()
		{
			new JavaScriptSerializer();
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("id: '{0}'", this._model.ID);
			sb.AppendFormat(",source: '{0}'", this._model.DataUrl);
			sb.AppendFormatIfTrue(this._model.Delay != 300, ",delay: {0}", new object[] { this._model.Delay });
			sb.AppendIfFalse(this._model.Enabled, ",disabled: true");
			if (!string.IsNullOrWhiteSpace(this._model.ClientEvents.FormatItem)) {
				sb.AppendFormat(@", formatItem: {0}", this._model.ClientEvents.FormatItem);
			} else {
				sb.AppendFormat(@", formatItem: function(item) {{
					return item['{2}'];
				}}", this._model.ID, this._model.DataValueField, this._model.DataLabelField);
			}
			if (!string.IsNullOrWhiteSpace(this._model.ClientEvents.OnItemFocus)) {
				sb.AppendFormat(@", focus: {0}", this._model.ClientEvents.OnItemFocus);
			} else {
				sb.AppendFormat(@", focus: function( event, ui ) {{
						$(this).val( ui.item['{2}'] );
						return false;
					}}", this._model.ID, this._model.DataValueField, this._model.DataLabelField);
			}
			if (!string.IsNullOrWhiteSpace(this._model.ClientEvents.OnItemSelect)) {
				sb.AppendFormat(@", select: {0}", this._model.ClientEvents.OnItemSelect);
			} else {
				sb.AppendFormat(@", select: function( event, ui ) {{
					// this - input control, so this.form['namefield']
					$(this)
						.val(ui.item['{2}'])
						.attr('data-value', ui.item['{1}']);
					$(this.form['{1}']).val(ui.item['{1}']);
					return false;
				}}", this._model.ID, this._model.DataValueField, this._model.DataLabelField);
			}
			sb.AppendFormatIfTrue(this._model.MinLength != 1, ",minLength: {0}", new object[] { this._model.MinLength });
			return sb.ToString();
		}

		public string RenderHtml()
		{
			Guard.IsNotNullOrEmpty(this._model.ID, "ID", "You need to set ID for this JQAutoComplete instance.");
			if (this._model.DisplayMode == AutoCompleteDisplayMode.Standalone) {
				return this.GetStandaloneJavascript();
			}
			return this.GetControlEditorJavascript();
		}
	}
}
