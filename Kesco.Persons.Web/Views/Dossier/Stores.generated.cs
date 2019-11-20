﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.36400
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
    
    #line 3 "..\..\Views\Dossier\Stores.cshtml"
    using Kesco.Persons.Web.Models.Dossier;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Views\Dossier\Stores.cshtml"
    using Kesco.Web.Mvc;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Dossier/Stores.cshtml")]
    public partial class _Views_Dossier_Stores_cshtml : Kesco.Persons.Web.SiteViewPage<Kesco.Persons.Web.Models.Dossier.DossierSectionContext>
    {
        public _Views_Dossier_Stores_cshtml()
        {
        }
        public override void Execute()
        {




            
            #line 4 "..\..\Views\Dossier\Stores.cshtml"
  
	int personID = Model.ViewModel.Model.PersonID;
	string url = Model.Section.GetURL(Model.ViewModel.Model.PersonID);
	int accessLevel = (int) Model.ViewModel.AccessLevel;
	var sectionID = Model.Section.ID.ToString();
	var currentAccess = Model.AccessGranted;
	var groups = Model.ViewModel.GetContextItemsGroup(c => c.TabID == Model.Section.ID, c => c.Caption);

	if (groups.Count == 0) {Html.DossierClientSideScript_HideSection(Model.Section.ID);}
    /* Ищем в URL параметр hideOldVer, пишем его в сессию. Требуется для скрытия кнопки "Открыть в старой версии"  */
    string hideOldVerRequest = HttpContext.Current.Request["hideOldVer"];
    var hideOldVerSession = HttpContext.Current.Session["hideOldVer"];
    string hideOldVer = "false";

    if (!String.IsNullOrEmpty(hideOldVerRequest))
    {
        HttpContext.Current.Session["hideOldVer"] = hideOldVer = hideOldVerRequest;
    }
    else if (hideOldVerSession != null)
    {
        hideOldVer = hideOldVerSession.ToString();
    }
	Html.DossierClientSideScript_OpenStoreSection(sectionID.ToString(), url);


            
            #line default
            #line hidden
WriteLiteral("\r\n");


            
            #line 29 "..\..\Views\Dossier\Stores.cshtml"
 foreach (var group in groups) {
	int i = 0;
	foreach (var item in group.Items) {

            
            #line default
            #line hidden
WriteLiteral("\t<tr class=\"hoverable dsl");


            
            #line 32 "..\..\Views\Dossier\Stores.cshtml"
                     Write(sectionID);

            
            #line default
            #line hidden
WriteLiteral("\">\r\n\t\t<td style=\"width: 180px; padding-left: 20px;\">\r\n");


            
            #line 34 "..\..\Views\Dossier\Stores.cshtml"
 			if (i++ == 0) {
				
            
            #line default
            #line hidden
            
            #line 35 "..\..\Views\Dossier\Stores.cshtml"
Write(HL(group.Caption));

            
            #line default
            #line hidden
            
            #line 35 "..\..\Views\Dossier\Stores.cshtml"
                      
			} else {
				
			}

            
            #line default
            #line hidden
WriteLiteral("\t\t</td>\r\n\t\t<td>\r\n");


            
            #line 41 "..\..\Views\Dossier\Stores.cshtml"
 			if (currentAccess > 0) {

            
            #line default
            #line hidden
WriteLiteral("\t\t\t<a href=\"javascript: void(0);\" title=\"");


            
            #line 42 "..\..\Views\Dossier\Stores.cshtml"
                                     Write(Kesco.Web.Mvc.UI.Controls.Localization.Resources.GUI_Menu_Edit_Edit);

            
            #line default
            #line hidden
WriteLiteral("\"\r\n\t\t\t\t\tonclick=\"dossierOpenStore_");


            
            #line 43 "..\..\Views\Dossier\Stores.cshtml"
                           Write(sectionID.ToString());

            
            #line default
            #line hidden
WriteLiteral("(");


            
            #line 43 "..\..\Views\Dossier\Stores.cshtml"
                                                   Write(item.ID);

            
            #line default
            #line hidden
WriteLiteral(", ");


            
            #line 43 "..\..\Views\Dossier\Stores.cshtml"
                                                               Write(personID);

            
            #line default
            #line hidden
WriteLiteral(", ");


            
            #line 43 "..\..\Views\Dossier\Stores.cshtml"
                                                                            Write(hideOldVer);

            
            #line default
            #line hidden
WriteLiteral(");\"\r\n\t\t\t\t\t><span class=\"ui-icon ui-icon-pencil text-ui-icon\"></span></a>\r\n");


            
            #line 45 "..\..\Views\Dossier\Stores.cshtml"
			}

            
            #line default
            #line hidden
WriteLiteral("\t\t\t");


            
            #line 46 "..\..\Views\Dossier\Stores.cshtml"
Write(Html.DisplayFor(m => item.LinkID, "PersonStatic", new {
				TooltipPositionMy = "middle left",
				TooltipPositionAt = "middle right",
				InitialLabel = HL(item.LinkText)
			}));

            
            #line default
            #line hidden
WriteLiteral("\r\n\t\t\t");


            
            #line 51 "..\..\Views\Dossier\Stores.cshtml"
Write(Html.Raw(HL(item.Label)));

            
            #line default
            #line hidden
WriteLiteral("\r\n\t\t</td>\r\n\t\t<td class=\"changed\">\r\n\t\t\t");


            
            #line 54 "..\..\Views\Dossier\Stores.cshtml"
Write(Html.DisplayFor(m => item.ChangedBy, "EmployeeStatic", new {
				TooltipPositionMy = "middle right",
				TooltipPositionAt = "middle left",
				InitialLabel = item.ChangedByFIO
			}));

            
            #line default
            #line hidden
WriteLiteral(" \r\n\t\t\t<span>");


            
            #line 59 "..\..\Views\Dossier\Stores.cshtml"
     Write(item.ChangedDate);

            
            #line default
            #line hidden
WriteLiteral("</span>\r\n\t\t</td>\r\n\t</tr>\r\n");


            
            #line 62 "..\..\Views\Dossier\Stores.cshtml"
	}
}

            
            #line default
            #line hidden

        }
    }
}
#pragma warning restore 1591