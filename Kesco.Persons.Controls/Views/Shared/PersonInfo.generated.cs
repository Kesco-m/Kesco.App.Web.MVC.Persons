﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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
    
    #line 2 "..\..\Views\Shared\PersonInfo.cshtml"
    using Kesco.ObjectModel;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Views\Shared\PersonInfo.cshtml"
    using Kesco.Persons.Controls;
    
    #line default
    #line hidden
    
    #line 5 "..\..\Views\Shared\PersonInfo.cshtml"
    using Kesco.Persons.Controls.Models.PersonInfo;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Views\Shared\PersonInfo.cshtml"
    using Kesco.Persons.ObjectModel;
    
    #line default
    #line hidden
    
    #line 6 "..\..\Views\Shared\PersonInfo.cshtml"
    using Kesco.Web.Mvc;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/PersonInfo.cshtml")]
    public partial class _Views_Shared_PersonInfo_cshtml : Kesco.Persons.Controls.SiteViewPage<Kesco.Persons.Controls.Models.PersonInfo.ViewModel>
    {
        public _Views_Shared_PersonInfo_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 7 "..\..\Views\Shared\PersonInfo.cshtml"
  
    ViewBag.Title = "Контакты сотрудника";
	Layout = "~/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 11 "..\..\Views\Shared\PersonInfo.cshtml"
  
	string id = ViewData.TemplateInfo.GetFullHtmlFieldId("");
	string name = ViewData.TemplateInfo.HtmlFieldPrefix;

            
            #line default
            #line hidden
WriteLiteral("\r\n<div");

WriteLiteral(" class=\"\"");

WriteLiteral(" style=\"margin-bottom: 5px;\"");

WriteLiteral(" >\r\n");

            
            #line 16 "..\..\Views\Shared\PersonInfo.cshtml"
 if (Model == null) {

            
            #line default
            #line hidden
WriteLiteral("\t");

WriteLiteral(" ");

            
            #line 17 "..\..\Views\Shared\PersonInfo.cshtml"
  Write(ViewData.ModelMetadata.NullDisplayText);

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 18 "..\..\Views\Shared\PersonInfo.cshtml"
} else {
	if (Model.Person == null) {

            
            #line default
            #line hidden
WriteLiteral("\t\t");

WriteLiteral(" - #");

            
            #line 20 "..\..\Views\Shared\PersonInfo.cshtml"
          Write(Model.PersonID);

            
            #line default
            #line hidden
WriteLiteral(" -\r\n");

            
            #line 21 "..\..\Views\Shared\PersonInfo.cshtml"
	} else {
		if (Model.HasLogotypes) {

            
            #line default
            #line hidden
WriteLiteral("\t\t");

WriteLiteral(" <img src=\"");

            
            #line 23 "..\..\Views\Shared\PersonInfo.cshtml"
                 Write(Configuration.AppSettings.URI_person_logo);

            
            #line default
            #line hidden
WriteLiteral("?id=");

            
            #line 23 "..\..\Views\Shared\PersonInfo.cshtml"
                                                                 Write(Model.Person.ID);

            
            #line default
            #line hidden
WriteLiteral("&h=50\" vspace=\"5\" hspace=\"5\" height=\"50px\" />\r\n");

            
            #line 24 "..\..\Views\Shared\PersonInfo.cshtml"
		}
		if(Model.Employee != null) {

            
            #line default
            #line hidden
WriteLiteral("\t\t\t<img");

WriteAttribute("src", Tuple.Create(" src=\"", 872), Tuple.Create("\"", 950)
            
            #line 26 "..\..\Views\Shared\PersonInfo.cshtml"
, Tuple.Create(Tuple.Create("", 878), Tuple.Create<System.Object, System.Int32>(Configuration.AppSettings.URI_user_photo
            
            #line default
            #line hidden
, 878), false)
, Tuple.Create(Tuple.Create("", 921), Tuple.Create("?id=", 921), true)
            
            #line 26 "..\..\Views\Shared\PersonInfo.cshtml"
, Tuple.Create(Tuple.Create("", 925), Tuple.Create<System.Object, System.Int32>(Model.Employee.ID
            
            #line default
            #line hidden
, 925), false)
, Tuple.Create(Tuple.Create("", 945), Tuple.Create("&w=70", 945), true)
);

WriteLiteral(" align=\"left\"");

WriteLiteral(" vspace=\"5\"");

WriteLiteral(" hspace=\"5\"");

WriteLiteral(" width=\"70px\"");

WriteLiteral(" />\r\n");

WriteLiteral("\t\t\t<div");

WriteLiteral(" style=\"padding-left: 5px; margin: 3px 0;\"");

WriteLiteral(">\r\n");

WriteLiteral("\t\t\t\t");

            
            #line 28 "..\..\Views\Shared\PersonInfo.cshtml"
           Write(Html.DisplayFor(m => m.Employee.FullName));

            
            #line default
            #line hidden
WriteLiteral("\r\n\t\t\t</div>\r\n");

WriteLiteral("\t\t\t<div");

WriteLiteral(" style=\"padding-left: 5px; margin: 3px 0;\"");

WriteLiteral(">\r\n");

WriteLiteral("\t\t\t\t");

            
            #line 31 "..\..\Views\Shared\PersonInfo.cshtml"
           Write(Html.DisplayFor(m => m.Employee.FullNameEn));

            
            #line default
            #line hidden
WriteLiteral("\r\n\t\t\t</div>\r\n");

            
            #line 33 "..\..\Views\Shared\PersonInfo.cshtml"
			if (Model.LastPassage != null) {

            
            #line default
            #line hidden
WriteLiteral("\t\t\t<div");

WriteLiteral(" style=\"padding-left: 5px; margin: 3px 0;\"");

WriteLiteral(">\r\n");

WriteLiteral("\t\t\t\t\t");

            
            #line 35 "..\..\Views\Shared\PersonInfo.cshtml"
               Write(Html.DisplayFor(m => m.LastPassage.Point));

            
            #line default
            #line hidden
WriteLiteral(" \r\n");

WriteLiteral("\t\t\t\t\t");

            
            #line 36 "..\..\Views\Shared\PersonInfo.cshtml"
               Write(Model.LastPassage.TimeAt.FromUtcToClient().ToString("F"));

            
            #line default
            #line hidden
WriteLiteral("\r\n\t\t\t</div>\r\n");

            
            #line 38 "..\..\Views\Shared\PersonInfo.cshtml"
			}
		}
		if (Model.AllContacts.Count > 0) {
			bool drawBorder = false;

            
            #line default
            #line hidden
WriteLiteral("\t\t\t");

WriteLiteral(" <div style=\"margin-top: 10px; clear: both; \">\r\n");

            
            #line 43 "..\..\Views\Shared\PersonInfo.cshtml"
				foreach(var c in Model.AllContacts) {

            
            #line default
            #line hidden
WriteLiteral("\t\t\t\t");

WriteLiteral(" <div style=\"min-height: 18x; padding-top: 2px; ");

            
            #line 44 "..\..\Views\Shared\PersonInfo.cshtml"
                                                              Write(drawBorder ? "border-top: 1px solid silver !important;" : "");

            
            #line default
            #line hidden
WriteLiteral("\">\r\n");

            
            #line 45 "..\..\Views\Shared\PersonInfo.cshtml"
					
            
            #line default
            #line hidden
            
            #line 45 "..\..\Views\Shared\PersonInfo.cshtml"
               Write(Html.DisplayFor(m => c.Contact, GetControlNameByContactType(c.ContactType), new {
						contactName = c.ContactName,
						contactIcon = GetControlIconByContactType(c.ContactType),
						PhoneType = c.Type,
						PhoneNumber = c.PhoneNumber,
                        CID = Model.Person.ID,
                        CType = 3,
						CssClass = (c.InDictionary ? "" : "clGrayTrans")
					}));

            
            #line default
            #line hidden
            
            #line 53 "..\..\Views\Shared\PersonInfo.cshtml"
                      
					if (!String.IsNullOrEmpty(c.Note)) {

            
            #line default
            #line hidden
WriteLiteral("\t\t\t\t\t\t");

WriteLiteral(" <small class=\"clGray\">(");

            
            #line 55 "..\..\Views\Shared\PersonInfo.cshtml"
                                              Write(c.Note);

            
            #line default
            #line hidden
WriteLiteral(")</small>\r\n");

            
            #line 56 "..\..\Views\Shared\PersonInfo.cshtml"
					}

            
            #line default
            #line hidden
WriteLiteral("\t\t\t\t");

WriteLiteral(" </div>\r\n");

            
            #line 58 "..\..\Views\Shared\PersonInfo.cshtml"
					drawBorder = true;
				}
				
            
            #line default
            #line hidden
            
            #line 60 "..\..\Views\Shared\PersonInfo.cshtml"
                                                        

            
            #line default
            #line hidden
WriteLiteral("\t\t\t\t");

WriteLiteral(" <div style=\"width: 150px;\"></div>\r\n");

WriteLiteral("\t\t\t");

WriteLiteral(" </div>\r\n");

            
            #line 63 "..\..\Views\Shared\PersonInfo.cshtml"
		} else {

            
            #line default
            #line hidden
WriteLiteral("\t\t\t");

WriteLiteral(" <div>");

            
            #line 64 "..\..\Views\Shared\PersonInfo.cshtml"
                Write(Kesco.Web.Mvc.SharedViews.Localization.Resources.PersonContacts_NoContacts);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n");

            
            #line 65 "..\..\Views\Shared\PersonInfo.cshtml"
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
