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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/EditorTemplates/Phone.cshtml")]
    public class Phone : Kesco.Web.Mvc.SharedViews.SharedViewPage<string>
    {
        public Phone()
        {
        }
        public override void Execute()
        {


            
            #line 2 "..\..\Views\Shared\EditorTemplates\Phone.cshtml"
  
	string name = ViewData.TemplateInfo.HtmlFieldPrefix;
	string id = ViewData.TemplateInfo.GetFullHtmlFieldId("");
	string icon = null; 
	if (ViewData.ModelMetadata.AdditionalValues["PhoneIcon"] != null) {
		icon = ViewData.ModelMetadata.AdditionalValues["PhoneIcon"].ToString();
	} else {
		icon = "phoneStandard.gif";
	}


            
            #line default
            #line hidden
WriteLiteral(@"<div class=""ui-select-box"" style=""position:relative; left: 0; top: 0; width: 200px; z-index: 1;"" >
	<a href=""javascript: void(0);"" 
		class=""ui-button ui-state-default ui-corner-all""
		style=""position: absolute; left: 0px; top: 0px; height: 18px; width: 20px; z-index: 1""
		data-bind=""visible: ");


            
            #line 16 "..\..\Views\Shared\EditorTemplates\Phone.cshtml"
                  Write(name);

            
            #line default
            #line hidden
WriteLiteral(", disabled: true\" \r\n\t\tonclick=\"ViewModel.makeCall($(\'#");


            
            #line 17 "..\..\Views\Shared\EditorTemplates\Phone.cshtml"
                              Write(id);

            
            #line default
            #line hidden
WriteLiteral("\').val())\"\r\n\t\t><img src=\"");


            
            #line 18 "..\..\Views\Shared\EditorTemplates\Phone.cshtml"
         Write(WebAssetImage(icon));

            
            #line default
            #line hidden
WriteLiteral("\" style=\"border: none;\" /></a>\r\n\t<span style=\"width: 100%\" class=\"ui-state-defaul" +
"t ui-widget ui-widget-content ui-corner-all ui-spinner\">\r\n\t\t<div style=\"padding-" +
"left: 20px;\" >\r\n\t");


            
            #line 21 "..\..\Views\Shared\EditorTemplates\Phone.cshtml"
Write(Html.KescoTextBoxFor(m => m, new { @class = "ui-spinner-input", style = "width: 100%", data_bind = "value: " + name + ", valueUpdate: 'afterkeydown'" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n\t\t</div>\r\n\t</span>\r\n</div>\r\n<script type=\"text/javascript\">\r\n\t$(document).ready" +
"(function () {\r\n\t\t$(\'#");


            
            #line 27 "..\..\Views\Shared\EditorTemplates\Phone.cshtml"
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
</script>


");


        }
    }
}
#pragma warning restore 1591