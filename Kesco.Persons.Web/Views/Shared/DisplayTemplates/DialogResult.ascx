<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DialogResult>" %>

	<script type="text/javascript" language="javascript">
		$(document).ready(function () {
<%	if (!String.IsNullOrWhiteSpace(Model.ReturnUri)) { %>
				$("#dialogResultForm").attr("action", "<%= Model.ReturnUri %>");
<%	} %>
<%	if (!String.IsNullOrWhiteSpace(Model.Key)) { %>
				$("# dialogResultField_Control").val("<%= Model.Key %>");
<%	} %>

		});
	</script>
