ko.extenders.capitalize = function(target, perform) {
    //create a writeable computed observable to intercept writes to our observable
    var result = ko.computed({
        read: target,  //always return the original observables value
        write: function(newValue) {
            var current = target(),
                valueToWrite = (perform && newValue)
					? (newValue.charAt(0).toUpperCase() + newValue.slice(1))
					: newValue;
 
            //only write if it changed
            if (valueToWrite !== current) {
                target(valueToWrite);
				target.notifySubscribers(valueToWrite);
            } else {
                //if the rounded value is the same, but a different value was written, force a notification for the current field
                if (newValue !== current) {
                    target.notifySubscribers(valueToWrite);
                }
            }
        }
    });
 
    //initialize with current value to make sure it is capitalized
    result(target());
 
    //return the new computed observable
    return result;
};

ko.deferred = function (observable, evaluator, owner) {
	var result = ko.observable(), currentDeferred;
	result.inProgress = ko.observable(false); // Track whether we're waiting for a result
	result.target = observable;
	observable.subscribe(function(newValue) {
		// Abort any in-flight evaluation to ensure we only notify with the latest value
		if (currentDeferred) { currentDeferred.reject(); }

		var evaluatorResult = evaluator.call(owner);
		// Cope with both asynchronous and synchronous values
		if (evaluatorResult && (typeof evaluatorResult.done == "function")) { // Async
			result.inProgress(true);
			currentDeferred = $.Deferred().done(function(data) {
				result.inProgress(false);
				result(data);
			});
			evaluatorResult.done(currentDeferred.resolve);
		} else // Sync
			result(evaluatorResult);
	});

	return result;
}

ko.deferred2 = function (evaluator, owner) {
	var result = ko.observable(), currentDeferred;
	result.inProgress = ko.observable(false); // Track whether we're waiting for a result
	var _deferred = ko.computed(function() {
		// Abort any in-flight evaluation to ensure we only notify with the latest value
		if (currentDeferred) { currentDeferred.reject(); }

		var evaluatorResult = evaluator.call(owner);
		// Cope with both asynchronous and synchronous values
		if (evaluatorResult && (typeof evaluatorResult.done == "function")) { // Async
			result.inProgress(true);
			currentDeferred = $.Deferred().done(function(data) {
				result.inProgress(false);
				result(data);
			});
			evaluatorResult.done(currentDeferred.resolve);
		} else // Sync
			result(evaluatorResult);
		return result;
	});
	return _deferred;
}
ko.bindingHandlers.dialog = {
        init: function(element, valueAccessor) {
            var options = ko.utils.unwrapObservable(valueAccessor());
            setTimeout(function() { $(element).dialog(options || {}); }, 0);

            //handle disposal (not strictly necessary in this scenario)
             ko.utils.domNodeDisposal.addDisposeCallback(element, function() {
                 $(element).dialog("destroy");
             });   
        },
        update: function(element, valueAccessor, allBindingsAccessor) {
             var shouldBeOpen = ko.utils.unwrapObservable(allBindingsAccessor().dialogVisible);
             $(element).dialog(shouldBeOpen ? "open" : "close");

        }
};

ko.bindingHandlers.hidden = {
	update: function(element, valueAccessor, allBindingsAccessor) {
		// First get the latest data that we're bound to
		var value = valueAccessor(), allBindings = allBindingsAccessor();
		 
		// Next, whether or not the supplied model property is observable, get its current value
		var valueUnwrapped = ko.utils.unwrapObservable(value);
		 
		// Now manipulate the DOM element
		if (valueUnwrapped == true)
			$(element).css({visibility: 'hidden'}); // Make the element hidden
		else
			$(element).css({visibility: 'visible'});   // Make the element visible
	}
};

