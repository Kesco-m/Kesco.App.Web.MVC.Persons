(function($) {

	var element;
	
	$.srv4js = srv4js;
	$.srv4js.element;
	$.srv4js.path = '/styles/';
	$.srv4js.Run = srv4js_doRun;
	$.srv4js.RunEx = srv4js_doRunEx;
	$.srv4js.SearchDoc = srv4js_doSearchDoc;
	$.srv4js.OpenDoc = srv4js_doOpenDoc;
	$.srv4js.SendMessage = srv4js_doSendMessage;
	$.srv4js.ERROR = -1;
	$.srv4js.NOT_STARTED = 0;
	$.srv4js.STARTED = 1;
	$.srv4js.PENDING = 2;
	$.srv4js.COMPLETE = 3;
	$.srv4js.state;
	
	var firstCallState;
	
	function srv4js_onreadystatechange () {
		if (window.console) console.log("DocViewCaller readyState:"+element.readyState);
		if (element.readyState != 4) return;
		setTimeout(function() { srv4js_call(firstCallState) }, 10);
		
	}
	
	window.srv4js_onreadystatechange = srv4js_onreadystatechange;  
	
	function srv4js(action, options) {
		var state = $.srv4js.state = $.extend({ args: null }, options, {
			status: $.srv4js.NOT_STARTED,
			action: action,
			error: 0,
			errorMsg: "",
			value: null
		});

		try {
			if (element) {
				if (element.readyState == 4) srv4js_call(state);
				else {
					alertMessage(
							"\u0417\u0430\u043F\u0443\u0441\u043A", 
							"\u041F\u043E\u0434\u043E\u0436\u0434\u0438\u0442\u0435 5 \u0441\u0435\u043A\u0443\u043D\u0434\u044B \u0438 \u043D\u0430\u0436\u043C\u0438\u0442\u0435 OK...",
							"OK", 
							function() {
								srv4js_call(state);
							}
						);
				}
			} else {
				firstCallState = state;
					
				var _bodyTags = document.getElementsByTagName("body");
				if (_bodyTags.length == 0) {
					alert("Не найден тэг BODY! Перезагрузите страницу!");
					return;
				}
				var _body = _bodyTags[0];
				var _div = document.createElement("DIV");
				_div.id = "kesco_silverHost";
				_body.appendChild(_div);

				Silverlight.createObject(
					"/styles/Silver4JS.xap",      // source
					kesco_silverHost,                     // parent element
					"silver4js_obj",                // id for generated object element
					{width: "1px", height: "1px"},
					{onError: silver4js_Error, onLoad: srv4js_LoadCtrl },
					"ipAddress="+(kesco_ip == undefined ? "" : kesco_ip),
					"context"                       // context helper for onLoad handler.
				);
				
			}
		} catch(e) {
			state.status = $.srv4js.ERROR;
			state.error = 1;
			state.errorMsg = e.description;
			if (state.callback != null && $.isFunction(state.callback)) 
				state.callback(state);
		}
		return state;
	}	

	window.silverError = silverError;

	function silverError(type, message)
	{
		postSilverlightError(type, message);
	}

	function silver4js_Error(sender, args) {
		var appSource = "";
		if (sender != null && sender != 0) {
			appSource = sender.getHost().Source;
		}

		var errorType = args.ErrorType;
		var iErrorCode = args.ErrorCode;

		if (errorType == "ImageError" || 
			errorType == "MediaError") {
		  return;
		}

		var errMsg = "Unhandled Error in Silverlight Application " 
			+ appSource + "\n";

		errMsg += "Code: " + iErrorCode + "    \n";
		errMsg += "Category: " + errorType + "       \n";
		errMsg += "Message: " + args.ErrorMessage + "     \n";

		if (errorType == "ParserError") {
			errMsg += "File: " + args.xamlFile + "     \n";
			errMsg += "Line: " + args.lineNumber + "     \n";
			errMsg += "Position: " + args.charPosition + "     \n";
		}
		else if (errorType == "RuntimeError") {
			if (args.lineNumber != 0) {
				errMsg += "Line: " + args.lineNumber + "     \n";
				errMsg += "Position: " + args.charPosition + 
					"     \n";
			}
			errMsg += "MethodName: " + args.methodName + "     \n";
		}
		postSilverlightError(1, errMsg);
	}

	
	function srv4js_LoadCtrl(sender, args) {
		$.srv4js.element = element = sender;
		srv4js_call($.srv4js.state);
	}	
	
	function srv4js_call(state) {
		var i;
		try {	
			if (element.Content.SL2JS.wait == 1) 
				throw new Error(10101,'\u0421\u043D\u0430\u0447\u0430\u043B\u0430 \u043D\u043E\u0431\u0445\u043E\u0434\u0438\u043C\u043E \u0432\u044B\u0431\u0440\u0430\u0442\u044C \u043E\u0434\u0438\u043D \u0434\u043E\u043A\u0443\u043C\u0435\u043D\u0442.\n\u041D\u0435\u043B\u044C\u0437\u044F \u043E\u0434\u043D\u043E\u0432\u0440\u0435\u043C\u0435\u043D\u043D\u043E \u0432\u044B\u0431\u0438\u0440\u0430\u0442\u044C \u043D\u0435\u0441\u043A\u043E\u043B\u044C\u043A\u043E \u0434\u043E\u043A\u0443\u043C\u0435\u043D\u0442\u043E\u0432.');
			
			state.status = $.srv4js.STARTED;
			state.value = element.Content.SL2JS.Execute(state.action, state.args);
			
			state.error = element.Content.SL2JS.error;
			state.errorMsg = element.Content.SL2JS.errorMsg;
		
			state.status = (state.error) ? $.srv4js.ERROR : $.srv4js.PENDING;
			
			if (state.value == "WAIT") { 
				window.setTimeout(function() { srv4js_checkWait(state); }, 250); 
				return;
			}
		} catch(e) {
			state.status = $.srv4js.ERROR;
			state.error = 1;
			state.errorMsg = e.description;
		}
		if (state.callback != null && $.isFunction(state.callback)) 
			state.callback(state);
	}
	
	function srv4js_checkWait(state) {
		state.status = $.srv4js.PENDING;
		if (element.Content.SL2JS.wait==1) {
			window.setTimeout(function() { srv4js_checkWait(state); }, 250); 
			return state;
		}
		state.value = element.Content.SL2JS.buffer;
		state.status = $.srv4js.COMPLETE;
		if (state.callback != null && $.isFunction(state.callback)) 
			state.callback(state);
		if(!state.isInDocView) window.focus();
		return state;
	}

	function srv4js_doRun(fileName,args) {
		fileName = $.trim(fileName);
		args = $.trim(args);
		return srv4js('RUN', { 
			args: 'fileName='+encodeURI(fileName)+'&arguments='+encodeURI(args) 
		});
	}

	function srv4js_doRunEx(fileName, args, wStyle)	{
		fileName = $.trim(fileName);
		args = $.trim(args);
		wStyle = $.trim(wStyle)
		return srv4js('RUN', { args: 'fileName='+encodeURI(fileName)+'&arguments='+encodeURI(args)+'&wStyle='+encodeURI(wStyle) });
	}

	function srv4js_doSearchDoc(search, types, persons, callback)	{
		return srv4js('SEARCH', { args: 'search='+encodeURI(search) + '&types='+encodeURI(types) + '&person='+encodeURI(persons) + '&_persons='+encodeURI(persons) + "A", callback: callback });
	}
	
    function srv4js_doOpenDoc(id, newwindow, openImage, callback)	{		 
		return srv4js('OPENDOC', { args: 'id='+encodeURI(id) + '&newwindow='+encodeURI(newwindow) + '&imageid='+encodeURI(openImage), callback: callback });
	}

	function srv4js_doSendMessage(id, signer, msg, empids, callback)
	{
		return srv4js('SENDMESSAGE', { args: 'id='+encodeURI(id) + '&userId='+encodeURI(signer) + '&message='+encodeURI(msg) + '&checkall=1' + ( empids.length > 0 ? '&empids=' + empids : ''), callback: callback });
	}

})(jQuery);