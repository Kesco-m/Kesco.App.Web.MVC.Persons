(function( $, undefined ) {

// used to prevent race conditions with remote data sources
var requestIndex = 0;

$.widget( "kesco.autocompleteEx", {
	version: "1.9.1",
	defaultElement: "<input>",
	options: {
		appendTo: "body",
		autoFocus: false,
		delay: 300,
		minLength: 1,
		position: {
			my: "left top",
			at: "left bottom",
			collision: "none"
		},
		source: null,

		limit: 8,
		source: null,
		highlight: false,
		localization: {
			found: 'Отображаются {0} записей. Найдено больше.',
			notFound: 'Записей не найдено.',
			search: 'Поиск',
			view: 'Просмотр'
		},
		formatItem: null,
		
		// callbacks
		change: null,
		close: null,
		focus: null,
		open: null,
		response: null,
		search: null,
		select: null
	},

	pending: 0,

	_create: function() {
		var suppressKeyPress, suppressKeyPressRepeat, suppressInput;

		var self = this,
			doc = this.element[ 0 ].ownerDocument;

		this.isMultiLine = this._isMultiLine();
		this.valueMethod = this.element[ this.element.is( "input,textarea" ) ? "val" : "text" ];
		this.isNewMenu = true;

		var uiDecoration = this.uiDecoration = self.element.addClass("ui-spinner-input")
			// decorate
			.wrap(this._uiDecorationHtml()).parent()
				.hover( 
					function( ev ) { if (!self.options.disabled) $( ev.currentTarget ).addClass( "ui-state-hover" ); },
					function( ev ) { $( ev.currentTarget ).removeClass( "ui-state-hover" ); }
				);
				
		if (self.options.highlight) uiDecoration.addClass("ui-state-highlight");

		this.element
			.addClass( "ui-autocomplete-input" )
			.attr( "autocomplete", "off" );

		this._on( this.element, {
			keydown: function( event ) {
				if ( this.element.prop( "readOnly" ) ) {
					suppressKeyPress = true;
					suppressInput = true;
					suppressKeyPressRepeat = true;
					return;
				}

				suppressKeyPress = false;
				suppressInput = false;
				suppressKeyPressRepeat = false;
				var keyCode = $.ui.keyCode;
				switch( event.keyCode ) {
				case keyCode.PAGE_UP:
					suppressKeyPress = true;
					this._move( "previousPage", event );
					break;
				case keyCode.PAGE_DOWN:
					suppressKeyPress = true;
					this._move( "nextPage", event );
					break;
				case keyCode.UP:
					suppressKeyPress = true;
					this._keyEvent( "previous", event );
					break;
				case keyCode.DOWN:
					suppressKeyPress = true;
					this._keyEvent( "next", event );
					break;
				case keyCode.ENTER:
				case keyCode.NUMPAD_ENTER:
					event.preventDefault();
					// when menu is open and has focus
					if ( this.menu.active ) {
						// #6055 - Opera still allows the keypress to occur
						// which causes forms to submit
						suppressKeyPress = true;
						event.preventDefault();
						this.menu.select( event );
					} else {
						// keypress is triggered before the input value is changed
						if (this.selectedItem == null) {
							this.search(null, event);
						} else {
							this.element.focusNextInputField();
						}
					}
					break;
				case keyCode.TAB:
					if ( this.menu.active ) {
						this.menu.select( event );
					} else {
						if (this.selectedItem == null && this._value()) {
							this.search(null, event );
						}
					}
					break;
				case keyCode.ESCAPE:
					event.preventDefault();
					setTimeout(function() {
						self._select(event, self.options.resetItem ? self.options.resetItem : "")
					}, 10);
					if ( this.menu.element.is( ":visible" ) ) {
						this._value( this.term );
						this._close( event );
					}
					break;
				default:
					suppressKeyPressRepeat = true;
					if ( this.menu.element.is( ":visible" ) ) {
						this._close( event );
					}
					suppressInput = true;

					this._delay(function() {
						if ( this.term != this._value() ) {
							this._close();
							this.selectedItem = null;
							this._trigger( "changing", event );
						}
					}, 100);

					// search timeout should be triggered before the input value is changed
					//this._searchTimeout( event );
					break;
				}
			},
			keypress: function( event ) {
				if ( suppressKeyPress ) {
					suppressKeyPress = false;
					event.preventDefault();
					return;
				}
				if ( suppressKeyPressRepeat ) {
					return;
				}

				// replicate some key handlers to allow them to repeat in Firefox and Opera
				var keyCode = $.ui.keyCode;
				switch( event.keyCode ) {
				case keyCode.PAGE_UP:
					this._move( "previousPage", event );
					break;
				case keyCode.PAGE_DOWN:
					this._move( "nextPage", event );
					break;
				case keyCode.UP:
					this._keyEvent( "previous", event );
					break;
				case keyCode.DOWN:
					this._keyEvent( "next", event );
					break;
				default: 
					break;
				}
			},
			input: function( event ) {
				if ( suppressInput ) {
					suppressInput = false;
					event.preventDefault();
					return;
				}
				//this._searchTimeout( event );
			},
			focus: function() {
				this.uiDecoration.addClass( "ui-state-active" ); 
				//this.selectedItem = null;
				this.term = 
					this.previous = this._value();
			},
			blur: function( event ) {
				if ( this.cancelBlur ) {
					delete this.cancelBlur;
					return;
				}
				self.uiDecoration.removeClass( "ui-state-active" );
				clearTimeout( this.searching );
				this.close( event );
				this._change( event );
			}
		});

		this._initSource();
		this.menu = $( "<ul>" )
			.addClass( "ui-autocomplete" )
			.appendTo( this.document.find( this.options.appendTo || "body" )[ 0 ] )
			.menu({
				// custom key handling for now
				input: $(),
				// disable ARIA support, the live region takes care of that
				role: null
			})
			.zIndex( this.element.zIndex() + 1 )
			.hide()
			.data( "ui-menu" );

		this._on( this.menu.element, {
			mousedown: function( event ) {
				// prevent moving focus out of the text field
				event.preventDefault();

				// IE doesn't prevent moving focus even with event.preventDefault()
				// so we set a flag to know when we should ignore the blur event
				this.cancelBlur = true;
				this._delay(function() {
					delete this.cancelBlur;
				});

				// clicking on the scrollbar causes focus to shift to the body
				// but we can't detect a mouseup or a click immediately afterward
				// so we have to track the next mousedown and close the menu if
				// the user clicks somewhere outside of the autocomplete
				var menuElement = this.menu.element[ 0 ];
				if ( !$( event.target ).closest( ".ui-menu-item" ).length ) {
					this._delay(function() {
						var that = this;
						this.document.one( "mousedown", function( event ) {
							if ( event.target !== that.element[ 0 ] &&
									event.target !== menuElement &&
									!$.contains( menuElement, event.target ) ) {
								that.close();
							}
						});
					});
				}
			},
			menufocus: function( event, ui ) {
				// #7024 - Prevent accidental activation of menu items in Firefox
				if ( this.isNewMenu ) {
					this.isNewMenu = false;
					if ( event.originalEvent && /^mouse/.test( event.originalEvent.type ) ) {
						this.menu.blur();

						this.document.one( "mousemove", function() {
							$( event.target ).trigger( event.originalEvent );
						});

						return;
					}
				}

				// back compat for _renderItem using item.autocomplete, via #7810
				// TODO remove the fallback, see #8156
				var item = ui.item.data( "ui-autocomplete-item" ) || ui.item.data( "item.autocomplete" );
				if ( false !== this._trigger( "focus", event, { item: item } ) ) {
					// use value to match what will end up in the input, if it was a key event
					if ( event.originalEvent && /^key/.test( event.originalEvent.type ) ) {
						this._value( item.value || "" );
					}
				} else {
					// Normally the input is populated with the item's value as the
					// menu is navigated, causing screen readers to notice a change and
					// announce the item. Since the focus event was canceled, this doesn't
					// happen, so we update the live region so that screen readers can
					// still notice the change and announce it.
					this.liveRegion.text( (item?item.value:"") || "" );
				}
			},
			menuselect: function( event, ui ) {
				// back compat for _renderItem using item.autocomplete, via #7810
				// TODO remove the fallback, see #8156
				var item = ui.item.data( "ui-autocomplete-item" ) || ui.item.data( "item.autocomplete" ),
					previous = this.previous;

				// only trigger when focus was lost (click on menu)
				if ( this.element[0] !== this.document[0].activeElement ) {
					this.element.focus();
					this.previous = previous;
					// #6109 - IE triggers two focus events and the second
					// is asynchronous, so we need to reset the previous
					// term synchronously and asynchronously :-(
					this._delay(function() {
						this.previous = previous;
						this.selectedItem = item;
					});
				}

				this._select(event, item);
				/*
				if ( false !== this._trigger( "select", event, { item: item } ) ) {
					this._value( item.value );
				}
				// reset the term after the select event
				// this allows custom select handling to work properly
				this.term = this._value();

				this.close( event );
				this.selectedItem = item;
				*/
			}
		});

		this.liveRegion = $( "<span>", {
				role: "status",
				"aria-live": "polite"
			})
			.addClass( "ui-helper-hidden-accessible" )
			.insertAfter( this.element );

		if ( $.fn.bgiframe ) {
			this.menu.element.bgiframe();
		}

		// turning off autocomplete prevents the browser from remembering the
		// value when navigating through history, so we re-enable autocomplete
		// if the page is unloaded before the widget is destroyed. #7790
		this._on( this.window, {
			beforeunload: function() {
				this.element.removeAttr( "autocomplete" );
			}
		});
	},

	_destroy: function() {
		clearTimeout( this.searching );
		this.element
			.removeClass( "ui-autocomplete-input" )
			.removeAttr( "autocomplete" );
		this.menu.element.remove();
		this.liveRegion.remove();
	},

	_setOption: function( key, value ) {
		this._super( key, value );
		if ( key === "source" ) {
			this._initSource();
		}
		if ( key === "appendTo" ) {
			this.menu.element.appendTo( this.document.find( value || "body" )[0] );
		}
		if ( key === "disabled" && value && this.xhr ) {
			this.xhr.abort();
		}
		if ( key === "highlight") {
			if (value) this.uiDecoration.addClass("ui-state-highlight");
			else this.uiDecoration.removeClass("ui-state-highlight");
		}

	},

	_isMultiLine: function() {
		// Textareas are always multi-line
		if ( this.element.is( "textarea" ) ) {
			return true;
		}
		// Inputs are always single-line, even if inside a contentEditable element
		// IE also treats inputs as contentEditable
		if ( this.element.is( "input" ) ) {
			return false;
		}
		// All other element types are determined by whether or not they're contentEditable
		return this.element.prop( "isContentEditable" );
	},

	_initSource: function() {
		var array, url,
			that = this;
		if ( $.isArray(this.options.source) ) {
			array = this.options.source;
			this.source = function( request, response ) {
			    var rez = $.ui.autocomplete.filter( array, request.term );
				response( rez.length > 0 ? $.extend({model: rez}, {status: "ok"}) : {model: rez} );
			};
		} else if ( typeof this.options.source === "string" ) {
			url = this.options.source;
			this.source = function( request, response ) {
				if ( that.xhr ) {
					that.xhr.abort();
				}
				that.xhr = $.ajax({
					url: url,
					data: request,
					dataType: "json",
					success: function( data ) {
						response( data );
					},
					error: function() {
						response( [] );
					}
				});
			};
		} else {
			this.source = this.options.source;
		}
	},

	_searchTimeout: function( event ) {
		clearTimeout( this.searching );
		this.searching = this._delay(function() {
			// only search if the value has changed
			if ( this.term !== this._value() ) {
				this.selectedItem = null;
				this.search( null, event );
			}
		}, this.options.delay );
	},

	search: function( value, event ) {
		
		$('.qtip:visible').qtip('hide');
		value = value != null ? value : this._value();

		// always save the actual value, not the one passed as an argument
		this.term = this._value();

		if ( value.length < this.options.minLength ) {
			this._close( event );
		}

		clearTimeout( this.closing );
		if ( this._trigger( "search", event ) === false ) {
			return;
		}

		return this._search( value );
	},

	update: function( value ) {
		this._trigger( "command", null, { item: { command: "getitem", id: value, parameters: {} } } );
	},

	_search: function( value ) {
		this.pending++;
		this.element.addClass( "ui-autocomplete-loading" );
		var request = $.extend({}, this.options.context, { q: value, limit: this.options.limit+1 });
		this.cancelSearch = false;

		if ($.isFunction(this.options.request)) {
			if ( false === this.options.request(request) ) {
				return;
			}
		}

		this.source( { term: value }, this._response() );
	},

	_response: function() {
		var that = this,
			index = ++requestIndex;

		return function( content ) {
			if ( index === requestIndex ) {
				that.__response( content );
			}

			that.pending--;
			if ( !that.pending ) {
				that.element.removeClass( "ui-autocomplete-loading" );
			}
		};
	},

	__response: function( content ) {
		var displayField = this.options.displayField;
		var keyField = this.options.keyField;
		if (content) {
			if (content.status && content.status == "ok") {
				content = $.map(content.model, function( item ) {
						return {
							label: item[displayField],
							value: item[keyField],
							data: item
						}
					});
			} else {
				content = [];
			}
		}
		if ( content ) {
			content = this._normalize( content );
		}
		this._trigger( "response", null, { content: content } );

		if ( !this.options.disabled && content && !this.cancelSearch ) {
			this._suggest( content );
			this._trigger( "open" );
		} else {
			// use ._close() instead of .close() so we don't cancel future searches
			this._close();
		}
	},

	suggest: function(content) {
		this.cancelSearch = false;
		this.__response(content);
	},

	
	close: function( event ) {
		this.cancelSearch = true;
		this._close( event );
	},

	_close: function( event ) {
		if ( this.menu.element.is( ":visible" ) ) {
			this.menu.element.hide();
			this.menu.blur();
			this.isNewMenu = true;
			this._trigger( "close", event );
		}
	},

	_change: function( event ) {		
		if ( this.previous !== this._value() ) {
			this._trigger( "change", event, { item: this.selectedItem } );
		}
	},

	_normalize: function( items ) {
		// assume all items have the right format when the first item is complete
		if ( items.length && items[0].label && items[0].value ) {
			return items;
		}
		return $.map( items, function( item ) {
			if ( typeof item === "string" ) {
				return {
					label: item,
					value: item
				};
			}
			return $.extend({
				label: item.label || item.value,
				value: item.value || item.label
			}, item );
		});
	},

	_select: function(ev, item) {
		
		if (item) {
			if (item.value == "__command__") {
				this._trigger( "command", ev, { item: item } );
			} else if ( false !== this._trigger( "select", ev, { item: item } ) ) {
				this.element.val( item.value );
				this.selectedItem = item;
			}
		}
		// reset the term after the select event
		// this allows custom select handling to work properly
		this.term = this.element.val();

		this._close( ev );
		if (item && item.value != "__command__")
			this.selectedItem = item;
	},

	_suggest: function( items ) {
		var ul = this.menu.element
			.empty()
			.zIndex( this.element.zIndex() + 1 );
		if (items && items.length == 1) {
			this._select(null, items[0]);
			this.element.focusNextInputField();
			return;
		}
		this._renderMenu( ul, items );
		this.menu.refresh();

		// size and position menu
		ul.show();
		this._resizeMenu();
		ul.position( $.extend({
			of: this.element
		}, this.options.position ));

		if ( this.options.autoFocus ) {
			this.menu.next();
		}
	},

	_resizeMenu: function() {
		var ul = this.menu.element;
		ul.outerWidth( Math.max(
			// Firefox wraps long text (possibly a rounding bug)
			// so we add 1px to avoid the wrapping (#7513)
			ul.width( "" ).outerWidth() + 1,
			this.element.outerWidth()
		) );
	},

	_renderMenu: function( ul, items ) {
		var that = this;
		var self = this;
		var itemsToShow = (items.length > this.options.limit)? items.slice(0, this.options.limit): items;
		$.each( itemsToShow, function( index, item ) {
			that._renderItemData( ul, item );
		});
		for (var j = items.length; j<1; j++) {
			ul.append( "<li style='width: 100%;' ><div style='line-height: 1.5;'>&nbsp;</div></li>" );
		}
		self._renderFooter(ul, items);
	},

	_renderItemData: function( ul, item ) {
		return this._renderItem( ul, item ).data( "ui-autocomplete-item", item );
	},

	_renderItem: function( ul, item) {
		var li = $( "<li></li>" );
		if (item.value == "__command__") {
			li.append( $( "<a></a>" ).html("<span class='ui-icon-search ui-icon'></span>"+item.label));
		} else {
			li.append( $( "<a></a>" )
			.html( $.isFunction(this.options.formatItem)?this.options.formatItem(item):item.label ) )
		}	
		li.appendTo( ul );
		
		return li;
	},

	_renderFooter: function (ul, items) {
		var li, div;
		if ((!items.length) || (items.length > this.options.limit) || (this.options.links && this.options.links.length)) {
			li = $( "<li></li>" )
				.data( "ui-autocomplete-item", { value: '__text__', label: ''} )
				;//.css({ clear: 'left', float: 'left', width: '100%' });
			div = $('<div style="border-top: 1px solid; padding-top: 5px; padding-bottom: 3px;"></div>');
			li.append(div);
			if (!items.length) {
				div.html(this.options.localization.notFound);
			} else if (items.length > this.options.limit) {
				div.html($.validator.format(this.options.localization.found, this.options.limit));
			}
			li.appendTo(ul);
		}
		if (this.options.links && this.options.links.length) {
			for(var i=0; i<this.options.links.length; i++) {
				var link = this.options.links[i];
				if (!link.show
					|| (link.show == 2 && items.length >= this.options.limit) 
					|| (link.show == 1 && items.length <= this.options.limit)
					) {
						li = $( "<li></li>" )
							.data( "ui-autocomplete-item", { 
									value: '__command__', 
									label: link.text, 
									command: link.command
								})
							.append(
									$('<a class="ui-menu-addonlink"></a>').html($.validator.format("<span class='ui-icon {0}'></span>{1}", link.icon, link.text ))
								)
							.appendTo(ul);
				}
			}
		
		}
	},

	_move: function( direction, event ) {
		if ( !this.menu.element.is( ":visible" ) ) {
			this.search( null, event );
			return;
		}
		if ( this.menu.isFirstItem() && /^previous/.test( direction ) ||
				this.menu.isLastItem() && /^next/.test( direction ) ) {
			this._value( this.term );
			this.menu.blur();
			return;
		}
		this.menu[ direction ]( event );
	},

	widget: function() {
		return this.menu.element;
	},

	_uiDecorationHtml: function() {
		return "<span class='ui-spinner ui-state-default ui-widget ui-widget-content ui-corner-all' style='width: 100%'></span>";
	},

	_value: function() {
		return this.valueMethod.apply( this.element, arguments );
	},

	_keyEvent: function( keyEvent, event ) {
		if ( !this.isMultiLine || this.menu.element.is( ":visible" ) ) {
			this._move( keyEvent, event );

			// prevents moving cursor to beginning/end of the text field in some browsers
			event.preventDefault();
		}
	}
});

}( jQuery ));

