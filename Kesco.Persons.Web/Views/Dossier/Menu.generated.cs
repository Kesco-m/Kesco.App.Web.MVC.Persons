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
    
    #line 3 "..\..\Views\Dossier\Menu.cshtml"
    using Kesco.Persons.Web;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Views\Dossier\Menu.cshtml"
    using Kesco.Persons.Web.Models.Dossier;
    
    #line default
    #line hidden
    
    #line 5 "..\..\Views\Dossier\Menu.cshtml"
    using Kesco.Web.Mvc;
    
    #line default
    #line hidden
    
    #line 6 "..\..\Views\Dossier\Menu.cshtml"
    using Kesco.Web.Mvc.UI;
    
    #line default
    #line hidden
    using MvcJqGrid;
    
    #line 2 "..\..\Views\Dossier\Menu.cshtml"
    using Resources;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Dossier/Menu.cshtml")]
    public partial class _Views_Dossier_Menu_cshtml : Kesco.Persons.Web.SiteViewPage<List<Kesco.Persons.BusinessLogic.Dossier.PersonMenuItem>>
    {
        public _Views_Dossier_Menu_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 7 "..\..\Views\Dossier\Menu.cshtml"
  
	int PersonID = (int) ViewData["PersonID"];
	int AccessLevel = (int)ViewData["AccessLevel"];
	bool Verified = (bool)ViewData["Verified"];
    bool IsBProject = (bool)ViewData["IsBProject"];
    
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
WriteLiteral("\r\n");

            
            #line 27 "..\..\Views\Dossier\Menu.cshtml"
Write(Html.CommonScriptCode("Dossier_MenuScript",
item => new System.Web.WebPages.HelperResult(__razor_template_writer => {

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "<script>\r\n \r\n\t");

WriteLiteralTo(__razor_template_writer, "\r\n\t(function(window, undefined) {\r\n\t\tvar url = \"");

            
            #line 32 "..\..\Views\Dossier\Menu.cshtml"
WriteTo(__razor_template_writer, Kesco.Persons.Web.Configuration.AppSettings.URI_person_search_old);

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "\";\r\n\t\tvar value = url.substring(0, url.lastIndexOf(\'/\') + 1);\r\n\r\n\t\tViewModel.prev" +
"VersionUrl  = value + \"person.aspx");

            
            #line 35 "..\..\Views\Dossier\Menu.cshtml"
                   WriteTo(__razor_template_writer, Html.Raw(Request.Url.Query));

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "\";\r\n\t})(this);\r\n\r\n    $(document).ready(function() {\r\n        function dispatchMe" +
"nuCommand(ev, ui) {\r\n            var cmd = ui.item.data(\'command\');\r\n           " +
" switch (cmd) {\r\n            case \'printRequisites\':\r\n                var url = " +
"\"");

            
            #line 43 "..\..\Views\Dossier\Menu.cshtml"
WriteTo(__razor_template_writer, Configuration.AppSettings.URI_store_person);

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "?personId=");

            
            #line 43 "..\..\Views\Dossier\Menu.cshtml"
                                                   WriteTo(__razor_template_writer, PersonID);

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "\";\r\n                window.open(url,\r\n                    \"_storePerson");

            
            #line 45 "..\..\Views\Dossier\Menu.cshtml"
  WriteTo(__razor_template_writer, PersonID);

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "\",\r\n                    \"scrollbars = yes,height=450,width=650,resizable = yes,to" +
"olbar=no,menubar=no,location=no\");\r\n                break;\r\n            case \'cr" +
"eateJuridical\':\r\n                openPopupWindow(\'");

            
            #line 49 "..\..\Views\Dossier\Menu.cshtml"
 WriteTo(__razor_template_writer, Url.Action("Index", "Juridical"));

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, @"',
                    null,
                    function(result) {
                        if (result) {
                            var personID = $.isArray(result) ? result[0].value : result;
                            if (personID)
                                openPopupWindow(
                                    '");

            
            #line 56 "..\..\Views\Dossier\Menu.cshtml"
      WriteTo(__razor_template_writer, Url.Action("Index", "Dossier"));

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "?id=\' + personID + \'&hideOldVer=");

            
            #line 56 "..\..\Views\Dossier\Menu.cshtml"
                                                                       WriteTo(__razor_template_writer, hideOldVer);

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, @"',
                                    null,
                                    null,
                                    ""wndPerson"" + personID,
                                    800,
                                    600,
                                    { close: false });
                        }
                    },
                    ""wndPersonCreateJuridical"",
                    800,
                    600,
                    { close: false });
                break;
            case 'createIndividual':
                openPopupWindow('");

            
            #line 71 "..\..\Views\Dossier\Menu.cshtml"
 WriteTo(__razor_template_writer, Url.Action("Index", "Natural"));

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, @"',
                    null,
                    function(result) {
                        if (result) {
                            var personID = $.isArray(result) ? result[0].value : result;
                            if (personID)
                                openPopupWindow(
                                    '");

            
            #line 78 "..\..\Views\Dossier\Menu.cshtml"
      WriteTo(__razor_template_writer, Url.Action("Index", "Dossier"));

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "?id=\' + personID + \'&hideOldVer=");

            
            #line 78 "..\..\Views\Dossier\Menu.cshtml"
                                                                       WriteTo(__razor_template_writer, hideOldVer);

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, @"',
                                    null,
                                    null,
                                    ""wndPerson"" + personID,
                                    800,
                                    600,
                                    { close: false });
                        }
                    },
                    ""wndPersonCreateNatural"",
                    800,
                    600,
                    { close: false });
                break;
            case 'searchPerson':
                openPopupWindow('");

            
            #line 93 "..\..\Views\Dossier\Menu.cshtml"
 WriteTo(__razor_template_writer, Url.Action("Index", "Search"));

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, @"',
                    null,
                    null,
                    ""wndSearchPerson"",
                    800,
                    600,
                    { close: false });
                break;
            case 'searchDocs':
                $.srv4js('SEARCHDISCONNECTED', { args: 'persons=");

            
            #line 102 "..\..\Views\Dossier\Menu.cshtml"
                                 WriteTo(__razor_template_writer, PersonID);

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "\', callback: docView_Result });\r\n                break;\r\n            case \'printD" +
"ossier\':\r\n                window.print();\r\n                break;\r\n            c" +
"ase \'deletePerson\':\r\n                openPopupWindow(\'");

            
            #line 108 "..\..\Views\Dossier\Menu.cshtml"
 WriteTo(__razor_template_writer, Url.Action("Delete", "Dossier", new { id = PersonID }));

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, @"',
                    null,
                    function(result) {
                        if (result) {
                            var personID = $.isArray(result) ? result[0] : result;
                            if (personID)
                                window.location.href = '");

            
            #line 114 "..\..\Views\Dossier\Menu.cshtml"
                         WriteTo(__razor_template_writer, Url.Action("Index", "Dossier"));

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "\' +\r\n                                    \"?id=\" +\r\n                              " +
"      personID +\r\n                                    \"&hideOldVer=\" +\r\n        " +
"                            \'");

            
            #line 118 "..\..\Views\Dossier\Menu.cshtml"
      WriteTo(__razor_template_writer, hideOldVer);

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, @"';
                            else closeDialog();
                        }
                    },
                    ""wndPersonDelete"",
                    800,
                    600,
                    { close: false });
                break;

            case 'addToPerson':
                var w = 800, h = 600;
                var isOldApp = ui.item.data('url').indexOf('/MVC/') == -1;
                var callback = function() { window.location.href = window.location.href; };
                if (ui.item.data('url').indexOf('Store.aspx') != -1) {
                    w = 520;
                    h = 300;
                    isOldApp = true;
                }
                if (ui.item.data('url').indexOf('Link.aspx') != -1) {
                    w = 520;
                    h = 300;
                }
                if (ui.item.data('url').indexOf('Card.aspx') != -1) {
                    w = 500;
                    h = 250;
                    callback = function(result) {
                        if ($.isArray(result)) {
                            editProps(0,
                                {
                                    type: 'POST',
                                    callbackUrl: '");

            
            #line 149 "..\..\Views\Dossier\Menu.cshtml"
                   WriteTo(__razor_template_writer, Url.FullPathAction("DialogResult"));

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "\',\r\n                                    Requisites: {\r\n                          " +
"              ID: 0,\r\n                                        PersonID: ");

            
            #line 152 "..\..\Views\Dossier\Menu.cshtml"
                   WriteTo(__razor_template_writer, PersonID);

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, @",
                                        From: result[0].From,
                                        To: result[0].To
                                    }
                                },
                                null,
                                null)
                        }
                    }
                }

                if (isOldApp)
                    DialogPageOpen(ui.item.data('url'),
                        $.validator.format('center:yes;status:no;help:no;resizable:yes;width:{0};height:{1};', w, h),
                        callback);
                else
                    openPopupWindow(ui.item.data('url'), null, callback, ""wndPersonAdd"", w, h);
                break;

            case 'toggleChangedBy':
                window.ViewModel.Params.PersonChange(window.ViewModel.Params.PersonChange() == 1 ? 0 : 1);
                ev.stopImmediatePropagation();
                $('.changed').toggle(window.ViewModel.Params.PersonChange() == 1);
                return false;
                break;
                case 'toggleOldProps':
                    var x = window.ViewModel.Params.PersonDateOld();
                    window.ViewModel.Params.PersonDateOld((x == 1) ? 0 : 1);
                    ev.stopImmediatePropagation();
                    window.location = '");

            
            #line 181 "..\..\Views\Dossier\Menu.cshtml"
       WriteTo(__razor_template_writer, Url.Action("Index"));

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "?id=");

            
            #line 181 "..\..\Views\Dossier\Menu.cshtml"
                                WriteTo(__razor_template_writer, PersonID);

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "&hideOldVer=");

            
            #line 181 "..\..\Views\Dossier\Menu.cshtml"
                                                       WriteTo(__razor_template_writer, hideOldVer);

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, @"';
                return false;
                break;
                case 'toggleStoreOldProps':
                    var x = window.ViewModel.Params.PersonDateOld();
                    window.ViewModel.Params.PersonDateOld((x == 2) ? 0 : 2);
                    ev.stopImmediatePropagation();
                    window.location = '");

            
            #line 188 "..\..\Views\Dossier\Menu.cshtml"
       WriteTo(__razor_template_writer, Url.Action("Index"));

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "?id=");

            
            #line 188 "..\..\Views\Dossier\Menu.cshtml"
                                WriteTo(__razor_template_writer, PersonID);

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "&hideOldVer=");

            
            #line 188 "..\..\Views\Dossier\Menu.cshtml"
                                                       WriteTo(__razor_template_writer, hideOldVer);

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, @"';
                    return false;
                break;
            default:
                break;
            }
        }

        $(""#menyBar"").menubar({
            menuIcon: true,
            buttons: true,
            position: {
                within: $(""#dialogHeader"").add(window).first()
            },
            select: dispatchMenuCommand
        }).show();

        if (window.ViewModel.Params.PersonChange() == 0) $('.changed').hide();
    });

	function docView_Result(rez, obj) {
		//значение контрола уже сброшено
		if (!rez.error)
			switch (rez.value) {
			case '-1': alert('Ошибка взаимодействия с архивом документов!'); break;
			case '0': break;
			default:
				break;
		}
		else
			alert(rez.errorMsg);
	}
