using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.SessionState;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.BusinessLogic.Dossier;
using Kesco.Persons.BusinessLogic.Persons;
using Kesco.Persons.ObjectModel;
using Kesco.Persons.Web.Models.Dossier;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.Filtering;
using Kesco.Web.Mvc.SharedViews;
using Kesco.Web.Mvc.SharedViews.Models;
using Kesco.Persons.BusinessLogic.DataAccess;
using System.Data;
using System.Threading.Tasks;
using System.Threading;

namespace Kesco.Persons.Web.Controllers
{
	public partial class DossierController : SharedModelController<PersonDossierModel>
	{
		public DossierController() : base() {
			UseCompressHtml = true;
		}

        public virtual ActionResult Index()
        {
            string employeeId = HttpContext.Request["employeeId"];
            int? personID = PersonAccessor.Accessor.GetPersonByEmployeeID(employeeId);
            if (String.IsNullOrEmpty(employeeId))
            {
                //if(personID == null) personID =UniqueIdQSFilterSetting.CreateInstance().InitFromQueryString("id", personID).GetValue();
                //Kesco.Employees.ObjectModel.Employee employee =
                //    Kesco.Employees.BusinessLogic.Repository.Employees.GetEmployeeByPersonID(Convert.ToInt32(personID));
                //ViewBag.titleInfo = employee.EmployerID + ' ' + employee.FullName;
                return View(new ViewModel());
            }

            if (personID == null || personID == 0)
            {
                return Redirect("Employee.aspx?employeeId=" + employeeId);
            }
            var AccessLevel = Repository.Persons.GetPersonAccessLevel(Convert.ToInt32(personID));
            if (AccessLevel == PersonAccessLevel.None)
            {
                return Redirect("Employee.aspx?employeeId=" + employeeId);
            }
            return Redirect("dossier.aspx?id=" + personID + "&hideOldVer=false");
        }


        public virtual void SaveActualContact(PersonDossierModel model)
        {
            Repository.ContactActuals.SaveActualContactInfo(model.PersonID);
        }

	    public virtual ActionResult Delete(int? id)
		{
			var vm = new Kesco.Persons.Web.Models.Dossier.Delete.ViewModel();
			vm.Model.PersonID = id;
			return View(vm);
		}

	    public virtual ActionResult Remove(int? id, int? replaceWithPersonID, bool? confirmed)
		{
			confirmed = confirmed ?? false;

			if (!id.HasValue) // Не указано лицо для удаления
				return JavaScriptAlert(
						Kesco.Localization.Resources.Ajax_Alert_Title_ApplicationError,
						Kesco.Persons.Web.Localization.Resources.Persons_Delete_Message_PersonNotSpecified
					);

			if (replaceWithPersonID.HasValue && id.Value == replaceWithPersonID.Value) {
				// Замена не возможна! Замещающее лицо совпадает с удаляемым лицом.
				return JavaScriptAlert(
						Kesco.Localization.Resources.Ajax_Alert_Title_ApplicationError,
						Kesco.Persons.Web.Localization.Resources.Persons_Delete_Message_PersonIsTheSameAsSpecifiedForReplcement
					);
			}

			var person = Repository.Persons.GetInstance(id.Value);
			if (person == null) {
				throw new ApplicationException(String.Format(
						Kesco.Persons.Web.Localization.Resources.Persons_Delete_Message_PersonNotFound, id.Value
					));
			}
			Person replacement = null;
			if (replaceWithPersonID.HasValue) {
				replacement = Repository.Persons.GetInstance(replaceWithPersonID.Value);
				if (replacement == null) {
					throw new ApplicationException(String.Format(
							Kesco.Persons.Web.Localization.Resources.Persons_Delete_Message_PersonNotFound, replacement.Nickname
						));
				}
			}
			if (!confirmed.Value) {

				// "Вы уверены, что хотите удалить лицо №{0}"
				var msg = String.Format(Kesco.Persons.Web.Localization.Resources.
						Persons_Delete_Message_ConfirmDeletion, person.Nickname);

				if (replacement != null) {
					// с заменой на лицо №{0}?
					msg += String.Format(Kesco.Persons.Web.Localization.Resources.
						Persons_Delete_Message_ConfirmDeletion_Replacement, replacement.Nickname);

					if (person.PersonType != replacement.PersonType) {
						// При замене, т.к. тип удаляемого лица отличается от типа замечающего лица, <br>реквизиты удаляемого лица будут удалены!
						msg += "<br><br>";
						msg += Kesco.Persons.Web.Localization.Resources.Persons_Delete_Message_ConfirmDeletion_DetailsOfPersonWillBeDeleted;
					}
				} else msg += "?";

				string script = String.Format(@"
						(function () {{
							var ca = {0};
							confirmAction(
								  ca.title
								, ca.message
								, ca.actionTitle
								, {1}
								, ca.cancelTitle
							)
						}})();
						"
						,  Kesco.Web.Mvc.Json.Serialize(new {
							title = Kesco.Persons.Web.Localization.Resources.Persons_Delete_Title_ConfirmDeletion,
							message = msg,
							actionTitle = Kesco.Persons.Web.Localization.Resources.Persons_Delete_ConfirmDeletion_ActionTitle,
							cancelTitle = global::Resources.Resources.GUI_Button_Cancel
						}, true)
						, @" function() { 
								ViewModel.Model.Confirmed(true); 
								ViewModel.deletePerson(); 
							} "
					);
				return JavaScript(script);
			}

