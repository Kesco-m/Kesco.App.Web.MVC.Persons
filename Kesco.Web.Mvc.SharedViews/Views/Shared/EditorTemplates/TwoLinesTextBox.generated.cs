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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/EditorTemplates/TwoLinesTextBox.cshtml")]
    public class TwoLinesTextBox : Kesco.Web.Mvc.SharedViews.SharedViewPage<string>
    {
        public TwoLinesTextBox()
        {
        }
        public override void Execute()
        {


            
            #line 2 "..\..\Views\Shared\EditorTemplates\TwoLinesTextBox.cshtml"
  
	string name = ViewData.TemplateInfo.HtmlFieldPrefix;
	string id = ViewData.TemplateInfo.GetFullHtmlFieldId("");
	ModelMetadata meta = ViewData.ModelMetadata;

	List<string> databindings = new List<string>();
	databindings.Add("decoration: true");
	databindings.Add((meta.IsRequired ? "valueEx: " : "value: ") + name);

	if (ViewData["Databinding"] != null)
		databindings.Add(ViewData["Databinding"].ToString());


            
            #line default
            #line hidden

            
            #line 14 "..\..\Views\Shared\EditorTemplates\TwoLinesTextBox.cshtml"
Write(Html.TextAreaFor(Model => Model, new { style = "width: 99%; height: 48px", rows = 2, data_bind = String.Join(", ", databindings) }));

            
            #line default
            #line hidden
WriteLiteral("\r\n");


        }
    }
}
#pragma warning restore 1591
