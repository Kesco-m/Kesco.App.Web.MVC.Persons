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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.4.1.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/DisplayTemplates/YahooIM.cshtml")]
    public class YahooIM : Kesco.Web.Mvc.SharedViews.SharedViewPage<string>
    {
        public YahooIM()
        {
        }
        public override void Execute()
        {


            
            #line 2 "..\..\Views\Shared\DisplayTemplates\YahooIM.cshtml"
  
	ModelMetadata metadata = ViewData.ModelMetadata;
	string contactIcon = (ViewData["contactIcon"] ?? String.Empty).ToString();
	string phoneType = (ViewData["PhoneType"] ??
			(metadata.AdditionalValues.ContainsKey("PhoneType") ? metadata.AdditionalValues["PhoneType"] : "")).ToString();


            
            #line default
            #line hidden

            
            #line 8 "..\..\Views\Shared\DisplayTemplates\YahooIM.cshtml"
 if (String.IsNullOrEmpty(Model)) {

            
            #line default
            #line hidden
WriteLiteral("\t");

WriteLiteral(" ");


            
            #line 9 "..\..\Views\Shared\DisplayTemplates\YahooIM.cshtml"
Write(ViewData.ModelMetadata.NullDisplayText);

            
            #line default
            #line hidden
WriteLiteral("\r\n");


            
            #line 10 "..\..\Views\Shared\DisplayTemplates\YahooIM.cshtml"
} else {
	if (!String.IsNullOrEmpty(contactIcon)) {

            
            #line default
            #line hidden
WriteLiteral("\t");

WriteLiteral(" <span style=\"position: relative; top: 0px; left: 0px; padding-left: 20px;  line-" +
"height: 20px;\"\r\n");



WriteLiteral("\t");

WriteLiteral("\t><img src=\"");


            
            #line 13 "..\..\Views\Shared\DisplayTemplates\YahooIM.cshtml"
           Write(WebAssetImage(contactIcon));

            
            #line default
            #line hidden
WriteLiteral("\" style=\"position: absolute; top: -2px; left: 1px; cursor: pointer;\" \r\n");



WriteLiteral("\t");

WriteLiteral("\t\ttitle=\"");


            
            #line 14 "..\..\Views\Shared\DisplayTemplates\YahooIM.cshtml"
        Write(Kesco.Web.Mvc.SharedViews.Localization.Resources.ContactControl_CommunicateWithContact);

            
            #line default
            #line hidden
WriteLiteral("\" \r\n");



WriteLiteral("\t");

WriteLiteral("\t\talt=\"");


            
            #line 15 "..\..\Views\Shared\DisplayTemplates\YahooIM.cshtml"
      Write(Kesco.Web.Mvc.SharedViews.Localization.Resources.ContactControl_CommunicateWithContact);

            
            #line default
            #line hidden
WriteLiteral("\" \r\n");



WriteLiteral("\t");

WriteLiteral("\t\tonclick=\"$(this).next().get(0).click()\" /> \r\n");


            
            #line 17 "..\..\Views\Shared\DisplayTemplates\YahooIM.cshtml"
	}

            
            #line default
            #line hidden
WriteLiteral("\t");

WriteLiteral("\t<a href=\"ymgr:sendim?");


            
            #line 18 "..\..\Views\Shared\DisplayTemplates\YahooIM.cshtml"
                     Write(@Model);

            
            #line default
            #line hidden
WriteLiteral("\" \r\n");



WriteLiteral("\t");

WriteLiteral("\t\ttitle=\"");


            
            #line 19 "..\..\Views\Shared\DisplayTemplates\YahooIM.cshtml"
        Write(Kesco.Web.Mvc.SharedViews.Localization.Resources.ContactControl_CommunicateWithContact);

            
            #line default
            #line hidden
WriteLiteral("\" \r\n");



WriteLiteral("\t");

WriteLiteral("\t\talt=\"");


            
            #line 20 "..\..\Views\Shared\DisplayTemplates\YahooIM.cshtml"
      Write(Kesco.Web.Mvc.SharedViews.Localization.Resources.ContactControl_CommunicateWithContact);

            
            #line default
            #line hidden
WriteLiteral("\" \r\n");



WriteLiteral("\t");

WriteLiteral("\t>");


            
            #line 21 "..\..\Views\Shared\DisplayTemplates\YahooIM.cshtml"
Write(Model);

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n");


            
            #line 22 "..\..\Views\Shared\DisplayTemplates\YahooIM.cshtml"
	if (!String.IsNullOrEmpty(contactIcon)) {

            
            #line default
            #line hidden
WriteLiteral("\t");

WriteLiteral(" </span>\r\n");


            
            #line 24 "..\..\Views\Shared\DisplayTemplates\YahooIM.cshtml"
	}
}

            
            #line default
            #line hidden

        }
    }
}
#pragma warning restore 1591