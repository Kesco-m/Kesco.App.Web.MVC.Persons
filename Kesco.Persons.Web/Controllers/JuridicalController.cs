using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentValidation;
using FluentValidation.Mvc;
using FluentValidation.Results;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.BusinessLogic.DataAccess;
using Kesco.Persons.ObjectModel;
using Kesco.Persons.Web.Models.Juridicals;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.SharedViews;
using Kesco.Persons.BusinessLogic.Persons;
using Kesco.Lib.Log;

namespace Kesco.Persons.Web.Controllers
{
	public class JuridicalController : SharedModelController<PersonModel>
    {
        //
        // GET: /Juridical/

		public JuridicalController()
			: base()
		{
			UseCompressHtml = true;
		}

		public class NewJuridicalForSaveModel
		{
			public PersonCardJuridical Card { get; set; }
			public int ResponsibleEmployeeID { get; set; }
			public List<int> PersonTypeIDs { get; set; }
			public bool Confirmed { get; set; }
		}


		public virtual ActionResult CreateNewJuridical(NewJuridicalForSaveModel model)
		{
			try {
				model.Card.Person.INN = model.Card.Person.INN == null || model.Card.Person.INN == "null" ? string.Empty : model.Card.Person.INN;
				model.Card.Person.BIK = model.Card.Person.BIK == null || model.Card.Person.BIK == "null" ? string.Empty : model.Card.Person.BIK;
				model.Card.Person.OGRN = model.Card.Person.OGRN == null || model.Card.Person.OGRN == "null" ? string.Empty : model.Card.Person.OGRN;
				model.Card.Person.OKPO = model.Card.Person.OKPO == null || model.Card.Person.OKPO == "null" ? string.Empty : model.Card.Person.OKPO;
				model.Card.Person.LoroConto = model.Card.Person.LoroConto == null || model.Card.Person.LoroConto == "null" ? string.Empty : model.Card.Person.LoroConto;
				model.Card.Person.BIKRKC = model.Card.Person.BIKRKC == null || model.Card.Person.BIKRKC == "null" ? string.Empty : model.Card.Person.BIKRKC;
				model.Card.Person.SWIFT = model.Card.Person.SWIFT == null || model.Card.Person.SWIFT == "null" ? string.Empty : model.Card.Person.SWIFT;
				model.Card.Person.Comment = model.Card.Person.Comment == null || model.Card.Person.Comment == "null" ? string.Empty : model.Card.Person.Comment;

				model.Card.IncorporationFormID = model.Card.IncorporationFormID == 0
													 ? null
													 : model.Card.IncorporationFormID;

				model.Card.ShortNameRus = model.Card.ShortNameRus == null || model.Card.ShortNameRus == "null" ? string.Empty : model.Card.ShortNameRus;
				model.Card.ShortNameRusGen = model.Card.ShortNameRusGen == null || model.Card.ShortNameRusGen == "null" ? string.Empty : model.Card.ShortNameRusGen;

				model.Card.ShortNameLat = model.Card.ShortNameLat == null || model.Card.ShortNameLat == "null" ? string.Empty : model.Card.ShortNameLat;
				model.Card.FullName = model.Card.FullName == null || model.Card.FullName == "null" ? string.Empty : model.Card.FullName;

				model.Card.OKONH = model.Card.OKONH == null || model.Card.OKONH == "null" ? string.Empty : model.Card.OKONH;
				model.Card.OKVED = model.Card.OKVED == null || model.Card.OKVED == "null" ? string.Empty : model.Card.OKVED;
				model.Card.KPP = model.Card.KPP == null || model.Card.KPP == "null" ? string.Empty : model.Card.KPP;
				model.Card.RwID = model.Card.RwID == null || model.Card.RwID == "null" ? string.Empty : model.Card.RwID;
				model.Card.AddressLegal = model.Card.AddressLegal == null || model.Card.AddressLegal == "null" ? string.Empty : model.Card.AddressLegal;
				model.Card.AddressLegalLat = model.Card.AddressLegalLat == null || model.Card.AddressLegalLat == "null" ? string.Empty : model.Card.AddressLegalLat;

				model.Card.Person.BusinessProjectID = model.Card.Person.BusinessProjectID == 0
														  ? null
														  : model.Card.Person.BusinessProjectID;

				model.Card.Person.TerritoryID = model.Card.Person.TerritoryID == 0
														  ? null
														  : model.Card.Person.TerritoryID;

				// сохранение лица
				var personID = Repository.Persons.SaveJuridicalCard(model.Card, model.Confirmed);

				// сохранение типов лиц
				if (model.PersonTypeIDs != null) {
					foreach (var personTypeID in model.PersonTypeIDs) {
						Repository.Persons.AssignPersonTypeToPerson(new PersonAccessor.PersonTypeForSave {
							WhatDo = 1,
							КодЛица = personID,
							КодТипаЛица = personTypeID
						});
					}
				}

				return JsonModel(new { PersonID = personID, Name = model.Card.Person.Nickname }, JsonRequestBehavior.AllowGet);
			} catch (SavePersonException siex) {
				return JsonError(siex.Message, siex.Issues, JsonRequestBehavior.AllowGet);

			} catch (Exception ex) {
				Logger.WriteEx(ex);
				return JsonError(ex.Message, ex.ToString(), JsonRequestBehavior.AllowGet);
			}
		}