ko.bindingHandlers.slideVisible = {
	update: function(element, valueAccessor, allBindingsAccessor) {
		// First get the latest data that we're bound to
		var value = valueAccessor(), allBindings = allBindingsAccessor();
		 
		// Next, whether or not the supplied model property is observable, get its current value
		var valueUnwrapped = ko.utils.unwrapObservable(value);
		 
		// Grab some more data from another binding property
		var duration = allBindings.slideDuration || 400; // 400ms is default duration unless otherwise specified
		 
		// Now manipulate the DOM element
		if (valueUnwrapped == true)
			$(element).slideDown(duration); // Make the element visible
		else
			$(element).slideUp(duration);   // Make the element invisible
	}
};

ko.observableArray.fn.joinListItemProperty = function(propName, separator) {
    return ko.computed(function() {
        var allItems = this(), propertyValues = [];
        for (var i = 0; i < allItems.length; i++) {
			propertyValues.push(ko.utils.unwrapObservable(allItems[i][propName]));
        }
        return propertyValues.join(separator?separator:",");
    }, this);
}

ko.observableArray.fn.filterByProperty = function(propName, matchValue) {
    return ko.computed(function() {
        var allItems = this(), matchingItems = [];
        for (var i = 0; i < allItems.length; i++) {
            var current = allItems[i];
            if (ko.utils.unwrapObservable(current[propName]) == matchValue)
                matchingItems.push(current);
        }
        return matchingItems;
    }, this);
}

ko.bindingHandlers.datepicker = {
	init: function(element, valueAccessor, allBindingsAccessor) {
		//initialize datepicker with some optional options
		var options = $.extend(allBindingsAccessor().datepickerOptions || {}, {
			constrainInput: true,
			onSelect: function(dateText, dpInstance) {
				var observable = valueAccessor();
				observable(Globalize.format($(this).datepicker("getDate"), "d"));
			}
		});
		$(element).datepicker(options);

		//handle the field changing
		ko.utils.registerEventHandler(element, "change", function () {
			var observable = valueAccessor();
			var date = null;
			var value = $(element).val();
			if (value) date = Globalize.parseDate(value);
			if (date == null) {
				var matches = value.match(/^(30|31|[1,2][0-9]|0?[1-9])\D?(1[0-2]|0?[1-9])?\D?(\d{4}|\d{2})?\D*/);
				if (matches != null) {
					date = new Date();
					date.setDate(parseInt(matches[1]));
					if (matches.length > 2) {
						if (matches[2]) date.setMonth(parseInt(matches[2])-1);
					}
					if (matches.length > 3 && matches[3]) {
						var year = parseInt(matches[3]);
						if (matches[3].length == 2) {
							year += 2000;
							if (year - new Date().getFullYear() > 10)
								year -= 100;
						}
						date.setFullYear(year);
					}
					//$(element).datepicker("setDate", date);
				} else {
					date = $(element).datepicker("getDate");
				}
			}
			observable(Globalize.format(date, "d"));
		});

		//handle disposal (if KO removes by the template binding)
		ko.utils.domNodeDisposal.addDisposeCallback(element, function() {
			$(element).datepicker("destroy");
		});

	},
	
	//update the control when the view model changes
	update: function(element, valueAccessor) {
		var value = ko.utils.unwrapObservable(valueAccessor());
		setTimeout(function() { $(element).datepicker("setDate", Globalize.format(value, "d")); }, 100);
	}
};

ko.bindingHandlers.personControl = {
		init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
			var $element = $(element),
				value = valueAccessor(), 
				binding = ko.utils.unwrapObservable(value),
				allBindings = allBindingsAccessor(),
				options = ko.mapping.toJS(value),
				subscription = null;
			
			$element.person(options);

			if (value.value.subscribe) {
				subscription = value.value.subscribe(function (newValue) {
					$element.dynamicLink('setValue', newValue)
				});
			}
			
			ko.utils.domNodeDisposal.addDisposeCallback(element, function() {
				if (subscription) subscription.dispose();
				$(element).person("destroy");
			});

		}
};

