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

namespace Kesco.Persons.Web.Views.Shared.EditorTemplates
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
    
    #line 4 "..\..\Views\Shared\EditorTemplates\PersonField.cshtml"
    using Kesco.Persons.Controls.ComponentModel;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Views\Shared\EditorTemplates\PersonField.cshtml"
    using Kesco.Web.Mvc;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Views\Shared\EditorTemplates\PersonField.cshtml"
    using Kesco.Web.Mvc.UI;
    
    #line default
    #line hidden
    using MvcJqGrid;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.4.1.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/EditorTemplates/PersonField.cshtml")]
    public class PersonField : Kesco.Persons.Web.SiteViewPage<int?>
    {
        public PersonField()
        {
        }
        public override void Execute()
        {




WriteLiteral("\r\n");


            
            #line 6 "..\..\Views\Shared\EditorTemplates\PersonField.cshtml"
  
	var descriptor = new PersonSelectAttribute();
	descriptor.OnMetadataCreated(ViewData.ModelMetadata);


            
            #line default
            #line hidden

            
            #line 10 "..\..\Views\Shared\EditorTemplates\PersonField.cshtml"
Write(Html.EditorFor(m => m, "PersonSelect"));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");


        }
    }
}
#pragma warning restore 1591
