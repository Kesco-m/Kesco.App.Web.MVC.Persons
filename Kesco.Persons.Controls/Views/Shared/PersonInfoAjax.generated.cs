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
    
    #line 2 "..\..\Views\Shared\PersonInfoAjax.cshtml"
    using Kesco.ObjectModel;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Views\Shared\PersonInfoAjax.cshtml"
    using Kesco.Persons.Controls;
    
    #line default
    #line hidden
    
    #line 5 "..\..\Views\Shared\PersonInfoAjax.cshtml"
    using Kesco.Persons.Controls.Models.PersonInfo;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Views\Shared\PersonInfoAjax.cshtml"
    using Kesco.Persons.ObjectModel;
    
    #line default
    #line hidden
    
    #line 6 "..\..\Views\Shared\PersonInfoAjax.cshtml"
    using Kesco.Web.Mvc;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/PersonInfoAjax.cshtml")]
    public partial class _Views_Shared_PersonInfoAjax_cshtml : Kesco.Persons.Controls.SiteViewPage<Kesco.Persons.Controls.Models.PersonInfo.ViewModel>
    {
        public _Views_Shared_PersonInfoAjax_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 7 "..\..\Views\Shared\PersonInfoAjax.cshtml"
  
	string id = ViewData.TemplateInfo.GetFullHtmlFieldId("");
	string name = ViewData.TemplateInfo.HtmlFieldPrefix;

            
            #line default
            #line hidden
WriteLiteral("\r\n<div");

WriteLiteral(" class=\"\"");

WriteLiteral(" style=\"margin-bottom: 5px;\"");

WriteLiteral(" >\r\n");

            
            #line 12 "..\..\Views\Shared\PersonInfoAjax.cshtml"
 if (Model == null) {

            
            #line default
            #line hidden
WriteLiteral("\t");

WriteLiteral(" ");

            
            #line 13 "..\..\Views\Shared\PersonInfoAjax.cshtml"
Write(ViewData.ModelMetadata.NullDisplayText);

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 14 "..\..\Views\Shared\PersonInfoAjax.cshtml"
} else {
	if (Model.Person == null) {

            
            #line default
            #line hidden
WriteLiteral("\t\t");

WriteLiteral(" - #");

            
            #line 16 "..\..\Views\Shared\PersonInfoAjax.cshtml"
    Write(Model.PersonID);

            
            #line default
            #line hidden
WriteLiteral(" -\r\n");

            
            #line 17 "..\..\Views\Shared\PersonInfoAjax.cshtml"
	} else {
		if(Model.Employee != null) {
		}
		if (Model.AllContacts.Count > 0) {
			bool drawBorder = false;

            
            #line default
            #line hidden
WriteLiteral("\t\t\t");

WriteLiteral(" <div style=\"margin-top: 10px; clear: both; \">\r\n");

            
            #line 23 "..\..\Views\Shared\PersonInfoAjax.cshtml"
				foreach(var c in Model.AllContacts) {

            
            #line default
            #line hidden
WriteLiteral("\t\t\t\t");

WriteLiteral(" <div style=\"min-height: 18x; padding-top: 2px; ");

            
            #line 24 "..\..\Views\Shared\PersonInfoAjax.cshtml"
                                                  Write(drawBorder ? "border-top: 1px solid silver !important;" : "");

            
            #line default
            #line hidden
WriteLiteral("\">\r\n");

            
            #line 25 "..\..\Views\Shared\PersonInfoAjax.cshtml"
					
            
            #line default
            #line hidden
            
            #line 25 "..\..\Views\Shared\PersonInfoAjax.cshtml"
Write(Html.DisplayFor(m => c.Contact, GetControlNameByContactType(c.ContactType), new {
						contactName = c.ContactName,
						contactIcon = GetControlIconByContactType(c.ContactType),
						PhoneType = c.Type,
						PhoneNumber = c.PhoneNumber,
						CssClass = (c.InDictionary ? "" : "clGrayTrans")
					}));

            
            #line default
            #line hidden
            
            #line 31 "..\..\Views\Shared\PersonInfoAjax.cshtml"
       
					if (!String.IsNullOrEmpty(c.Note)) {

            
            #line default
            #line hidden
WriteLiteral("\t\t\t\t\t\t");

WriteLiteral(" <small class=\"clGray\">(");

            
            #line 33 "..\..\Views\Shared\PersonInfoAjax.cshtml"
                            Write(c.Note);

            
            #line default
            #line hidden
WriteLiteral(")</small>\r\n");

            
            #line 34 "..\..\Views\Shared\PersonInfoAjax.cshtml"
					}

            
            #line default
            #line hidden
WriteLiteral("\t\t\t\t");

WriteLiteral(" </div>\r\n");

            
            #line 36 "..\..\Views\Shared\PersonInfoAjax.cshtml"
					drawBorder = true;
				}
				
            
            #line default
            #line hidden
            
            #line 38 "..\..\Views\Shared\PersonInfoAjax.cshtml"
                                            

            
            #line default
            #line hidden
WriteLiteral("\t\t\t\t");

WriteLiteral(" <div style=\"width: 150px;\"></div>\r\n");

WriteLiteral("\t\t\t");

WriteLiteral(" </div>\r\n");

            
            #line 41 "..\..\Views\Shared\PersonInfoAjax.cshtml"
		} else {

            
            #line default
            #line hidden
WriteLiteral("\t\t\t");

WriteLiteral(" <div>");

            
            #line 42 "..\..\Views\Shared\PersonInfoAjax.cshtml"
       Write(Kesco.Web.Mvc.SharedViews.Localization.Resources.PersonContacts_NoContacts);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n");

            
            #line 43 "..\..\Views\Shared\PersonInfoAjax.cshtml"
		}
	}
}

            
            #line default
            #line hidden
WriteLiteral("</div>");

        }
    }
}
#pragma warning restore 1591