(function( $, undefined ) {

var _clearFocusing = false;
var focusing = false;
var autocomplete = $.ui.autocomplete;
$.widget( "kesco.selectBox", {
		options: {
			disabled: false,
			required: false,
			keyField: "value",
			displayField: "label",
			minLength: 100,
			limit: 10,
			context: {},
			buttons: {
				search: false,
				view: false
			},
			localization: {
				found: 'Отображаются {0} записей. Найдено больше.',
				notFound: 'Записей не найдено.',
				search: 'Поиск',
				view: 'Просмотр'
			},
			command: null,
			focus: null,
			select: null,
			formatItem: null,
			links: [
			]
		},

		_create : function() {

			var self = this,
				$this = $(this);
			var o = this.options, e = this.element;
			var _select = o.select || 0;
			
			var container = 
				e.wrap("<div class='kesco-ui-select-box' style='position: relative; left: 0; top: 0;'></div>").parent();

			this.infoButtons = $("<span style='position: absolute; left: 0px; top: 0px; z-index: 1'></span>");
			e.before(this.infoButtons);

			var aco = $.extend({}, o, {
				minLength: 100,
				position: { my: "left top+3" },
				highlight: o.required && !e.val(),
				focus: function(ev, ui) {
					return false;
				},
				search: function(ev, ui ) {
					self._change("");
					self._trigger( "command", ev, { 
						item: { 
							command: "search", 
							parameters: { 
								Search: self.autocomplete.val() 
							} 
						} 
					});
					return false;
				},
				select: function(ev, ui ) {
					
					if (false !== self._trigger( "select", ev, { item: ui.item } ) ) {
						self.setValue(ui.item);
						if (ui.item.value != "__command__") {
							self.autocomplete.autocompleteEx("option", {
								resetItem : ui.item
							});
						}
					}  
					
					//ev.preventDefault();
					return false;
				},
				resetItem: { 
					value: ""+self.element.attr("data-value"),
					label: ""+self.element.attr("data-label")
				},
				changing: function(ev) {
					//if (!self.autocomplete.val()) 
					self._change("");
				}

			});
			

			this.autocomplete = $( "<input type='text' >" )
				.attr("id", this.element.attr("id")+"___Autocomplete")
				.css({
					'width': '100%',
					'background': 'none repeat scroll 0px 0px transparent',
					'border': 'medium none',
					'margin': '0.2em 0',
					'padding': '0px',
					'vertical-align': 'middle',
					'position': 'absolute'
				})
				.insertBefore(e)
				.autocompleteEx(aco)
				.bind("focus", function(ev) { /*_clearFocusing = false;*/ })
				.bind("blur", function( event ) {
					var $this = $(this);
					$('.qtip:visible').qtip('hide');
					if (false !== self._trigger("blur", event)) {
						var val = e.val();
						if (!val && self.autocomplete.val()) {
							// double check: FF doesn't focus the input 
							// while transition of blur
							clearTimeout(focusing);
							
							if (document.activeElement != self.autocomplete.get(0) 
								&& !$(".ui-dialog:visible").length) {
								self.autocomplete.focus();
								focusing = setTimeout(function() {
									self.autocomplete.focus();
								}, 200);
							}
							//self.autocomplete.autocompleteEx("tryToComplete");
						}
					}
					return false;
					
				}).wrap("<div style='position: relative; height: 17px; margin: 0 28px 0 3px'></div>");

			this.buttons = $("<span style='position: absolute; right: -2px; top: 0px;'></span>");
			container.append(this.buttons);
			this.searchButton = $("<button style='margin-left: 0px; height: 17px; border: medium none; top: 1px;' type='button' onclick='return false;'></button>")
				.html("...")
				.button({
						text: true,
						icons1: { primary: 'ui-icon-search' }
					})
				.blur(function(ev) { 
						setTimeout(function() {
							if (document.activeElement != self.autocomplete[0]) {
								self.close(); 
							}
						}, 50);
					})
				.click(function(event) {
						//alert("hi");
						//self.focus();
						var term = self.autocomplete.val();
						self.autocomplete.autocompleteEx("search", term?term:"?").focus();
						event.preventDefault();
						return false;
					})
				.appendTo(this.buttons).hide();

			this.searchButton.find(".ui-button-text")
				.css({
						'padding-left': '7px',
						'padding-right': '7px'
					});

			this.viewButton = $("<button style='margin-left: 0px; height: 17px; border: medium none; top: 1px;' type='button' onclick='return false;'></button>")
				.html( o.localization.view)
				.attr({
						title: o.localization.view,
						alt: o.localization.view
					})
				.button({
						text: false,
						icons: { primary: 'ui-icon-document' }
					})
				.blur(function(ev) { 
						setTimeout(function() {
							if (document.activeElement != self.autocomplete[0]) self.close(); 
						}, 50);
					})
				.click(function(event) {
						self._trigger( "command", event, { item: { value: '__command__', label: o.localization.view, command: 'view' } } );
						event.preventDefault();
						return false;
					})
				.appendTo(this.buttons).hide();
				
			this._toggleButtons();
			
			this.setValue( { value: e.val(), label: e.attr("data-label"), data: {} } );
			
			e.css({ position: 'absolute', left: -2});

		},

		clearFocusing: function(clear) {
			function _TRUE(undefined) { return clear == undefined || !!clear; }
			_clearFocusing = _TRUE();
			
		},
		
		_setOption: function( key, value ) {
			$.Widget.prototype._setOption.apply( this, arguments );
			if ( key === "displayField" ) {
				this.autocomplete.autocompleteEx("option", "displayField", value);
			}

			if ( key === "disabled") {
				var ac = this.autocomplete[0];
				if (value && !ac.disabled) ac.disabled = true;
				if (!value && ac.disabled) ac.removeAttribute("disabled");

				this.autocomplete.autocompleteEx("option", key, value);
				this.searchButton.button("option", key, value);
			}
		},

		_change: function(value) {
			if (value != this.element.val()) {
				this.element.val(value).change();
				this._toggleButtons();				
			}
			this.autocomplete.autocompleteEx("option", "highlight", this.options.required && !this.element.val());
		},
		
		_toggleButtons: function() {
			var val = this.element.val();
			this.viewButton.toggle(this.options.buttons.view === true && val != "");
			this.searchButton.toggle(!this.options.buttons.view || this.options.buttons.search === true && val == "");
		},

		focus: function() {
			this.autocomplete.focus();
		},
		
		getInput: function () {
			return this.autocomplete;
		},

		getValue: function () {
			var selected = this.autocomplete.data("kesco-autocompleteEx").selectedItem;
			return {
				value: this.element.val(),
				label: this.autocomplete.val(),
				data: (selected ? selected.data : null)
			};
		},

		setValue: function (item) {
			$('.qtip:visible').qtip('hide');
			
			this.autocomplete.val(item.label);
			this._change(item.value);			
			this.autocomplete.data("kesco-autocompleteEx").selectedItem = item.value?item:null;
		},
		
		update: function(value) {
			this.autocomplete.autocompleteEx("update", value);
		},
		
		suggest: function(content) {
			
			this.autocomplete.autocompleteEx("suggest", content);
		},
		
		addButton: function(options) {
			//return;
			var self = this;
			var button = $("<a class='ui-button ui-state-default ui-corner-all' style='height: 18px; width: 20px; top: 0px; overflow: hidden;' href='javascript: void(0);' ></a>")
				.attr("data-bind", options["data-bind"])
				.hover(
					function() { $(this).addClass('ui-state-hover'); }
					, function() { $(this).removeClass('ui-state-hover'); }
				)
				.focus(function() { $(this).addClass('ui-state-focus'); })
				.blur(function(ev) { 
						$(this).removeClass('ui-state-focus');
						setTimeout(function() {
							if (document.activeElement != self.autocomplete[0]) self.close(); 
						}, 50);
					})
				.click(options.click || function(event) {
						//alert("hi");
						//self.focus();
						self._trigger( "command", event, { item: { value: '__command__', label: options.text, command: options.command } } );
						self.autocomplete.focus();
						event.preventDefault();
						return false;
					})
				.appendTo(this.infoButtons);
			if (options.icon) {
				button.append($("<span class='ui-icon "+options.icon+"' style=''>&nbsp;</span>"));
			} else if (options.image) {
				button.append($("<img style='border: none;' src='"+options.image+"' />"));
			} else {
				button.text(options.text);
			}
			this.infoButtons.addClass("ui-corner-right");
			self.autocomplete.parent().css("padding-left", "22px");
		},
		
		close: function() {
			this.autocomplete.autocompleteEx("close");
		},

		destroy: function() {
			this.element
				.removeAttr( "kesco-select" )
			this.autocomplete.element.remove();
			$.Widget.prototype.destroy.call( this );
		}

});

}(jQuery));