ko.bindingHandlers.employeeControl = {
		init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
			var $element = $(element),
				value = valueAccessor(), 
				binding = ko.utils.unwrapObservable(value),
				allBindings = allBindingsAccessor();
				options = ko.mapping.toJS(value);
			
			$element.employee(options);

			var subscription = null;

			if (value.value.subscribe) {
				subscription = value.value.subscribe(function (newValue) {
					$element.dynamicLink('setValue', newValue)
				});
			}
			
			ko.utils.domNodeDisposal.addDisposeCallback(element, function() {
				if (subscription) subscription.dispose();
				$(element).employee("destroy");
			});

		},
		
		update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
			if (!$(element).data('actual')) {
				var value = valueAccessor();
				var options = ko.mapping.toJS(value);
				$(element).employee('option', options);
			}
			$(element).data('actual', false);
		}
};

ko.bindingHandlers.dynamicLink = {
		init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
			var $element = $(element);

			var value = valueAccessor(), 
				binding = ko.utils.unwrapObservable(value),
				allBindings = allBindingsAccessor();
				
			var options = ko.mapping.toJS(value);
			var subscription = null;
			if (value.value.subscribe) {
				subscription = value.value.subscribe(function (newValue) {
					var val = newValue == null?"":newValue;
					var item = $element.dynamicLink('getValue')
					if (val != item.value) {
						$element.dynamicLink('update', val);
					}
				});
			}
			
			$element.dynamicLink(options);
			
			if (binding.tooltipSource) {
				$(element).one("mouseenter", function() {
					$(element).initToolTip(function() {
						var uri = binding.tooltipSource;
						var item = $(this).dynamicLink('getValue');
						if (item && item.value)
							uri = uri.replace('/0', '/' + item.value);
						return uri;
					}, $(document.body)).mouseenter();
				});
			}

			ko.utils.registerEventHandler(element, "click", function () {
				var value = ko.mapping.toJS(valueAccessor());
				if (value.click && $.isFunction(value.click)) {
					value.click(value.value);
				}
			});

			ko.utils.domNodeDisposal.addDisposeCallback(element, function() {
				if (subscription)
					subscription.dispose();
				if ($(element).dynamicLink != undefined) $(element).dynamicLink("destroy");
			});

		},
		
		update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
 			if (!$(element).data('actual')) {
				var value = valueAccessor();
				var options = ko.mapping.toJS(value);
				if ($(element).dynamicLink != undefined) $(element).dynamicLink('option', options);
			}
 			$(element).data('actual', false);
		}
};

ko.bindingHandlers.masked = {
		/*
			masked: { value: property, options: { mask: '99999' } }
		*/
		init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
			var value = valueAccessor(),
				binding = ko.utils.unwrapObservable(value);
			$(element)
				.mask(value.options)
				.bind("blur", function (event) {
					var value = valueAccessor();
					value.value($(event.target).mask("value"));
				});
		},
		
		update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
			var value = valueAccessor(),
				binding = ko.utils.unwrapObservable(value);
			$(element).mask("value", value.value());
			$(element).mask(binding.options);
		}

};

ko.bindingHandlers.selectBox = {

		init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
			var $element = $(element);

			var value = valueAccessor(), 
				binding = ko.utils.unwrapObservable(value),
				allBindings = allBindingsAccessor()
				subscription = null;
				
			if (value.subscribe) {
				subscription = value.subscribe(function (newValue) {
					var val = newValue == null?"":newValue;
					var item = $element.selectBox('getValue')
					if (val != item.value) {
						$element.selectBox('update', val);
					}
				});
			}
			
			$element.bind("change", function (event, data, formatted) { 
				var val = $(this).val();
				if (value.peek() != val)
					value(val === ""?null:val); 
			});

			ko.utils.domNodeDisposal.addDisposeCallback(element, function() {
				if (subscription)
					subscription.dispose();
				
			});

		},
		
		update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
		/*	var 
				$element = $(element);
			$element.data('actual', false);
		*/
		}

};

ko.bindingHandlers.selectTextBox = {

		init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
			
			$(element).bind("change", function (event, data, formatted) { 
				var value = valueAccessor();
				var val = $(this).val();
				if (value() != val)
				{
					value(val === ""?null:val); 
				}
			});
			
		},
		
		update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
			var value = valueAccessor();
			$(element).selectTextBox("update", value());
		}

};

