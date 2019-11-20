<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DialogResultModel>" %>

	<script type="text/javascript" language="javascript">
		$(document).ready(function () {
<%	if (!String.IsNullOrWhiteSpace(Model.DialogResult.ReturnUri)) { %>
				$("#dialogResultForm").attr("action", "<%= Model.DialogResult.ReturnUri %>");
<%	} %>
<%	if (!String.IsNullOrWhiteSpace(Model.DialogResult.Key)) { %>
				$("#dialogResultField_Control").val("<%= Model.DialogResult.Key %>");
<%	} %>

		});

	</script>
