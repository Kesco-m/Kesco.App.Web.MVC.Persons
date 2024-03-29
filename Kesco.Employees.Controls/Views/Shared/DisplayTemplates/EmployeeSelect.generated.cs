﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
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
    
    #line 2 "..\..\Views\Shared\DisplayTemplates\EmployeeSelect.cshtml"
    using Kesco;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Views\Shared\DisplayTemplates\EmployeeSelect.cshtml"
    using Kesco.Employees.Controls;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Views\Shared\DisplayTemplates\EmployeeSelect.cshtml"
    using Kesco.Web.Mvc;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/DisplayTemplates/EmployeeSelect.cshtml")]
    public partial class _Views_Shared_DisplayTemplates_EmployeeSelect_cshtml_ : Kesco.Web.Mvc.SharedViews.Views.Shared.DisplayTemplates.SelectBox
    {
        public _Views_Shared_DisplayTemplates_EmployeeSelect_cshtml_()
        {
        }
        public override void Execute()
        {
            
            #line 5 "..\..\Views\Shared\DisplayTemplates\EmployeeSelect.cshtml"
  
	string id = ViewData.TemplateInfo.GetFullHtmlFieldId("");
	string name = ViewData.TemplateInfo.HtmlFieldPrefix;
	ModelMetadata metaData = ViewData.ModelMetadata;

	Dictionary<string, object> htmlAttributes = new Dictionary<string, object>();
	if (ViewBag.HtmlAttributes != null) {
		if (ViewBag.HtmlAttributes is IDictionary<string, object>) {
			htmlAttributes.Merge((IDictionary<string, object>) ViewBag.HtmlAttributes);
		} else {
			htmlAttributes.Merge((object) ViewBag.HtmlAttributes);
		}
	}
	
	htmlAttributes.PrependInValue("class", " ", "employee");
	
	ViewBag.HtmlAttributes = htmlAttributes;
		
	
            
            #line default
            #line hidden
            
            #line 23 "..\..\Views\Shared\DisplayTemplates\EmployeeSelect.cshtml"
                                               
	base.Execute();


            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 27 "..\..\Views\Shared\DisplayTemplates\EmployeeSelect.cshtml"
  
	
	Html.RegisterCommonScriptCode("EmployeeSelectDisplayControl", () => @"
		!(function() {{ var env = window.Env || {{}}; window.Env = env; env.URI_user_info = env.URI_user_info || '{0}'; }})();

		function EmployeeSelectDisplayControl_TooltipSource(ev) {{
			var uri = Env.URI_user_info;
			var item = $(this).dynamicLink('getValue');
			if (item && item.value)
				uri = uri.replace('/0', '/' + item.value);
			return uri;
		}}

		$(document).on('click', 'a.employee', function(ev) {{
				var value = $(this).dynamicLink('getValue');
				window.ViewModel.showUser(value.value);
			}});

		$('a.employee')
			.one('mouseenter', function(ev) {{
				var $this = $(this);
				$this.initToolTip(EmployeeSelectDisplayControl_TooltipSource, $(document.body));
				setTimeout(function() {{ $this.mouseenter(); }}, 10);
			}});

	".FormatWith(Kesco.Employees.Controls.Configuration.AppSettings.URI_user_info));
	

            
            #line default
            #line hidden
WriteLiteral("\t\t  \r\n");

        }
    }
}
#pragma warning restore 1591