ko.bindingHandlers.selectBoxEnabled = {
	update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var value = ko.utils.unwrapObservable(valueAccessor());
		$(element).selectBox("option", "disabled", !value);
	}
};

ko.bindingHandlers.valueEx = {
	init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
		ko.bindingHandlers.value.init(element, valueAccessor, allBindingsAccessor, viewModel);

		var value = ko.utils.unwrapObservable(valueAccessor());
		if (!value) $(element).parent().parent().addClass("ui-state-highlight");
		else $(element).parent().parent().removeClass("ui-state-highlight");
		
		ko.utils.registerEventHandler(element, "change", function(ev) {
			var value = ko.utils.unwrapObservable(valueAccessor());
			if (!value) $(element).parent().parent().addClass("ui-state-highlight");
			else $(element).parent().parent().removeClass("ui-state-highlight");
        });
		
		$(element).bind("keydown", function() {
			var value = ko.utils.unwrapObservable(valueAccessor());
			if (!value) $(element).parent().parent().addClass("ui-state-highlight");
			else $(element).parent().parent().removeClass("ui-state-highlight");
        });
		
	},
	update: function (element, valueAccessor, allBindingsAccessor, context) {
		ko.bindingHandlers.value.update(element, valueAccessor, allBindingsAccessor, context);

		var value = ko.utils.unwrapObservable(valueAccessor());
		if (!value) $(element).parent().parent().addClass("ui-state-highlight");
		else $(element).parent().parent().removeClass("ui-state-highlight");

	}
};

ko.bindingHandlers.valueTextBox = {
	init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
		ko.bindingHandlers.value.init(element, valueAccessor, allBindingsAccessor, viewModel);

		var checkRequired = function()
		{			
			if (allBindingsAccessor().requiredCallBack && $.isFunction(allBindingsAccessor().requiredCallBack))
			{
				var value = ko.utils.unwrapObservable(valueAccessor());
				if ((!value || value.length == 0) && allBindingsAccessor().requiredCallBack(element.name)) $(element).parent().parent().addClass("ui-state-highlight");
				else $(element).parent().parent().removeClass("ui-state-highlight");
			}
		}
		checkRequired();
		ko.utils.registerEventHandler(element, "change", function(ev) {
			var accessor = valueAccessor();
			accessor($(element).val());
			checkRequired();
        });
		
		$(element).bind("keydown", function(e) {	
			checkRequired();
        });
		
	},
	update: function (element, valueAccessor, allBindingsAccessor, context) {
		ko.bindingHandlers.value.update(element, valueAccessor, allBindingsAccessor, context);

		if (allBindingsAccessor().requiredCallBack && $.isFunction(allBindingsAccessor().requiredCallBack))
		{
			var value = ko.utils.unwrapObservable(valueAccessor());
			if (!value && allBindingsAccessor().requiredCallBack(element.name)) $(element).parent().parent().addClass("ui-state-highlight");
			else $(element).parent().parent().removeClass("ui-state-highlight");
		}

	}
};


