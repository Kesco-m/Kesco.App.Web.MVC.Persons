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

namespace Kesco.Web.Mvc.SharedViews.Views.Default
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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Default/Index.cshtml")]
    public class Index : Kesco.Web.Mvc.SharedViews.SharedViewPage<Kesco.Web.Mvc.SharedViews.Models.IndexViewModel>
    {
        public Index()
        {
        }
        public override void Execute()
        {


            
            #line 2 "..\..\Views\Default\Index.cshtml"
  
    ViewBag.Title = "Просмотр документа";
    Layout = "~/Views/Shared/_Layout.cshtml";


            
            #line default
            #line hidden
WriteLiteral("\r\n<div  id=\"");


            
            #line 7 "..\..\Views\Default\Index.cshtml"
      Write(ViewData.TemplateInfo.HtmlFieldPrefix);

            
            #line default
            #line hidden
WriteLiteral("__Container\" style=\"overflow: auto;\">\r\n");


            
            #line 8 "..\..\Views\Default\Index.cshtml"
Write(Html.DisplayFor(x => x.Model));

            
            #line default
            #line hidden
WriteLiteral(@"
<hr />
<a id=""MMM"" href=""javascript: void(0)"" onclick=""ViewModel.showPerson(1603)"" data-bind=""
		dynamicLink: {
			value: 1603,
			source: '/mvc/persons/personselect.aspx/getitem',
			tooltipSource: '/mvc/persons/personselect.aspx/personinfo/0'
		}
	"">sdfdsffdsf</a>
</div>

<script type=""text/javascript"" language=""javascript"">
	$(function () {
		");



WriteLiteral("\r\n\t\t$(window).resize(function() {\r\n\t\t\tvar $parent = $(\"#dialogContentPane\");\r\n\t\t\t" +
"$(\"#");


            
            #line 26 "..\..\Views\Default\Index.cshtml"
   Write(ViewData.TemplateInfo.HtmlFieldPrefix);

            
            #line default
            #line hidden
WriteLiteral("__Container\").width($parent.width())\r\n\t\t\t$(\"#");


            
            #line 27 "..\..\Views\Default\Index.cshtml"
   Write(ViewData.TemplateInfo.HtmlFieldPrefix);

            
            #line default
            #line hidden
WriteLiteral("__Container\").height($parent.height())\r\n\t\t});\r\n\r\n\r\n\t});\r\n</script>\r\n\r\n");


DefineSection("Header", () => {

WriteLiteral("\r\n\t");


            
            #line 35 "..\..\Views\Default\Index.cshtml"
Write(Html.ActionLink("Редактировать", "Edit", "Default", new {  data_bind="jqueryui: { widget: 'button', options: { icons: { primary: 'ui-icon-pencil' } }}" } ));

            
            #line default
            #line hidden
WriteLiteral("\r\n");


});

WriteLiteral("\r\n\r\n\r\n");


DefineSection("Footer", () => {

WriteLiteral("\r\n\t");


            
            #line 40 "..\..\Views\Default\Index.cshtml"
Write(Html.DisplayFor(m => m.Model, "ChangedByInfo"));

            
            #line default
            #line hidden
WriteLiteral("\r\n\t<br />\r\n");


});

WriteLiteral("\r\n");


        }
    }
}
#pragma warning restore 1591
