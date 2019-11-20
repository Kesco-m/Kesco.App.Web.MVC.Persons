using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using FluentValidation;
using FluentValidation.Mvc;
using FluentValidation.Results;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.ObjectModel;
using Kesco.Persons.Web.Models.Naturals;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.SharedViews;
using Kesco.Persons.BusinessLogic.DataAccess;
using Kesco.Persons.BusinessLogic.Persons;
using Kesco.Lib.Log;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Kesco.Employees.ObjectModel;

namespace Kesco.Persons.Web.Controllers
{
    public class NaturalController : SharedModelController<PersonModel>
    {

        public NaturalController()
            : base()
        {
            UseCompressHtml = true;
        }

        /// <summary>
        /// Indexes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="personID">The person ID.</param>
        /// <param name="idClient">The id client.</param>
        /// <param name="act">0 - данные, 1 - карточка</param>
        /// <param name="employerId">Id лица компании-работодателя</param>
        /// <returns></returns>
        public ActionResult Index(int? id, int? personID, int? idClient, int? act, int? employerId)
        {
            if (employerId == 0) employerId = null;
            personID = personID ?? idClient;
            ViewModel model = new ViewModel();
            if (ClientContext != null && ClientContext.Requisites != null)
                model.InitFromClientContext(ClientContext.Requisites);
            else if (id.HasValue && id.Value != 0)
            {
                model.InitFromCard(id.Value);
                if (personID != null)
                {
                    List<Employee> employeeList =
                        Repository.ResponsibleEmployees.GetResponsibleEmployeesByPersonId(Convert.ToInt32(personID));
                    foreach (var employee in employeeList)
                    {
                        model.Model.ResponsibleEmployees.Add(new PersonModel.SimplePersonModelClass()
                                                                 {
                                                                     FullName = employee.FullName,
                                                                     ID = employee.ID.ToString()
                                                                 });
                    }
                }
            }
                
            else if (personID.HasValue)
                model.InitFromPerson(personID.Value, act ?? 0);
            else
                model.Init();

            if (employerId.HasValue)
                model.Model.EmployerId = employerId;

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
            switch (command)
            {
                case "save":
                    result = Save(control, model);
                    return true;
                case "translitname":
                    result = TranslitName(control, model);
                    return true;
                case "chooseempls":
                    result = ChooseEmpls(control, model);
                    return true;
                default:
                    return false;
            }
        }


        private ActionResult TranslitName(string name, PersonModel model)
        {
            string transaltedName = null; PersonAccessor.Accessor.TraslitSentence(model.Card.FirstNameRus);
            string script = null;
            switch (name)
            {
                case "lastName":
                    transaltedName = PersonAccessor.Accessor.TraslitSentence(model.Card.LastNameRus);
                    script = String.Format(@"window.ViewModel.Model.Card.LastNameLat('{0}');", transaltedName);
                    break;
                case "firstName":
                    transaltedName = PersonAccessor.Accessor.TraslitSentence(model.Card.FirstNameRus);
                    script = String.Format(@"window.ViewModel.Model.Card.FirstNameLat('{0}');", transaltedName);
                    break;
                case "middleName":
                    transaltedName = PersonAccessor.Accessor.TraslitSentence(model.Card.MiddleNameRus);
                    script = String.Format(@"window.ViewModel.Model.Card.MiddleNameLat('{0}');", transaltedName);
                    break;
                default:
                    return null;
            }
           
            return JavaScript(script);
        }

