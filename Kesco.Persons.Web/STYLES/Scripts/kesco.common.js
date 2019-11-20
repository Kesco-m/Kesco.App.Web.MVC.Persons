Date.prototype.toLocalDate = function () {
	this.setMinutes(this.getMinutes() - this.getTimezoneOffset());
	return this;
}

Date.prototype.toUtcDate = function () {
	this.setMinutes(this.getMinutes() + this.getTimezoneOffset());
	return this;
}
    
$.fn.focusNextInputField = function() {
    return this.each(function() {
		try	{
			
			var fields = $(':tabbable').filter('input[type!=button],textarea,select,a');
			var index = fields.index( this );
			if ( index > -1 && ( index + 1 ) < fields.length ) {
				fields.eq( index + 1 ).focus();	
			}
		} catch(e) {}
        return false;
    });     
};

jQuery.extend({
	ReadIdsFromCookie: function (s)	{
		var ids = [];
		arr = new Array();
		var pat = new RegExp("(?:^|%1E|" +String.fromCharCode(30) + ")([\\d]+)(?:" +String.fromCharCode(30) + "|" +String.fromCharCode(31) + "|%1E|%1F|$)","gi");
		while (arr = pat.exec(s)) {
			ids.push(arr[1]);
		}
		return ids;
	},

	getScriptEx : function( url, data, callback ) {
		return jQuery.get( url, data, callback);
	}

});

(function ($) {
	$.fn.getScriptEx = function( url, data, callback ) {
		return jQuery.get( url, data, callback, "script" );
	};
	
	$.fn.getCursorPosition = function() {  
        var CaretPos = 0;  
        if ( document.selection ) {  
             this.focus();  
             var Sel = document.selection.createRange();  
             Sel.moveStart ('character', -this.val().length);  
             CaretPos = Sel.text.length;  
        } else if (this[0] != null && (this[0].selectionStart || this[0].selectionStart == '0')) {  
            CaretPos = this[0].selectionStart;  
        }  
        return CaretPos;  
    } 
	
	$.fn.getSelectionLength = function() {  
        var CaretPos = 0;  
        if ( document.selection ) {  
             this.focus();  
             var Sel = document.selection.createRange();  
             CaretPos = Sel.text.length;  
        } else if (this[0] != null && ((this[0].selectionStart || this[0].selectionStart == '0') &&
            (this[0].selectionEnd || this[0].selectionEnd == '0'))) {  
            CaretPos = Math.abs(this[0].selectionEnd - this[0].selectionStart);  
        }  
        return CaretPos;  
    } 
	
	$.fn.setCursorPosition = function(position) {
	    if (this[0] != null) {
			 if (this[0].setSelectionRange) {
				this[0].focus();
				this[0].setSelectionRange(position, position);
			 }
			 else if (this[0].createTextRange) {
				var range = this[0].createTextRange();
				range.collapse(true);
				range.moveEnd('character', position);
				range.moveStart('character', position);
				range.select();
			 }
		 }
	}
})(jQuery);

	
function KescoPersonControl() {
}

function setActiveTreeUnfocused() {
	var activeTree;
	if (jQuery.jstree) {
		activeTree = jQuery.jstree._focused();
		if (activeTree) activeTree.unset_focus();
	}
}

