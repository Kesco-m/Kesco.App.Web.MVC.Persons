(function( $ ) {

$.widget( "kesco.person", {
	options: {
		container: null,
		value: null
	},

	_create: function() {
		var self = this,
			e = this.element;
		
		e.dynamicLink($.extend({ source: Env.URI_person_item }, this.options || {}));
		
		e.bind("click", function(ev) {
				var value = e.dynamicLink('getValue');
				window.ViewModel.showPerson(value.value);
				return false;
			});

		e.one("mouseenter", function() {
			e.initToolTip(function() {
				var uri = Env.URI_person_info;
				var item = $(this).dynamicLink('getValue');
				if (item && item.value)
					uri = uri.replace('/0', '/' + item.value);
				return uri;
			}, $(document.body)).mouseenter();
		});
			
	},
	
	_setOption: function( key, value ) {
		if ($.isFunction(this._super)) this._super( key, value );
		else $.Widget.prototype._setOption.call(this, key, value);
		if ( key === "value" ) {
			this.element.dynamicLink('option', 'value', value);
		}
		if ( key === "source" ) {
			this.element.dynamicLink('option', 'source', value);
		}
	},

	destroy: function() {
		this.element.off("click");
		this.element.dynamicLink("destroy");
		if ($.isFunction(this._super)) this._super();
		else $.Widget.prototype.destroy.call(this);
	},

	widget: function() {
		return this.element;
	}
});

}( jQuery ) );

(function( $ ) {

$.widget( "kesco.employee", {
	options: {
		container: null,
		value: null
	},

	_create: function() {
		var self = this,
			e = this.element;
		
		e.dynamicLink($.extend({ source: Env.URI_user_item }, this.options || {}));
		
		e.bind("click", function(ev) {
				var value = e.dynamicLink('getValue');
				window.ViewModel.showUser(value.value);
				return false;
			});

		e.one("mouseenter", function() {
			e.initToolTip(function() {
				var uri = Env.URI_user_info;
				var item = $(this).dynamicLink('getValue');
				if (item && item.value)
					uri = uri.replace('/0', '/' + item.value);
				return uri;
			}, $(document.body)).mouseenter();
		});
			
	},
	
	_setOption: function( key, value ) {
		if ($.isFunction(this._super)) this._super( key, value );
		else $.Widget.prototype._setOption.call(this, key, value);
		if ( key === "value" ) {
			this.element.dynamicLink('option', 'value', value);
		}
		if ( key === "source" ) {
			this.element.dynamicLink('option', 'source', value);
		}
	},

	destroy: function() {
		this.element.off("click");
		this.element.dynamicLink("destroy");
		if ($.isFunction(this._super)) this._super();
		else $.Widget.prototype.destroy.call(this);
	},

	widget: function() {
		return this.element;
	}
});

}( jQuery ) );

(function( $ ) {

var requestIndex = 0;

$.widget( "kesco.dynamicLink", {
	options: {
		source: null,
		value: null,
		emptyValue: '',
		emptyLabel: '-- Не указан --'
	},

	_create: function() {
		var self = this;
		var e = this.element;
		var o = this.options;
		this._initSource();
		
		this.setValue({
			value: o.value,
			label: e.html()
		});
	},
	
	_setOption: function( key, value ) {
		this._super( key, value );
		if ( key === "value" ) {
			this._updateTimeout();
		}
		if ( key === "source" ) {
			this._initSource();
		}
	},

	_updateTimeout: function(  ) {
		clearTimeout( this.updating );
		this.updating = this._delay(function() {
			this.update( null );
		}, 10 );
	},

	_initSource: function() {
		var array, url,
			that = this;
		if ( $.isArray(this.options.source) ) {
			array = this.options.source;
			this.source = function( request, response ) {
				response( $.ui.autocomplete.filter( array, request.term ) );
			};
		} else if ( typeof this.options.source === "string" ) {
			url = this.options.source;
			this.source = function( request, response ) {
				if ( that.xhr ) {
					that.xhr.abort();
				}
				setTimeout(function() {
					that.xhr = $.ajax({
						url: url,
						data: request,
						success: function( data ) {
							response( data );
						},
						error: function() {
							response( [] );
						}
					});
				}, 5);
			};
		} else {
			this.source = this.options.source;
		}
	},

	update: function(value) {
		value = value == null? this.options.value : value;
		return this._update( value );
	},

	_update: function( value ) {
		this.pending++;
		this.element.addClass( "ui-autocomplete-loading" );
		if (!value || value == '0') {
			this.setValue( { value: 0, label: ''});
		} else this.source( { 
			id: value,
			control: this.element.attr('id'),
			mode: 1
		}, this._response() );
	},

	_response: function() {
		var that = this,
			index = ++requestIndex;

		return function( content ) {

			that.pending--;
			if ( !that.pending ) {
				that.element.removeClass( "ui-autocomplete-loading" );
			}
		};
	},

	setValue: function(item) {
		item = this._normalizeItem(item);
		this.element.data('value', item);
		this.element.attr('title', item.label);
		this.element.attr('alt', item.label);
		this.element.html(item.label);
	},

	getValue: function(item) {
		return this.element.data('value');
	},

	_normalizeItem: function(item) {
		if ( typeof item === "string" ) {
			return {
				label: item,
				value: item,
				data: item
			};
		}
		return $.extend({
				label: item?(item.label || item.value):"",
				value: item?(item.value || item.label):"",
				data: (item)?(item.data || item.value || item.label):null
			}, item );
	},
	
	destroy: function() {
		this._super();
	},

	widget: function() {
		return this.element;
	}
});

}( jQuery ) );