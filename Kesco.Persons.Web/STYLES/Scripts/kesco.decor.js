(function( $ ) {

$.widget( "kesco.decor", {
	options: {
		'class': 'ui-spinner',
		elementClass: "ui-spinner-input",
		width: "100%"
	},

	_create: function() {
		var self = this;
		var uiDecoration = this.uiDecoration = this.element
			.addClass(this.options['elementClass'])
			.bind({
				focus: function() {
					self.uiDecoration.addClass( "ui-state-active" );
				},
				blur: function( event ) {
					self.uiDecoration.removeClass( "ui-state-active" );
				}
			})
			.css( { 
				"width": "100%",
				"position": 'absolute' 
			})
			// decorate
			.wrap(this._uiDecorationHtml())
				.parent().parent()
				.addClass(this.options['class'])
				.hover( 
					function( event ) { $( event.currentTarget ).addClass( "ui-state-hover" ); },
					function( event ) { $( event.currentTarget ).removeClass( "ui-state-hover" ); }
				);
	},

	_uiDecorationHtml: function() {
		var width = this.options["width"]?"width: "+this.options["width"]:"";
		var height = "17px";
		if (this.element.is('textarea')) {
			height = ""+(this.element.height()+5)+"px";
		}
		return "<span class='ui-state-default ui-widget ui-widget-content ui-corner-all' style='"+width+"'><div style='margin-right: 8px; position: relative; height: "+height+"'></div></span>";
	},
	
	_setOption: function( key, value ) {

		if ( key === "elementClass" ) {
			this.uiDecoration.removeClass(this.options[key]);
		}

		if ( key === "class" ) {
			this.element.removeClass(this.options[key]);
		}

		if ($.isFunction(this._super)) this._super( key, value );
		else $.Widget.prototype._setOption.call(this, key, value);
		
		if ( key === "elementClass" ) {
			this.uiDecoration.addClass(this.options[key]);
		}

		if ( key === "class" ) {
			this.element.addClass(this.options[key]);
		}

	},

	destroy: function() {
		this.element
			.removeClass( this.options.elementClass )
		if ($.isFunction(this._super)) this._super();
		else $.Widget.prototype.destroy.call(this);
		this.uiDecoration.replaceWith(this.element);
	},

	widget: function() {
		return this.uiDecoration;
	}
});

}( jQuery ) );