</script>");

            
            #line 220 "..\..\Views\Dossier\Menu.cshtml"
       })));

            
            #line default
            #line hidden
WriteLiteral("\r\n<style");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(">\r\n\t#oldVersion { white-space:nowrap; border-width:0px; font-weight:bold; backgro" +
"und-color:inherit; float:right; }\r\n\t#oldVersion a { text-decoration: none; }\r\n\t#" +
"oldVersion a:hover { text-decoration: underline; }\r\n</style>\r\n\r\n<ul");

WriteLiteral(" id=\"menyBar\"");

WriteLiteral(" class=\"menubar\"");

WriteLiteral(" style=\"float: left; display: none;\"");

WriteLiteral(">\r\n\t<li>\r\n\t\t<a");

WriteLiteral(" href=\"#Edit\"");

WriteLiteral(">");

            
            #line 229 "..\..\Views\Dossier\Menu.cshtml"
             Write(Resources.Persons_Dossier_Menu_Person);

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n\t\t<ul>\r\n\t\t\t<li");

WriteLiteral(" data-command=\"createJuridical\"");

WriteLiteral("><a");

WriteLiteral(" href=\"javascript: void(0);\"");

WriteLiteral("><span");

WriteLiteral(" class=\"ui-icon ui-icon-home\"");

