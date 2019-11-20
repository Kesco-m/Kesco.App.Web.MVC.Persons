﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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
    
    #line 2 "..\..\Views\TerritorySelectUsage\Index.cshtml"
    using Kesco.Web.Mvc;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/TerritorySelectUsage/Index.cshtml")]
    public partial class _Views_TerritorySelectUsage_Index_cshtml : Kesco.Territories.Controls.SiteViewPage<Kesco.Territories.Controls.Controllers.TerritorySelectUsageController.ViewModel>
    {
        public _Views_TerritorySelectUsage_Index_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\TerritorySelectUsage\Index.cshtml"
  
    ViewBag.Title = "Использование элемента управления 'Выбор территории'";
	Layout = "~/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<div");

WriteLiteral(" id=\"container\"");

WriteLiteral(" style=\"overflow: auto;\"");

WriteLiteral(">\r\n<table");

WriteLiteral(" width=\"100%\"");

WriteLiteral(">\r\n<tr>\r\n    <td>\r\n        <span");

WriteLiteral(" style=\"float: left;\"");

WriteLiteral(@">
        <pre>
            // Модель данных
	        public class DataModel
	        {


				[Display(Name = ""Страна"")]
				[TerritorySelect(CLID = 62)]
				public int CountryID { get; set; }

	        }
        </pre>
        </span>
        <span");

WriteLiteral(" style=\"float: left;\"");

WriteLiteral(">\r\n        <pre>\r\n            // Модель представления\r\n\t        public class View" +
"Model : ViewModel&lt;DataModel&gt; { }\r\n        </pre>\r\n        </span>\r\n       " +
" <span");

WriteLiteral(" style=\"float: left;\"");

WriteLiteral(">\r\n        <pre>\r\n            ");

WriteLiteral("@* Форма редактирования *");

WriteLiteral("@\r\n            ");

WriteLiteral("@using (var form = Html.BeginForm())\r\n            {\r\n                ");

WriteLiteral("@Html.EditorForModel();\r\n            }\r\n        </pre>\r\n        </span>\r\n        " +
"<span");

WriteLiteral(" style=\"float: left;\"");

WriteLiteral(">\r\n        <pre>\r\n            ");

WriteLiteral("@* Просмотр *");

WriteLiteral("@\r\n            ");

WriteLiteral("@Html.EditorForModel();\r\n        </pre>\r\n        </span>\r\n        <span");

WriteLiteral(" style=\"float: left;\"");

WriteLiteral(">\r\n        <pre>\r\n            ");

WriteLiteral("@* Состояние модели *");

WriteLiteral("@\r\n        </pre>\r\n        <pre");

WriteLiteral(" data-bind=\"text: ko.toJSON(ko.mapping.toJS(ViewModel.Model))\"");

WriteLiteral(">\r\n            ");

WriteLiteral("@* Состояние модели *");

WriteLiteral("@\r\n        </pre>\r\n        </span>\r\n    </td>\r\n</tr>\r\n<tr");

WriteLiteral(" valign=\"top\"");

WriteLiteral(">\r\n    <td>\r\n        <hr />\r\n        <h3>Форма редактирования</h3>\r\n");

            
            #line 61 "..\..\Views\TerritorySelectUsage\Index.cshtml"
        
            
            #line default
            #line hidden
            
            #line 61 "..\..\Views\TerritorySelectUsage\Index.cshtml"
         using (var form = Html.BeginForm())
        {
            
            
            #line default
            #line hidden
            
            #line 63 "..\..\Views\TerritorySelectUsage\Index.cshtml"
       Write(Html.EditorFor(m => m.Model));

            
            #line default
            #line hidden
            
            #line 63 "..\..\Views\TerritorySelectUsage\Index.cshtml"
                                         ;
        }

            
            #line default
            #line hidden
WriteLiteral("        <hr />\r\n        <h3>Просмотр</h3>\r\n");

WriteLiteral("        ");

            
            #line 67 "..\..\Views\TerritorySelectUsage\Index.cshtml"
   Write(Html.DisplayFor(m => m.Model));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </td>\r\n</tr>\r\n</table>\r\n</div>\r\n\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(" language=\"javascript\"");

WriteLiteral(">\r\n\t$(function () {\r\n\r\n\t\t");

WriteLiteral("\r\n\t\t$(window).resize(function() {\r\n\t\t\tvar $parent = $(\"#dialogContentPane\");\r\n\t\t\t" +
"$(\"#container\").width($parent.width())\r\n\t\t\t$(\"#container\").height($parent.height" +
"())\r\n\t\t});\r\n\r\n\t});\r\n</script>\r\n");

            
            #line 85 "..\..\Views\TerritorySelectUsage\Index.cshtml"
  
	Html.RegisterScript(@"
		ViewModel.Model.CountryID.__SearchParameters.AreaIDs.push(2); // поиск стран
		ViewModel.Model.CountryID.__SearchParameters.HowSearch(1); // искать начинающие с
	");

            
            #line default
            #line hidden
WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591