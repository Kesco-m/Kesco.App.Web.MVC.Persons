/*!
* jQuery UI Numeric Up/Down v1.4.2
*
* Copyright 2011, Tony Kramer
* Dual licensed under the MIT or GPL Version 2 licenses.
* https://github.com/flamewave/jquery-ui-numeric/raw/master/GPL-LICENSE.txt
* https://github.com/flamewave/jquery-ui-numeric/raw/master/MIT-LICENSE.txt
*/

/*
* For documentation and for the latest version, see:
* https://github.com/flamewave/jquery-ui-numeric
*
* Dependencies:
* - jQuery (1.4.2)
* - jQuery-ui (1.8.6 - core, widget, button)
*/
(function($)
{
    // Default CSS Classes:
    // .ui-numeric { display: inline-block; }
    // .ui-numeric input[type=text], .ui-numeric input[type=number] { border: none; text-align: right; margin: 0px; vertical-align: top; }
    // .ui-numeric-currency { display: inline-block; padding: 0px 2px; vertical-align: top; }
    // .ui-numeric-buttons { display: inline-block; padding-left: 2px; }
    // .ui-numeric-buttons .ui-button { margin: 0px; width: 1.55em; height: 1.55em; }
    // .ui-numeric-disabled {}

    $.widget('ui.numeric', {
        version: '1.4.2',
        options: {
            disabled: false,
            keyboard: true,
            showCurrency: false,
            currencySymbol: null,
            title: null,

            buttons: true,
            upButtonIcon: 'ui-icon-triangle-1-n',
            upButtonTitle: null,
            downButtonIcon: 'ui-icon-triangle-1-s',
            downButtonTitle: null,

            emptyValue: 0,
            minValue: false,
            maxValue: false,
			
			messageRange: null,

            smallIncrement: 1,
            increment: 5,
            largeIncrement: 10,
            calc: null,
            format: null
        },

        _adjustmentFlag: false,
        _keyDownFlag: false,
        _timer: null,
        _name: 'numeric',
        _value: 0,

        _create: function()
        {
            var type = this.element.attr('type').toLowerCase();
            if (type !== 'text' && type !== 'number')
                throw 'numeric widget can only be applied to text and number inputs.';

            this._checkFormat();

            this._name = this.element.attr('id') || this.element.attr('name');
            this._value = this._getInputValue(this.element.attr('value'), true);

            if (this.options.minValue !== false && this._value < this.options.minValue)
                this._value = this.options.minValue;

            if (this.options.maxValue !== false && this._value > this.options.maxValue)
                this._value = this.options.maxValue;

            // Fix for issue #3 - Change event firing when initializing - credit to glittle for the fix.
            this.element.attr('value', this._format(this._value));
            this.element.attr('title', this.options.title || $.ui.numeric.globalization.defaultTooltip).wrap($('<div class="ui-widget ui-widget-content ui-corner-all ui-numeric" />'));

            if (this.options.showCurrency)
                this._createCurrency();

            if (this.options.buttons)
                this._createButtons();

            var self = this;
            this.element.bind({
                keydown: function(event) { return self._onKeyDown(event) },
                keyup: function(event) { return self._onKeyUp(event) },
                change: function(event) { return self._onChange(event) }
            });

            if (this.options.disabled || this.element.attr('disabled'))
                this._setOption('disabled', true);

            // Prevent memory leaks.
            $(window).bind('unload', function() { self.destroy(); });
        },

        destroy: function()
        {
            var self = this;
            this.element.unbind({
                keydown: function(event) { return self._onKeyDown(event) },
                keyup: function(event) { return self._onKeyUp(event) },
                change: function(event) { return self._onChange(event) }
            });

            if (this.options.showCurrency)
                $('#' + this._name + '_currency').remove();

            if (this.options.buttons)
                $('#' + this._name + '_buttons').remove();

            this.element.unwrap();
            $.Widget.prototype.destroy.call(this);

            // Ensure that once the widget is destoryed, the page doesn't try to destroy it on unload.
            $(window).unbind('unload', function() { self.destroy(); });
        },

        _createCurrency: function()
        {
            this.element.before($('<div/>').attr('id', this._name + '_currency').addClass('ui-numeric-currency').html(this.options.currencySymbol || Number.globalization.defaultCurrencyFormat.symbol));
        },

        _createButtons: function()
        {
            var btnUp = $('<button type="button"></button>')
                .attr('title', this.options.upButtonTitle || $.ui.numeric.globalization.defaultUpTooltip)
                .bind({
                    keydown: function(event) { keydown(event, false); },
                    keyup: function() { up(); },
                    mousedown: function(event) { down(event, false); },
                    mouseup: function() { up(); }
                })
                .button({ text: false, label: 'U', icons: { primary: this.options.upButtonIcon} });

            var btnDown = $('<button type="button"></button>')
                .attr('title', this.options.downButtonTitle || $.ui.numeric.globalization.defaultDownTooltip)
                .bind({
                    keydown: function(event) { keydown(event, true); },
                    keyup: function() { up(); },
                    mousedown: function(event) { down(event, true); },
                    mouseup: function() { up(); }
                })
                .button({ text: false, label: 'D', icons: { primary: this.options.downButtonIcon} });

            this._addButtons(btnUp, btnDown);
            var self = this;

            function keydown(event, neg)
            {
                // Allow space or enter to trigger the button.
                if (event.which == 32 || event.which == 13)
                {
                    down(event, neg);
                    event.target.focus();
					event.stopImmediatePropagation();
                }
            }

            function down(event, neg)
            {
                // TODO: Fix if there are more than one numeric instances on the page, then if a button on one instance is clicked, then a button on
                // another instance is clicked, both buttons are shown to have focus.

                // Fixes an issue where if the other button is clicked, then both buttons are shown to have focus.
                (neg ? btnUp : btnDown).blur();
                var inc = self._getIncrement(event.ctrlKey, event.shiftKey);
                self._adjustValueRecursive(neg ? -inc.value : inc.value, inc.type);
            }

            function up()
            {
                clearTimeout(self._timer);
            }
        },

        _addButtons: function(btnUp, btnDown)
        {
            this.element.after(
                $('<div/>')
                    .attr('id', this._name + '_buttons')
                    .addClass('ui-numeric-buttons')
                    .append(btnUp)
                    .append(btnDown)
            );
        },

        _setOption: function(key, value)
        {
            switch (key)
            {
                case 'disabled':
                    this.element.parent()[value ? 'addClass' : 'removeClass']('ui-numeric-disabled ui-state-disabled').attr('aria-disabled', value);
                    this._adjustmentFlag = true;
                    if (value)
                        this.element.attr({ disabled: 'disabled', value: '' });
                    else
                        this.element.removeAttr('disabled').attr('value', this._format(this._value));
                    this._adjustmentFlag = false;

                    if (this.options.buttons)
                        $('#' + this._name + '_buttons button').button(value ? 'disable' : 'enable');
                    break;

                case 'emptyValue':
                    this.options.emptyValue = value;
                    this._setValue(this._value);
                    break;

                case 'minValue':
                    this.options.minValue = value === false ? false : _numVal(value);
                    if (this.options.minValue !== false && this._value < this.options.minValue)
                        this._setValue(this.options.minValue);
                    break;

                case 'maxValue':
                    this.options.maxValue = value === false ? false : _numVal(value);
                    if (this.options.maxValue !== false && this._value > this.options.maxValue)
                        this._setValue(this.options.maxValue);
                    break;

                case 'format':
                    this.options.format = value;
                    this._checkFormat();
                    this._setValue(this._value);
                    break;

                case 'title':
                    this.options.title = value || $.ui.numeric.globalization.defaultTooltip;
                    this.element.attr('title', this.options.title);
                    break;

                case 'showCurrency':
                    if (value && !this.options.showCurrency)
                        this._createCurrency();

                    else if (!value && this.options.showCurrency)
                        $('#' + this._name + '_currency').remove();

                    this.options.showCurrency = value;
                    break;

                case 'currencySymbol':
                    this.options.currencySymbol = value || Number.globalization.defaultCurrencyFormat.symbol;
                    if (this.options.showCurrency)
                        $('#' + this._name + '_currency').html(this.options.currencySymbol);
                    break;

                case 'buttons':
                    if (value && !this.options.buttons)
                        this._createButtons();

                    else if (!value && this.options.buttons)
                        $('#' + this._name + '_buttons').remove();

                    this.options.buttons = value;
                    break;

                case 'upButtonIcon':
                    this.options.upButtonIcon = value;
                    if (this.options.buttons)
                        $('#' + this._name + '_buttons').find('button:eq(0)').button('option', 'icons', { primary: value });
                    break;

                case 'upButtonTitle':
                    this.options.upButtonTitle = value || $.ui.numeric.globalization.defaultUpTooltip;
                    if (this.options.buttons)
                        $('#' + this._name + '_buttons').find('button:eq(0)').attr('title', this.options.upButtonTitle);
                    break;

                case 'downButtonIcon':
                    this.options.downButtonIcon = value;
                    if (this.options.buttons)
                        $('#' + this._name + '_buttons').find('button:eq(1)').button('option', 'icons', { primary: value });
                    break;

                case 'downButtonTitle':
                    this.options.downButtonTitle = value || $.ui.numeric.globalization.defaultDownTooltip;
                    if (this.options.buttons)
                        $('#' + this._name + '_buttons').find('button:eq(1)').attr('title', this.options.downButtonTitle);
                    break;

                default:
                    $.Widget.prototype._setOption.call(this, key, value);
                    break;
            }
            return this;
        },

        _checkFormat: function()
        {
			// !!!FDV
			var o=this.options;
			if(typeof o.format==='string')
				o.format={format:o.format,decimalChar:'.',thousandsChar:','};
			else
				o.format=$.extend({format:'0',decimalChar:'.',thousandsChar:','},o.format);
			
			this.widget().attr("input-culture",o.format.culture?o.format.culture:'');

            //this.options.format = $.extend({}, Number.globalization.defaultFormat, typeof this.options.format === 'string' ? { format: this.options.format} : this.options.format);
        },

        _getInputValue: function(val, parse)
        {
			val=val.replace(this.options.currencySymbol,'');
			return parse?_numVal(val,this.options.format.culture):val;
			
        },

        _setInputValue: function(val)
        {
            // Set a flag to keep the "onchange" event from calling this method causing an infinate loop.
            this._adjustmentFlag = true;
            this.element.attr('value', this._format(val)).change();
            this._adjustmentFlag = false;
        },

        _setValue: function(val, isFromTimer)
        {
			isFromTimer = !!isFromTimer;
            val = _numVal(val);
			
			var msg, warning;
			if (this.options.minValue !== false && this.options.maxValue !== false) {
				msg = $.validator.format("Значение должно быть между {0} и {1} включительно.", this.options.minValue, this.options.maxValue);
			} else if (this.options.minValue !== false) {
				msg = $.validator.format("Значение должно быть больше или равно {0}.", this.options.minValue, this.options.maxValue);
			} else if (this.options.maxValue !== false) {
				msg = $.validator.format("Значение должно быть меньше или равно {1}.", this.options.minValue, this.options.maxValue);
			}
			
            if (this.options.minValue !== false && val < this.options.minValue) {
				warning = msg;
                val = this.options.minValue;
			}

            if (this.options.maxValue !== false && val > this.options.maxValue) {
				warning = msg;
                val = this.options.maxValue;
			}

			if (isFromTimer && warning) {
				alert(warning);
			}
			
            this._value = val;
			
            if (!this.options.disabled)
                this._setInputValue(val);
				
        },

        _format: function(val)
        {
			// !!! FDV Changed
			var o = this.options;
			var format = this.options.showCurrency?("c"+this.options.format.precision):("n"+this.options.format.precision);
			return isNaN(val) || (o.emptyValue!==false && val===o.emptyValue)?'':Globalize.format(val, format,this.options.format.culture);

            // return isNaN(val) || (this.options.emptyValue !== false && val === this.options.emptyValue) ? '' : $.formatNumber(val, this.options.format);
        },

        _getIncrement: function(ctrl, shift)
        {
            if (ctrl)
                return { value: this.options.smallIncrement, type: 2 };

            else if (shift)
                return { value: this.options.largeIncrement, type: 3 };

            return { value: this.options.increment, type: 1 };
        },

        _adjustValue: function(amount, type, isFromTimer)
        {
            if (this.options.disabled)
                return;

            this._setValue($.isFunction(this.options.calc) ? this.options.calc(this._value, type, amount < 0 ? 2 : 1) : this._value + amount, isFromTimer);
            this.select();
        },

        _adjustValueRecursive: function(amount, type)
        {
            $.ui.numeric._current = this;
            $.ui.numeric._timerCallback(amount, type, true);
        },

        _onKeyDown: function(event)
        {
            if (this.options.disabled)
                return;

            // Fix for issue #4: format characters were not allowed for non-standard format character values.
            // toLowerCase is used to normalize characters in case the user has caps lock on.
            // Note: The $.keyCodeToCharCode() method is written for use with the standard US 101 keyboard. It may need to be modified for other keyboard layouts.
            var v_char = String.fromCharCode($.keyCodeToCharCode(event.which, event.shiftKey)).toLowerCase();
            if (v_char === this.options.format.decimalChar.toLowerCase() || v_char == this.options.format.thousandsChar.toLowerCase())
            {
                this._keyDownFlag = true;
                return;
            }

            switch (event.which)
            {
                // The following are non-control keys that we want to allow through to perform their default function with no other actions.                               
                case 109: // Negative Sign
                    this._keyDownFlag = true;
                    return;

                case 38: // Up Arrow
                case 40: // Down Arrow
                    if (this.options.keyboard)
                    {
                        var inc = this._getIncrement(event.ctrlKey, event.shiftKey);
                        this._adjustValue(event.which == 40 ? -inc.value : inc.value, inc.type);
                    }
                    return;

                case 33: // Page Up
                    if (this.options.keyboard)
                        this._adjustValue(this.options.largeIncrement, 3);
                    return;

                case 34: // Page Down
                    if (this.options.keyboard)
                        this._adjustValue(-this.options.largeIncrement, 3);
                    return;

                    // The following are keyboard shortcuts that we still want to allow.
                case 65: // A (select all)
                case 67: // C (copy)
                case 86: // V (paste)
                case 88: // X (cut)
                case 89: // Y (redo)
                case 90: // Z (undo)
                    if (event.ctrlKey)
                        return;
                    break;
            }

            if (isControlKey(event.which))
                return;

            if (!isNumericKey(event.which))
            {
                event.preventDefault();
                event.stopPropagation();
                return;
            }
        },

        _onKeyUp: function()
        {
            // Make sure the value gets updated after every keypress if the current value of the input parses to a valid number.
            var v = parseFloat(this._getInputValue(this.element.attr('value'), false));
            if (!isNaN(v))
                this._value = v;

            this._keyDownFlag = false;
        },

        _onChange: function(event)
        {
            if (!this._adjustmentFlag && !this._keyDownFlag)
                this._setValue(this._getInputValue(event.target.value, true));
        },

        // Gets or sets the numeric value as a JavaScript Number object.
        value: function(val)
        {
            // Method called as a getter.
            if (val === undefined)
                return this._value;

            this._setValue(val);
            return this;
        },

        // Selects the input value.
        select: function()
        {
            if (!this.options.disabled)
                this.element.select();

            return this;
        }
    });

    $.ui.numeric.globalization = {
        defaultTooltip: 'Укажите значение или используйте кнопки и клавиши стрелок на клавиатуру, чтобы поменять значение. Удерживайте Ctrl или Shift соответсвенно для меньшего или большего изменения значения.',
        defaultUpTooltip: 'Увеличить значение. Удерживайте Ctrl или Shift для меньшего или большего увеличения значения.',
        defaultDownTooltip: 'Уменьшить значение. Удерживайте Ctrl или Shift для меньшего или большего уменьшения значения.'
    };

    $.ui = $.ui || {};
    $.ui.numeric._current = null;
    $.ui.numeric._timerCallback = function(amount, type, first)
    {
        clearTimeout($.ui.numeric._current._timer);
        $.ui.numeric._current._adjustValue(amount, type, first);
        $.ui.numeric._current._timer = setTimeout('jQuery.ui.numeric._timerCallback(' + amount + ',' + type + ',false)', first ? 1000 : 50);
    }

    var KEY_CODE_MAP_SHIFT = {
        48: 41, // )
        49: 33, // !
        50: 64, // @
        51: 35, // #
        52: 36, // $
        53: 37, // %
        54: 94, // ^
        55: 38, // &
        56: 42, // *
        57: 40, // (

        59: 58,   // : for geko and opera
        61: 43,   // + for geko and opera
        186: 58,  // : for webkit and IE
        187: 43,  // + for webkit and IE
        188: 60,  // <
        109: 95,  // _ for geko
        189: 95,  // _ for webkit and IE
        190: 62,  // >
        191: 63,  // ?
        192: 126, // ~
        219: 123, // {
        220: 124, // |
        221: 125, // }
        222: 34   // "
    };

    var KEY_CODE_MAP_NORMAL = {
        59: 59, // ; for geko and opera
        61: 61, // = for geko and opera

        96: 48,  // numpad 0
        97: 49,  // numpad 1
        98: 50,  // numpad 2
        99: 51,  // numpad 3
        100: 52, // numpad 4
        101: 53, // numpad 5
        102: 54, // numpad 6
        103: 55, // numpad 7
        104: 56, // numpad 8
        105: 57, // numpad 9

        106: 42, // numpad *
        107: 43, // numpad +
        109: 45, // numpad -
        110: 46, // numpad .
        111: 47, // numpad /

        186: 59, // ; for webkit and IE
        187: 61, // = for webkit and IE
        188: 44, // ,
        109: 45, // - for geko
        189: 45, // - for webkit and IE
        190: 46, // .
        191: 47, // /
        192: 96, // `
        219: 91, // [
        220: 92, // \
        221: 93, // ]
        222: 39  // '
    };

    $.keyCodeToCharCode = function(keyCode, shift)
    {
        if (keyCode >= 48 && keyCode <= 57 && !shift) // 0-9
            return keyCode;

        if (keyCode >= 65 && keyCode <= 90) // A-Z and a-z
            return shift ? keyCode : keyCode + 32;

        if (keyCode === 9 || keyCode === 32) // tab, space
            return keyCode;

        return shift ? KEY_CODE_MAP_SHIFT[keyCode] : KEY_CODE_MAP_NORMAL[keyCode];
    }

    function isControlKey(keyCode)
    {
        return (keyCode <= 47 && keyCode != 32)
            || (keyCode >= 91 && keyCode <= 95)
            || (keyCode >= 112 && [188, 190, 191, 192, 219, 220, 221, 222].indexOf(keyCode) == -1)
    }

    function isNumericKey(keyCode)
    {
        // 48 - 57 are numerical key codes for key pad nubers, 96 - 105 are numerical key codes for num pad numbers.
        return (keyCode >= 48 && keyCode <= 57) || (keyCode >= 96 && keyCode <= 105);
    }

    function _numVal(val, culture)
    {
		// FDV CHANGED 
        if (typeof val !== 'number')
            val = Globalize.parseFloat(val, 10, culture); 

        if (isNaN(val))
            return 0;

        return val;
    }


})(jQuery);