WriteLiteral("></span>");

            
            #line 231 "..\..\Views\Dossier\Menu.cshtml"
                                                                                                          Write(Resources.Persons_Dossier_Menu_NewJur);

            
            #line default
            #line hidden
WriteLiteral("</a></li>\r\n\t\t\t<li");

WriteLiteral(" data-command=\"createIndividual\"");

WriteLiteral("><a");

WriteLiteral(" href=\"javascript: void(0);\"");

WriteLiteral("><span");

WriteLiteral(" class=\"ui-icon ui-icon-person\"");

WriteLiteral("></span>");

            
            #line 232 "..\..\Views\Dossier\Menu.cshtml"
                                                                                                             Write(Resources.Persons_Dossier_Menu_NewInd);

            
            #line default
            #line hidden
WriteLiteral("</a></li>\r\n\t\t\t<li");

WriteLiteral(" data-command=\"searchPerson\"");

WriteLiteral("><a");

WriteLiteral(" href=\"javascript: void(0);\"");

WriteLiteral("><span");

WriteLiteral(" class=\"ui-icon ui-icon-search\"");

WriteLiteral("></span>");

            
            #line 233 "..\..\Views\Dossier\Menu.cshtml"
                                                                                                         Write(Resources.Persons_Dossier_Menu_Search);

            
            #line default
            #line hidden
