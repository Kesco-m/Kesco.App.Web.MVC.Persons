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
    
    #line 4 "..\..\Views\Dossier\Contacts.cshtml"
    using Kesco.Persons.Web.Models.Dossier;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Views\Dossier\Contacts.cshtml"
    using Kesco.Web.Mvc;
    
    #line default
    #line hidden
    using MvcJqGrid;
    
    #line 5 "..\..\Views\Dossier\Contacts.cshtml"
    using Resources;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Dossier/Contacts.cshtml")]
    public partial class _Views_Dossier_Contacts_cshtml : Kesco.Persons.Web.SiteViewPage<Kesco.Persons.Web.Models.Dossier.DossierSectionContext>
    {
        public _Views_Dossier_Contacts_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

            
            #line 6 "..\..\Views\Dossier\Contacts.cshtml"
  
    int ii = 0;
	int personID = Model.ViewModel.Model.PersonID;
	int accessLevel = (int)Model.ViewModel.AccessLevel;
	var sectionID = Model.Section.ID.ToString();
	var currentAccess = Model.AccessGranted;
	var groups = Model.ViewModel.GetContextItemsGroup(cxti => cxti.TabID == Model.Section.ID, cxti => cxti.Caption);
    var doisserSection = Model.Section;
	if (groups.Count == 0) {Html.DossierClientSideScript_HideSection(Model.Section.ID);}
    var modalCaption = Resources.Persons_Doisser_Actuality_Checked_modalCaption;
    var modalDialogFirst = Resources.Persons_Doisser_Actuality_Checked_modalDialogFirst;
    var modalDialogSecond = Resources.Persons_Doisser_Actuality_Checked_modalDialogSecond;
    var modalButtonOkCaption = Resources.Persons_Doisser_Actuality_Checked_modalButtonOkCaption;
    var ParameterNone = Resources.Persons_Link_Parameter0;
   
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
    

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n<tr");

WriteAttribute("class", Tuple.Create(" class=\"", 1644), Tuple.Create("\"", 1667)
, Tuple.Create(Tuple.Create("", 1652), Tuple.Create("dsl", 1652), true)
            
            #line 38 "..\..\Views\Dossier\Contacts.cshtml"
, Tuple.Create(Tuple.Create("", 1655), Tuple.Create<System.Object, System.Int32>(sectionID
            
            #line default
            #line hidden
, 1655), false)
);

WriteLiteral(">\r\n\t<td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n    \r\n");

            
            #line 41 "..\..\Views\Dossier\Contacts.cshtml"
    
            
            #line default
            #line hidden
            
            #line 41 "..\..\Views\Dossier\Contacts.cshtml"
     if (accessLevel > 0)
    {

            
            #line default
            #line hidden
WriteLiteral("        <a");

WriteAttribute("href", Tuple.Create(" href=\"", 1740), Tuple.Create("\"", 1919)
, Tuple.Create(Tuple.Create("", 1747), Tuple.Create("javascript:ContactActualWIndow.RenderWindow(", 1747), true)
            
            #line 43 "..\..\Views\Dossier\Contacts.cshtml"
, Tuple.Create(Tuple.Create(" ", 1791), Tuple.Create<System.Object, System.Int32>(sectionID
            
            #line default
            #line hidden
, 1792), false)
, Tuple.Create(Tuple.Create("", 1804), Tuple.Create(",", 1804), true)
, Tuple.Create(Tuple.Create(" ", 1805), Tuple.Create("\'", 1806), true)
            
            #line 43 "..\..\Views\Dossier\Contacts.cshtml"
, Tuple.Create(Tuple.Create("", 1807), Tuple.Create<System.Object, System.Int32>(modalCaption
            
            #line default
            #line hidden
, 1807), false)
, Tuple.Create(Tuple.Create("", 1822), Tuple.Create("\',", 1822), true)
, Tuple.Create(Tuple.Create(" ", 1824), Tuple.Create("\'", 1825), true)
            
            #line 43 "..\..\Views\Dossier\Contacts.cshtml"
                 , Tuple.Create(Tuple.Create("", 1826), Tuple.Create<System.Object, System.Int32>(modalDialogFirst
            
            #line default
            #line hidden
, 1826), false)
, Tuple.Create(Tuple.Create("", 1845), Tuple.Create("\',", 1845), true)
, Tuple.Create(Tuple.Create(" ", 1847), Tuple.Create("\'", 1848), true)
            
            #line 43 "..\..\Views\Dossier\Contacts.cshtml"
                                        , Tuple.Create(Tuple.Create("", 1849), Tuple.Create<System.Object, System.Int32>(modalDialogSecond
            
            #line default
            #line hidden
, 1849), false)
, Tuple.Create(Tuple.Create("", 1869), Tuple.Create("\',", 1869), true)
, Tuple.Create(Tuple.Create(" ", 1871), Tuple.Create("\'", 1872), true)
            
            #line 43 "..\..\Views\Dossier\Contacts.cshtml"
                                                                , Tuple.Create(Tuple.Create("", 1873), Tuple.Create<System.Object, System.Int32>(modalButtonOkCaption
            
            #line default
            #line hidden
, 1873), false)
, Tuple.Create(Tuple.Create("", 1896), Tuple.Create("\',", 1896), true)
, Tuple.Create(Tuple.Create(" ", 1898), Tuple.Create("\'", 1899), true)
            
            #line 43 "..\..\Views\Dossier\Contacts.cshtml"
                                                                                           , Tuple.Create(Tuple.Create("", 1900), Tuple.Create<System.Object, System.Int32>(ParameterNone
            
            #line default
            #line hidden
, 1900), false)
, Tuple.Create(Tuple.Create("", 1916), Tuple.Create("\'", 1916), true)
, Tuple.Create(Tuple.Create(" ", 1917), Tuple.Create(")", 1918), true)
);

WriteLiteral("> ");

            
            #line 43 "..\..\Views\Dossier\Contacts.cshtml"
                                                                                                                                                                                            Write(Resources.Persons_Doisser_Actuality_Is_Checked);

            
            #line default
            #line hidden
WriteLiteral(" :</a>\r\n");

            
            #line 44 "..\..\Views\Dossier\Contacts.cshtml"
    }
    else
    {
       
            
            #line default
            #line hidden
            
            #line 47 "..\..\Views\Dossier\Contacts.cshtml"
  Write(Resources.Persons_Doisser_Actuality_Is_Checked);

            
            #line default
            #line hidden
            
            #line 47 "..\..\Views\Dossier\Contacts.cshtml"
                                                       
            
            #line default
            #line hidden
            
            #line 47 "..\..\Views\Dossier\Contacts.cshtml"
                                                  Write(Html.Raw(":"));

            
            #line default
            #line hidden
            
            #line 47 "..\..\Views\Dossier\Contacts.cshtml"
                                                                       
    }  

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 50 "..\..\Views\Dossier\Contacts.cshtml"
	
            
            #line default
            #line hidden
            
            #line 50 "..\..\Views\Dossier\Contacts.cshtml"
  if (Model.ViewModel.ContactActuality == null || Model.ViewModel.ContactActuality.ChangedBy == null)
 {
		
            
            #line default
            #line hidden
            
            #line 52 "..\..\Views\Dossier\Contacts.cshtml"
Write(Resources.Persons_Dossier_ActualityNotChecked);

            
            #line default
            #line hidden
            
            #line 52 "..\..\Views\Dossier\Contacts.cshtml"
                                                
 }
 else
 {
		
            
            #line default
            #line hidden
            
            #line 56 "..\..\Views\Dossier\Contacts.cshtml"
Write(Html.DisplayFor(m => m.ViewModel.ContactActuality.ChangedBy, "EmployeeStatic", new
{
    TooltipPositionMy = "middle right",
    TooltipPositionAt = "middle left",
    InitialLabel = HL(Model.ViewModel.ContactActualityChangedBy.GetInstanceFriendlyName())
}));

            
            #line default
            #line hidden
            
            #line 61 "..\..\Views\Dossier\Contacts.cshtml"
  

            
            #line default
            #line hidden
WriteLiteral("\t\t<span>");

            
            #line 62 "..\..\Views\Dossier\Contacts.cshtml"
    Write((Model.ViewModel.ContactActuality.ChangedDate.HasValue ? (Model.ViewModel.ContactActuality.ChangedDate.Value.FromUtcToClient().ToString("F")) : ""));

            
            #line default
            #line hidden
WriteLiteral("</span>\r\n");

            
            #line 63 "..\..\Views\Dossier\Contacts.cshtml"
 }

            
            #line default
            #line hidden
WriteLiteral("\t</td>\r\n</tr>\r\n");

            
            #line 66 "..\..\Views\Dossier\Contacts.cshtml"
 foreach (var type in groups)
{
    int i = 0;
    foreach (var item in type.Items)
    {

            
            #line default
            #line hidden
WriteLiteral("\t<tr");

WriteAttribute("class", Tuple.Create(" class=\"", 2811), Tuple.Create("\"", 2844)
, Tuple.Create(Tuple.Create("", 2819), Tuple.Create("hoverable", 2819), true)
, Tuple.Create(Tuple.Create(" ", 2828), Tuple.Create("dsl", 2829), true)
            
            #line 71 "..\..\Views\Dossier\Contacts.cshtml"
, Tuple.Create(Tuple.Create("", 2832), Tuple.Create<System.Object, System.Int32>(sectionID
            
            #line default
            #line hidden
, 2832), false)
);

WriteLiteral(">\r\n        <td");

WriteLiteral(" style=\"width: 180px; padding-left: 20px;\"");

WriteLiteral(" class=\"ui-label\"");

WriteLiteral(" nowrap=\"nowrap\"");

WriteLiteral(">\r\n");

            
            #line 73 "..\..\Views\Dossier\Contacts.cshtml"
          
            
            #line default
            #line hidden
            
            #line 73 "..\..\Views\Dossier\Contacts.cshtml"
           if (i++ == 0)
            {
                
            
            #line default
            #line hidden
            
            #line 75 "..\..\Views\Dossier\Contacts.cshtml"
           Write(Html.Raw(type.Caption + ":"));

            
            #line default
            #line hidden
            
            #line 75 "..\..\Views\Dossier\Contacts.cshtml"
                                             
            }

            
            #line default
            #line hidden
WriteLiteral("        </td>\r\n\t\t<td>\r\n");

            
            #line 79 "..\..\Views\Dossier\Contacts.cshtml"
			
            
            #line default
            #line hidden
            
            #line 79 "..\..\Views\Dossier\Contacts.cshtml"
    if (accessLevel >= 2 && currentAccess > 0)
   {

            
            #line default
            #line hidden
WriteLiteral("\t\t\t<a");

WriteLiteral(" href=\"javascript: void(0);\"");

WriteAttribute("onclick", Tuple.Create(" onclick=\"", 3150), Tuple.Create("\"", 3212)
, Tuple.Create(Tuple.Create("", 3160), Tuple.Create("editContact(", 3160), true)
            
            #line 81 "..\..\Views\Dossier\Contacts.cshtml"
, Tuple.Create(Tuple.Create("", 3172), Tuple.Create<System.Object, System.Int32>(item.ID
            
            #line default
            #line hidden
, 3172), false)
, Tuple.Create(Tuple.Create("", 3182), Tuple.Create(",", 3182), true)
            
            #line 81 "..\..\Views\Dossier\Contacts.cshtml"
, Tuple.Create(Tuple.Create(" ", 3183), Tuple.Create<System.Object, System.Int32>(sectionID
            
            #line default
            #line hidden
, 3184), false)
, Tuple.Create(Tuple.Create("", 3196), Tuple.Create(",", 3196), true)
            
            #line 81 "..\..\Views\Dossier\Contacts.cshtml"
 , Tuple.Create(Tuple.Create(" ", 3197), Tuple.Create<System.Object, System.Int32>(hideOldVer
            
            #line default
            #line hidden
, 3198), false)
, Tuple.Create(Tuple.Create("", 3211), Tuple.Create(")", 3211), true)
);

WriteAttribute("title", Tuple.Create("\r\n\t\t\t\ttitle=\"", 3213), Tuple.Create("\"", 3296)
            
            #line 82 "..\..\Views\Dossier\Contacts.cshtml"
, Tuple.Create(Tuple.Create("", 3226), Tuple.Create<System.Object, System.Int32>(Kesco.Web.Mvc.UI.Controls.Localization.Resources.GUI_Menu_Edit_Edit
            
            #line default
            #line hidden
, 3226), false)
);

WriteLiteral("\r\n\t\t\t\t><span");

WriteLiteral(" class=\"ui-icon ui-icon-pencil text-ui-icon\"");

WriteLiteral("></span>\r\n\t\t\t</a>\r\n");

            
            #line 85 "..\..\Views\Dossier\Contacts.cshtml"
   }

            
            #line default
            #line hidden
            
            #line 86 "..\..\Views\Dossier\Contacts.cshtml"
  
   string text = item.LinkText, phone = "";
   if (item.Order >= 20 && item.Order <= 39 && text.Contains((char)31))
   {
       string[] strArr = text.Split(new char[] { (char)31 });
       text = strArr[0];
       phone = strArr[1];
      // @Html.Raw("["  + text + "->" + phone + "]")
   }

            
            #line default
            #line hidden
WriteLiteral("\t\t");

WriteLiteral(" ");

            
            #line 95 "..\..\Views\Dossier\Contacts.cshtml"
Write(Html.DisplayFor(m => text, GetControlNameByContactType(item.Order), new { PhoneNumber = phone, }));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 96 "..\..\Views\Dossier\Contacts.cshtml"
		
            
            #line default
            #line hidden
            
            #line 96 "..\..\Views\Dossier\Contacts.cshtml"
Write(Html.Raw(HL(item.Label)));

            
            #line default
            #line hidden
            
            #line 96 "..\..\Views\Dossier\Contacts.cshtml"
                           

            
            #line default
            #line hidden
WriteLiteral("\r\n\t\t</td>\r\n\t\t<td");

WriteLiteral(" class=\"changed\"");

WriteLiteral(">\r\n");

WriteLiteral("\t\t\t");

            
            #line 100 "..\..\Views\Dossier\Contacts.cshtml"
Write(Html.DisplayFor(m => item.ChangedBy, "EmployeeStatic", new
{
    TooltipPositionMy = "middle right",
    TooltipPositionAt = "middle left",
    InitialLabel = HL(item.ChangedByFIO)
}));

            
            #line default
            #line hidden
WriteLiteral(" \r\n\t\t\t<span>");

            
            #line 106 "..\..\Views\Dossier\Contacts.cshtml"
     Write(item.ChangedDate);

            
            #line default
            #line hidden
WriteLiteral("</span>\r\n\t\t</td>\r\n\t</tr>\r\n");

            
            #line 109 "..\..\Views\Dossier\Contacts.cshtml"
	}
}

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
