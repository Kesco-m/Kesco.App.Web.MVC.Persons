﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.225
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kesco.Persons.Web.Views.Shared.EditorTemplates
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
    
    #line 3 "..\..\Views\Shared\EditorTemplates\PersonLinkParameter.cshtml"
    using Kesco.Persons.Web;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Views\Shared\EditorTemplates\PersonLinkParameter.cshtml"
    using Kesco.Web.Mvc;
    
    #line default
    #line hidden
    using MvcJqGrid;
    
    #line 2 "..\..\Views\Shared\EditorTemplates\PersonLinkParameter.cshtml"
    using Resources;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/EditorTemplates/PersonLinkParameter.cshtml")]
    public partial class _PersonLinkParameter : Kesco.Persons.Web.SiteViewPage<int>
    {
        public _PersonLinkParameter()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

            
            #line 6 "..\..\Views\Shared\EditorTemplates\PersonLinkParameter.cshtml"
   
	string id = ViewData.TemplateInfo.GetFullHtmlFieldId("");
	string name = ViewData.TemplateInfo.HtmlFieldPrefix;

            
            #line default
            #line hidden
WriteLiteral("\r\n<style");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(">\r\n\t.linkParams td { padding: 0px; }\r\n</style>\r\n\r\n<table");

WriteLiteral(" class=\"linkParams\"");

WriteLiteral(" cellpadding=\"0\"");

WriteLiteral(" cellspacing=\"0\"");

WriteLiteral(" border=\"0\"");

WriteLiteral(">\r\n<tr>\r\n\t<td><input");

WriteLiteral(" type=\"radio\"");

WriteAttribute("id", Tuple.Create(" id=\"", 411), Tuple.Create("\"", 424)
            
            #line 16 "..\..\Views\Shared\EditorTemplates\PersonLinkParameter.cshtml"
, Tuple.Create(Tuple.Create("", 416), Tuple.Create<System.Object, System.Int32>(id
            
            #line default
            #line hidden
, 416), false)
, Tuple.Create(Tuple.Create("", 421), Tuple.Create("__0", 421), true)
);

WriteAttribute("name", Tuple.Create(" name=\"", 425), Tuple.Create("\"", 437)
            
            #line 16 "..\..\Views\Shared\EditorTemplates\PersonLinkParameter.cshtml"
, Tuple.Create(Tuple.Create("", 432), Tuple.Create<System.Object, System.Int32>(name
            
            #line default
            #line hidden
, 432), false)
);

WriteLiteral(" data-bind=\"checkedValue:0, checked: ");

            
            #line 16 "..\..\Views\Shared\EditorTemplates\PersonLinkParameter.cshtml"
                                                                                   Write(name);

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral("/></td>\r\n\t<td>");

            
            #line 17 "..\..\Views\Shared\EditorTemplates\PersonLinkParameter.cshtml"
Write(Resources.Persons_Link_Parameter0);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n</tr>\r\n<tr>\r\n\t<td><input");

WriteLiteral(" type=\"radio\"");

WriteAttribute("id", Tuple.Create(" id=\"", 573), Tuple.Create("\"", 586)
            
            #line 20 "..\..\Views\Shared\EditorTemplates\PersonLinkParameter.cshtml"
, Tuple.Create(Tuple.Create("", 578), Tuple.Create<System.Object, System.Int32>(id
            
            #line default
            #line hidden
, 578), false)
, Tuple.Create(Tuple.Create("", 583), Tuple.Create("__1", 583), true)
);

WriteAttribute("name", Tuple.Create(" name=\"", 587), Tuple.Create("\"", 599)
            
            #line 20 "..\..\Views\Shared\EditorTemplates\PersonLinkParameter.cshtml"
, Tuple.Create(Tuple.Create("", 594), Tuple.Create<System.Object, System.Int32>(name
            
            #line default
            #line hidden
, 594), false)
);

WriteLiteral(" data-bind=\"checkedValue:1, checked: ");

            
            #line 20 "..\..\Views\Shared\EditorTemplates\PersonLinkParameter.cshtml"
                                                                                   Write(name);

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral("/></td>\r\n\t<td>");

            
            #line 21 "..\..\Views\Shared\EditorTemplates\PersonLinkParameter.cshtml"
Write(Resources.Persons_Link_Parameter1);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n</tr>\r\n<tr>\r\n\t<td><input");

WriteLiteral(" type=\"radio\"");

WriteAttribute("id", Tuple.Create(" id=\"", 735), Tuple.Create("\"", 748)
            
            #line 24 "..\..\Views\Shared\EditorTemplates\PersonLinkParameter.cshtml"
, Tuple.Create(Tuple.Create("", 740), Tuple.Create<System.Object, System.Int32>(id
            
            #line default
            #line hidden
, 740), false)
, Tuple.Create(Tuple.Create("", 745), Tuple.Create("__2", 745), true)
);

WriteAttribute("name", Tuple.Create(" name=\"", 749), Tuple.Create("\"", 761)
            
            #line 24 "..\..\Views\Shared\EditorTemplates\PersonLinkParameter.cshtml"
, Tuple.Create(Tuple.Create("", 756), Tuple.Create<System.Object, System.Int32>(name
            
            #line default
            #line hidden
, 756), false)
);

WriteLiteral(" data-bind=\"checkedValue:2, checked: ");

            
            #line 24 "..\..\Views\Shared\EditorTemplates\PersonLinkParameter.cshtml"
                                                                                   Write(name);

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral("/></td>\r\n\t<td>");

            
            #line 25 "..\..\Views\Shared\EditorTemplates\PersonLinkParameter.cshtml"
Write(Resources.Persons_Link_Parameter2);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n</tr>\r\n</table>\r\n\r\n");

        }
    }
}
#pragma warning restore 1591