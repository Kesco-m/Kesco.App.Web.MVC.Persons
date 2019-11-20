using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.BusinessLogic.DataAccess;
using Kesco.Persons.Controls.DataAccess;
using Kesco.Web.Mvc.UI.Infrastructure;
using Kesco.Persons.ObjectModel;
using Kesco.Web.Mvc;

namespace Kesco.Persons.Controls.Controllers
{
    /// <summary>
    /// Реализует контроллер для элементов управления "Выбор орг-правю формы"
    /// </summary>
    public class PersonThemeSelectController : KescoSelectBaseController<PersonThemeSelectAccessor, PersonTheme, PersonThemeSelectAccessor.SearchParameters, int>
    {
        public override ActionResult Dispatch(string command, string control, int mode, int? id, PersonThemeAccessor.SearchParameters parameters)
        {
            try
            {
                var cmd = command.ToLower();
                switch (cmd)
                {
                    case "getitem":
                        return GetItem(control, mode, id);
                    case "search":
                        return Search(control, mode, parameters);
                    case "setvalue":
                        return SetValue(control, id);
                    case "advsearch":
                        return AdvSearch(control, mode, parameters);
                    case "details":
                    case "view":
                        return Details(control, mode);
                }
                throw new Exception(String.Format("Неизвестная команда: {0}/{2} - {1}", command, control, cmd));
            }
            catch (Exception ex)
            {
                Kesco.Logging.Logger.WriteEx(ex);
                return JavaScriptAlert(
                        Kesco.Localization.Resources.Ajax_Alert_Title_ApplicationError,
                        ex.Message
                    );
            }
        }



        public override ActionResult AdvSearch(string control, int mode, PersonThemeAccessor.SearchParameters parameters)
        {
            return ChoosePersonTypes();
        }

