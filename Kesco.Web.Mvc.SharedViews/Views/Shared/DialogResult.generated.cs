﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.18444
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
    using Kesco.Web.Mvc;
    
    #line 2 "..\..\Views\Shared\DialogResult.cshtml"
    using Kesco.Web.Mvc.Compression;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Views\Shared\DialogResult.cshtml"
    using Kesco.Web.Mvc.Compression.Resource;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/DialogResult.cshtml")]
    public partial class _Views_Shared_DialogResult_cshtml : Kesco.Web.Mvc.SharedViews.SharedViewPage<Kesco.Web.Mvc.DialogResultModel>
    {
        public _Views_Shared_DialogResult_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 4 "..\..\Views\Shared\DialogResult.cshtml"
  
	bool fl = "1".Equals(HttpContext.Current.Request.Form["escaped"]);
	string prefix = "'";
	string suffix = "'";

	if (fl)
	{
		prefix = "unescape('";
		suffix = "')";
	}

            
            #line default
            #line hidden
WriteLiteral("\r\n<!DOCTYPE html>\r\n<html");

WriteLiteral(" xmlns=\"http://www.w3.org/1999/xhtml\"");

WriteLiteral(">\r\n<head");

WriteLiteral(" runat=\"server\"");

WriteLiteral(">\r\n\t<meta");

WriteLiteral(" http-equiv=\"Content-Type\"");

WriteLiteral(" content=\"text/html; charset=utf-8\"");

WriteLiteral("/>\r\n\t<META");

WriteLiteral(" http-equiv=\"CACHE-CONTROL\"");

WriteLiteral(" content=\"NO-CACHE\"");

WriteLiteral(" />\r\n\t<meta");

WriteLiteral(" http-equiv=\"X-UA-Compatible\"");

WriteLiteral(" content=\"IE=edge\"");

WriteLiteral(" />\r\n");

WriteLiteral("\t");

            
            #line 21 "..\..\Views\Shared\DialogResult.cshtml"
Write(Html.Raw(Html.CompositeScriptResource_RenderScriptTag(WebAssetScript("jquery.js"))));

            
            #line default
            #line hidden
WriteLiteral("\r\n\t<title>ReturnResult</title>\r\n</head>\r\n<body>\r\n\t<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">

	    function IsJsonString(str) {
	        try {
	            JSON.parse(str);
	        } catch (e) {
	            return false;
	        }
	        return true;
	    }


	    $(document).ready(function () {
	        var w = window.opener || window.parent.opener,
	            wHost = window.parent || window.self;
	        try {
	            if (w && !w.closed && w.$ && w.$.windowManager) {
	                //alert('");

            
            #line 42 "..\..\Views\Shared\DialogResult.cshtml"
                     Write(Html.Raw(Model.DialogResult.Value));

            
            #line default
            #line hidden
WriteLiteral("\');\r\n\t           \r\n\t                \r\n                    if(IsJsonString(\'");

            
            #line 45 "..\..\Views\Shared\DialogResult.cshtml"
                                 Write(Html.Raw(Model.DialogResult.Value.Replace(@"\", @"\\").Replace(@"""", @"\""").Replace(@"'", @"\'") ));

            
            #line default
            #line hidden
WriteLiteral("\'))\r\n                    {\r\n                        var obj = JSON.parse(\'");

            
            #line 47 "..\..\Views\Shared\DialogResult.cshtml"
                                          Write(Html.Raw(Model.DialogResult.Value.Replace(@"\", @"\\").Replace(@"""", @"\""").Replace(@"'", @"\'") ));

            
            #line default
            #line hidden
WriteLiteral("\');\r\n\t                    var sectionId = obj[0].sectionId;\r\n\t                   " +
" if(!sectionId) {\r\n\t                        w.$.windowManager.closeDialogEx(wHos" +
"t, ");

            
            #line 50 "..\..\Views\Shared\DialogResult.cshtml"
                                                            Write(Html.Raw(prefix + Model.DialogResult.Value.Replace(@"\", @"\\").Replace(@"""", @"\""").Replace(@"'", @"\'") + suffix ));

            
            #line default
            #line hidden
WriteLiteral(@");
	                     } else {
	                        w.blur();
	                        w.focus();
	                        w.refreshSection(sectionId, false, true);
	                    }
                    } else {
                         w.$.windowManager.closeDialogEx(wHost, ");

            
            #line 57 "..\..\Views\Shared\DialogResult.cshtml"
                                                            Write(Html.Raw(prefix + Model.DialogResult.Value.Replace(@"\", @"\\").Replace(@"""", @"\""").Replace(@"'", @"\'") + suffix ));

            
            #line default
            #line hidden
WriteLiteral(@");
                    }
	                
	                
	            }
	        } catch(e) {
	            alert(e.Description);
	        }
	        if (!wHost.closed) {
	                wHost.close();
	        }
	       
	    });

	</script>
</body>
</html>
");

        }
    }
}
#pragma warning restore 1591