ko.bindingHandlers.valueTimeTextBox = {
	init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
		ko.bindingHandlers.value.init(element, valueAccessor, allBindingsAccessor, viewModel);

		var checkRequired = function()
		{			
			if (allBindingsAccessor().requiredCallBack && $.isFunction(allBindingsAccessor().requiredCallBack))
			{
				var value = ko.utils.unwrapObservable(valueAccessor());
				if ((!value || value.length == 0) && allBindingsAccessor().requiredCallBack(element.name)) $(element).parent().parent().addClass("ui-state-highlight");
				else $(element).parent().parent().removeClass("ui-state-highlight");
			}
		}
		checkRequired();
		ko.utils.registerEventHandler(element, "change", function(ev) {
			var accessor = valueAccessor();
			accessor($(element).val());
			checkRequired();
        });
		
		$(element).bind("contextmenu", function(e) { e.preventDefault(); return false;});
		
		$(element).bind("keydown", function(e) {	
			isNumeric = (e.keyCode >= 48 /* KeyboardEvent.DOM_VK_0 */ && e.keyCode <= 57 /* KeyboardEvent.DOM_VK_9 */) ||
                       (e.keyCode >= 96 /* KeyboardEvent.DOM_VK_NUMPAD0 */ && e.keyCode <= 105 /* KeyboardEvent.DOM_VK_NUMPAD9 */);
			//isAlphabet = (e.keyCode >= 65 && e.keyCode <= 90);
			var pos = $(element).getCursorPosition();
			if (isNumeric /*|| isAlphabet*/ || e.keyCode == 8 || e.keyCode == 46){
				/*setTimeout( function(){	*/	
					var selen = $(element).getSelectionLength();					
					var len = $(element).val().length;
					var	tmp = "";
					if (selen > 0 && (e.keyCode == 8 || e.keyCode == 46)) {
						var part1 = $(element).val().substr(0, pos - (document.selection ? selen : 0));
						var part2 = $(element).val().substr(pos - (document.selection ? selen : 0), selen);
						var part3 = $(element).val().substr(pos + (!document.selection ? selen : 0), len - pos - (!document.selection ? selen : 0));
						var part2 = part2.replace(/\d/g, "0");
						tmp = part1 + part2 + part3;
					}
					else {																
						var code = String.fromCharCode((96 <= e.keyCode && e.keyCode <= 105)? e.keyCode-48 : e.keyCode);
						if (e.keyCode == 46) {
							code = '0';
							if (pos == 2 || pos == 5) {
								pos = pos + 1;
							};	
						}
						
						if (e.keyCode == 8) {
							code = '0';
							pos = pos - 1;
							if (pos == 2 || pos == 5) {
								pos = pos - 1;
							};	
						}
						else {
							if (pos == 2 || pos == 5) {
								pos = pos + 1;
							};
						}
						if (pos < len + 1)
						{
							tmp = $(element).val().substr(0, pos) + code + $(element).val().substr(pos + 1, len - pos + 1);
						}
					}
					var accessor = valueAccessor();
					accessor(tmp);
					if (e.keyCode == 8) {
					    $(element).setCursorPosition(pos); 
					}					
					else
					    $(element).setCursorPosition(pos + 1);
					
					checkRequired();
				/*}, 10);	*/			
			}
			if (!(e.keyCode < 48) || e.keyCode == 8 || e.keyCode == 46) {
				e.preventDefault();
				//if (e.keyCode == 46)
				//	$(element).setCursorPosition(lastPos + 1);
				/*var lastPos = $(element).getCursorPosition();
				if (pos == lastPos) {
					$(element).setCursorPosition(lastPos + 1);
				}*/
			}
        });
		
	},
	update: function (element, valueAccessor, allBindingsAccessor, context) {
		ko.bindingHandlers.value.update(element, valueAccessor, allBindingsAccessor, context);

		if (allBindingsAccessor().requiredCallBack && $.isFunction(allBindingsAccessor().requiredCallBack))
		{
			var value = ko.utils.unwrapObservable(valueAccessor());
			if (!value && allBindingsAccessor().requiredCallBack(element.name)) $(element).parent().parent().addClass("ui-state-highlight");
			else $(element).parent().parent().removeClass("ui-state-highlight");
		}

	}
};

