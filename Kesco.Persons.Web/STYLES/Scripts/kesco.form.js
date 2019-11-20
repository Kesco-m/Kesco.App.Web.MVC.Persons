function dialogForm(formID, options) {

	var defaults = { 
		resources: {
			operationComplete : "Operation was successfully completed.",
		}
	};

	options = $.extend(defaults, options);

	// Init Form
	var $form = $(formID);
	var self = this;

	this.formID = formID;

	var validatorOptions = $.extend({ 
			errorContainer: validatorBox,
			errorLabelContainer: $("ol", validatorBox),
			wrapper: 'li'
		}, {});

	var data = {
		target : this
	};

	$form.data('dialogForm', data);

	this.validator = $form.validate(validatorOptions);
			
	this.databind = function(model) {
		for (var p in model.Item) {
			$("[name="+p+"]", $form).val(model.Item[p]);
		}
	}

	this.error = function (errMsg, errDetails) {
		this.errorBox.html(errMsg).show();
		if (errDetails) {
			var errorList = $("<ul></ul>").css("display", "none");
			$.map(errDetails, function(item, index) {
					if (item && item.ErrorMessage)
						errorList.append($("<li></li").html(item.ErrorMessage));
				});
			var detailsLink = $('<a href="javascript: void(0);">...</a>')
				.click(function() {
					errorList.toggle();
				});
			this.errorBox.append(detailsLink);
			this.errorBox.append(errorList);
		}
	}

	this.clearError = function () {
		this.errorBox.html("&nbsp;").hide();
	}

	this.message = function (msg) {
		this.messageBox.html(msg).show("blind", null, 200, function () {
				window.setTimeout(function() {
						this.messageBox.hide("blind", null, 200);
					}, 2000 );
			});
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
					self.error(response.error.Content || response.error, response.error_details);
				}
			}
		});
	}

	$form.ajaxForm({
			context: $form,
			beforeSubmit: function(xhr) {
				return self.validator.valid();
											
			},
			beforeSend: function(xhr) {
				self.errorBox.html("&nbsp;").hide();
				if (self.loadingImg) self.loadingImg.show();
			},
			complete: function(xhr, status) {
				if (self.loadingImg) self.loadingImg.hide();
			},
    		error: function (xhr, status, errorThrown) {
    			self.errorBox.html(status + " | " + errorThrown).show();
    		}, 
			success: function (response) {
				if (response.status == "ok") {
					//$this.resetForm();
					if (response.message) {
						self.message(response.message);
					} else {
						self.message(options.resources.operationCompleted);
					}
					if ( $.isFunction(self.complete) ) {
						self.complete(response);
					}
				} else if (response.status == "error") {
					self.error(response.error.Content || response.error, response.error_details);
				}
			}
		});

	return this;
};

