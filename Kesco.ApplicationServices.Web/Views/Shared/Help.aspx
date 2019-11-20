<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Dialog.Master" Inherits="System.Web.Mvc.ViewPage<Kesco.Common.HelpTopic>" culture="auto" uiculture="auto" %>
<%@ Import Namespace="Resources" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%: Model.Subject %> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
<b style="text-transform:uppercase;"><%: Model.Subject %></b>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <%: Model.Content %> <%: Model.ID.Culture %> <%: System.Globalization.CultureInfo.CurrentCulture.DisplayName %>
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" runat="server">
	<div id="kescoButtonBar"></div>
	<script type="text/javascript" language="javascript">
		$(document).ready(function () {
			$("#kescoButtonBar").kescoButtonBar({
				buttons: {
					close: { text: '<%: Resources.GUI_Button_Close %>', buttonType: 'close'}
				}
			});
		});
	</script>
</asp:Content>

