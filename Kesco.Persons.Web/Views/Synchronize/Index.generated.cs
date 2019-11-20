﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.36400
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 3 "..\..\Views\Synchronize\Index.cshtml"
    using Kesco.Persons.Web.Models.Synchronize;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Views\Synchronize\Index.cshtml"
    using Kesco.Web.Mvc;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Synchronize/Index.cshtml")]
    public partial class _Views_Synchronize_Index_cshtml : Kesco.Persons.Web.SiteViewPage<Kesco.Persons.Web.Models.Synchronize.ViewModel>
    {
        public _Views_Synchronize_Index_cshtml()
        {
        }
        public override void Execute()
        {




            
            #line 4 "..\..\Views\Synchronize\Index.cshtml"
  
	Model
		.GetScriptCapabilities()
			.DisableTreeScript()
			.DisableGridScript();
	ViewBag.Title = Request["title"] ?? Kesco.Persons.Web.Localization.Resources.Persons_Synchronize_PageTitle;
	Layout = "~/Views/Shared/_Layout.cshtml";


            
            #line default
            #line hidden
WriteLiteral(@"<style>
	.differences { padding: 5px; }
	.differences .caption { padding: 3px; }
	.difference { border-bottom: 1px solid gray; padding: 3px 0; }
	.difference .field { width: 180px; padding-right: 20px; display: inline-block; }
	.difference .insteadof { width: 180px; padding-right: 20px; display: inline-block; text-align: right; color: Gray; }
	.difference .source { }
	.difference .target { color: Gray;}
</style>
");


            
            #line 21 "..\..\Views\Synchronize\Index.cshtml"
 if (Model.Differences.HasDiffences) {

            
            #line default
            #line hidden
WriteLiteral("\t<div>");


            
            #line 22 "..\..\Views\Synchronize\Index.cshtml"
  Write(global::Resources.Resources.Persons_Synchronize_DifferencesHeader);

            
            #line default
            #line hidden
WriteLiteral(":</div>\r\n");


            
            #line 23 "..\..\Views\Synchronize\Index.cshtml"
	
            
            #line default
            #line hidden
            
            #line 23 "..\..\Views\Synchronize\Index.cshtml"
  if (Model.Differences.Data.Count > 0) {

            
            #line default
            #line hidden
WriteLiteral("\t<div class=\"differences\">\r\n\t\t<div class=\"caption ui-widget-header\">");


            
            #line 25 "..\..\Views\Synchronize\Index.cshtml"
                                    Write(global::Resources.Resources.Persons_Synchronize_Differences_Requisites);

            
            #line default
            #line hidden
WriteLiteral(":</div>\r\n");


            
            #line 26 "..\..\Views\Synchronize\Index.cshtml"
 		foreach (Difference diff in Model.Differences.Data) {

            
            #line default
            #line hidden
WriteLiteral("\t\t<div class=\"difference\">\r\n\t\t\t<span class=\"field\">");


            
            #line 28 "..\..\Views\Synchronize\Index.cshtml"
                  Write(diff.Field);

            
            #line default
            #line hidden
WriteLiteral("</span> \r\n\t\t\t<span class=\"source\">");


            
            #line 29 "..\..\Views\Synchronize\Index.cshtml"
                   Write(diff.Source);

            
            #line default
            #line hidden
WriteLiteral("</span><br />\r\n\t\t\t<span class=\"insteadof\">вместо</span> \r\n\t\t\t<span class=\"target\"" +
">");


            
            #line 31 "..\..\Views\Synchronize\Index.cshtml"
                   Write(diff.Target);

            
            #line default
            #line hidden
WriteLiteral("</span>\r\n\t\t</div>\r\n");


            
            #line 33 "..\..\Views\Synchronize\Index.cshtml"
		}

            
            #line default
            #line hidden
WriteLiteral("\t</div>\r\n");


            
            #line 35 "..\..\Views\Synchronize\Index.cshtml"
	}
            
            #line default
            #line hidden
            
            #line 35 "..\..\Views\Synchronize\Index.cshtml"
  
	
	
            
            #line default
            #line hidden
            
            #line 37 "..\..\Views\Synchronize\Index.cshtml"
  if (Model.Differences.WorkPlaces.Count > 0) {

            
            #line default
            #line hidden
WriteLiteral("\t<div class=\"differences\">\r\n\t\t<div class=\"caption ui-widget-header\">Места работы:" +
"</div>\r\n");


            
            #line 40 "..\..\Views\Synchronize\Index.cshtml"
 		foreach (WorkPlaceDifference diff in Model.Differences.WorkPlaces) {

            
            #line default
            #line hidden
WriteLiteral("\t\t<div class=\"difference\">\r\n\t\t\t<span class=\"field\">");


            
            #line 42 "..\..\Views\Synchronize\Index.cshtml"
                  Write(diff.PersonNickname);

            
            #line default
            #line hidden
WriteLiteral("</span> \r\n\t\t\t<span class=\"source\">");


            
            #line 43 "..\..\Views\Synchronize\Index.cshtml"
                   Write(diff.Position);

            
            #line default
            #line hidden
WriteLiteral("</span><br />\r\n\t\t</div>\r\n");


            
            #line 45 "..\..\Views\Synchronize\Index.cshtml"
		}

            
            #line default
            #line hidden
WriteLiteral("\t</div>\r\n");


            
            #line 47 "..\..\Views\Synchronize\Index.cshtml"
	}
            
            #line default
            #line hidden
            
            #line 47 "..\..\Views\Synchronize\Index.cshtml"
  
}

            
            #line default
            #line hidden
WriteLiteral("\r\n");


DefineSection("Header", () => {

WriteLiteral(@"
	<div id=""toolBar"" class=""ui-widget-header ui-corner-all kui-toolbar""
		style=""padding: 2px;""
		data-bind=""visible: PageLoaded"">
		<table cellspacing=""0"" cellpadding=""0"" border=""0"" style=""margin-top: -2px;"" width=""100%"">
		<tr valign=""middle"">
			<td >&nbsp;
				<button type=""button"" data-bind="" 
						click: function() { ViewModel.Model.Confirmed(false); $('#SubmitButton').click(); },
						jqueryui: { widget: 'button', options: { icons: { primary: 'ui-icon-transferthick-e-w' } } }
					"">");


            
            #line 60 "..\..\Views\Synchronize\Index.cshtml"
   Write(global::Resources.Resources.GUI_Button_Synchronize);

            
            #line default
            #line hidden
WriteLiteral("</button>\r\n\t\t\t\t<button type=\"button\" id=\"btnCancel\" data-bind=\" \r\n\t\t\t\t\t\tclick: cl" +
"oseDialog, \r\n\t\t\t\t\t\tjqueryui: { widget: \'button\', options: { icons: { primary: \'u" +
"i-icon-cancel\' } } }\r\n\t\t\t\t\t\">");


            
            #line 64 "..\..\Views\Synchronize\Index.cshtml"
   Write(global::Resources.Resources.GUI_Button_Cancel);

            
            #line default
            #line hidden
WriteLiteral("</button>\r\n\t\t\t</td>\r\n\t\t</tr>\r\n\t\t</table>\r\n\t</div>\r\n");


});


        }
    }
}
#pragma warning restore 1591