(function( $, undefined ) {

var _clearFocusing = false;
var focusing = false;
var autocomplete = $.ui.autocomplete;
$.widget( "kesco.selectTextBox", {
		options: {
			disabled: false,
			//required: false,
			requiredCallBack: null,
			sourceCallBack: null,
			keyField: "value",
			displayField: "label",
			minLength: 100,
			limit: 10,
			context: {},
			buttons: {
				search: true
			},
			localization: {
				found: 'Отображаются {0} записей. Найдено больше.',
				notFound: 'Записей не найдено.',
				search: 'Поиск',
				view: 'Просмотр'
			},
			command: null,
			focus: null,
			select: null,
			formatItem: null,
			links: [
			]
		},

		_create : function() {
			
			var self = this;
			var o = this.options, e = this.element;
			var container =
				e.wrap("<div class='kesco-ui-select-box' style='position: relative; left: 0; top: 0;'></div>").parent();

			
			this.infoButtons = $("<span style='position: absolute; left: 0px; top: 0px; z-index: 1'></span>");
			e.before(this.infoButtons);
			this.autocomplete = $("<input type='text' >").attr("id", e.attr("id")+"___Autocomplete")
				.css({
					'width': '100%',
					'background': 'none repeat scroll 0px 0px transparent',
					'border': 'medium none',
					'margin': '0.2em 0',
					'padding': '0px',
					'vertical-align': 'middle',
					'position': 'absolute'
				})
				.insertBefore(e);
			var aco = $.extend({}, o, {
				minLength: 100,
				highlight: !self.autocomplete.val() && o.requiredCallBack && o.requiredCallBack(e.attr("name")),
				position: { my: "left top+3" },
				focus: function(ev, ui) {
					return false;
				},
				search: function () {
					if (o.sourceCallBack && $.isFunction(o.sourceCallBack))
					{
						self.autocomplete.autocompleteEx("suggest", {model: /*ViewModel.EmailStrongListFormatted()*/o.sourceCallBack(), status: 'ok'}).focus();
					}
					return false;
				},
				select: function (ev, ui) {
					self.autocomplete.val(ui.item.label);
					return false;
				},
				change: function () {
					e.val($(this).val()).change();
					self.autocomplete.autocompleteEx("option", "highlight", !$(this).val() && o.requiredCallBack && o.requiredCallBack(e.attr("name")));
				}
			});

			this.autocomplete.autocompleteEx(aco)
				.bind("keydown", function( event ) {
					if (event.keyCode == 9/*VK_TAB*/)
					{
						e.val($(this).val()).change();
					}					
				})
				.wrap("<div style='position: relative; height: 17px; margin: 0 28px 0 3px'></div>");        

			this.buttons = $("<span style='position: absolute; right: -2px; top: 0px;'></span>");
			container.append(this.buttons);
			this.searchButton = $("<button style='margin-left: 0px; height: 17px; border: medium none; top: 1px;' type='button' onclick='return false;'></button>")
				.html("...")
				.button({
					text: true,
					icons1: { primary: 'ui-icon-search' }
				})
				.click(function (event) {
					var term = self.autocomplete.val();
					self.autocomplete.autocompleteEx("search", term ? term : "?").focus();
					event.preventDefault();
					return false;
				})
				.appendTo(this.buttons).hide();

			this.searchButton.find(".ui-button-text")
				.css({
					'padding-left': '7px',
					'padding-right': '7px'
				});

			this.searchButton.toggle(o.buttons.search === true);

			this.autocomplete.val(e.val());
			
			e.css({ position: 'absolute', left: -2 });

		},		
		
		update: function(value) {
			this.autocomplete.val(value);
			this.autocomplete.autocompleteEx("option", "highlight", !value && this.options.requiredCallBack && this.options.requiredCallBack(this.element.attr("name")));
		},

		destroy: function() {
			this.element
				.removeAttr( "kesco-select" )
			this.autocomplete.element.remove();
			$.Widget.prototype.destroy.call( this );
		}

});

}(jQuery));

(function( $, undefined ) {


	window.KescoLookup_AdvSearch = function ($controlID, url) {
		openPopupWindow(url, null, function(result) {
				var item;
				//alert('result value = '+result);
				if (result) {
					item = $.isArray(result)?result[0]:result;
					$($controlID).selectBox("update", item.value);
				} else {
					$($controlID).selectBox("setValue", $($controlID).selectBox('option', 'resetItem'));
				}
				$($controlID).selectBox("focus");
			}, "wnd"+new Date().valueOf(), 800, 600);
	}

}(jQuery));