		public ActionResult Index(int? id, int? personID, int? idClient)
        {
			personID = personID ?? idClient;
			ViewModel model = new ViewModel();
			if (personID.HasValue) {
				model.InitFromPerson(personID.Value);
				model.HidePersonTypesSection = true;
			} else
				model.Init();
			return View(model);
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
		protected override bool DoDispatch(string command, string control, PersonModel model, out ActionResult result)
		{
			result = null;
			switch (command) {
				case "save":
					result = Save(control, model);
					return true;
                case "chooseempls":
                    result = ChooseEmpls(control, model);
                    return true;
				default:
					return false;
			}
		}

        /// <summary>
        /// Вызов формы редактирования ответственных сотрудников
        /// </summary>
        /// <param name="model">модель</param>
        /// <returns>скрипт с ответным действием</returns>
        public ActionResult ChooseEmpls(string ids, PersonModel model)
        {
            string script = String.Format(@"
				(function() {{
					var selected = '{3}';
					var callbackUrl = encodeURIComponent('{0}');
					var title = encodeURIComponent('{4}');
					var url = '{1}?{2}&selectedid='+selected;
					url = $.validator.format(url, callbackUrl, title);

					openPopupWindow(url, null, function(result) {{
                    if ($.isArray(result)) {{
					ViewModel.Model.ResponsibleEmployees.removeAll();
					for(var i=0; i<result.length; i++)
						if(result[i] != null)ViewModel.Model.ResponsibleEmployees.push({{ID: ko.observable(result[i].value), FullName: ko.observable(result[i].label) }});
				    }}
					}}, 'wnd_ChooseEmployees', 800, 600);
				}})();",
            Url.FullPathAction("DialogResult", "Default"),
            Configuration.AppSettings.URI_user_search,
            Configuration.URI_user_search_QS ?? "t=1",
            ids,
                //model.ResponsibleEmployees.Join(',').TrimEnd(new char[] { ',' }),
            global::Resources.Resources.Kesco_Persons_Web_VW_1000
            );

            return JavaScript(script);
        }

		public ActionResult Save(string control, PersonModel model)
		{
			PersonModelValidator validator = new PersonModelValidator(model.Card.PersonID == null || model.Card.PersonID.Value == 0);

			ValidationResult validationResults = validator.Validate(model);
			if (!validationResults.IsValid) {
				validationResults.AddToModelState(ModelState, null);
			}
            ModelState["Model.PersonTypes.PersonThemeID"].Errors.Clear();
			if (!ModelState.IsValid) {
				return JavaScriptAlert(
					Kesco.Persons.Web.Localization.Resources.Validation_ErrorDlg_Title,
					Kesco.Persons.Web.Localization.Resources.Validation_Person_ErrorDlg_Message
                       
						+ "<br clear='all'/><ul>" +
						String.Join("\n",
								GetModelErrorMessages().Select(e => String.Format("<li>{0}</li>", e))
							)
						+ "</ul>");
			}

			List<FormatRegistration> FormatRegistrations = Repository.FormatRegistrations.GetAllFormats();
			if (!FormatRegistrations.Exists(f => f.ID == model.Card.TerritoryID))
				model.Card.INN = model.Card.OKPO = "";

			// если указан идентификатор лица, то операция - редактировать карточку
			// иначе вернуть карточку
			bool saveEmplsAndThemes = (model.Card.PersonID == null || model.Card.PersonID.Value == 0);
			try {
				model.Card.PersonID = Repository.Persons.TryToSaveJuridical(new JuridicalCardForSave {
					NewID = 0,
					WhatDo = (model.Card.PersonID.HasValue && model.Card.PersonID.Value != 0)
						? SaveAction.РедактироватьЛицо 
						: SaveAction.СоздатьЛицо,
					Check = model.Confirmed || (model.Card.PersonID.HasValue && model.Card.PersonID.Value != 0),
					КодЛица = model.Card.PersonID ?? 0,
					КодКарточки = model.Card.ID ?? 0,
					Кличка = model.Card.Nickname ?? String.Empty,
					КодБизнесПроекта = model.Card.BusinessProjectID,
					КодТерритории = model.Card.TerritoryID,
					ГосОрганизация = model.Card.IsStateOrganization ? 1 : 0,
					БИК = model.Card.BIK ?? String.Empty,
					ИНН = model.Card.INN ?? String.Empty,
					ОГРН = model.Card.OGRN ?? String.Empty,
					ОКПО = model.Card.OKPO ?? String.Empty,
					КорСчет = model.Card.LoroConto ?? String.Empty,
					БИКРКЦ = model.Card.BIKRKC ?? String.Empty,
					SWIFT = model.Card.SWIFT ?? String.Empty,
					Примечание = model.Card.Comment ?? String.Empty,
					Проверено = model.Card.Verified,

					От = model.Card.Requisites.From ?? new DateTime(1980, 1, 1),
					До = (model.Card.Requisites.To ?? new DateTime(2049, 12, 31)).AddDays(1),
					КодОргПравФормы = model.Card.Requisites.IncorporationFormID,
					КраткоеНазваниеЛат = model.Card.Requisites.ShortNameLat ?? String.Empty,
					КраткоеНазваниеРус = model.Card.Requisites.ShortNameRus ?? String.Empty,
					КраткоеНазваниеРусРП = model.Card.Requisites.ShortNameRusGenitive ?? String.Empty,
					ПолноеНазвание = model.Card.Requisites.FullName ?? String.Empty,
					ОКОНХ = model.Card.Requisites.OKONH ?? String.Empty,
					ОКВЭД = model.Card.Requisites.OKVED ?? String.Empty,
					КПП = model.Card.Requisites.KPP ?? String.Empty,
					КодЖД = model.Card.Requisites.RwID ?? String.Empty,
					АдресЮридический = model.Card.Requisites.AddressLegal ?? String.Empty,
					АдресЮридическийЛат = model.Card.Requisites.AddressLegalLat ?? String.Empty
				});
			} catch (System.Data.SqlClient.SqlException ex) {
				
			}
			catch (SavePersonException ex) {
				string script2 = String.Format(@"(function() {{
					var duplicates = {3};
					var callbackUrl = encodeURIComponent('{0}');
					var url = '{1}&callbackUrl={{0}}';
					url = $.validator.format(url, callbackUrl);
					openPopupWindow(url, {{
							type: 'POST',
							Duplicates: duplicates
						}}, function (result) {{
							
							ViewModel.Model.Confirmed(false);
							if ($.isArray(result)) {{
								var person = result[0];
								if (person.value == {2}) {{ // если value == PersonID, создаём/сохраняем лицо
									ViewModel.Model.Confirmed(true);
									ViewModel.save();
								}} else {{ // иначе закрываем и открываем похожее лицо
									closeDialogAndReturnValue(JSON.stringify(result));
								}}
								
							}}
						}}, 'wnd_Duplicates', 670, 400);
				}})()",
					Url.FullPathAction("DialogResult", "Default"),
					Url.Action("Index", "NaturalDuplicates", new { id = model.Card.PersonID ?? 0, t = 1 }),
					model.Card.PersonID ?? 0,
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

			string errMsg = String.Empty;
			if (saveEmplsAndThemes) {
				try {
                    var responsibleEmployeesIDs = model.ResponsibleEmployees.Select(employee => Convert.ToInt32(employee.ID)).ToList();
                    Repository.Persons.MergeResponsibleEmployees(model.Card.PersonID.Value, String.Join(",", responsibleEmployeesIDs));
                    //Repository.Persons.AssignResponsibleEmployee(
                    //        model.Card.PersonID.Value,
                    //        UserContext.EmployeeInfo.ID
                    //    );
				} catch (Exception ex) {
					errMsg = String.Format(
							Resources.Resources.Persons_Natural_SaveErrorWhileSavingResps,
							ex.Message
						);
					Logger.WriteEx(new DetailedException(errMsg, ex));
				}
				try {
					Repository.Persons.SavePersonTypes(
						model.Card.PersonID.Value,
						String.Join(",", model.PersonTypes.PersonTypeIDs)
					);
				} catch (Exception ex2) {
					var errMsg2 = String.Format(
							Resources.Resources.Persons_Natural_SaveErrorWhileSavingPersonTypes,
							ex2.Message
						);
					Logger.WriteEx(new DetailedException(errMsg2, ex2));
					if (errMsg != String.Empty)
						errMsg += "\n\n";
					errMsg += errMsg2;
				}
			}
            string sectionID = HttpContext.Request["sectionId"] ??
                               HttpUtility.ParseQueryString(HttpContext.Request.UrlReferrer.ToString()).Get("sectionId");
			string script = String.Format(@"
					(function() {{
						var result = {0};
						var errMsg = {1};
						if (errMsg) {{
							alert(errMsg);
						}}
						returnResult(result);
					}})();",
					 Kesco.Web.Mvc.Json.Serialize(new {
						 value = model.Card.PersonID,
						 label = model.Card.Nickname,
                         sectionId = sectionID
					 }, true),
					 Kesco.Web.Mvc.Json.Serialize(errMsg)
				 );

			return JavaScript(script);
		}





	}
}