        protected string coalesco(params string[] values)
        {
            string value = null;
            if (values != null)
                value = values.FirstOrDefault(val => !String.IsNullOrWhiteSpace(val));
            return value ?? String.Empty;
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


        /// <summary>
        /// Сохраняет карточку физ лица.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ActionResult Save(string control, PersonModel model)
        {
            PersonModelValidator validator = new PersonModelValidator(model.EditCard || !model.Card.PersonID.HasValue || model.Card.PersonID.Value == 0);

            ValidationResult validationResults = validator.Validate(model);
            if (!validationResults.IsValid)
            {
                validationResults.AddToModelState(ModelState, null);
            }
            ModelState["Model.PersonTypes.PersonThemeID"].Errors.Clear();
            if (!ModelState.IsValid)
            {
                return JavaScriptAlert(
                    Kesco.Persons.Web.Localization.Resources.Validation_ErrorDlg_Title,
                    Kesco.Persons.Web.Localization.Resources.Validation_Person_ErrorDlg_Message
                        + "<br clear='all'/><ul>" +
                        String.Join("\n",
                                GetModelErrorMessages().Select(e => String.Format("<li>{0}</li>", e))
                            )
                        + "</ul>");
            }

            // если указан идентификатор лица, то операция - создать/редактировать карточку
            // иначе вернуть карточку
            bool saveEmplsAndThemes = (model.Card.PersonID == null || model.Card.PersonID.Value == 0);
            try
            {

                //  Транслитерация
                if (!StringExtensions.HasOnlyLatinChars(model.Card.LastNameLat))
                    model.Card.LastNameLat = Repository.Persons.TraslitSentence(StringExtensions.Coalesco(model.Card.LastNameLat, model.Card.LastNameRus));
                if (!StringExtensions.HasOnlyLatinChars(model.Card.FirstNameLat))
                    model.Card.FirstNameLat = Repository.Persons.TraslitSentence(StringExtensions.Coalesco(model.Card.FirstNameLat, model.Card.FirstNameRus));
                if (!StringExtensions.HasOnlyLatinChars(model.Card.MiddleNameLat))
                    model.Card.MiddleNameLat = Repository.Persons.TraslitSentence(StringExtensions.Coalesco(model.Card.MiddleNameLat, model.Card.MiddleNameRus));

                List<FormatRegistration> FormatRegistrations = Repository.FormatRegistrations.GetAllFormats();
                if (!FormatRegistrations.Exists(f => f.ID == model.Card.TerritoryID))
                    model.Card.INN = model.Card.OKPO = "";

                var card = new IndividualCardForSave
                {
                    Check = model.Confirmed || (
                            model.EditCard
                                ? (model.Card.ID.HasValue && model.Card.ID.Value != 0)
                                : (model.Card.PersonID.HasValue && model.Card.PersonID.Value != 0)
                        ),
                    NewID = 0,
                    WhatDo = model.EditCard
                            ? ((model.Card.ID.HasValue && model.Card.ID.Value != 0)
                                ? SaveAction.РедактироватьКарточку
                                : SaveAction.СоздатьКарточку)
                            : ((model.Card.PersonID.HasValue && model.Card.PersonID.Value != 0)
                                ? SaveAction.РедактироватьЛицо
                                : SaveAction.СоздатьЛицо),
                    ДатаРождения = model.Card.Birthday,
                    От = model.Card.From ?? new DateTime(1980, 1, 1),
                    До = (model.Card.To ?? new DateTime(2049, 12, 31)).AddDays(1),

                    ИмяЛат = model.Card.FirstNameLat ?? String.Empty,
                    ИмяРус = model.Card.FirstNameRus ?? String.Empty,
                    ИНН = model.Card.INN ?? String.Empty,
                    Кличка = model.Card.Nickname ?? String.Empty,
                    КодБизнесПроекта = model.Card.BusinessProjectID,
                    КодКарточки = model.Card.ID ?? 0,
                    КодЛица = model.Card.PersonID ?? 0,
                    КодОргПравФормы = model.Card.IncorporationFormID,
                    КодТерритории = model.Card.TerritoryID,
                    ОГРН = model.Card.OGRN ?? String.Empty,
                    ОКПО = model.Card.OKPO ?? String.Empty,
                    ОтчествоЛат = model.Card.MiddleNameLat ?? String.Empty,
                    ОтчествоРус = model.Card.MiddleNameRus ?? String.Empty,
                    Пол = model.Card.Sex ?? 'М',
                    Примечание = model.Card.Comment ?? String.Empty,
                    Проверено = model.Card.Verified,
                    ФамилияЛат = model.Card.LastNameLat ?? String.Empty,
                    ФамилияРус = model.Card.LastNameRus ?? String.Empty,

                    ОКОНХ = String.Empty,
                    ОКВЭД = String.Empty,
                    КПП = String.Empty,
                    КодЖД = String.Empty,
                    АдресЮридический = model.Card.AddressLegal ?? String.Empty,
                    АдресЮридическийЛат = model.Card.AddressLegalLat ?? String.Empty,
                };

                if (model.Card.IncorporationFormID.HasValue)
                {
                    card.ОКОНХ = model.Card.OKONH ?? String.Empty;
                    card.ОКВЭД = model.Card.OKVED ?? String.Empty;
                    card.КПП = model.Card.KPP ?? String.Empty;
                    card.КодЖД = model.Card.RwID ?? String.Empty;
                }

                // если редактируем реквизиты, 
                // то всегда обновляем лицо
                if (model.EditCard)
                {
                    SaveAction action = card.WhatDo;
                    card.WhatDo = SaveAction.РедактироватьЛицо;
                    Repository.Persons.TryToSaveIndividual(card);
                    card.WhatDo = action;
                }
                model.Card.PersonID = Repository.Persons.TryToSaveIndividual(card);
            }
            catch (SavePersonException ex)
            {
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
                            .Select(gr => new
                            {
                                PersonID = gr.Key,
                                Nickname = gr.First().Nickname,
                                Issues = gr.Select(i => new
                                {
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

            // Создание связи между лицом-работником и работадателем
            if (model.EmployerId.HasValue && model.Card.PersonID.HasValue)
            {
                try
                {
                    PersonLinkAccessor.PersonLinkForSave sqlParams = new PersonLinkAccessor.PersonLinkForSave();
                    sqlParams.WhatDo = 0;
                    sqlParams.КодЛицаРодителя = model.EmployerId.Value;
                    sqlParams.КодЛицаПотомка = model.Card.PersonID.Value;
                    sqlParams.КодТипаСвязиЛиц = 1;
                    sqlParams.Параметр = 0;
                    Repository.Links.MergePersonLink(sqlParams);
                }
                catch (Exception ex)
                {
                    Logger.WriteEx(new DetailedException("Ошибка создания связи между лицом-работником и работадателем", ex));
                }
            }

            string errMsg = String.Empty;
            if (saveEmplsAndThemes)
            {
                try
                {
                    var responsibleEmployeesIDs = model.ResponsibleEmployees.Select(employee => Convert.ToInt32(employee.ID)).ToList();
                    Repository.Persons.MergeResponsibleEmployees(model.Card.PersonID.Value, String.Join(",", responsibleEmployeesIDs));
                    //Repository.Persons.AssignResponsibleEmployee(
                    //        model.Card.PersonID.Value,
                    //        UserContext.EmployeeInfo.ID
                    //    );
                }
                catch (Exception ex)
                {
                    errMsg = String.Format(
                            Resources.Resources.Persons_Natural_SaveErrorWhileSavingResps,
                            ex.Message
                        );
                    Logger.WriteEx(new DetailedException(errMsg, ex));
                }
                try
                {
                    Repository.Persons.SavePersonTypes(
                        model.Card.PersonID.Value,
                        String.Join(",", model.PersonTypes.PersonTypeIDs)
                    );
                }
                catch (Exception ex2)
                {
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


            var parametrs = Kesco.Web.Mvc.Json.Serialize(new
            {
                value = model.Card.PersonID,
                label = model.Card.Nickname
            }, true);

            string sectionID = HttpContext.Request["sectionId"] ??
                               HttpUtility.ParseQueryString(HttpContext.Request.UrlReferrer.ToString()).Get("sectionId");
            if (sectionID != null)
            {

                var values = JsonConvert.DeserializeObject<JObject>(parametrs);
                values.Add("sectionId", sectionID);
                parametrs = Kesco.Web.Mvc.Json.Serialize(values, true);
            }

            string script = String.Format(@"
					(function() {{
						var result = {0};
						var errMsg = {1};
						if (errMsg) {{
							alert(errMsg);
						}}
						returnResult(result);
					}})();",
                     parametrs, Kesco.Web.Mvc.Json.Serialize(errMsg)
                 );

            return JavaScript(script);
        }

        /// <summary>
        /// Удаление карточки лица
        /// </summary>
        public ActionResult Delete(PersonModel model)
        {
            if (!model.Card.ID.HasValue || model.Card.ID.Value == 0) return null;

            var card = new Kesco.Persons.ObjectModel.PersonCardNatural { ID = model.Card.ID.Value };
            Repository.NaturalPersonCards.Delete(card);

            // Возврат значения для обновления родительского окна после удаления
            return ReturnDialogValue(model.Card.ID);
        }



    }
}