			replaceWithPersonID = replaceWithPersonID ?? -1;
            
			Repository.Persons.DeletePerson(id.Value, replaceWithPersonID.Value);

			if (replaceWithPersonID.Value == -1) {
				return JavaScriptAlert(
						Kesco.Persons.Web.Localization.Resources.Persons_Delete_Title_DeleteSuccess,
						String.Format(Kesco.Persons.Web.Localization.Resources.Persons_Delete_Message_DeleteSuccess, person.Nickname),
						String.Format(@" 
							function() {{
								var dialogResult = [{0}];
								dialogResult = JSON.stringify(dialogResult);
								closeDialogAndReturnValue(dialogResult);
							}}", 0)
					);
			} else {
				return JavaScriptAlert(
						Kesco.Persons.Web.Localization.Resources.Persons_Delete_Title_DeleteSuccess,
						String.Format(Kesco.Persons.Web.Localization.Resources.Persons_Delete_Message_DeleteWithReplaceSuccess, person.Nickname, replacement.Nickname),
						String.Format(@" 
							function() {{
								var dialogResult = [{0}];
								dialogResult = JSON.stringify(dialogResult);
								closeDialogAndReturnValue(dialogResult);
							}}", replaceWithPersonID)
					);
			}
		}

		public class DossierSectionRequest
		{
			public int PersonID { get; set; }
			public int SectionID { get; set; }
			public bool ForceReload { get; set; }
		}

		public virtual ActionResult RefreshSection(DossierSectionRequest model)
		{
			ViewModel mdl = new ViewModel(model.PersonID, new int[] { model.SectionID }, model.ForceReload);
			var section = mdl.Sections.FirstOrDefault(s => s.ID == model.SectionID);
			if (section != null) {
				return PartialView(
						String.IsNullOrEmpty(section.ViewName) ? "DefaultSectionView" : section.ViewName,
						new DossierSectionContext {
							Section = section,
							ViewModel = mdl
						}
					);
			} else
				throw new ApplicationException(
						@"Вкладка досье с кодом #{0} не может быть отображена. 
						Не найдено соответствующее представление".FormatWith(model.SectionID)
					);
		}

		/// <summary>
		/// Выполняет диспетчеризацию команд.
		/// </summary>
		/// <param name="command">Команда</param>
		/// <param name="control">Идентификатор элемента управления на стороне клиента</param>
		/// <param name="model">Модель</param>
		/// <param name="result">Результат действия, если команда обработана, иначе null.</param>
		/// <returns>
		/// Возвращает истину, если команда обработана, иначе false
		/// </returns>
		protected override bool DoDispatch(string command, string control, PersonDossierModel model, out ActionResult result)
		{
			result = null;
			switch (command.ToLower()) {
				case "personchecked":
					result = PersonChecked(control, model);
					return true;
				case "editjuridical":
                    result = EditJuridical(model, control);
					return true;
				case "editnatural":
                    result = EditNatural(model, control);
					return true;
				case "chooseempls":
					result = ChooseEmpls(model);
					return true;
				case "saveresponsibleemployees":
					result = SaveResponsibleEmployees(model);
					return true;
				case "choosetypes":
					result = EditTypes(control, model);
					return true;
				case "checkpersonthemes":
					result = CheckPersonThemes(control, model);
					return true;
				case "savetypes":
					result = SaveTypes(control, model);
					return true;
                case "saveactualcontact":
                    SaveActualContact(model);
                    return true;
				default:
					return false;
			}
		}

		/// <summary>
		/// Вызов формы редактирования ответственных сотрудников
		/// </summary>
		/// <param name="model">модель данных</param>
		/// <returns>скрипт с ответным действием</returns>
		public ActionResult ChooseEmpls(PersonDossierModel model)
		{
			string selected = "";

			// Получение ответственных сотрудников
			List<Kesco.Employees.ObjectModel.Employee> ResponsibleEmployees = Kesco.Persons.BusinessLogic.Repository.ResponsibleEmployees.GetResponsibleEmployeesByPersonId(model.PersonID);
			foreach(Kesco.Employees.ObjectModel.Employee empl in ResponsibleEmployees)
				selected += empl.ID + ",";

			string script = String.Format(@"
				(function() {{
					var selected = '{3}';
					var callbackUrl = encodeURIComponent('{0}');
					var title = encodeURIComponent('{4}');
					var url = '{1}?{2}&selectedid='+selected;
					url = $.validator.format(url, callbackUrl, title);

					openPopupWindow(url, null, function(result) {{
						if ($.isArray(result)) {{
							var res = [];
							for(var i=0; i<result.length;i++)
								res.push(result[i].value);
							ViewModel.Model.ResponsibleEmpls(res.join(','));

							ViewModel.dispatchModelCommand('SaveResponsibleEmployees');
						}}
					}}, 'wnd_ChooseEmployees', 800, 600);
				}})();",
			Url.FullPathAction("DialogResult", "Default"),
			Configuration.AppSettings.URI_user_search,
			Configuration.URI_user_search_QS ?? "t=1",
			selected.TrimEnd(new char[]{','}),
			global::Resources.Resources.Kesco_Persons_Web_VW_1000
			);

			return JavaScript(script);
		}

		/// <summary>
		/// Сохранение переданных ответственных сотрудников
		/// </summary>
		/// <param name="model">модель данных</param>
		/// <returns>скрипт с ответным действием</returns>
		public ActionResult SaveResponsibleEmployees(PersonDossierModel model)
		{
			Repository.Persons.MergeResponsibleEmployees(model.PersonID, model.ResponsibleEmpls);

			var responsibles = Repository.ResponsibleEmployees.GetResponsibleEmployeesByPersonId(model.PersonID);

			// показать форму для указания признака, что лицо является личным
//            if (responsibles.Count > 0) {
//                return JavaScript(String.Format(@"
//					(function() {{
//						showPersonal
//					}})();"
//                    ,
//                ));
//            }

			// и вызываем перегрузку всей формы
			return JavaScript("window.location.href = window.location.href;");
		}

		public class ResponsiblePersonality {
			public int SectionID { get; set; }
			public int PersonID { get; set; }
			public int EmployeeID { get; set; }
			public bool IsPersonal { get; set; }
		}
		/// <summary>
		/// Sets the responsible as personal.
		/// </summary>
		/// <param name="id">Код связи лицо-сотрудник.</param>
		/// <returns></returns>
		public ActionResult SetResponsiblePersonality(ResponsiblePersonality model)
		{
			Repository.ResponsibleEmployees.SetResponsiblePersonality(model.PersonID, model.EmployeeID, model.IsPersonal?1:0);
			// и вызываем перегрузку всей формы
			return JavaScript(String.Format("refreshSection({0}, false, true);", model.SectionID));
		}

		/// <summary>
		/// Вызов формы редактирования данных юридического лица
		/// </summary>
		/// <param name="model">модель данных</param>
		/// <returns>скрипт с ответным действием</returns>
		public ActionResult EditJuridical(PersonDossierModel model, string sectionId = null)
		{
			string script = String.Format(@"
				(function() {{
					var url = '{0}';
					openPopupWindow(url, null, function(result) {{
						if ($.isArray(result)) {{
							window.location.href = window.location.href;
						}}
					}}, 'wnd_EditJuridical', 500, 460);
				}})();",
				Url.FullPathAction("Index","Juridical", new {
					personID = model.PersonID,
                    sectionId = sectionId,
					callbackUrl = Url.FullPathAction("DialogResult", "Default")
				})

			);

			return JavaScript(script);
		}

		/// <summary>
		/// Вызов формы редактирования данных физического лица
		/// </summary>
		/// <param name="model">модель данных</param>
		/// <returns>скрипт с ответным действием</returns>
		public ActionResult EditNatural(PersonDossierModel model, string sectionId)
		{
			string script = String.Format(@"
				(function() {{
					var url = '{0}';
					openPopupWindow(url, null, function(result) {{
						if ($.isArray(result)) {{
							window.location.href = window.location.href;
						}}
					}}, 'wnd_EditNatural', 770, 460);
				}})();",
				Url.FullPathAction("Index", "Natural", new
				{
					personID = model.PersonID,
                    sectionId = sectionId,
					callbackUrl = Url.FullPathAction("DialogResult", "Default")
				})

			);

			return JavaScript(script);
		}

		/// <summary>
		/// Вызов формы редактирования тем лиц
		/// </summary>
		/// <param name="model">модель данных</param>
		/// <returns>скрипт с ответным действием</returns>
		public ActionResult EditTypes(string control, PersonDossierModel model)
		{
			string selected = "";

			// Получение тем сотрудников
            List<Kesco.Persons.BusinessLogic.DataAccess.PersonTypeAccessor.PersonTypeInfo> types = Kesco.Persons.BusinessLogic.Repository.PersonTypes.GetPersonTypesByPersonID(model.PersonID);
			selected = String.Join(",", types.GroupBy(t => t.КодТемыЛица).Select(g => g.Key.ToString()));

			string script = String.Format(@"
				(function() {{
					var selected = '{3}';
					var callbackUrl = encodeURIComponent('{0}');
					var title = encodeURIComponent('{4}');
					var url = '{1}?{2}&selectedid='+selected+'&clientId={5}';
					url = $.validator.format(url, callbackUrl, title);

					openPopupWindow(url, {{
							type: 'GET'
						}}, function(result) {{
						if ($.isArray(result)) {{
							var res = [];
							for(var i=0; i<result.length;i++)
								res.push(result[i].value);
							ViewModel.Model.PersonThemes(res.join(','));

							ViewModel.dispatchModelCommand('CheckPersonThemes', {6});
						}}
					}}, 'wnd_ChooseTypes', 800, 600);
				}})();",
				Url.FullPathAction("DialogResult", "Default"),
				Configuration.AppSettings.URI_theme_search,
				Configuration.URI_theme_search_QS ?? "t=1",
				selected,
				global::Resources.Resources.Kesco_Persons_Web_VW_1004,
				model.PersonID,
                Kesco.Web.Mvc.Json.Serialize(control)
			);

			return JavaScript(script);
		}

        protected string RenderRecords(List<Kesco.Persons.BusinessLogic.DataAccess.PersonTypeAccessor.PersonTypeInfo> personsTypeInfo, int clientID, int sectionID)
        {
            bool isNatural = true;
            PersonCardType personType = Repository.Persons.GetInstance(clientID).PersonType;
            if (personType == PersonCardType.Juridical)
            {
                isNatural = false;
            }
            bool check = false;
            String w = null;
            var catalogsIDs = personsTypeInfo.GroupBy(a => a.КодТемыЛица);
            List<int> personTypesIDs = PersonTypeAccessor.Accessor.GetPersonTypeIDListByPersonID(clientID);
            w += String.Format(@"
            <script language='javascript'>
             window.document.title = 'Типы лиц в каталогах'; 
            
            function SaveTypesNew()
            {{
                 var typeIDs = '';
                checkboxes = document.getElementsByName('checkBoxInput');
                for(var i=0, n=checkboxes.length;i<n;i++) {{
                    if(checkboxes[i].checked){{
                        typeIDs += checkboxes[i].id; typeIDs += ', ';
                    }}
                }}
                if(typeIDs == '' && '{2}' == 'False'){{  alert('У лица должен быть хотя бы один тип'); return; }};
                finalTypeIDs = typeIDs.substring(0, typeIDs.length - 2);
                dialogArguments.ViewModel.saveTypes(finalTypeIDs, {1});
                self.close();
                    
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
", Kesco.Persons.Web.Configuration.AppSettings.URI_Styles_Css, sectionID, isNatural);
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
                    check = personTypesIDs.Contains(personTypeInfo.КодТипаЛица);
                    if (i == 0)
                    {
                        w += String.Format(@"<tr class='themeTd'><td>{0}</td><td class='tdContent'>
                        <input id='{4}' name='checkBoxInput' type=checkbox {2} {3}>
                        {1}</td></tr>", personTypeInfo.ТемаЛица, personTypeInfo.Каталог, ((check || replyThemes == 1) ? "checked" : ""), ((replyThemes == 1) ? "disabled" : ""), personTypeInfo.КодТипаЛица);

                    }
                    else
                    {
                        w += String.Format(@"<tr class='themeTd'><td></td><td class='tdContent'>
                         <input id='{2}' name='checkBoxInput' type=checkbox {1}>
                         {0}</td></tr>", personTypeInfo.Каталог, ((check) ? "checked" : ""), personTypeInfo.КодТипаЛица);
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


            return w;
        }

        /// <summary>
        /// Вызов функции ассинхронного сохранения типов
        /// </summary>
        /// <param name="themes">Темы</param>
        /// <param name="idClient">Клиент</param>
        /// <param name="sectionID">Номер обновляемой секции</param>
        /// <returns></returns>
        public ActionResult AvailablePersonTypes(string themes, int idClient, int sectionID)
        {
            var ids = themes.ToArray<int>(new char[] { ',' }, s => Int32.Parse(s));
            var list = Repository.PersonTypes.GetAvailablePersonTypesByThemeIDs(themes);
            string types = "'" + String.Join(",", list.Select(m => m.КодТипаЛица)) + "'";

            var needToClarify = list.Count != ids.Length;
            if (!needToClarify)
            {
                if (String.IsNullOrEmpty(themes))
                {
                    return Content(String.Format(@"
                <script language='javascript'>
                window.dialogArguments.ViewModel.clearTypes({1});
                self.close();
                </script>
            ", types, sectionID));
                }
                return Content(String.Format(@"
                <script language='javascript'>
                window.dialogArguments.ViewModel.saveTypes({0}, {1});
                self.close();
                </script>
            ", types, sectionID));
            }
            return Content(RenderRecords(list, idClient, sectionID));

        }


        /// <summary>
        /// Проверка выбранных тем лиц.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="model">Модель.</param>
        /// <returns></returns>
        public ActionResult CheckPersonThemes(string control, PersonDossierModel model)
        {
            // 1. Необходимо проверить если хотя бы одна тема в нескольких каталогах
            string themes = model.PersonThemes ?? "";



            if (String.IsNullOrEmpty(themes))
            {
                PersonCardType personType = Repository.Persons.GetInstance(model.PersonID).PersonType;
                if (personType == PersonCardType.Juridical)
                {
                    return JavaScript(String.Format(@"alert('У лица должен быть хотя бы один тип');   ViewModel.dispatchModelCommand('{0}', '{1}');", "choosetypes", control));
                }
            }

            string script = String.Format(@"(function() {{
                    window.showModalDialog('{1}?themes={0}&idClient={2}&sectionID={3}', window.self, 'dialogHeight:300px;dialogWidth:500px;resizable:no;scroll:no;');
				}})()",
                themes,
                Url.FullPathAction("AvailablePersonTypes", "Dossier"),
                model.PersonID,
                control
            );

            return JavaScript(script);
        }
        /// <summary>
        /// Проверка выбранных тем лиц.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="model">Модель.</param>
        /// <returns></returns>
//        public ActionResult CheckPersonThemes(string control, PersonDossierModel model)
//        {
//            // 1. Необходимо проверить если хотя бы одна тема в нескольких каталогах
//            string themes = model.PersonThemes ?? "";
//            var ids = themes.ToArray<int>(new char[] { ',' }, s => Int32.Parse(s));
//            var list = Repository.PersonTypes.GetTypeIDsByThemeIDs(model.PersonThemes);

//            var needToClarify = list.Count != ids.Length;

//            string script = String.Format(@"(function() {{
//					var arr = {0};
//					var selected = '{1}';
//					var url = '{2}' + '?ids=' + selected+'&IdClient={4}';
//
//					if ({3}) {{
//						window.self.focus();
//						window.showModalDialog(url, '', 'dialogHeight:300px;dialogWidth:500px;resizable:yes;scroll:yes;');
//						var r2 = $.cookie('RetVal');
//
//						if( !r2 || r2 == 'false' ) return;
//						if( !r2 || r2 == 'true' ) {{ // сохранение каталогов сделали в старом приложении - просто передергиваем страницу
//							ViewModel.Processing(true);
//							window.location.href = window.location.href;
//							return;
//						}}
//						arr = r2.split(',');
//					}}
//
//					window.ViewModel.Model.PersonTypes(arr.join(','));
//					ViewModel.dispatchModelCommand('SaveTypes');
//				}})()",
//                Kesco.Web.Mvc.Json.Serialize(list, true),
//                themes,
//                Configuration.AppSettings.URI_person_catalogs,
//                Kesco.Web.Mvc.Json.Serialize(needToClarify),
//                model.PersonID
//            );

//            return JavaScript(script);
//        }


		/// <summary>
		/// Сохранение переданных тем лиц
		/// </summary>
		/// <param name="model">модель данных</param>
		/// <returns>скрипт с ответным действием</returns>
		public ActionResult SaveTypes(string control, PersonDossierModel model)
		{
			Repository.Persons.SavePersonTypes(model.PersonID, model.PersonTypes ?? "");

			// и вызываем перегрузку всей формы
			return JavaScript(@"
				ViewModel.Processing(true);
				window.location.href = window.location.href;
			");
		}

		/// <summary>
		/// Изменение статуса лица "проверено"
		/// </summary>
		/// <param name="model">модель данных</param>
		public ActionResult PersonChecked(string control, PersonDossierModel model)
		{
			Person person = Repository.Persons.GetInstance(model.PersonID);

			if (person == null)
				throw new ApplicationException(
					String.Format(Kesco.Persons.Web.Localization.Resources
						.ViewModel_Exception_JuridicalPerson_Requisites_PersonNotFound,
					model.PersonID
				));

			// Если флаг снимается, то проверяем в бухгалтерии
            //if (model.Verified ) {
            //    if (Repository.Persons.CheckPersonAbacusStatus(person.ID) || Repository.Persons.CheckPerson1SStatus(person.ID))
            //        throw new Kesco.Log.LogicalException(
            //            Kesco.Persons.Web.Localization.Resources
            //                .Dossier_CheckPersonAbacusAnd1S_Error,
            //            null,
            //            Assembly.GetExecutingAssembly().GetName()
            //        );
            //}

			try {
				if (person.PersonType == PersonCardType.Juridical) {
					Repository.Persons.TryToSaveJuridical(new JuridicalCardForSave {
						NewID = 0,
						WhatDo = SaveAction.РедактироватьЛицо,
						Check = model.Confirmed,
						КодЛица = person.ID,
						КодКарточки = 0,
						Кличка = person.Nickname ?? String.Empty,
						КодБизнесПроекта = person.BusinessProjectID,
						КодТерритории = person.TerritoryID,
						ГосОрганизация = person.IsStateOrganization ? 1 : 0,
						БИК = person.BIK ?? String.Empty,
						ИНН = person.INN ?? String.Empty,
						ОГРН = person.OGRN ?? String.Empty,
						ОКПО = person.OKPO ?? String.Empty,
						КорСчет = person.LoroConto ?? String.Empty,
						БИКРКЦ = person.BIKRKC ?? String.Empty,
						SWIFT = person.SWIFT ?? String.Empty,
						Примечание = person.Comment ?? String.Empty,
						Проверено = !model.Verified
					});
				}
				else if (person.PersonType == PersonCardType.Natural) {
					Repository.Persons.TryToSaveIndividual(new IndividualCardForSave {
						Check = model.Confirmed,
						NewID = 0,
						WhatDo = SaveAction.РедактироватьЛицо,
						ИмяЛат = String.Empty,
						ИмяРус = String.Empty,
						ИНН = person.INN ?? String.Empty,
						Кличка = person.Nickname ?? String.Empty,
						КодБизнесПроекта = person.BusinessProjectID,
						КодКарточки = 0,
						КодЛица = person.ID,
						КодТерритории = person.TerritoryID,
						КПП = String.Empty,
						ОГРН = person.OGRN ?? String.Empty,
						ОКВЭД = String.Empty,
						ОКОНХ = String.Empty,
						ОКПО = person.OKPO ?? String.Empty,
                        ДатаРождения = person.Birthday,
						ОтчествоЛат = String.Empty,
						ОтчествоРус = String.Empty,
						Пол = 'M',
						Примечание = person.Comment ?? String.Empty,
						Проверено = !model.Verified,
						ФамилияЛат = String.Empty,
						ФамилияРус = String.Empty
					});
				}
			} catch (SavePersonException ex) {
				string script2 = String.Format(@"(function() {{
					var duplicates = {3};
					var callbackUrl = encodeURIComponent('{0}');
					var url = '{1}&callbackUrl={{0}}';
					url = $.validator.format(url, callbackUrl);
					openPopupWindow(url, {{
							type: 'POST',
							Duplicates: duplicates
						}}, function (result) {{
							if (window.console) console.log(result);
							ViewModel.Model.Confirmed(false);
							if ($.isArray(result)) {{
								var person = result[0];
								if (person.value == {2}) {{ // если value == PersonID, создаём/сохраняем лицо
									ViewModel.Model.Confirmed(true);
									ViewModel.dispatchModelCommand('personchecked');
								}} else {{ // иначе закрываем и открываем похожее лицо
									//closeDialogAndReturnValue(JSON.stringify(result));
								}}

							}}
						}}, 'wnd_Duplicates', 670, 400);
				}})()",
					Url.FullPathAction("DialogResult", "Default"),
					Url.Action("Index", "NaturalDuplicates", new { id = model.PersonID, t = 1 }),
					model.PersonID,
					Kesco.Web.Mvc.Json.Serialize(
						ex.Issues
							.GroupBy(issue => issue.PersonID)
							.Select(gr => new {
								PersonID = gr.Key,
								Nickname = gr.First().Nickname,
								Issues = gr.Select(i => new {
									Field = i.Field,
									Granted = i.Granted,
									R = i.R,
									Value = i.Value
								}).ToList()
							})
						, true
					)
				);

				return JavaScript(script2);
			}
			// прочитаем заново лицо
			person = Repository.Persons.GetInstance(model.PersonID);

			// и устанавливаемвызываем перегрузку всей формы
			return JavaScript(String.Format(@"
					var person = {0};
					ViewModel.Model.Verified(person.Verified);
					ViewModel.CheckedBy(person.ChangedBy);
					ViewModel.CheckedDate(Globalize.format(person.ChangedDate.toLocalDate(), 'f'));
					window.location.href = window.location.href;
				",
				Kesco.Web.Mvc.Json.Serialize(person, true)
			 ));
		}

		/// <summary>
		/// подгружаются данные физического лица
		/// </summary>
		/// <param name="id">Id сотрудника - нужно для совместимости с подгружаемыми скриптами приложения "Пользователи"</param>
		public ActionResult UserProxy(int id)
		{
			System.Net.HttpWebRequest rq = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(Configuration.AppSettings.URI_user_form_simple + "?id=" + id);
			if (rq.CookieContainer == null) {
				rq.CookieContainer = new CookieContainer();
			}
			rq.CookieContainer.Add(new Cookie("tz", UserContext.ClientTimeZoneOffset.ToString(), "/", Configuration.AppSettings.Domain));
			rq.Method = "GET";
			rq.Credentials = System.Net.CredentialCache.DefaultCredentials;
			System.Net.WebResponse rs = rq.GetResponse();
			System.IO.Stream stream = rs.GetResponseStream();
			StreamReader readStream = new StreamReader(stream, System.Text.Encoding.UTF8);
			string s = readStream.ReadToEnd();

			// подмена относительных ссылок на абсолютные в приложении "USERS"
			//Regex r = new Regex("([^0-9a-zа-я\\/:-_.])([0-9a-zа-я\\/:-_.]{1,}.aspx)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
			//s = r.Replace(s, new MatchEvaluator(replaceURL));

//            s += @"
//<script language='javascript'>
//	function iframe_window_resize()
//	{
//		parent.document.all('ifrUser').style.height=document.body.scrollHeight + 5 + 'px';
//	}
//	window.attachEvent('onresize',iframe_window_resize);
//	window.attachEvent('onload',iframe_window_resize);
//	window.document.attachEvent('onclick',iframe_window_resize);
//	window.document.attachEvent('onkeypress',iframe_window_resize);
//</script>";
			return Content(s);
		}

		/// <summary>
		/// Синхронизация данных физ лица и сотрудника
		/// </summary>
		/// <param name="personId">Id лица</param>
		/// <param name="userId">Id сотрудника</param>
        public ActionResult SyncUser(string personId, string userId)
        {
            string script = String.Format(@"
				(function() {{
                    	var url = '{0}';
			            var value = url.substring(0, url.lastIndexOf('/') + 1);
			            value += 'personSynchronize.aspx?idPerson={1}&idEmployee={2}&date={3}';
			            DialogPageOpen(value, '', function () {{ window.location.href = window.location.href; }});
				}})();",
                 Configuration.AppSettings.URI_person_search_old,
                personId,
                userId,
                DateTime.Now.Millisecond.ToString(CultureInfo.InvariantCulture) + DateTime.Now.Second.ToString(CultureInfo.InvariantCulture) + DateTime.Now.Minute.ToString(CultureInfo.InvariantCulture)
            );
            return JavaScript(script);
        }

		private string replaceURL(Match m)
		{
			if (Regex.IsMatch(m.Groups[2].Value, "^http", RegexOptions.IgnoreCase)) return m.Value;
			return m.Groups[1].Value + Regex.Replace(Configuration.AppSettings.URI_user_search, "search.aspx", m.Groups[2].Value, RegexOptions.IgnoreCase);
		}
	}
}
