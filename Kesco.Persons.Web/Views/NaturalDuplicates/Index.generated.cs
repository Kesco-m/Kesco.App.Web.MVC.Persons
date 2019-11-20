﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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
    
    #line 4 "..\..\Views\NaturalDuplicates\Index.cshtml"
    using Kesco.Persons.Web;
    
    #line default
    #line hidden
    
    #line 5 "..\..\Views\NaturalDuplicates\Index.cshtml"
    using Kesco.Persons.Web.Localization;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Views\NaturalDuplicates\Index.cshtml"
    using Kesco.Web.Mvc;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Views\NaturalDuplicates\Index.cshtml"
    using Kesco.Web.Mvc.UI;
    
    #line default
    #line hidden
    using MvcJqGrid;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/NaturalDuplicates/Index.cshtml")]
    public partial class _Views_NaturalDuplicates_Index_cshtml : Kesco.Persons.Web.SiteViewPage<Kesco.Persons.Web.Models.NaturalDuplicates.ViewModel>
    {
        public _Views_NaturalDuplicates_Index_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 6 "..\..\Views\NaturalDuplicates\Index.cshtml"
  
	Model
		.GetScriptCapabilities()
			.DisableGridScript()
			.DisableTreeScript();
	ViewBag.Title = Model.PageTitle;
	Layout = "~/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<div");

WriteLiteral(" id=\"formContainer\"");

WriteLiteral(" style=\"overflow: auto;\"");

WriteLiteral(" class=\"ui-widget\"");