if (ko.bindingHandlers.value['after']) ko.bindingHandlers.value['after'].push('jqOptions');
ko.bindingHandlers.jqOptions = {
	init: function (element, valueAccessor, allBindingsAccessor, context) {
		var value = allBindingsAccessor().value;
		var $element = $(element);
		if (window.console) console.log("jqOptions.init", allBindingsAccessor());
		//ko.bindingHandlers.options.init(element, valueAccessor, allBindingsAccessor, context);						
		
		$(element).selectmenu({
			change: function(ev, ui) { 
				if (window.console) console.log("jqOptions.selectmenu.change", ui);
				value(ui.value);
			}
		});

		if (allBindingsAccessor().required && !$(element).val())
			$("#"+$element.attr('id')+"-button").addClass('ui-state-highlight');
	},
	
	update: function (element, valueAccessor, allBindingsAccessor, context) {
		var value = allBindingsAccessor().value;
		
		if (value() && allBindingsAccessor().optionsCaptionClear)
			allBindingsAccessor().optionsCaption = null;
		if (window.console) console.log("jqOptions.update", value() == $(element).val());
		
		ko.bindingHandlers.options.update(element, valueAccessor, allBindingsAccessor, context);
		ko.bindingHandlers.value.update(element, value, allBindingsAccessor, context);
		//if (window.console) console.log("jqOptions.update.selectmenu", valueAccessor());
		$(element).selectmenu();

		var val = value();
		if (allBindingsAccessor().required) {
			if (val) $("#"+$(element).attr('id')+"-button").removeClass('ui-state-highlight');
			else $("#"+$(element).attr('id')+"-button").addClass('ui-state-highlight');
		}
	}
};

ko.bindingHandlers.decoration = {
	init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
		var value = valueAccessor(), allBindings = allBindingsAccessor();
		// Next, whether or not the supplied model property is observable, get its current value
		var valueUnwrapped = ko.utils.unwrapObservable(value);
		if (valueUnwrapped === true)
			$(element).decor();
		else $(element).decor(valueUnwrapped);
	}
};

ko.bindingHandlers.selectmenu = {
	init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
		var value = valueAccessor(), allBindings = allBindingsAccessor();
		// Next, whether or not the supplied model property is observable, get its current value
		var valueUnwrapped = ko.utils.unwrapObservable(value);
		var options = $.extend(value, {
			disabled: value.enable === false
		});

		$(element).selectmenu();
	},
	update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
		// First get the latest data that we're bound to
		var value = valueAccessor(), allBindings = allBindingsAccessor();

		// Next, whether or not the supplied model property is observable, get its current value
		var valueUnwrapped = ko.utils.unwrapObservable(value);
		valueUnwrapped = $.extend(valueUnwrapped, { disabled: valueUnwrapped.enable === false });
		$(element).selectmenu("option", valueUnwrapped);
	}

};

ko.bindingHandlers.latinOnly = {
	init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
		$(element).keypress(function(ev) {
			var value = valueAccessor();
			var valueUnwrapped = ko.utils.unwrapObservable(value);
			if (!valueUnwrapped) return;
			var code = ev.charCode || ev.which || ev.keyCode;
			if (code > 127) ev.preventDefault();
		});
	}
};
		
ko.bindingHandlers.disableEx = {
	update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
		var value = valueAccessor();
		var valueUnwrapped = ko.utils.unwrapObservable(value);
		var $element = $(element);
		if (valueUnwrapped) {
			if ($element.is(":focus"))
				$element.focusNextInputField();
			ko.bindingHandlers.disable.update(element, valueAccessor, allBindingsAccessor, viewModel);
		} else {
			ko.bindingHandlers.disable.update(element, valueAccessor, allBindingsAccessor, viewModel);
			$element.focus();
		}
	}
};

ko.bindingHandlers.selectmenuEnabled = {
	update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
		var value = ko.utils.unwrapObservable(valueAccessor());
		$(element).selectmenu("option", "disabled", !value);
	}
};

ko.bindingHandlers.button = {
	init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
		var value = valueAccessor(), allBindings = allBindingsAccessor();
		// Next, whether or not the supplied model property is observable, get its current value
		var valueUnwrapped = ko.utils.unwrapObservable(value);

		var options = $.extend(value, {
			disabled: value.enable === false
		});

		$(element).button(options).show();
	},

	update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
		// First get the latest data that we're bound to
		var value = valueAccessor(), allBindings = allBindingsAccessor();

		// Next, whether or not the supplied model property is observable, get its current value
		var valueUnwrapped = ko.utils.unwrapObservable(value);
		valueUnwrapped = $.extend(valueUnwrapped, { disabled: valueUnwrapped.enable === false });
		$(element).button("option", valueUnwrapped);
	}
};

