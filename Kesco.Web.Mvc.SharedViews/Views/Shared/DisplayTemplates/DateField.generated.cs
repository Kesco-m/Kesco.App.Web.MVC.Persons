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

namespace Kesco.Web.Mvc.SharedViews.Views.Shared.DisplayTemplates
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
    
    #line 2 "..\..\Views\Shared\DisplayTemplates\DateField.cshtml"
    using Kesco.Web.Mvc.UI;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.4.1.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/DisplayTemplates/DateField.cshtml")]
    public class DateField : Kesco.Web.Mvc.SharedViews.SharedViewPage<DateTime?>
    {
        public DateField()
        {
        }
        public override void Execute()
        {


WriteLiteral("<span data-bind=\"text: Globalize.format(");


            
            #line 3 "..\..\Views\Shared\DisplayTemplates\DateField.cshtml"
                                    Write(ViewData.TemplateInfo.HtmlFieldPrefix);

            
            #line default
            #line hidden
WriteLiteral("(), \'d\', \'kesco\')\"></span>");


        }
    }
}
#pragma warning restore 1591