WriteLiteral(">\r\n");

            
            #line 16 "..\..\Views\NaturalDuplicates\Index.cshtml"
	
            
            #line default
            #line hidden
            
            #line 16 "..\..\Views\NaturalDuplicates\Index.cshtml"
    Html.EnableClientValidation(); 
            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

            
            #line 18 "..\..\Views\NaturalDuplicates\Index.cshtml"
	
            
            #line default
            #line hidden
            
            #line 18 "..\..\Views\NaturalDuplicates\Index.cshtml"
  using (Html.BeginForm("PageForm", "Person", FormMethod.Post, new { id = "PageForm", name = "PageForm", onsubmit = "return false;" })) {
		
            
            #line default
            #line hidden
            
            #line 19 "..\..\Views\NaturalDuplicates\Index.cshtml"
Write(Html.ValidationSummary(true));

            
            #line default
            #line hidden
            
            #line 19 "..\..\Views\NaturalDuplicates\Index.cshtml"
                               

            
            #line default
            #line hidden
WriteLiteral("\t<div");

WriteLiteral(" id=\"DuplicatesDialog_List\"");

WriteLiteral(" style=\"padding: 10px;\"");

WriteLiteral(">\r\n\t\t<div");

WriteLiteral(" class=\"ui-corner-all ui-state-highlight\"");

WriteLiteral(" style=\"margin: 0 0 10px; padding: 10px;\"");

WriteLiteral(">\r\n");

WriteLiteral("\t\t");

            
            #line 22 "..\..\Views\NaturalDuplicates\Index.cshtml"
Write(Html.Raw(global::Resources.Resources.Kesco_Persons_CreateIndividual_DuplicatesDialog_Warning));

            
            #line default
            #line hidden
WriteLiteral("\r\n\t\t<p");

WriteLiteral(" data-bind=\"visible: !AllowSave()\"");

WriteLiteral(">\r\n\t\t\t<span");

WriteLiteral(" class=\"ui-icon ui-icon-alert\"");

WriteLiteral("></span>\r\n");

WriteLiteral("\t\t\t");

            
            #line 25 "..\..\Views\NaturalDuplicates\Index.cshtml"
Write(global::Resources.Resources.Kesco_Persons_CreateIndividual_2113);

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n\t\t</div>\r\n\t\t<table");

WriteLiteral(" style=\"width: 100%\"");

WriteLiteral(">\r\n\t\t<tbody>\r\n");

            
            #line 29 "..\..\Views\NaturalDuplicates\Index.cshtml"
		
            
            #line default
            #line hidden
            
            #line 29 "..\..\Views\NaturalDuplicates\Index.cshtml"
   foreach (var d in Model.Model.Duplicates.OrderBy(d => d.Nickname)) {

            
            #line default
            #line hidden
WriteLiteral("\t\t<tr>\r\n\t\t\t<td");

WriteLiteral(" colspan=\"2\"");

WriteLiteral(" style=\"border-bottom: 1px solid\"");

WriteLiteral(">&nbsp;</td>\r\n\t\t</tr>\r\n");

WriteLiteral("\t\t<tr");

WriteLiteral(" valign=\"top\"");

WriteLiteral(">\r\n\t\t\t<td>\r\n\t\t\t<b>");

            
            #line 35 "..\..\Views\NaturalDuplicates\Index.cshtml"
 Write(Html.DisplayFor(m => d.PersonID, "PersonStaticLink", new
 {
					 ID = "Persons_" + d.PersonID,
                     personid = d.PersonID,
					 UseViewModelBinding = false,
					 Actual = true,
					 TooltipPositionMy = "middle left",
					 TooltipPositionAt = "middle right",
					 InitialLabel = d.Nickname
				 }));

            
            #line default
            #line hidden
WriteLiteral("</b>\r\n\t\t\t\t<table");

WriteLiteral(" style=\"width: 100%\"");

WriteLiteral(" cellspacing=\"0\"");

WriteLiteral(">\r\n\t\t\t\t<tbody>\r\n");

            
            #line 47 "..\..\Views\NaturalDuplicates\Index.cshtml"
				
            
            #line default
            #line hidden
            
            #line 47 "..\..\Views\NaturalDuplicates\Index.cshtml"
     foreach (var issue in d.Issues) {

            
            #line default
            #line hidden
WriteLiteral("\t\t\t\t\t<tr");

WriteLiteral(" class=\"hoverable\"");

WriteLiteral(">\r\n\t\t\t\t\t\t<td");

WriteLiteral(" width=\"20px\"");

WriteLiteral(" >&nbsp;</td>\r\n\t\t\t\t\t\t<td");

WriteLiteral(" width=\"160px\"");

WriteLiteral(" nowrap=\"nowrap\"");

WriteLiteral(">");

            
            #line 50 "..\..\Views\NaturalDuplicates\Index.cshtml"
                                    Write(issue.Field);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n\t\t\t\t\t\t<td");

WriteLiteral(" width=\"220px\"");

WriteLiteral(">");

            
            #line 51 "..\..\Views\NaturalDuplicates\Index.cshtml"
                    Write(issue.Value);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n\t\t\t\t\t\t<td><span");

WriteAttribute("style", Tuple.Create(" style=\"", 1940), Tuple.Create("\"", 1987)
, Tuple.Create(Tuple.Create("", 1948), Tuple.Create("color:", 1948), true)
            
            #line 52 "..\..\Views\NaturalDuplicates\Index.cshtml"
, Tuple.Create(Tuple.Create(" ", 1954), Tuple.Create<System.Object, System.Int32>((issue.R < 1)?"green":"black"
            
            #line default
            #line hidden
, 1955), false)
);

WriteLiteral("\r\n\t\t\t\t\t\t\t>");

            
            #line 53 "..\..\Views\NaturalDuplicates\Index.cshtml"
    Write(issue.Equality);

            
            #line default
            #line hidden
WriteLiteral("</span></td>\r\n\t\t\t\t\t</tr>\r\n");

            
            #line 55 "..\..\Views\NaturalDuplicates\Index.cshtml"
				}

            
            #line default
            #line hidden
WriteLiteral("\t\t\t\t</tbody>\r\n\t\t\t\t</table>\r\n\t\t\t</td>\r\n\t\t\t<td");

WriteLiteral(" style=\"width: 100px\"");

WriteLiteral(">\r\n\t\t\t\t<button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" data-bind=\"\r\n\t\t\t\t\tjqueryui: { widget: \'button\', options: { icon: { primary: \'ui-" +
"icon-flag\' } } }\r\n\t\t\t\t\"");

WriteAttribute("onclick", Tuple.Create("\r\n\t\t\t\tonclick=\'", 2245), Tuple.Create("\'", 2340)
, Tuple.Create(Tuple.Create("", 2260), Tuple.Create("window.ViewModel.itLooksLike(", 2260), true)
            
            #line 63 "..\..\Views\NaturalDuplicates\Index.cshtml"
, Tuple.Create(Tuple.Create("", 2289), Tuple.Create<System.Object, System.Int32>(Html.Raw(Kesco.Web.Mvc.Json.Serialize(d, true))
            
            #line default
            #line hidden
, 2289), false)
, Tuple.Create(Tuple.Create("", 2339), Tuple.Create(")", 2339), true)
);

WriteLiteral(";\r\n\t\t\t\t>");

            
            #line 64 "..\..\Views\NaturalDuplicates\Index.cshtml"
 Write(global::Resources.Resources.Kesco_Persons_CreateIndividual_2108);

            
            #line default
            #line hidden
WriteLiteral("</button>\r\n\t\t\t</td>\r\n\t\t</tr>\r\n");

            
            #line 67 "..\..\Views\NaturalDuplicates\Index.cshtml"
		}

            
            #line default
            #line hidden
WriteLiteral("\t\t</tbody>\r\n\t\t</table>\r\n\t</div>\r\n");

            
            #line 71 "..\..\Views\NaturalDuplicates\Index.cshtml"

	}

            
            #line default
            #line hidden
WriteLiteral("\r\n</div>\r\n\r\n");

