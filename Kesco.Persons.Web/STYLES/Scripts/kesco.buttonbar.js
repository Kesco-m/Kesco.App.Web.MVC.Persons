
(function($) {
	var methods = {
		init: function(options) {
				var context = this;
				var defaults = {
					form: null,
					buttons: {
						/*cancel: {
							text: 'Закрыть',
							buttonType: 'close'
						},
						help: {
							text: 'Помощь',
							buttonType: 'help'
						}*/
					}
					
				};
				var opts = $.extend(defaults, options);
				return this.each(function () {
						var self = this;
						var $this = $(this);
						var data = $this.data('kescoButtonBar');
						var buttonsBar = null;

						if ( !data ) {
							buttonsBar = $this
								.css("padding", "5px").css('text-align', "center")
								.addClass('ui-dialog-buttonpane ui-helper-clearfix');
						

							$this.data('kescoButtonBar', {
								target : $this,
								options: opts
							});

							if (opts.buttons && typeof(opts.buttons) == "object") {
								$.each(opts.buttons, function(name, props) {
										props = $.isFunction( props ) ? { click: props, text: name } : props;

										var button = $('<button type="button"></button>').attr( props, true ).unbind('click')
											.click(function() {
													//props.click.apply($this, arguments);
													switch (props.buttonType) {
														case "callback":
															if ($.isFunction(props.callback)) 
																props.callback();
															else {
																var callback = eval(""+props.callback);
																callback();
															}
															break;
														case "close":
															if ($.isFunction(window.closeDialog)) window.closeDialog();
															break;
														case "reset":
															$this.resetForm(); break;
														case "submit":
															if (props.targetForm) {
																var $form = $(props.targetForm);
																if (props.uri) {
																	$form.attr("action", props.uri);
																}
																$form.submit(); 
															}
															break;
														case "kescoSubmit":
															if (props.kescoForm && $.fn.kescoForm) {
																$(props.kescoForm).kescoForm("submit", props.uri); 
															}
															break;
														case "open":
															var cntrl = $(this).attr("id");
															var dialog = popupWindow({
																	resizable: 1,
																	centerScreen: 1,
																	location: 0,
																	windowURL: props.uri, 
																	windowName: "wnd_"+cntrl,
																	width: props.dialogWidth || 600,
																	height: props.dialogHeight || 400,
																	callbackRegKey: cntrl,
																	callback: function (data) {
																		if (!dialog.closed) dialog.close();
																	}
																});

															break;
														default:
															break;
													}
											})
											.appendTo(buttonsBar);
										if ($.fn.button) {
											button.button({ icons: props.icons });
										}
									});
							}

						}

				});
		},
		destroy: function() {
				return this.each(function(){
					var $this = $(this),
						data = $this.data('kescoButtonBar');
					$(window).unbind('.kescoButtonBar');
					$this.html("");
					$this.removeData('kescoButtonBar');
				})
		}
	};


	$.fn.kescoButtonBar = function(method) {
		if ( methods[method] ) {
			return methods[ method ].apply( this, Array.prototype.slice.call( arguments, 1 ));
		} else if ( typeof method === 'object' || ! method ) {
			return methods.init.apply( this, arguments );
		} else {
			$.error( 'Method ' +  method + ' does not exist on jQuery.kescoForm' );
		}    

	};

}) (jQuery);

