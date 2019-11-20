Read the full T4JS documentation at http://t4js.codeplex.com/


Set the GenerateReadme setting to false to stop generating this file


======== Getting Started ========

* Start by editing the settings found in T4JS.tt.settings.t4
* Decorate the action methods you want to generate script objects and URLs for with the AjaxEndpoint attribute
* Run the T4JS Template
* Add the output JavaScript file to your views
* You're all set to start writing beautiful JavaScript


======== URL Building ========

<script type="text/javascript">
	// Example JavaScript
	
	// Build with no parameters

	// This will be the namespace you configure in the settings file, not necessarily "app.net".
	var url = app.net.endpoints.home.index.build();
	console.log(url);


	// Build with a single parameter
	url = app.net.endpoints.home.index.build(99);
	console.log(url);

	// Build with a multiple parameters
	url = app.net.endpoints.home.index.build([99, "test", "hello"]);
	console.log(url);

	// Build with a custom query string definition
	url = app.net.endpoints.home.index.build({something: 99, somethingElse: 88});
	console.log(url); 
</script>
