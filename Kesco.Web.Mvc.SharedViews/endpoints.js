(function(namespace){
	var fn = window,
		  spaces = namespace.split("."),
		  ns, 
		  index,
		  segmentLength;
		
	for (index = 0, segmentLength = spaces.length; index < segmentLength; index++) {
		ns = spaces[index];
		if (typeof fn[ns] === "undefined") {
			fn[ns] = {};
		}
		
		fn = fn[ns];
	}
	
	fn.endpoint = {
		query: function(endpoint, parameters){
			var url = "";
				
			if(parameters){
				if(this._isArray(parameters)){
					url += this.buildArray(endpoint, parameters);
				}
				else if(this._isObject(parameters)){
					url += this.buildObject(parameters);
				}
				else{
					url += this.buildValue(endpoint, parameters);
				}
			}
			
			return url ? endpoint.url + "?" + url : endpoint.url;
		},
		
		buildArray: function(endpoint, parameters){
			var url = "", i;
			
			if(!endpoint.query.length){
				return url;
			}
			
			for(i = 0; i < parameters.length; i++){
				url += endpoint.query[i] + "=" + parameters[i];
						
				if(i < parameters.length - 1){
					url += "&";
				}
			}
			return url;
		},
		buildObject: function(parameters){
			var url = "", param;

			for(param in parameters){
				if(parameters.hasOwnProperty(param)){
					url += param + "=" + parameters[param] + "&";
				}
			}
			
			url = url.substring(0, url.length - 1);
			return url;
		},
		buildValue: function(endpoint, parameters){		
			return endpoint.query.length ? endpoint.query[0] + "=" + parameters : "";
		},
		
		_isNumeric:function(val) {
			return typeof val === "number" && isFinite(val);
		},
		_isFunction: function(val) {
			return typeof val === "function";
		},
		_isArray: function(val) {
			return val ? this._isNumeric(val.length) && this._isFunction(val.splice) : false;
		},
		_isObject: function(val){
			return typeof val === 'object';
		},
	
		Url: function(url, method, query){
			return {
					build: function(param){
						return app.net.endpoint.query({url: this.uri, method: this.method, query: this.query}, param);
					},
					uri: url,
					method: method,
					query: query
			};
		}
	};
	
}("app.net"));



app.net.endpoints = (function(prefix){
	return {



	}	
}("/"));


