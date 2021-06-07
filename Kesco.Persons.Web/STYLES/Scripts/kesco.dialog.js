(function( $, undefined ) {

	var _timestamp = new Date().valueOf(); // используется для генерации имени окна
	var _windows = {};
	var _windowName = window.self.name?window.self.name:("Wnd"+_timestamp);
	var _lastPositionX = 20;
	var _lastPositionY = 20;
	
	$.windowManager = {};

	$.extend($.windowManager, {
		defaultSettings: {
			centerBrowser:0, // center window over browser window? {1 (YES) or 0 (NO)}. overrides top and left
			centerScreen:0, // center window over entire screen? {1 (YES) or 0 (NO)}. overrides top and left
			height:500, // sets the height in pixels of the window.
			left:0, // left position when the window appears.
			location:0, // determines whether the address bar is displayed {1 (YES) or 0 (NO)}.
			menubar:0, // determines whether the menu bar is displayed {1 (YES) or 0 (NO)}.
			resizable:0, // whether the window can be resized {1 (YES) or 0 (NO)}. Can also be overloaded using resizable.
			scrollbars:0, // determines whether scrollbars appear on the window {1 (YES) or 0 (NO)}.
			status:0, // whether a status line appears at the bottom of the window {1 (YES) or 0 (NO)}.
			width:500, // sets the width in pixels of the window.
			windowNameSuffix:null, // name of window set from the name attribute of the element that invokes the click
			url:null, // url used for the popup
			top:0, // top position when the window appears.
			toolbar:0, // determines whether a toolbar (includes the forward and back buttons) is displayed {1 (YES) or 0 (NO)}.
			callbackKey: null,
			callback: null,
			close: true
		},

		openWindow: function(settings) {
			settings = $.extend({}, this.defaultSettings, settings || {});
		
			if (!settings.windowNameSuffix) settings.windowNameSuffix = ""+new Date().valueOf().toString();

			var windowFeatures =    'height=' + settings.height +
									',width=' + settings.width +
									',toolbar=' + settings.toolbar +
									',scrollbars=' + settings.scrollbars +
									',status=' + settings.status + 
									',resizable=' + settings.resizable +
									',location=' + settings.location +
									',menuBar=' + settings.menubar;

			var centeredY,centeredX;
			var wnd;
			var wndName = this.generateWindowName(settings.windowNameSuffix);
            wnd = Kesco.windowOpen(settings.url, wndName, null, settings.control);           
			var wndInfo = { window: wnd, settings: settings };
				
			_windows[wndName] = wndInfo;
				
			return wndInfo;
		},
		
		getContext: function(wnd) {
			var name, wndInfo;
			
			for(var p in _windows) {
				if (_windows[p] && _windows[p].window == wnd) {
					name = p, wndInfo = _windows[p];
					break;
				}
			}
			if (wndInfo) {
				return { 
						url: wndInfo.settings.context.url,
						type: wndInfo.settings.context.type,
						context: JSON.stringify(wndInfo.settings.context) 
					};
			}
			return {
					url: '',
					context: ''
				}
		},

		closeDialogEx: function(wnd, dialogResult) {
			var name = null, wndInfo = null;
			
			window.self.focus();

			for(var p in _windows) {
				if (_windows[p] && _windows[p].window == wnd) {
					name = p, wndInfo = _windows[p];
					break;
				}
			}
			
			if (wndInfo) {

 				if (wndInfo.settings.callback && $.isFunction(wndInfo.settings.callback)) {
					
					var result = dialogResult?JSON.parse(dialogResult):dialogResult;
					wndInfo.settings.callback(result);
				}

 				//if (wndInfo.settings.close && !wndInfo.window.closed) {
					//wndInfo.window.close();
				//}


				delete _windows[name];
			}
			window.self.focus();
		},

		closeDialog: function(windowNameSuffix, dialogResult) {
			var name = this.generateWindowName(windowNameSuffix);
			window.focus();
			alert("name: "+name+"; result: "+dialogResult);
			var wndInfo = _windows[name];
			if (wndInfo) {
				if (wndInfo.settings.callback && $.isFunction(wndInfo.settings.callback)) {
					var result = dialogResult?JSON.parse(dialogResult):dialogResult;
					wndInfo.settings.callback(result);
				}

				if (wndInfo.settings.close && !wndInfo.window.closed) {
					wndInfo.window.close();
				}
				

				//_windows[name] = null;
			}
		},

		closeWindow: function(name) {
			var wndInfo = _windows[name];
			if (wndInfo && wndInfo.settings.close && !wndInfo.window.closed) {
				wndInfo.window.close();
				_windows[name] = null;
			}
		},

		generateWindowName: function(suffix) {
			return _windowName+"_childWnd"+suffix;
		},

		closeAll: function() {
			return;
			for(var wndName in _windows) {
				var wndInfo = _windows[wndName];
				if (wndInfo && wndInfo.settings.close && !wndInfo.window.closed) { 
					wndInfo.window.close();
					delete _windows[wndName];
				}
			}
		},

		debug: function() {
			if (!window.console) return;
			for(var wndName in _windows) {
				var wndInfo = _windows[wndName];
				console.log($.validator.format("Окно: {0}; {1}", wndName, wndInfo.settings.url));
			}
		},

		Windows: function() {
			return _windows;
		}

	});

	$(window).unload(function() {
		//$.windowManager.closeAll();
	});

}(jQuery));

