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

namespace Kesco.Web.Mvc.SharedViews.Views.Shared.EditorTemplates
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
    using Kesco.Web.Mvc;
    
    #line 3 "..\..\Views\Shared\EditorTemplates\Sex.cshtml"
    using Kesco.Web.Mvc.SharedViews.Localization;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Views\Shared\EditorTemplates\Sex.cshtml"
    using Kesco.Web.Mvc.UI;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.4.1.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/EditorTemplates/Sex.cshtml")]
    public class Sex : Kesco.Web.Mvc.SharedViews.SharedViewPage<char?>
    {
        public Sex()
        {
        }
        public override void Execute()
        {




            
            #line 4 "..\..\Views\Shared\EditorTemplates\Sex.cshtml"
  
	string id = ViewData.TemplateInfo.GetFullHtmlFieldId("");


            
            #line default
            #line hidden
WriteLiteral("<table>\r\n<tr nowrap=\"nowrap\">\r\n\t<td><input type=\"radio\" name=\"");


            
            #line 9 "..\..\Views\Shared\EditorTemplates\Sex.cshtml"
                           Write(ViewData.TemplateInfo.HtmlFieldPrefix);

            
            #line default
            #line hidden
WriteLiteral("\" id=\"");


            
            #line 9 "..\..\Views\Shared\EditorTemplates\Sex.cshtml"
                                                                         Write(id);

            
            #line default
            #line hidden
WriteLiteral("__male\" value=\"М\" data-bind=\"\r\n\t\t\tchecked: ");


            
            #line 10 "..\..\Views\Shared\EditorTemplates\Sex.cshtml"
        Write(ViewData.TemplateInfo.HtmlFieldPrefix);

            
            #line default
            #line hidden
WriteLiteral("\r\n\t\t\"/></td>\r\n\t<td><label for=\"");


            
            #line 12 "..\..\Views\Shared\EditorTemplates\Sex.cshtml"
             Write(id);

            
            #line default
            #line hidden
WriteLiteral("__male\">");


            
            #line 12 "..\..\Views\Shared\EditorTemplates\Sex.cshtml"
                         Write(Resources.Kesco_Web_Mvc_SharedApp_LBL_20000);

            
            #line default
            #line hidden
WriteLiteral("</label></td>\r\n\t<td><input type=\"radio\" name=\"");


            
            #line 13 "..\..\Views\Shared\EditorTemplates\Sex.cshtml"
                           Write(ViewData.TemplateInfo.HtmlFieldPrefix);

            
            #line default
            #line hidden
WriteLiteral("\" id=\"");


            
            #line 13 "..\..\Views\Shared\EditorTemplates\Sex.cshtml"
                                                                         Write(id);

            
            #line default
            #line hidden
WriteLiteral("__female\" value=\"Ж\" data-bind=\"\r\n\t\t\tchecked: ");


            
            #line 14 "..\..\Views\Shared\EditorTemplates\Sex.cshtml"
        Write(ViewData.TemplateInfo.HtmlFieldPrefix);

            
            #line default
            #line hidden
WriteLiteral("\r\n\t\t\"/></td>\r\n\t<td><label for=\"");


            
            #line 16 "..\..\Views\Shared\EditorTemplates\Sex.cshtml"
             Write(id);

            
            #line default
            #line hidden
WriteLiteral("__female\">");


            
            #line 16 "..\..\Views\Shared\EditorTemplates\Sex.cshtml"
                           Write(Resources.Kesco_Web_Mvc_SharedApp_LBL_20001);

            
            #line default
            #line hidden
WriteLiteral("</label></td>\r\n</tr>\r\n</table>\r\n");


        }
    }
}
#pragma warning restore 1591