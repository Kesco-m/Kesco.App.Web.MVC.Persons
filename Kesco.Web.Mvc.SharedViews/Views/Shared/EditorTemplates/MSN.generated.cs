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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.4.1.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/EditorTemplates/MSN.cshtml")]
    public class MSN : Kesco.Web.Mvc.SharedViews.SharedViewPage<string>
    {
        public MSN()
        {
        }
        public override void Execute()
        {


            
            #line 2 "..\..\Views\Shared\EditorTemplates\MSN.cshtml"
  
	string name = ViewData.TemplateInfo.HtmlFieldPrefix;
	string id = ViewData.TemplateInfo.GetFullHtmlFieldId("");


            
            #line default
            #line hidden
WriteLiteral(@"<div class=""ui-select-box"" style=""position:relative; left: 0; top: 0; width: 200px; z-index: 1;"" >
	<a href=""javascript: void(0)"" 
		class=""ui-button ui-state-default ui-corner-all""
		style=""position: absolute; left: 0px; top: 0px; height: 18px; width: 20px; z-index: 1""
		data-bind=""visible: ");


            
            #line 10 "..\..\Views\Shared\EditorTemplates\MSN.cshtml"
                  Write(name);

            
            #line default
            #line hidden
WriteLiteral("\" \r\n\t\tonclick=\"ViewModel.openMessengerChat($(\'#");


            
            #line 11 "..\..\Views\Shared\EditorTemplates\MSN.cshtml"
                                       Write(id);

            
            #line default
            #line hidden
WriteLiteral("\').val())\"\r\n\t\t><img src=\"");


            
            #line 12 "..\..\Views\Shared\EditorTemplates\MSN.cshtml"
         Write(WebAssetImage("Messenger.gif"));

            
            #line default
            #line hidden
WriteLiteral("\" style=\"border: none;\" /></a>\r\n\t\t<span style=\"width: 100%\" class=\"ui-state-defau" +
"lt ui-widget ui-widget-content ui-corner-all ui-spinner\">\r\n\t\t\t<div style=\"paddin" +
"g-left: 20px;\" >\r\n\t\t\t");


            
            #line 15 "..\..\Views\Shared\EditorTemplates\MSN.cshtml"
Write(Html.KescoTextBoxFor(m => m, new { style = "width: 100%;", data_bind = "decoration: true, value: " + name + ", valueUpdate: 'afterkeydown'" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n\t\t\t</div>\r\n\t\t</span>\r\n</div>\r\n<script type=\"text/javascript\">\r\n\t$(document).rea" +
"dy(function () {\r\n\t\t$(\'#");


            
            #line 21 "..\..\Views\Shared\EditorTemplates\MSN.cshtml"
  Write(id);

            
            #line default
            #line hidden
WriteLiteral(@"').parent().parent().hover(
			function () { $(this).addClass('ui-state-hover'); },
			function () { $(this).removeClass('ui-state-hover'); }
		)
		.focus(function () { $(this).addClass('ui-state-focus'); })
		.blur(function () { $(this).removeClass('ui-state-focus'); });
	});
</script>");


        }
    }
}
#pragma warning restore 1591
