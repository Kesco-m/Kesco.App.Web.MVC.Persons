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

namespace Kesco.Web.Mvc.SharedViews.Views.Shared
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
    
    #line 3 "..\..\Views\Shared\Error.cshtml"
    using Kesco.Web.Mvc.SharedViews.Localization;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.4.1.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/Error.cshtml")]
    public class Error : Kesco.Web.Mvc.SharedViews.SharedViewPage<System.Web.Mvc.HandleErrorInfo>
    {
        public Error()
        {
        }
        public override void Execute()
        {

WriteLiteral("\r\n");


WriteLiteral("\r\n");


            
            #line 5 "..\..\Views\Shared\Error.cshtml"
  
	ViewBag.Title = Resources.Kesco_Web_Mvc_SharedApp_LBL_13112;
	Layout = "~/Views/Shared/_ErrorLayout.cshtml";
	Response.StatusCode = 500;


            
            #line default
            #line hidden
WriteLiteral("\r\n<div class=\"ui-state-error ui-corner-all\" style=\"margin: .7em .7em; padding: 0 " +
".7em;\"> \r\n\t<p><span class=\"ui-icon ui-icon-alert\" style=\"float: left; margin-rig" +
"ht: .3em;\"></span> \r\n    <p>");


            
            #line 13 "..\..\Views\Shared\Error.cshtml"
  Write(Resources.Kesco_Web_Mvc_SharedApp_LBL_13112);

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n\t<p>");


            
            #line 14 "..\..\Views\Shared\Error.cshtml"
Write(Model.Exception.Message);

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n\t<p>&raquo; <a href=\"javascript: void(0);\" onclick=\"$(\'#errorDetails\').togg" +
"le();\">");


            
            #line 15 "..\..\Views\Shared\Error.cshtml"
                                                                             Write(Resources.Kesco_Web_Mvc_SharedApp_LBL_13113);

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n\t</p>\t\r\n</div>\t\r\n<pre id=\"errorDetails\" style=\"font-size: 10px; display:non" +
"e;\"\r\n>");


            
            #line 19 "..\..\Views\Shared\Error.cshtml"
Write(Model.Exception.ToString());

            
            #line default
            #line hidden
WriteLiteral("</pre>\r\n\r\n\r\n");


DefineSection("Header", () => {

WriteLiteral("\r\n\t<div class=\"ui-widget-header\" style=\"text-transform:uppercase; height: 20px; \"" +
"\r\n\t\t>");


            
            #line 24 "..\..\Views\Shared\Error.cshtml"
Write(Resources.Kesco_Web_Mvc_SharedApp_LBL_13112);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n");


});


        }
    }
}
#pragma warning restore 1591