WriteLiteral("</a></li>\r\n\t\t\t<li");

WriteLiteral(" data-command=\"searchDocs\"");

WriteLiteral("><a");

WriteLiteral(" href=\"javascript: void(0);\"");

WriteLiteral("><span");

WriteLiteral(" class=\"ui-icon ui-icon-copy\"");

WriteLiteral("></span>");

            
            #line 234 "..\..\Views\Dossier\Menu.cshtml"
                                                                                                     Write(Resources.Persons_Dossier_Menu_Docs);

            
            #line default
            #line hidden
WriteLiteral("</a></li>\r\n\t\t\t<li");

WriteLiteral(" data-command=\"printDossier\"");

WriteLiteral("><a");

WriteLiteral(" href=\"javascript: void(0);\"");

WriteLiteral("><span");

WriteLiteral(" class=\"ui-icon ui-icon-print\"");

WriteLiteral("></span>");

            
            #line 235 "..\..\Views\Dossier\Menu.cshtml"
                                                                                                        Write(Resources.Persons_Dossier_Menu_Print);

            
            #line default
            #line hidden
WriteLiteral("</a></li>\r\n           \r\n");

            
            #line 237 "..\..\Views\Dossier\Menu.cshtml"
            
            
            #line default
            #line hidden
            
            #line 237 "..\..\Views\Dossier\Menu.cshtml"
             if (IsBProject)
            { 

            
            #line default
            #line hidden
WriteLiteral("                <li");

WriteLiteral(" data-command=\"printRequisites\"");

WriteLiteral("><a");

WriteLiteral(" href=\"javascript: void(0);\"");

WriteLiteral("><span");

WriteLiteral(" class=\"ui-icon ui-icon-print\"");

WriteLiteral("></span>");

            
            #line 239 "..\..\Views\Dossier\Menu.cshtml"
                                                                                                                        Write(Resources.Persons_Synchronize_Differences_Requisites);

            
            #line default
            #line hidden
WriteLiteral("</a></li>\r\n");

            
            #line 240 "..\..\Views\Dossier\Menu.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 242 "..\..\Views\Dossier\Menu.cshtml"
			
            
            #line default
            #line hidden
            
            #line 242 "..\..\Views\Dossier\Menu.cshtml"
    if (AccessLevel > 3 || (!Verified && AccessLevel == 3))
			{

            
            #line default
            #line hidden
WriteLiteral("\t\t\t\t<li");

WriteLiteral(" data-command=\"deletePerson\"");

WriteLiteral("><a");

WriteLiteral(" href=\"javascript: void(0);\"");

WriteLiteral("><span");

WriteLiteral(" class=\"ui-icon ui-icon-trash\"");

WriteLiteral("></span>");

            
            #line 244 "..\..\Views\Dossier\Menu.cshtml"
                                                                                                         Write(Resources.GUI_Button_Delete.ToLower());

            
            #line default
            #line hidden
WriteLiteral("</a></li>\r\n");

            
            #line 245 "..\..\Views\Dossier\Menu.cshtml"
			}

            
            #line default
            #line hidden
WriteLiteral("\t\t</ul>\r\n\t</li>\r\n");

            
            #line 248 "..\..\Views\Dossier\Menu.cshtml"
	
            
            #line default
            #line hidden
            
            #line 248 "..\..\Views\Dossier\Menu.cshtml"
  if (Model.Count>0) {

            
            #line default
            #line hidden
WriteLiteral("\t<li>\r\n\t\t<a");

WriteLiteral(" href=\"#Edit\"");

WriteLiteral(">");

            
            #line 250 "..\..\Views\Dossier\Menu.cshtml"
             Write(Resources.Persons_Dossier_Menu_AddToPerson);

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n\t\t<ul>\r\n");

            
            #line 252 "..\..\Views\Dossier\Menu.cshtml"
			
            
            #line default
            #line hidden
            
            #line 252 "..\..\Views\Dossier\Menu.cshtml"
    foreach (var item in Model) {
				if (item.Separator == 1) {

            
            #line default
            #line hidden
WriteLiteral("\t\t\t\t\t<li><span></span></li>\r\n");

            
            #line 255 "..\..\Views\Dossier\Menu.cshtml"
				}

            
            #line default
            #line hidden
WriteLiteral("\t\t\t\t<li");

WriteLiteral(" data-command=\"addToPerson\"");

WriteLiteral(" data-url=\"");

            
            #line 256 "..\..\Views\Dossier\Menu.cshtml"
                                         Write(Url.GetURL(item, PersonID));

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral("\r\n\t\t\t\t\t><a");

WriteLiteral(" href=\"javascript: void(0);\"");

WriteLiteral(">");

            
            #line 257 "..\..\Views\Dossier\Menu.cshtml"
                                 Write(item.Caption);

            
            #line default
            #line hidden
WriteLiteral("</a></li>\r\n");

            
            #line 258 "..\..\Views\Dossier\Menu.cshtml"
			}

            
            #line default
            #line hidden
WriteLiteral("\t\t</ul>\r\n\t</li>\r\n");

            
            #line 261 "..\..\Views\Dossier\Menu.cshtml"
	}

            
            #line default
            #line hidden
WriteLiteral("\t<li>\r\n\t\t<a");

WriteLiteral(" href=\"#Edit\"");

WriteLiteral(">");

            
            #line 263 "..\..\Views\Dossier\Menu.cshtml"
             Write(Resources.Persons_Dossier_Menu_View);

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n\t\t<ul>\r\n\t\t\t<li");

WriteLiteral(" data-command=\"toggleChangedBy\"");

WriteLiteral("><a");

WriteLiteral(" href=\"javascript: void(0);\"");

WriteLiteral("><span");

WriteLiteral(" class=\"ui-icon ui-icon-check\"");

WriteLiteral(" data-bind=\"visible: window.ViewModel.Params.PersonChange()\"");

WriteLiteral("></span>");

            
            #line 265 "..\..\Views\Dossier\Menu.cshtml"
                                                                                                                                                                       Write(Resources.Persons_Dossier_Menu_ChangedBy);

            
            #line default
            #line hidden
WriteLiteral("</a></li>\r\n            <li");

WriteLiteral(" data-command=\"toggleOldProps\"");

WriteLiteral("><a");

WriteLiteral(" href=\"javascript: void(0);\"");

WriteLiteral("><span");

WriteLiteral(" class=\"ui-icon ui-icon-check\"");

WriteLiteral(" data-bind=\"visible: (window.ViewModel.Params.PersonDateOld()==1?1:0)\"");

WriteLiteral("></span>");

            
            #line 266 "..\..\Views\Dossier\Menu.cshtml"
                                                                                                                                                                                         Write(Resources.Persons_Dossier_Menu_OldProps);

            
            #line default
            #line hidden
WriteLiteral("</a></li>\r\n            \r\n            <li");

WriteLiteral(" data-command=\"toggleStoreOldProps\"");

WriteLiteral("><a");

WriteLiteral(" href=\"javascript: void(0);\"");

WriteLiteral("><span");

WriteLiteral(" class=\"ui-icon ui-icon-check\"");

WriteLiteral(" data-bind=\"visible: (window.ViewModel.Params.PersonDateOld()==2?1:0)\"");

WriteLiteral("></span>");

            
            #line 268 "..\..\Views\Dossier\Menu.cshtml"
                                                                                                                                                                                              Write(Resources.Persons_Dossier_Menu_StoreOldProps);

            
            #line default
            #line hidden
WriteLiteral("</a></li>\r\n\t\t</ul>\r\n\t</li>\r\n</ul>\r\n\r\n");

        }
    }
}
#pragma warning restore 1591
