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
    
    #line 2 "..\..\Views\Shared\EditorTemplates\TerritorySelect.cshtml"
    using Kesco.Territories.Controls;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Views\Shared\EditorTemplates\TerritorySelect.cshtml"
    using Kesco.Territories.Controls.ComponentModel;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Views\Shared\EditorTemplates\TerritorySelect.cshtml"
    using Kesco.Web.Mvc;
    
    #line default
    #line hidden
    
    #line 5 "..\..\Views\Shared\EditorTemplates\TerritorySelect.cshtml"
    using Kesco.Web.Mvc.UI.ComponentModel.DataAnnotations;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/EditorTemplates/TerritorySelect.cshtml")]
    public partial class _Views_Shared_EditorTemplates_TerritorySelect_cshtml_ : Kesco.Web.Mvc.SharedViews.Views.Shared.EditorTemplates.SelectBox
    {
        public _Views_Shared_EditorTemplates_TerritorySelect_cshtml_()
        {
        }
        public override void Execute()
        {
            
            #line 6 "..\..\Views\Shared\EditorTemplates\TerritorySelect.cshtml"
  
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
	
	htmlAttributes.PrependInValue("class", " ", "territory");
	
	ViewBag.HtmlAttributes = htmlAttributes;
		
	
            
            #line default
            #line hidden
            
            #line 24 "..\..\Views\Shared\EditorTemplates\TerritorySelect.cshtml"
                                               
	base.Execute();

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

        }
    }
}
#pragma warning restore 1591