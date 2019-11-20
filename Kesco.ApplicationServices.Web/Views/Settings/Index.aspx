<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Dialog.Master" Inherits="System.Web.Mvc.ViewPage<Kesco.ApplicationServices.Web.Models.UserSettingsViewModel>" %>
<%@ Import Namespace="Kesco.ApplicationServices.Web.Localization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: Resources.ApplicationServices_Index_PageTitle %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
<div id="toolBar" class="ui-widget-header ui-corner-all kui-toolbar">
		<table cellspacing="0" cellpadding="0" border="0" style="margin-top: -2px; " width="100%">
		<tr valign="middle">
			<td nowrap="nowrap">
	<button id="tbRefreshButton" onclick="refreshPage()"><%= Resources.GUI_Button_Refresh %></button>
	&nbsp;&nbsp;
	<button id="tbFindButton" onclick="toggleSearchForm()"><%= Kesco.Web.Mvc.UI.Controls.Localization.Resources.GUI_Menu_Edit_Find %></button>
			</td>
			<td>&nbsp;</td>
			<td nowrap="nowrap" style="width: 60px;">
				<% Html.RenderPartial("ThemeRoller"); %>
			</td>
			<td nowrap="nowrap" style="width: 30px;">
				<a href="javascript: void(0)" id="tbHelpButton" onclick="showHelp()"
					><%= Kesco.Web.Mvc.UI.Controls.Localization.Resources.GUI_Menu_Help %></a>
			</td>
		</tr>
		</table>
</div>
<script type="text/javascript">
	$(function() {
		$("#tbRefreshButton").button({
			text: false,
			icons: {
				primary: "ui-icon-refresh"
			}
		}).clickOnEnter();
		
		$("#tbHelpButton").button({
			text: false,
			icons: {
				primary: "ui-icon-help"
			}
		}).clickOnEnter();
	});

	function refreshPage() {
		window.location.reload();
	}

</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%
	Model.Grid.DataUrl = Url.Action("UserSettingsGrid_DataRequested");
	Model.Grid.EditUrl = Url.Action("UserSettingsGrid_EditRows");
%>		
<%= Html.KescoGrid(Model.Grid, "UserSettingsGrid") %>
	<script type="text/javascript">
	$(function() {
			var resizeGrid = function() {
				var $grid = $("#UserSettingsGrid");
				var $parent = $("#dialogContentPane");
				$grid.setGridHeight($parent.height()-80, true);
				$grid.setGridWidth($parent.width()-20, true);
			};
			$(window).resize(resizeGrid);
	});
	</script>
    <%  Html.RenderPartial("DialogResultInit", Model); %>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" runat="server">
	<script type="text/javascript">

		function test() {
			var postData = { 
				settings: [
					{ clid: 62, key: 'width', value: 330 },
					{ clid: 62, key: 'height', value: 440 }
				]
			};

			var json = JSON.stringify(postData);

			$.ajax({
				type: 'POST',
				url: '<%= Url.FullPathAction("SaveUserSettings") %>',
				data: json,
				contentType: 'application/json; charset=utf-8',
				success: function(response) {
					alert(response.status);
				},
				dataType: "json",
				traditional: true
			});
		}

	</script>
</asp:Content>
