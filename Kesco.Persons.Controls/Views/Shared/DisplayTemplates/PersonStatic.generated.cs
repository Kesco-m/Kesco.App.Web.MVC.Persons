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
    
    #line 2 "..\..\Views\Shared\DisplayTemplates\PersonStatic.cshtml"
    using Kesco.Web;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Views\Shared\DisplayTemplates\PersonStatic.cshtml"
    using Kesco.Web.Mvc;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Views\Shared\DisplayTemplates\PersonStatic.cshtml"
    using Kesco.Web.Mvc.UI;
    
    #line default
    #line hidden
    
    #line 5 "..\..\Views\Shared\DisplayTemplates\PersonStatic.cshtml"
    using Kesco.Web.Mvc.UI.Controls.DataAccess;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/DisplayTemplates/PersonStatic.cshtml")]
    public partial class _Views_Shared_DisplayTemplates_PersonStatic_cshtml_ : Kesco.Persons.Controls.SiteViewPage<int?>
    {
        public _Views_Shared_DisplayTemplates_PersonStatic_cshtml_()
        {
        }
        public override void Execute()
        {
            
            #line 6 "..\..\Views\Shared\DisplayTemplates\PersonStatic.cshtml"
  
	ModelMetadata metaData = ViewData.ModelMetadata;
	string initialLabel = ((string)ViewData["InitialLabel"]) ?? String.Empty;
	string tooltipPositionMy = ((string)ViewData["TooltipPositionMy"]) ?? String.Empty;
	string tooltipPositionAt = ((string)ViewData["TooltipPositionAt"]) ?? String.Empty;
	string cssClass = ((string) ViewData["CssClass"]) ?? String.Empty;
	string value = Model.HasValue ? Model.ToString() : String.Empty;

	cssClass = "personStatic " + cssClass;

	Html.RegisterCommonScriptCode("PersonStatic", @"
			$(document).on('click', 'a.personStatic', function() {
				window.ViewModel.showPerson($(this).data('person-id'));
			});

			$(document).on('mouseenter', 'a.personStatic', function() {
				var $this = $(this);
				if (!$this.data('qtip')) {
					$this.initToolTip(function() {
						var uri = Env.URI_person_info;
							uri = uri.replace('/0', '/' + $this.data('person-id'));
						return uri;
					}, $(document.body)).qtip('show');
				}
			});
	");	

            
            #line default
            #line hidden
WriteLiteral("\r\n<a");

WriteAttribute("class", Tuple.Create(" class=\"", 1176), Tuple.Create("\"", 1195)
            
            #line 33 "..\..\Views\Shared\DisplayTemplates\PersonStatic.cshtml"
, Tuple.Create(Tuple.Create("", 1184), Tuple.Create<System.Object, System.Int32>(cssClass
            
            #line default
            #line hidden
, 1184), false)
);

WriteLiteral(" data-person-id=\"");

            
            #line 33 "..\..\Views\Shared\DisplayTemplates\PersonStatic.cshtml"
                                   Write(value);

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral(" href=\"javascript: void(0)\"");

WriteLiteral(" data-tip-pos-my=\"");

            
            #line 33 "..\..\Views\Shared\DisplayTemplates\PersonStatic.cshtml"
                                                                                         Write(tooltipPositionMy);

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral(" data-tip-pos-at=\"");

            
            #line 33 "..\..\Views\Shared\DisplayTemplates\PersonStatic.cshtml"
                                                                                                                                Write(tooltipPositionAt);

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral("\r\n\t>");

            
            #line 34 "..\..\Views\Shared\DisplayTemplates\PersonStatic.cshtml"
Write(Html.Raw(initialLabel));

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n");

        }
    }
}
#pragma warning restore 1591