DefineSection("Header", () => {

WriteLiteral("\r\n\t<div");

WriteLiteral(" id=\"toolBar\"");

WriteLiteral(" class=\"ui-widget-header ui-corner-all kui-toolbar\"");

WriteLiteral("\r\n\t\tstyle=\"padding: 2px;\"");

WriteLiteral("\r\n\t\tdata-bind=\"visible: PageLoaded\"");

WriteLiteral(">\r\n\t\t<table");

WriteLiteral(" cellspacing=\"0\"");

WriteLiteral(" cellpadding=\"0\"");

WriteLiteral(" border=\"0\"");

WriteLiteral(" style=\"margin-top: -2px;\"");

WriteLiteral(" width=\"100%\"");

WriteLiteral(">\r\n\t\t<tr");

WriteLiteral(" valign=\"middle\"");

WriteLiteral(">\r\n\t\t\t<td >&nbsp;\r\n\t\t\t\t<button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" data-bind=\" \r\n\t\t\t\t\t\tclick: closeDialog,\r\n\t\t\t\t\t\tjqueryui: { widget: \'button\', opt" +
"ions: { icons: { primary: \'ui-icon-disk\' } } }\r\n\t\t\t\t\t\"");

WriteLiteral(">");

            
            #line 86 "..\..\Views\NaturalDuplicates\Index.cshtml"
   Write(global::Resources.Resources.Kesco_Persons_CreateIndividual_DuplicatesDialog_ContinueEditingButton);

            
            #line default
            #line hidden
WriteLiteral("</button>\r\n\t\t\t\t<button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" data-bind=\"\r\n\t\t\t\t\t\tvisible: AllowSave, \r\n\t\t\t\t\t\tclick: saveIt, \r\n\t\t\t\t\t\tjqueryui: " +
"{ widget: \'button\', options: { icons: { primary: \'ui-icon-cancel\' } } }\r\n\t\t\t\t\t\"");

WriteLiteral("\r\n\t\t\t\t\t>");

            
            #line 92 "..\..\Views\NaturalDuplicates\Index.cshtml"
  Write(global::Resources.Resources.Kesco_Persons_CreateIndividual_DuplicatesDialog_ConfirmCreatingButton);

            
            #line default
            #line hidden
WriteLiteral("</button>\r\n\r\n\t\t\t</td>\r\n\t\t\t<td");

WriteLiteral(" nowrap=\"nowrap\"");

WriteLiteral(" style=\"width: 30px;\"");

WriteLiteral(">\r\n\t\t\t\t<button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" style=\"\"");

WriteLiteral(" data-bind=\"\r\n\t\t\t\t\t\t\tjqueryui: { widget: \'button\', options: { text: false, icons:" +
" { primary: \'ui-icon-help\' }}}, \r\n\t\t\t\t\t\t\tclick: showHelp\r\n\t\t\t\t\t\t\"");

WriteLiteral("\r\n\t\t\t\t\t>");

            
            #line 100 "..\..\Views\NaturalDuplicates\Index.cshtml"
  Write(Kesco.Web.Mvc.UI.Controls.Localization.Resources.GUI_Menu_Help);

            
            #line default
            #line hidden
WriteLiteral("</button>\r\n\t\t\t</td>\r\n\t\t</tr>\r\n\t\t</table>\r\n\t</div>\r\n");

});

WriteLiteral("\r\n\r\n");

DefineSection("Footer", () => {

WriteLiteral("\r\n");

            
            #line 109 "..\..\Views\NaturalDuplicates\Index.cshtml"
Write(Html.CommonScriptCode("Natural_Footer",
item => new System.Web.WebPages.HelperResult(__razor_template_writer => {

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, @"<script>
	$(document).ready(function () {
		$(""#PageForm"").submit(function () {
			// this - DOM element - form
			var $form = $(this);
			return false;
		});


		window.ViewModel.saveIt = function () {
			window.ViewModel.Model.Confirmed(true);

			var dialogResult = [];
			dialogResult.push({
				value: window.ViewModel.Model.PersonID(),
				label: ""Confirmed""
			});
			dialogResult = JSON.stringify(dialogResult);
			closeDialogAndReturnValue(dialogResult);
		};

		window.ViewModel.itLooksLike = function(duplicate) {
			//alert(this.PersonID);
			//window.ViewModel.Model.PersonID(duplicate.PersonID);
			//window.ViewModel.Model.Confirmed(false);

			var dialogResult = [];
			dialogResult.push({
				value: duplicate.PersonID,
				label: duplicate.Nickname
			});
			dialogResult = JSON.stringify(dialogResult);
			closeDialogAndReturnValue(dialogResult);
		};

		$(window).resize(function () {
			$(""#formContainer"").height($(""#dialogContentPane"").height());
			$(""#formContainer"").width($(""#dialogContentPane"").outerWidth());
		});

	});

</script>");

            
            #line 152 "..\..\Views\NaturalDuplicates\Index.cshtml"
       })));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

        }
    }
}
#pragma warning restore 1591