function dialogForm(formID, options) {

	var defaults = {
		localization: {
			messageTitle: 'Сообщение',
			applicationErrorTitle: 'Ошибка приложения N5',
			requestErrorTitle: 'Ошибка запроса',
			requestErrorMessage: 'Возникла ошибка в процессе выполнения запроса. <br><br>Статус: {0}<br><br>Ошибка:{1}',
			ok: 'Ок'
		}
	}
	// Init Form
	var $form = $(formID);
	var self = this;

	this.options = $.extend(defaults, options);
	this.formID = formID;
	this.validatorBox = $('<div style="display: none; padding: 5px; color: red; font-weight: bold; border: 1px dashed silver" />');

	$form.prepend(this.validatorBox);

	var validatorOptions = $.extend({ 
			errorContainer: this.validatorBox,
			errorLabelContainer: $("ol", this.validatorBox),
			wrapper: 'li',
			errorClass: 'field-validation-error'
		}, {});

	$form.data('dialogForm', {
		target : this
	});

	this.validator = $form.validate(validatorOptions);
			
	this.databind = function(model) {
		for (var p in model.Item) {
			$("[name="+p+"]", $form).val(model.Item[p]);
		}
	}

	this.error = function (errMsg, errDetails) {
		var errDetails2 = "<ul>";
		$.map(errDetails, function(item, index) {
				if (item) {
					if (item.ErrorMessage) {
						errDetails2 += "<li>"+(item.ErrorMessage || item.Exception.Message)+"</li>";
					} else if (item.Errors) {
						$.map(item.Errors, function(err) {
							errDetails2 += "<li>"+(err.ErrorMessage || err.Exception.Message)+"</li>";
						});
					}
				}
			});
		errDetails2 += "</ul>";
		alertMessage(self.options.localization.applicationErrorTitle, ""+errMsg+""+errDetails2+"", self.options.localization.ok);
	}

	this.clearError = function () {
	}

	this.message = function (msg) {
	}
				
	this.load = function (uri) {
		$.ajax({
    		url: uri,
    		cache: false,
			type: "POST",
			context: $form,
    		dataType: 'json',
			beforeSend: function(xhr) {
    			self.clearError(); 
    			if (self.loadingImg) self.loadingImg.show();
			}
			, complete: function(xhr, status) {
    			if (self.loadingImg) self.loadingImg.hide();
			}
			, error: function (xhr, status, errorThrown) {
    			self.error(status + " | " + errorThrown, response.error_details); 
    		}
			, success: function (response) {
				if (response.status == "ok") {
					if ($.isFunction(self.databind)) {
						self.databind(response.model);
					}
					if (response.message) {
						self.message(response.message);
					}
				} else if (response.status == "error") {
					self.error(response.error.Content || response.error,  response.error_details.Errors || response.error_details);
				}
			}
		});
	}

	$form.ajaxForm({
			//context: $form,
			dataType: 'text json',
			beforeSerialize: function($form, options) {
				return self.validator.valid();
			},
			beforeSubmit: function(data, $form, options) {
				data.push( { name: '_local_timezone_offset_', value: new Date().getTimezoneOffset() } );
				if ( $.isFunction(self.beforeSubmit) ) {
					return self.beforeSubmit(data, $form, options);
				}
			},
			error: function(xhr, status, errorThrown) {
				alertMessage(
						self.options.localization.requestErrorTitle, 
						$.validator.format(self.options.localization.requestErrorMessage, status, xhr.responseText), 
						self.options.localization.ok
					);
			},
			success: function (response) {
				if (response.status == "ok") {
					//$this.resetForm();
					if (response.message) {
						alertMessage(
								self.options.localization.messageTitle, 
								response.message, 
								self.options.localization.ok
							);
					}
					if ( $.isFunction(self.complete) ) {
						self.complete(response);
					}
				} else if (response.status == "error") {
					self.error(response.error.Content || response.error, response.error_details.Errors || response.error_details);
				}
			}
		});

	return this;
};

function ajaxLoadJson(uri, context, success, error, complete, beforeSend) {
    $.ajax({
    	url: uri,
    	cache: false,
		type: "POST",
		context: context,
    	dataType: 'json'
		, beforeSend: beforeSend
		, complete: complete
		, error: error
		, success: success
    });
};

function ajaxLoadHtml(uri, context, success, error, complete, beforeSend) {
    $.ajax({
    	url: uri,
    	cache: false,
		type: "POST",
		context: $this,
    	dataType: 'html'
		, beforeSend: beforeSend
		, complete: complete
		, error: error
		, success: success
    });
};

function alertMessage(dialogTitle, dialogMessage, okButtonTitle, action) {
	dialogMessage = '<p style="margin-left: 30px;"><span class="ui-icon ui-icon-alert" style="position:absolute; left: 10px;"></span>'+dialogMessage+'</p>';
	var $dialog = $("<span style='max-width: 400px; position: absolute; top: -2000px; display: inline;'></span>").attr("title", dialogTitle?dialogTitle:"---").html(dialogMessage).appendTo(document.body).show();
	var $activeElement = $( document.activeElement );
	setTimeout(function() {
			var $window = $(window);
			var widthWnd = $window.width();
			var heightWnd = $window.height();
			var width = $dialog.width();
			var height = $dialog.height();
			var square = width * height;
			//alert("w: "+width+" h: "+height+" s: "+ square);
			if (width - widthWnd > -100) width = widthWnd - 50;
			if (height - heightWnd > -100) height = heightWnd - 50;
			if (height < 100) height = 100;
			//alert("w: "+width+" h: "+height+" s: "+ square);
			height += 100;
			var buttons = {};
			buttons[okButtonTitle] = function(event) {
						$dialog.dialog( "close" );
						if ($.isFunction(action)) {
							action.apply($dialog);
						}
						//return false;
					};
			$dialog
				.css({ 
					position: "",
					display: "block",
					top: "",
					"max-width": ""
				})
				.keydown(function(e) {
					if ( e.keyCode == $.ui.keyCode.ENTER ) {
						e.stopImmediatePropagation();
					}
				})
				.dialog({
					resizable: true,
					width: width,
					height: height,
					modal: true ,
					buttons: buttons ,
					close: function() {
						$dialog.dialog("destroy");
						$dialog.remove();
						if ($activeElement && $activeElement.length) {
							$activeElement = $activeElement.find(':tabbable');
							if ($activeElement && $activeElement.length)  $activeElement.get(0).focus();
						}
					},
					open: function() {
						// Find a button and focus it
						$(this).parents().find('.ui-dialog-buttonpane button:first').button('widget').focus();
						setActiveTreeUnfocused();
					}
				});
			
		}
		, 150);
}