        /// <summary>
        /// Вызывает диалог выбора тем лиц
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="model">The model.</param>
        /// <returns>Скрипт с выводом диалога выбора лиц</returns>
        public ActionResult ChoosePersonTypes()
        {
            string script = String.Format(@"(function() {{
					var selected = ViewModel.getAllSelectedThemes();
					var callbackUrl = encodeURIComponent('{0}');
					var title = 'Выбор темы лица';
					var url;
					url = '{1}?{2}&selectedid=' + selected +'&clientId=0';
					url = $.validator.format(url, callbackUrl, title);
                    
					openPopupWindow(url, {{
							type: 'GET'
						}}, function (result) {{
							if ($.isArray(result)) {{
								var resultAr = [];

								for (var i = 0; i < result.length; i++) resultAr.push(result[i].value);
                                if (window.console) console.log(resultAr.join(','));
							    if (result.length > 0)
									window.ViewModel.checkPersonThemesFromModal(resultAr.join(','));
                               if (result.length == 0){{
                                    window.ViewModel.renderAllTypesAndThemes('');
				                  }}
                                    
								
							}}
						}}, 'wnd_ChooseThemes', 800, 600);
				}})()",
                Url.FullPathAction("DialogResult", "Default"),
                Configuration.AppSettings.URI_theme_search,
                Configuration.URI_theme_search_QS ?? "t=1"
            );

            return JavaScript(script);
        }

        public ActionResult CheckPersonThemesFromModal(string themesIDs)
        {
            var list = Repository.PersonTypes.GetAvailablePersonTypesByThemeIDs(themesIDs);
            var arrThemes = themesIDs.Split(',');
            if (list.Count != arrThemes.Length)
            {
                string renderModalWindow = string.Format(@"
                     var themesIDs  =  window.ViewModel.getAllRenderedTypes();
                     
                     window.showModalDialog('{1}?themes={0}&types='+themesIDs+'&advSearch=true', window.self, 'dialogHeight:300px;dialogWidth:500px;resizable:no;scroll:no;');
                ", themesIDs, Url.FullPathAction("AvailablePersonTypes", "PersonThemeSelect"));
                return JavaScript(renderModalWindow);
            }
            string stringTypeIDs = string.Join(",", list.Select(m => m.КодТипаЛица).Distinct());
            string renderPersonList = String.Format(@"
                ViewModel.renderAllTypesAndThemes('{0}');
                ", stringTypeIDs);
            return JavaScript(renderPersonList);
            return null;
        }

        public ActionResult RenderAllThemesAndTypes(string typeIDs)
        {
            List<PersonTypeAccessor.PersonTypeInfo> personTypes = Repository.PersonTypes.GetPersonTypesByTypeIDs(typeIDs);
            var groupedThemes = personTypes.GroupBy(m => m.ТемаЛица);
            string renderTypesList = String.Format(@"
                $('.typeLines').remove()
            ");

            foreach (var theme in groupedThemes)
            {
                var themeName = theme.Key;
                string themeID = theme.Select(m => m.КодТемыЛица).FirstOrDefault().ToString();
                var listCatalogNames = theme.Select(m => m.Каталог).ToArray();
                string stringCatalogNames = string.Join(",", listCatalogNames);
                var listTypeIDs = theme.Select(m => m.КодТипаЛица).ToArray();
                string stringTypeIDs = string.Join(",", listTypeIDs);

                renderTypesList += RenderPersonThemesAndCatalogs(themeID, stringTypeIDs, stringCatalogNames, themeName);

            }
            return JavaScript(renderTypesList);
        }

        public string RenderPersonThemesAndCatalogs(string themeID, string typeIDs, string catalogNames, string themeName)
        {
            bool RenderEditButton = (Repository.PersonTypes.GetAvailablePersonTypesByThemeIDs(themeID).Count != 1);
            return string.Format(@"
            // Создание эллемента отображения выбраной темы
                    var innerTR = document.createElement('tr');
                    innerTR.className = 'typeLines';
                    innerTR.id = '{0}tr';
                    innerTD = document.createElement('td');
                    var span = document.createElement('span');
                    span.onclick = function(){{ ViewModel.removeTypeField('{0}'); }};
                    span.className = 'ui-icon ui-icon-minusthick text-ui-icon';
                    innerTD.appendChild(span);
                    innerTR.appendChild(innerTD);
                    var innerTD = document.createElement('td');
                    if('{4}' == 'true' || '{4}' == 'True')
                    {{ //Render edit button
                        var span = document.createElement('span');
                        span.onclick = function(){{ ViewModel.updateTypeField('{0}'); }};
                        span.className = 'ui-icon ui-icon-pencil text-ui-icon';
                        innerTD.appendChild(span);
                    }}
                    innerTR.appendChild(innerTD);
                    innerTD = document.createElement('td');
                    var label = document.createElement('label');
                    label.setAttribute('for','text');
                    label.innerHTML = '{3}';
                    label.id = '{0}';
                    label.className = 'selectedPersonThemes';
                    innerTD.appendChild(label); 
                    innerTR.appendChild(innerTD);

                     // Создание эллемента отображения типа
                    innerTD = document.createElement('td');
                    label = document.createElement('label');
                    label.setAttribute('for','text');
                    label.innerHTML = '{2}';
                    label.id = '{1}';
                    label.className = 'selectedPersonTypes';
                    innerTD.appendChild(label);
                    innerTR.appendChild(innerTD);
                    
                    $('#themesList')[0].appendChild(innerTR);
            ", themeID, typeIDs, catalogNames, themeName, RenderEditButton);
        }


        public List<string> GetThemeChild(List<PersonTheme> lstTheme, string themeID)
        {
           List<string> removedThemesIDs = new List<string>();
           List<string> childThemesIDs = lstTheme.Where(t => t.Parent.ToString() == themeID).Select(m => m.ID.ToString()).ToList();

            foreach (string childThemesID in childThemesIDs)
            {
              
                if (!String.IsNullOrEmpty(childThemesID))
                {
                    removedThemesIDs.Add(childThemesID);
                    removedThemesIDs.AddRange(GetThemeChild(lstTheme, childThemesID));
                }
            }
            return removedThemesIDs;
        }

        public List<string> GetThemeParent(List<PersonTheme> lstTheme, string themeID)
        {
            List<string> removedThemesIDs = new List<string>();
            int parentID =
                lstTheme.Where(t => t.ID.ToString() == themeID).Select(k => k.ID).FirstOrDefault();

            themeID = lstTheme.Where(t => t.ID == parentID).Select(t => t.Parent).FirstOrDefault().ToString();

            
            if (!String.IsNullOrEmpty(themeID))
            {
                removedThemesIDs.Add(themeID);
                removedThemesIDs.AddRange(GetThemeParent(lstTheme, themeID));
            }

            return removedThemesIDs;
        }

        public ActionResult CheckPersonThemes(string currentThemeID, string selectedPersonThemesIds)
        {
            // 1. Необходимо проверить если хотя бы одна тема в нескольких каталогах

            var list = Repository.PersonTypes.GetTypeIDsByThemeIDs(currentThemeID);
            var needToClarify = list.Count != 1;
            if (list.Count == 0) return null;
            if (!needToClarify)
            {
                //uncheck all childs and parents

                List<PersonTheme> lstTheme = Repository.PersonTheme.GetAll();
                List<string> removedThemesIDs = new List<string>();
                removedThemesIDs.AddRange(GetThemeParent(lstTheme, currentThemeID));
                removedThemesIDs.AddRange(GetThemeChild(lstTheme, currentThemeID));
                string removedThemeList = string.Join(",", removedThemesIDs);
                var removedList = Repository.PersonTypes.GetAvailablePersonTypesByThemeIDs(removedThemeList);
                List<string> removesTypeList = removedList.Where(m => removedThemesIDs.Contains(m.КодТемыЛица.ToString())).Select(t => t.КодТипаЛица.ToString()).ToList();
                string removesTypeString = string.Join(",", removesTypeList);
                string[] selectedPersonThemesIdsArray = selectedPersonThemesIds.Split(',');
                bool has = selectedPersonThemesIdsArray.Contains(currentThemeID);
                if (has) return null;
                string renderPersonList = String.Format(@"
                ViewModel.addNewPersonType('{0}', '{1}');
                ", list.Select(m => m).FirstOrDefault(), removesTypeString);
                return JavaScript(renderPersonList);
            }
            string script = String.Format(@"(function() {{
                    var existValue = $('#{0}tr');
                    if(existValue[0] == null)
                    {{
                    window.showModalDialog('{1}?themes={0}', window.self, 'dialogHeight:300px;dialogWidth:500px;resizable:no;scroll:no;');    
                    }}
                    else
                    {{
                     var themesIDs  =  $('#{0}tr label')[1].id;
                     window.showModalDialog('{1}?themes={0}&types='+themesIDs, window.self, 'dialogHeight:300px;dialogWidth:500px;resizable:no;scroll:no;');
                    }}

			    }})()",
                currentThemeID,
                Url.FullPathAction("AvailablePersonTypes", "PersonThemeSelect")
               
                );
            return JavaScript(script);
        }


        public ActionResult AvailablePersonTypes(string themes, string types, string advSearch = null)
        {
            var list = Repository.PersonTypes.GetAvailablePersonTypesByThemeIDs(themes);
            

            List<PersonTheme> lstTheme = Repository.PersonTheme.GetAll();
            List<string> removedThemesIDs = new List<string>();
            removedThemesIDs.AddRange(GetThemeParent(lstTheme, themes));
            removedThemesIDs.AddRange(GetThemeChild(lstTheme, themes));
            string removedThemeList = string.Join(",", removedThemesIDs);
            var removedList = Repository.PersonTypes.GetAvailablePersonTypesByThemeIDs(removedThemeList);
            List<string> removesTypeList = removedList.Where(m => removedThemesIDs.Contains(m.КодТемыЛица.ToString())).Select(t => t.КодТипаЛица.ToString()).ToList();
            string removesTypeString = string.Join(",", removesTypeList);

            return Content(RenderRecords(list, types, advSearch, removesTypeString));

        }

        protected string RenderRecords(List<PersonTypeAccessor.PersonTypeInfo> personsTypeInfo, string types, string fromAdvSearch, string removesTypeString)
        {
            bool check = false;
            bool isAdvSearch = !String.IsNullOrEmpty(fromAdvSearch);
            String w = null;
            var catalogsIDs = personsTypeInfo.GroupBy(a => a.КодТемыЛица);
            var test = personsTypeInfo.GroupBy(m => m.ТемаЛица);

            string PersonTypesArray = "[";
            foreach (var item in test.Select(m => m.Key))
            {
                string resonTypesTempArray = "['" + item + "'";
                foreach (var result in personsTypeInfo.Where(m => m.ТемаЛица == item).Select(o => o.КодТипаЛица))
                {
                    resonTypesTempArray += ",'" + result + "'";
                }
                resonTypesTempArray += "],";
                PersonTypesArray += resonTypesTempArray;
            }
            PersonTypesArray = PersonTypesArray.Remove(PersonTypesArray.Length - 1);
            PersonTypesArray += "]";
           

            w += String.Format(@"
            <script language='javascript'>
             window.document.title = 'Типы лиц в каталогах'; 
            function SaveTypesNew()
            {{
            
            var typeIDs = '';
            checkboxes = document.getElementsByName('checkBoxInput');
            for(var i=0, n=checkboxes.length;i<n;i++) {{
                if(checkboxes[i].checked){{
                    typeIDs += checkboxes[i].id; typeIDs += ',';
                }}
            }}
            finalTypeIDs = typeIDs.substring(0, typeIDs.length - 1);
            var allTypes = {3};
            var res = finalTypeIDs.split(',');
            var clearNames = [];
            for(var i=0, n=allTypes.length;i<n;i++) {{
	            var contain = 0;
	            for(var k=1, s=allTypes[i].length;k<=s;k++) {{
		            for(var t=0, y=res.length;t<y;t++) {{
			            if(allTypes[i][k] == res[t]){{contain = 1; }}
		            }}
	            }}
	
	            if(contain == 0){{ clearNames[clearNames.length] = allTypes[i][0]}}
	    	   
            }}
            

            var deletedTypeIDs = '';
            checkboxes = document.getElementsByName('checkBoxInput');
            for(var i=0, n=checkboxes.length;i<n;i++) {{
                if(!checkboxes[i].checked){{
                    deletedTypeIDs += checkboxes[i].id; deletedTypeIDs += ',';
                }}
            }}
            
            finalDeletedTypeIDs  = deletedTypeIDs.substring(0, deletedTypeIDs.length - 1);
            
            if(clearNames.length != 0){{
                var alertMsg = '{4}' + ' ' + clearNames + ' ' + '{5}';
                if (confirm(alertMsg)) {{
                    if('{1}' == 'true' || '{1}' == 'True')
                    {{
                        dialogArguments.ViewModel.renderAllTypesAndThemes(finalTypeIDs);
                        self.close();
                    }}
                    else
                    {{
                        if('{0}' == 'true' || '{0}' == 'True')
                        {{
                            if(finalTypeIDs == ''){{self.close(); return;  }}
                            dialogArguments.ViewModel.addNewPersonType(finalTypeIDs, '{2}');
                        }}
                        else
                        {{
                            dialogArguments.ViewModel.updateOldPersonType(finalTypeIDs, finalDeletedTypeIDs);
                        }}
                        self.close();   
                    }}
                }} 
            }}
            else
            {{
                if('{1}' == 'true' || '{1}' == 'True')
                {{
                    dialogArguments.ViewModel.renderAllTypesAndThemes(finalTypeIDs);
                    self.close();
                }}
                else
                {{
                    if('{0}' == 'true' || '{0}' == 'True')
                    {{
                        if(finalTypeIDs == ''){{self.close(); return;  }}
                        dialogArguments.ViewModel.addNewPersonType(finalTypeIDs, '{2}');
                    }}
                    else
                    {{
                        dialogArguments.ViewModel.updateOldPersonType(finalTypeIDs, finalDeletedTypeIDs);
                    }}
                    self.close();   
                }}
            }}
                                       
            }}

            function checkAll(check){{
                checkboxes = document.getElementsByName('checkBoxInput');
                for(var i=0, n=checkboxes.length;i<n;i++) {{
                if(!checkboxes[i].disabled){{ checkboxes[i].checked = check;}}
              }}

            }}
            </script>
              <style>
                .tableTypes {{
	                border-collapse:collapse;
	                table-layout:fixed;
	                width:500px;
                }}

                .gridHeader
                {{
                    background: url(/MVC/persons/styles/css/images/ui-bg_highlight-soft_75_cccccc_1x100.png) 50% 50% repeat-x;
                  
                }}
                .Tdata {{
	                height:245px;
	                width:500px;
	                overflow-y:auto;
	                overflow-x:hidden;
                    background-color: #EEEEEE;
                }}
                td, th{{
	                padding:2px 4px;
	                border-left:1px solid #c5c5c5;
	                border-bottom:1px solid #c5c5c5;
    	           
                }}
                td:first-child, th:first-child {{border-left:0;}}
                .DivFixTable {{
	                width:500px;
	                border:1px solid #c5c5c5;
                }}

                #MyTable .tdContent
                {{
                    padding-top: 5px;
                    padding-bottom: 5px;
                }}

                .themeTd
                {{
                    font-size: 13pt;
                    background-color: #EEEEEE;
                }}
            </style>
", String.IsNullOrEmpty(types), isAdvSearch, removesTypeString, PersonTypesArray, Localization.Resources.PersonSelectTheme_ThemeNoCheck_Themes, Localization.Resources.PersonSelectTheme_ThemeNoCheck_Dialog);
            w +=
                String.Format(
                    @"<div class='DivFixTable'>
                                <table class='tableTypes'>
		                            <col width=100> <col width=120>");
            w += "<tr class='gridHeader'><th>Тип лица</th><th> <input type=checkbox " + ((check) ? "checked" : "") +
                 " onclick=checkAll(this.checked)> В каталогах</th>";
            w += "</table>";


            w += string.Format(@"<div class='Tdata'><table id='MyTable' class='tableTypes'><col width=100> <col width=120>");

            foreach (var catalogsID in catalogsIDs)
            {
                int i = 0;

                //Если у темы лица есть только 1 каталог, активируем cheackbox и делаем его readonly
                int replyThemes = personsTypeInfo.Count(m => m.КодТемыЛица == catalogsID.Key);
                foreach (var personTypeInfo in catalogsID)
                {
                    //check = personTypesIDs.Contains(personTypeInfo.КодТипаЛица);
                    if (i == 0)
                    {
                        w += String.Format(@"<tr class='themeTd'><td>{0}</td><td class='tdContent'>
                        <input id='{4}' name='checkBoxInput' type=checkbox {2} {3}>
                        {1}</td></tr>", personTypeInfo.ТемаЛица, personTypeInfo.Каталог, ((check || replyThemes == 1) ? "checked" : ""), ((replyThemes == 1) ? "disabled" : ""), personTypeInfo.КодТипаЛица);

                    }
                    else
                    {
                        w += String.Format(@"<tr class='themeTd'><td></td><td class='tdContent'>
                         <input id='{1}' name='checkBoxInput' type=checkbox >
                         {0}</td></tr>", personTypeInfo.Каталог, personTypeInfo.КодТипаЛица);
                    }
                    i++;
                }
            }
            w += "</table></div>";
            w += "<table class='tableTypes' style='text-align: right;'><tr class='gridHeader'><td>";
            w += "<input type='button' onclick=SaveTypesNew();  value='Сохранить'>";
            w += "&nbsp;";
            w += "<input type='button' onclick=self.close();  value='Отмена'>";
            w += "</tr></tr></table>";
            //w +="<tr>";
            //w +="<td align='right' colspan=2 noWrap>&nbsp;</td>";
            //w +="</tr>";

            w += "</div>";

            w += String.Format(@" <script language='javascript'> 
                                checkboxes = document.getElementsByName('checkBoxInput');
                                 for(var i=0, n=checkboxes.length;i<n;i++) {{
                                    if('{0}' != null) {{
                                        var t1 = '{0}'.replace(/\s/g, '').split(',');
                                        for (var o=0; o<t1.length; o++)
                                        {{
                                            if(t1[o].toString() == checkboxes[i].id)
                                            {{
                                                checkboxes[i].checked = true;
                                            }}
                                        }}
                                        
                                    }}
                                }}
                                </script> ", types);

            return w;
        }

        /// <summary>
        /// Gets the corporate culture settings.
        /// </summary>
        /// <returns></returns>
        protected override CultureSettings GetCorporateCultureSettings()
        {
            return Configuration.AppSettings.Culture;
        }

        /// <summary>
        /// Gets the advanced search URL.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override string GetAdvancedSearchUrl(BusinessLogic.DataAccess.PersonThemeAccessor.SearchParameters parameters)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Возвращает URL-адрес для просмотра досье на лицо
        /// </summary>
        /// <param name="clid">Идентификатор клиента.</param>
        /// <returns>URL-адрес для просмотра досье на лицо</returns>
        protected override string GetDetailsUrl()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the entry label.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <returns></returns>
        protected override string GetEntryLabel(PersonTheme entry)
        {
            return entry.Name;
        }
    }
}