ko.bindingHandlers.spinner = {
	init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
		var value = valueAccessor(), allBindings = allBindingsAccessor();
		// Next, whether or not the supplied model property is observable, get its current value
		var valueUnwrapped = ko.utils.unwrapObservable(value);

		var spinnerOptions = {
			disabled: value.enable === false,
			step: value.step || 1,
			page: value.page || 10,
			min: value.min || null,
			max: value.max || null,
			culture: value.culture || 'kesco',
			numberFormat: value.numberFormat || 'n',
			change: function (ev) {
				$(this).change();
			}
		};

		$(element).spinner(spinnerOptions);
	},

	update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
		// First get the latest data that we're bound to
		var value = valueAccessor(), allBindings = allBindingsAccessor();

		// Next, whether or not the supplied model property is observable, get its current value
		var valueUnwrapped = ko.utils.unwrapObservable(value);
		valueUnwrapped = $.extend(valueUnwrapped, { disabled: valueUnwrapped.enable === false });
		$(element).spinner("option", valueUnwrapped);
	}
};

(function($) {

    ko.bindingHandlers['jqueryui'] = {
        update: function(element, valueAccessor, allBindingsAccessor, viewModel) {
            var widgetBindings = _getWidgetBindings(element, valueAccessor, allBindingsAccessor, viewModel);
        
            // Attach the jQuery UI Widget and/or update its options.
            // (The syntax is the same for both.)
            $(element)[widgetBindings.widgetName](widgetBindings.widgetOptions);
        }
    };
    
    function _getWidgetBindings(element, valueAccessor, allBindingsAccessor, viewModel) {
        // Extract widgetName and widgetOptions from the data binding,
        // with some sanity checking and error reporting.
        // Returns dict: widgetName, widgetOptions.
        
        var value = valueAccessor(),
            myBinding = ko.utils.unwrapObservable(value),
            allBindings = allBindingsAccessor();
        
        if (typeof(myBinding) === 'string') {
            // Short-form data-bind='jqueryui: "widget_name"'
            // with no additional options
            myBinding = {'widget': myBinding};
        }
        
        var widgetName = myBinding.widget,
            widgetOptions = myBinding.options; // ok if undefined
        
        // Sanity check: can't directly check that it's truly a _widget_, but
        // can at least verify that it's a defined function on jQuery:
        if (typeof $.fn[widgetName] !== 'function') 
        {
            throw new Error("jqueryui binding doesn't recognize '" + widgetName 
                + "' as jQuery UI widget");
        }
        
        // Sanity check: don't confuse KO's 'options' binding with jqueryui binding's 'options' property
        if (allBindings.options && !widgetOptions && element.tagName !== 'SELECT') {
            throw new Error("jqueryui binding options should be specified like this:\n"
                + "  data-bind='jqueryui: {widget:32f00c915f33483b915ab82fa06f475f1c9946a3quot;" + widgetName + "32f00c915f33483b915ab82fa06f475f1c9946a3quot;, options:{...} }'");
        }
        
        return {
            widgetName: widgetName,
            widgetOptions: widgetOptions
        };
    }
})(jQuery);

//an observable that retrieves its value when first bound
ko.onDemandObservable = function(callback, target, isArray) {
    var _value = (isArray)?ko.observableArray() : ko.observable();  //private observable

    var result = ko.computed({
        read: function() {
            //if it has not been loaded, execute the supplied function
            if (!result.loaded()) {
                callback.call(target);
            }
            //always return the current value
            return _value();
        },
        write: function(newValue) {
            //indicate that the value is now loaded and set it
            result.loaded(true);
            _value(newValue);
        },
        deferEvaluation: true  //do not evaluate immediately when created
    });

    //expose the current state, which can be bound against
    result.loaded = ko.observable();
    //load it again
    result.refresh = function() {
        result.loaded(false);
    };

    return result;
};
