var Kesco = {
          
    event: (function () { var r = 0; function a(e) { var t, n, r; (t = (t = e) || window.event).isFixed || (t.isFixed = !0, t.preventDefault = t.preventDefault || function () { this.returnValue = !1 }, t.stopPropagation = t.stopPropagaton || function () { this.cancelBubble = !0 }, t.target || (t.target = t.srcElement), !t.relatedTarget && t.fromElement && (t.relatedTarget = t.fromElement == t.target ? t.toElement : t.fromElement), null == t.pageX && null != t.clientX && (n = document.documentElement, r = document.body, t.pageX = t.clientX + (n && n.scrollLeft || r && r.scrollLeft || 0) - (n.clientLeft || 0), t.pageY = t.clientY + (n && n.scrollTop || r && r.scrollTop || 0) - (n.clientTop || 0)), !t.which && t.button && (t.which = 1 & t.button ? 1 : 2 & t.button ? 3 : 4 & t.button ? 2 : 0)), e = t; var a = this.events[e.type]; for (var l in a) { !1 === a[l].call(this, e) && (e.preventDefault(), e.stopPropagation()) } } return { add: function (t, e, n) { t.setInterval && t != window && !t.frameElement && (t = window), n.guid || (n.guid = ++r), t.events || (t.events = {}, t.handle = function (e) { if ("undefined" != typeof Event) return a.call(t, e) }), t.events[e] || (t.events[e] = {}, t.addEventListener ? t.addEventListener(e, t.handle, !1) : t.attachEvent && t.attachEvent("on" + e, t.handle)), t.events[e][n.guid] = n }, remove: function (e, t, n) { var r = e.events && e.events[t]; if (r) { for (var a in delete r[n.guid], r) return; for (var a in e.removeEventListener ? e.removeEventListener(t, e.handle, !1) : e.detachEvent && e.detachEvent("on" + t, e.handle), delete e.events[t], e.events) return; delete e.handle, delete e.events } } } })(),

    globals: {
        documentMouseOver: false,
        domain: "kescom.com",
        version: "",
        versionHandler: "dialogResult.ashx",

        settingsFormLocation: "",
        settingsFormLocationAdv: "",

        defaultPages: [
            "default.htm", "default.html", "default.asp", "default.aspx",
            "index.htm", "index.html",
            "search.aspx",
            "roles.aspx"],

        qsParams: {
            opener: "opener"
        },

        action: {
            setWH: "set_wh",
            setLT: "set_lt"
        },

        resizeParams: {
            timeout: false,
            time: null,
            delta: 200
        },
        
        locationPrefix: "Location_",        
        locationSuffixDefault: "default",

        cookieExpires: 365,        

        cookieNames: {
            dialingDefaultPhone: "dialing_defaultphone",
            dialingShowPhone: "dialing_showPhoto"            
        }

    },

    scriptPath: function (filename) {
        var scriptElements = document.getElementsByTagName('script');
        for (var i = 0; i < scriptElements.length; i++) {
            var source = scriptElements[i].src;
            if (source.indexOf(filename) > -1) {
                return source;
            }
        }
        return false;
    },


    windowResize: function () {

        if (new Date() - Kesco.globals.resizeParams.time < Kesco.globals.resizeParams.delta) {
            setTimeout(Kesco.windowResize, Kesco.globals.resizeParams.delta);
        } else {
            Kesco.globals.resizeParams.timeout = false;
            Kesco.windowSaveLocationToDB(Kesco.globals.action.setWH);
        }
        
    },
      
    windowLocationParamName: function (href, ctrl, client) {
        
        var url = href == null ? location : Kesco.getHrefLocation(href);
        var pathname = url.pathname;
        var parts = pathname.split('/');

        var baseUrl = "";

        if (pathname.slice(0, 1) !== "/") pathname = "/" + pathname;

        var paramName = "";
        var domainName = "";
        var hostnameParts = url.hostname.split('.');

        if (hostnameParts.length < 2) {
            var jsHref = Kesco.scriptPath("kesco.js");
            domainName = Kesco.getQSParamValue("domain", jsHref);
            if (domainName == null || domainName == "")
                domainName = Kesco.globals.domain;
        }
        else
            domainName = hostnameParts[hostnameParts.length - 2] + "." + hostnameParts[hostnameParts.length - 1];

        if (parts.length == 1 && parts[0] == "") {
            paramName += domainName;
        }
        else {
            paramName += pathname.substring(1).toLowerCase();
            var lastPart = parts[parts.length - 1].toLowerCase();
            if (lastPart == "")
                paramName += Kesco.globals.locationSuffixDefault;
            else if ($.inArray(lastPart, Kesco.globals.defaultPages) != -1) {
                var re = new RegExp(lastPart, "g");
                paramName = paramName.replace(re, Kesco.globals.locationSuffixDefault)
            }
        }
               
        paramName = $.trim(paramName);//$.trim(paramName.replace(/[.|/]/g, '_'));       

        if (paramName.indexOf("/") < 0 && client != null) {

            var clientParts = client.paramName.split('/');
            var newName = "";
            for (var i = 0; i < clientParts.length - 1; i++)
                newName += (newName.length > 0 ? "/" : "") + clientParts[i];
            paramName = newName + "/" + paramName;

        }

        var opener = paramName;

        if (ctrl != null) {
            if (typeof ctrl === 'string' && ctrl!="")
                opener = paramName + '#' + ctrl;
            else if (ctrl.id != null && ctrl.id != "")            
                opener = paramName + '#' + ctrl.id;
        }

        var iisName = url.hostname.split('.')[0];

        if (url.protocol != "" && url.host != "")
            baseUrl = url.protocol + "//" + url.host;

        if (baseUrl !== "" && pathname.indexOf("/") > -1) { 
            baseUrl += "/"; 
            if (parts[0] == "")
                baseUrl += parts.length > 1 ? parts[1] : parts[0];
            else if (parts.length > 2)
                baseUrl += parts[0] + "/" + parts[1];
            else
                baseUrl += parts[0];
        }

        if (baseUrl == "" && client != null)
            baseUrl = client.baseUrl;


        return { iisName: iisName.toLowerCase(), paramName: paramName.toLowerCase(), opener: opener.toLowerCase(), domainName: domainName.toLowerCase(), baseUrl: baseUrl.toLowerCase() };
    },
    
    windowSaveLocationToDB: function (action, async, isDefaultSize) {
        
        var hrefs = [];
        var href = Kesco.globals.settingsFormLocation;

        if (href == null || href == "") return;

        var currentOpener = Kesco.getOpenerInfo();
        if (currentOpener == null) return;
        
        async = async == null ? true : async;

        var windowLocation = Kesco.getWindowLocation();
        var dt = new Date().getTime();

        href = Kesco.addQSParamValue(href, "action", isDefaultSize ? "default" : action);
        href = Kesco.addQSParamValue(href, "dt", dt);
        hrefs[0] = href;
        if (isDefaultSize) {
            var hrefAdv = Kesco.globals.settingsFormLocationAdv;
            if (hrefAdv != null && hrefAdv != "") {
                hrefAdv = Kesco.addQSParamValue(hrefAdv, "action", "default");
                hrefAdv = Kesco.addQSParamValue(hrefAdv, "dt", dt);
      
                hrefs[1] = hrefAdv;
            }
        }
        windowLocation.client = currentOpener;

        Kesco.windowSaveLocationToDB_sendPost(0, hrefs, async, windowLocation, isDefaultSize);       
    },


    windowSaveLocationToDB_sendPost: function (inx, hrefs, async, wLocation, isDefaultSize) {

        $.support.cors = true;

        $.ajax ({
            url: hrefs[inx],
            type: 'post',
            async: async,
            data: JSON.stringify(wLocation),
            contentType: 'application/json',
            crossDomain: true,
            xhrFields: { withCredentials: true },
            success: function (data) {
                if (isDefaultSize) {
                    var msg = (inx == 1 ? "В рабочей сети: " : "") + data;
                    alert(msg);
                }
                inx++;
                if (inx < hrefs.length)
                    Kesco.windowSaveLocationToDB_sendPost(inx, hrefs, async, wLocation, isDefaultSize);
            },
            error: function (data) {
                try { console.log("error:", data); } catch (e) { }
            }
        });

    },

    windowLoadLocationFromDB: function (client, server) {
        var href = Kesco.globals.settingsFormLocation;

        if (href == null || href == "" || client == null) return;

        var windowLocation = {};
        var locationInfo = {};

        locationInfo.server = server.paramName;
        locationInfo.version = Kesco.getServerVersion(server);
        locationInfo.client = client.opener;        
        locationInfo.x = window.screen.availWidth;
        locationInfo.y = window.screen.availHeight;

        if (locationInfo.version == "") return;

        var dt = new Date().getTime();

        href = Kesco.addQSParamValue(href, "action", "get");
        href = Kesco.addQSParamValue(href, "dt", dt);

        $.support.cors = true;

        $.ajax({
            url: href,
            type: 'post',
            async: false,  
            data: JSON.stringify(locationInfo),
            contentType: 'application/json',
            dataType: 'json',
            crossDomain: true,
            xhrFields: { withCredentials: true },
            success: function (data) {
                windowLocation = data;
            },
            error: function (data) {
                try { console.log("error:", data); } catch (e) { }
            }
        });

        return windowLocation;

    },



    windowOpen: function (href, target, params, ctrl) {

      
        var paramsDefault = {
            status: "no",
            toolbar: "no",
            menubar: "no",
            location: "no",
            resizable: "yes",
            scrollbars: "yes"
        };

        /*
        * если надо учитывать передаваемые параметры
        var paramsJsonString = "";
        if (params != null) params = params.toLowerCase();
        else params = "";

        var paramArray = params.split(',');

        for (var i = 0; i < paramArray.length; i++) {
            var pValue = paramArray[i].split('=');
            paramsJsonString += (i > 0 ? "," : "") + "\"" + pValue[0] + "\":\"" + pValue[1] + "\"";
        }

        var paramsJson = JSON.parse("{" + paramsJsonString + "}");
        */

        var paramsJson = paramsDefault;

        href = Kesco.getHrefLocation(href).href;

        var client = Kesco.windowLocationParamName(null, ctrl); //кто открывает
        var server = Kesco.windowLocationParamName(href, null, client); //что открываем

        var location = Kesco.windowLoadLocationFromDB(client, server);

        if (location) {
            if (location.Width != null && location.Height != null) {
                paramsJson.width = location.Width;
                paramsJson.height = location.Height;
            }
            else {
                paramsJson.width = null;
                paramsJson.height = null;
            }

            if (location.Left != null && location.Top != null) {
                paramsJson.top = location.Top;
                paramsJson.left = location.Left;
            }
            else {
                paramsJson.left = null;
                paramsJson.top = null;
            }
        } else {
            paramsJson.width = null;
            paramsJson.height = null;
            paramsJson.left = null;
            paramsJson.top = null;
        }

        if (paramsJson.status == null) paramsJson.status = paramsDefault.status;
        if (paramsJson.toolbar == null) paramsJson.toolbar = paramsDefault.toolbar;
        if (paramsJson.menubar == null) paramsJson.menubar = paramsDefault.menubar;
        if (paramsJson.location == null) paramsJson.location = paramsDefault.location;
        if (paramsJson.resizable == null) paramsJson.resizable = paramsDefault.resizable;
        if (paramsJson.scrollbars == null) paramsJson.scrollbars = paramsDefault.scrollbars;

        params = JSON.stringify(paramsJson).replace(/{|}|\"/g, "").replace(/:/g, "=");

        if (target == null)
            target = "_blank";

        href = Kesco.addQSParamValue(href, Kesco.globals.qsParams.opener, client.opener);

        return window.open(href, target, params);
    },

    windowShowModalDialog: function (href, arguments, params, ctrl) {

        
        var paramsDefault = {
            resizable: "yes",
            scroll: "no"
        };

             
          /*
        * если надо учитывать передаваемые параметры
        var paramsJsonString = "";
        if (params == null) params = paramsDefault;
        else params = params.toLowerCase();

        var param = params.split(';');
        for (var i = 0; i < param.length; i++) {
            if (param[i].length == 0)
                continue;
            var paramValue = param[i].split(':');
            paramsJsonString += (i > 0 ? "," : "") + "\"" + paramValue[0] + "\":\"" + paramValue[1] + "\"";
        }

        */

        var paramsJson = paramsDefault;

        href = Kesco.getHrefLocation(href).href;

        var client = Kesco.windowLocationParamName(null, ctrl); //кто открывает
        var server = Kesco.windowLocationParamName(href, null, client); //что открываем

        var location = Kesco.windowLoadLocationFromDB(client, server);

        if (location) {
            if (location.Width != null && location.Height != null) {
                paramsJson.dialogWidth = location.Width + "px";
                paramsJson.dialogHeight = location.Height + "px";
            }
            else {
                paramsJson.dialogWidth = null;
                paramsJson.dialogHeight = null;
            }

            if (location.Left != null && location.Top != null) {
                paramsJson.dialogTop = location.Top + "px";
                paramsJson.dialogLeft = location.Left + "px";
            }
            else {
                paramsJson.dialogTop = null;
                paramsJson.dialogLeft = null;
            }
        } else {
            paramsJson.dialogWidth = null;
            paramsJson.dialogHeight = null;
            paramsJson.dialogTop = null;
            paramsJson.dialogLeft = null;
        }

        if (paramsJson.resizable == null) paramsJson.resizable = paramsDefault.resizable;
        if (paramsJson.scroll == null) paramsJson.scroll = paramsDefault.scroll;

        params = JSON.stringify(paramsJson).replace(/{|}|\"/g, "").replace(/,/g, ";");
        
        href = Kesco.addQSParamValue(href, Kesco.globals.qsParams.opener, client.opener);

        return window.showModalDialog(href, arguments, params);
    },


    getServerVersion: function (server) {
        var version = "";
        var href = server.baseUrl + "/" + Kesco.globals.versionHandler;
        href = Kesco.addQSParamValue(href, "version", "true");

        $.support.cors = true;
        
        $.ajax({
            url: href,
            type: 'post',
            async: false,           
            contentType: 'application/json',
            dataType: 'json',
            crossDomain: true,
            xhrFields: { withCredentials: true },
            success: function (data) {
                version = data.Major + '.' + data.Minor + '.' + data.Build + '.' + data.Revision;        
            },
            error: function (data) {
                try { console.log("error:", data); } catch (e) { }
            }
        });

        return version;
    },

    getWindowLocation: function () {

        var windowLocation = {};
        var params;

        if (window.dialogHeight || window.dialogWidth || window.dialogTop || window.dialogLeft) {

            windowLocation.width = window.dialogWidth;
            windowLocation.height = window.dialogHeight;

            windowLocation.left = window.dialogLeft;
            windowLocation.top = window.dialogTop;
        }
        else {

            if (window.screenX)
                windowLocation.left = window.screenX;
            else
                windowLocation.left = window.screenLeft-8;

            if (window.screenY)
                windowLocation.top = window.screenY;
            else
                windowLocation.top = window.screenTop-31;

            windowLocation.width = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth;
            windowLocation.height = window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight;

        }

        params = Kesco.windowLocationParamName(); 

        windowLocation.server = params.paramName;       
        windowLocation.client = params.opener;       

        windowLocation.x = window.screen.availWidth;
        windowLocation.y = window.screen.availHeight;

        windowLocation.version = Kesco.globals.version;

        return windowLocation;
    },

    getCurrentUTCTicks: function () {
        var ts = new Date().getTime();
        ts -= ts % 1000;
        ts = ts / 1000;
        return ts;
    },
   
    getOpenerInfo: function () {

        var openerValue = Kesco.getQSParamValue(Kesco.globals.qsParams.opener);
        if (openerValue == null || openerValue == "") return;
        openerValue = openerValue.toLowerCase();
        return openerValue;
    },

    getHrefLocation: function (href) {
        var l = document.createElement("a");
        l.href = href;
        return l;
    },

    getQSParamValue: function (key, href) {

        var query = href == null ? window.location.search.substring(1) : Kesco.getHrefLocation(href).search.substring(1);
        var vars = query.split('&');
        for (var i = 0; i < vars.length; i++) {
            var pair = vars[i].split('=');
            if (decodeURIComponent(pair[0]).toLowerCase() === key.toLowerCase()) {
                return decodeURIComponent(pair[1]);
            }
        }
    },

    addQSParamValue: function (href, paramName, paramValue) {

        if (href == null || href == "") href = "about:blank";
        var separator = href.indexOf('?') > -1 ? "&" : "?";
        href = href + separator + paramName + "=" + encodeURIComponent(paramValue);

        return href;
    },
    

    removeLocationCookies: function () {
        var cookies = $.cookie();        
        var expiredDate = new Date();

        var params = Kesco.windowLocationParamName();

        expiredDate.setDate(expiredDate.getDate() - 1);        

        for (var cookieName in cookies) {
            if (cookieName.toLowerCase().indexOf(Kesco.globals.locationPrefix.toLowerCase()) == 0) {
                $.cookie(cookieName, "", { expires: expiredDate, path: '/', domain: params.domainName });
                $.removeCookie(cookieName, { path: '/' });              
            }            
        } 
    },

    removeLocationStorage: function () {
        window.localStorage.clear();        
    },


    convertFormat: function (num) {
        return {
            from: function (baseFrom) {
                return {
                    to: function (baseTo) {
                        return parseInt(num, baseFrom).toString(baseTo);
                    }
                };
            }
        };
    },

    toLocalTime: function (utcDateTime) {
        var notDateTime = new RegExp("[^0-9 :\./-]");
        var notDigit = new RegExp("[^0-9]");

        if (notDateTime.test(utcDateTime))
            return utcDateTime;

        var existDate = false, existTime = false, existMS = false;
        var yearLen = 0, monthLen = 0, dayLen = 0, hourLen = 0, minuteLen = 0, secondLen = 0, milsecLen = 0;
        var year = 1900, month = 1, monthday = 1, hour = 0, minute = 0, second = 0, milsec = 0;
        var result;

        var parts = utcDateTime.split(" ");
        for (var i = 0; i < parts.length; i++) {
            result = parts[i].match(/(\d+)/ig);
            if (parts[i].match(/[\.\/-]/ig)) {
                if (result != null && parts[i].match(/[\.]/ig)) {
                    if (result.length > 0) { dayLen = result[0].length; monthday = result[0]; }
                    if (result.length > 1) { monthLen = result[1].length; month = result[1]; }
                    if (result.length > 2) { yearLen = result[2].length; year = result[2]; }
                    existDate = true;
                }
                if (!existDate) {
                    if (result != null && parts[i].match(/(\/)/ig)) {
                        if (result.length > 0) { monthLen = result[0].length; month = result[0]; }
                        if (result.length > 1) { dayLen = result[1].length; monthday = result[1]; }
                        if (result.length > 2) { yearLen = result[2].length; year = result[2]; }
                        existDate = true;
                    }
                }
                if (!existDate) {
                    if (result != null && parts[i].match(/(-)/ig)) {
                        if (result.length > 0) { yearLen = result[0].length; year = result[0]; }
                        if (result.length > 1) { monthLen = result[1].length; month = result[1]; }
                        if (result.length > 2) { dayLen = result[2].length; monthday = result[2]; }
                        existDate = true;
                    }
                }

                if (yearLen == 2) year = 2000 + parseInt(year);
            }
            else if (parts[i].match(/[:]/ig)) {
                if (result != null && parts[i].match(/(:)/ig)) {
                    if (result.length > 0) { hourLen = result[0].length; hour = result[0]; }
                    if (result.length > 1) { minuteLen = result[1].length; minute = result[1]; }
                    if (result.length > 2) { secondLen = result[2].length; second = result[2]; }

                    existTime = true;
                }

            }
            else if (parts[i].length == 3 && !notDigit.test(parts[i])) {

                milsecLen = 3; milsec = parts[i];
                existMS = true;
            }
        }

        var newDate = new Date(year, month - 1, monthday, hour, minute, second, milsec);
        if (newDate != "Invalid Date") {
            var localDateTime = "";
            newDate = new Date(newDate.valueOf() - (newDate.getTimezoneOffset() * 60 * 1000));

            if (existDate) {
                localDateTime += ('0' + newDate.getDate()).slice(-dayLen);
                localDateTime += '.' + ('0' + (newDate.getMonth() + 1)).slice(-monthLen);
                if (yearLen > 0) localDateTime += '.' + ('' + newDate.getFullYear()).slice(-yearLen);
            }
            if (localDateTime.length > 0 && existTime) localDateTime += '&nbsp;';

            if (existTime) {
                localDateTime += ('0' + newDate.getHours()).slice(-hourLen);
                localDateTime += ':' + ('0' + newDate.getMinutes()).slice(-minuteLen);
                if (secondLen > 0) localDateTime += ':' + ('0' + newDate.getSeconds()).slice(-secondLen);
                if (existMS)
                    localDateTime += '&nbsp;' + ('00' + newDate.getMilliseconds()).slice(-milsecLen);
            }

            return localDateTime;
        }


        return utcDateTime;

    },

    wait: function (show) {

        if (v4_stopAsyncEvent) v4_stopAsyncEvent = show;

        if (show == false) {
            var objDiaolgOverlay = document.getElementById('v4_divDialogOverlay');
            if (!objDiaolgOverlay) return;

            objDiaolgOverlay.style.display = "none";
            try {
                if (active) {
                    var active_obj = document.getElementById(active);
                    if (active_obj) {
                        var nc = active_obj.getAttribute('nc');
                        if (nc == null) {
                            active_obj.focus();
                        }
                    }
                }
            }
            catch (e) { }
            return;
        }

        if (document.activeElement)
            active = document.activeElement.id;
        var winW = window.innerWidth == undefined ? document.body.clientWidth : window.innerWidth;
        var winH = window.innerHeight == undefined ? document.body.clientHeight : window.innerHeight;
        var divDialogOverlay = document.getElementById('v4_divDialogOverlay');
        if (divDialogOverlay) {
            divDialogOverlay.style.display = "block";
            divDialogOverlay.style.height = winH + "px";
            divDialogOverlay.style.cursor = 'wait';
        }
    },

    makeCall: function (phoneURI, callURI, paramsURI, target) {

        var defaultPhone = $.cookie(Kesco.globals.cookieNames.dialingDefaultPhone);
        var href = phoneURI;
        var _target = "phone_";
        if (defaultPhone) {
            var dfv = defaultPhone.split('_');
            if (dfv.length == 2 || dfv.length == 3 && dfv[2] == "1") {
                href = callURI;
                _target = "call_";
            }
        }

        href += (href.indexOf("?") > -1 ? "&" : "?") + paramsURI;

        var number = Kesco.getQSParamValue("InterNumber", href);

        if (number == null || number == "")
            _target += "0"
        else
            _target += number.replace(/[^\d]/g, "");

        var wnd = Kesco.windowOpen(href, _target, null, 'makeCall');
        wnd.focus();
    }
};


$(document).ready(function () {
    
    $(document)
        .on('mouseleave', function () {        
        Kesco.globals.documentMouseOver = false;
    }) .on('mouseover', function () {
        if (!Kesco.globals.documentMouseOver) {

            Kesco.windowSaveLocationToDB(Kesco.globals.action.setLT);
            Kesco.globals.documentMouseOver = true;
        }
    });

   

    $(document)
        .on('keydown', function (ev) {
            if (!ev) ev = event;
            if ((ev.altKey || ev.ctrlKey) && ev.keyCode == 115) {
                Kesco.windowSaveLocationToDB(Kesco.globals.action.setLT, false);                
                return;
            }

            if (ev.ctrlKey && ev.altKey && ev.keyCode == 76) {
                Kesco.windowSaveLocationToDB(Kesco.globals.action.setLT, false, true);
                return;
            }
        });


    $(window).resize(function () {
        Kesco.globals.resizeParams.time = new Date();
        if (Kesco.globals.resizeParams.timeout === false) {
            Kesco.globals.resizeParams.timeout = true;
            setTimeout(Kesco.windowResize, Kesco.globals.resizeParams.delta);
        }       
    });
    
    Kesco.removeLocationCookies();
    Kesco.removeLocationStorage();
        
    /*Отмена стандартного поведения кнопки F1*/
    Kesco.event.add(document, 'help', new Function("return false;"));
    Kesco.event.add(window, 'help', new Function("return false;"));

});

