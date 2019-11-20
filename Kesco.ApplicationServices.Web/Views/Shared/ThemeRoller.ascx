<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div>
	<button id="themeRollerBtn"><%= Kesco.Web.Mvc.UI.Controls.Localization.Resources.GUI_Menu_View_Theme %></button>
	<button><%= Kesco.Web.Mvc.UI.Controls.Localization.Resources.GUI_Menu_View_Theme %></button>
</div>

	<script type="text/javascript">
		$(document).ready(function() {

			var active = null;
			var menu = $( "<ul></ul>" )
				.addClass( "ui-autocomplete" )
				.appendTo( $(document.body) )
				.menu({
					focus: function (e, ui) {
						active = ui;
					},
					selected: function(event, ui) {
						var item = ui.item.data( "item");
						$.ajax({
    						url: '<%= Url.FullPathAction("ChangeTheme") %>',
    						cache: false,
							type: 'POST',
							data: {
								theme: item
							}
							, error: function (xhr, status, errorThrown) {
								alert("Error");
    						}
							, success: function (response) {
								if (response.status == "ok") {
									window.location.reload();
								} else if (response.status == "error") {
									alert(response.error.Content || response.error);
								}
							}
						});
					}, 
					blur: function (e, ui) {
						active = null;
						ui;
					}
				})
				.zIndex( $( "#rerun" ).zIndex() + 1 )
				.css({ top: 0, left: 0 })
				.width(100)
				.hide();

	///*
			$(["classic", "humanity", "lightness", "redmond", "sunny"])
				.map(function(index, item) {
					var label = item;
					if (item == '<%= Session["User.Theme"] %>') {
						label = "<b>"+item+"</b>";
					}
					$( "<li></li>" )
						.data("item", item)
						.append( $( "<a></a>" )
							.html(label)
							/*.click(function(e) {
								e.stopPropagation();
								e.preventDefault();
								menu.hide();
							})*/
						)
						.appendTo( menu );
				});

				menu.menu("refresh");

				var suppressKeyPress;
	//*/
				$( "#themeRollerBtn" )
					.button({
						text: false,
						icons: {
							primary: "kui-icon-application_view_tile"
						}
					})
					.click(function() {
						window.location.reload();
						return false;
					})
					.next()
						.button( {
							text: false,
							icons: {
								primary: "ui-icon-triangle-1-s"
							}
						})
						.keydown(function (event) {
							var self = this;
							suppressKeyPress = false;
							var keyCode = $.ui.keyCode;
							switch( event.keyCode ) {
							case keyCode.UP:
								menu.menu("previous");
								// prevent moving cursor to beginning of text field in some browsers
								event.stopPropagation();
								event.preventDefault();
								break;
							case keyCode.DOWN:
								menu.menu("next");
								// prevent moving cursor to end of text field in some browsers
								event.stopPropagation();
								event.preventDefault();
								break;
							case keyCode.TAB:
							case keyCode.ESCAPE:
								menu.menu("deactivate", event );
								menu.hide();
								break;
							case keyCode.ENTER:
							case keyCode.NUMPAD_ENTER:
								// when menu is open and has focus
								if (active) {
									menu.menu('select', event);
								}
								self.click();
								event.stopPropagation();
								event.preventDefault();
								break;
							default:
								// keypress is triggered before the input value is changed
								break;
							}
						})
						.click(function(e) {
							if (menu.is(':visible')) {
							}
							menu.toggle().position({
								of: this,
								my: "right top",
								at: "right bottom"
							});
							e.preventDefault();
							return false;
						})
						.parent()
							.buttonset();

		});
	</script>