function confirmAction(dialogTitle, dialogMessage, actionButtonTitle, action, cancelButtonTitle, cancelAction, context, selector) {
	var $dialog = {};

	dialogMessage = '<p><span class="ui-icon ui-icon-alert" style="float:left; margin:0 7px 20px 0;"></span>'+dialogMessage+'</p>';
	$dialog = $("<span>").attr("title", dialogTitle).html(dialogMessage);
	$dialog.appendTo(document.body).show();	

	setTimeout(function() {
		var doCancelOnClose = true;
		var $activeElement = $( document.activeElement );
		var width = $dialog.width();
		var height = $dialog.height();
		
		var square = width * height;
		//alert("w: "+width+" h: "+height+" s: "+ square);
		if (width < 350) width = 350;
		if (width > 500) width = 500;
		height = Math.ceil(square/width);
		if (height < 100) height = 100;
		if (height > 300) height = 300;
		//alert("w: "+width+" h: "+height+" s: "+ square);
		height += 80;
		
		//actionButtonTitle = actionButtonTitle | "OK";
		//cancelButtonTitle = cancelButtonTitle | "Cancel";
		var buttons = {};
		
		buttons[actionButtonTitle] = function() {
					doCancelOnClose = false;
					$( this ).dialog( "close" );
				};
		buttons[cancelButtonTitle] = function() {
					$( this ).dialog( "close" );
				};
		
		$dialog.css("display","block").dialog({
			resizable: false ,
			width: !selector ? width : 'inhired',
			height: !selector ? height : 'inhired',
			modal: true ,
			buttons: buttons ,
			close: function(event) {
				if ((doCancelOnClose && !($( this ).data('cancelOnOk'))) && $.isFunction(cancelAction)) {
					cancelAction.apply(context || $dialog);
				}
				else if ($.isFunction(action)) {
					$( this ).data('cancelOnOk', false);
					action.apply(context || $dialog);
				}
							
				$dialog.dialog("destroy").remove();
				
				if ($activeElement && $activeElement.length) {
					$activeElement = $activeElement.find(':tabbable');
					if ($activeElement && $activeElement.length)  $activeElement.get(0).focus();
				}
			},
			open: function() {
				setActiveTreeUnfocused();
				// Find Cancel button and focus it
				$(this).parents().find('.ui-dialog-buttonpane button:first').button('widget').focus();
			}
		});
	}
	,50);

}

function dialogWindow(dialogTitle, action, cancelAction, context, selector) {
	var $dialog = {};
	$dialog = $(selector);	

	setTimeout(function() {
		var doCancelOnClose = true;
		var $activeElement = $( document.activeElement );
		var width = $dialog.width();
		var height = $dialog.height();
		var buttons = {};
		$dialog.css("display","block").dialog({
			resizable: false ,
			width: 'inhired',
			height: 'inhired',
			modal: true ,
			buttons: buttons ,
			close: function(event) {
				if ((doCancelOnClose && !($( this ).data('cancelOnOk'))) && $.isFunction(cancelAction)) {
					cancelAction.apply(context || $dialog);
				}
				else if ($.isFunction(action)) {
					$( this ).data('cancelOnOk', false);
					action.apply(context || $dialog);
				}
			
				if ($activeElement && $activeElement.length) {
					$activeElement = $activeElement.find(':tabbable');
					if ($activeElement && $activeElement.length)  $activeElement.get(0).focus();
				}
			},
			open: function() {
				setActiveTreeUnfocused();
				// Find Cancel button and focus it
				$(this).parents().find('.ui-dialog-buttonpane button:first').button('widget').focus();
			}
		});
	}
	,50);

}


(function ($) {
	$.fn.preventEnterOrSpaceKeyEventPropagation = function() {
		var args = arguments[0] || {}; // It's your object of arguments
		this.bind("keydown", function(event) {
			if ( event.keyCode == $.ui.keyCode.SPACE || event.keyCode == $.ui.keyCode.ENTER ) {
				//$(this).click();
				event.stopImmediatePropagation();
				//event.preventDefault();
			}
		});
	};
})(jQuery);

(function ($) {
	$.fn.clickOnEnter = function() {
		var args = arguments[0] || {}; // It's your object of arguments
		this.bind("keydown", function(event) {
			if ( event.keyCode == $.ui.keyCode.SPACE || event.keyCode == $.ui.keyCode.ENTER ) {
				//$(this).click();
				event.stopImmediatePropagation();
				//event.preventDefault();
			}
		});
	};
})(jQuery);

(function ($) {
	$.fn.styleTable = function (options) {
		var defaults = {
			css: 'styleTable'
		};
		options = $.extend(defaults, options);

		return this.each(function () {

			input = $(this);
			input.addClass(options.css);

			input.find("tr").on('mouseover mouseout', function (event) {
				if (event.type == 'mouseover') {
					$(this).children("td").addClass("ui-state-hover");
				} else {
					$(this).children("td").removeClass("ui-state-hover");
				}
			});

			input.find("th").addClass("ui-state-default");
			input.find("td").addClass("ui-widget-content");

			input.find("tr").each(function () {
				$(this).children("td:not(:first)").addClass("first");
				$(this).children("th:not(:first)").addClass("first");
			});
		});
	};
})(jQuery);

