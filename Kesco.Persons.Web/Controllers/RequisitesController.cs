using System;
using System.Linq;
using System.Web.Mvc;
using FluentValidation;
using FluentValidation.Mvc;
using FluentValidation.Results;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.BusinessLogic.Persons;
using Kesco.Persons.ObjectModel;
using Kesco.Persons.Web.Models.Requisites;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.SharedViews;

namespace Kesco.Persons.Web.Controllers
{
	public class RequisitesController : SharedModelController<Requisites>
	{

		public RequisitesController()
			: base()
		{
			UseCompressHtml = true;
		}

		public ActionResult Index(int? id, int? personID, int? areaID, int? type)
		{
			var model = new ViewModel();
            if (type != null) model.Model.OperationTypeId = type;
			if (ClientContext != null && ClientContext.Requisites != null) {
				model.InitFromClientContext(ClientContext.Requisites);
			} else if (id.HasValue && id.Value != 0) {
				model.InitFromCard(id.Value);
			} else if (personID.HasValue && personID.Value != 0) {
				model.InitFromPerson(personID.Value);
			} else if (areaID.HasValue && areaID.Value != 0) {
				model.InitFromTerritory(areaID.Value);
			} else {
				model.InitFromTerritory(Kesco.Territories.ObjectModel.Territory.Russia);
			}
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
		protected override bool DoDispatch(string command, string control, Requisites model, out ActionResult result)
		{
			result = null;
			switch (command) {
				case "save":
					result = Save(control, model);
					return true;
				default:
					return false;
			}
		}

		public ActionResult Save(string control, Requisites model)
		{
			string script = String.Empty;

			RequisitesValidator validator = new RequisitesValidator();

			ValidationResult validationResults = validator.Validate(model);
			if (!validationResults.IsValid) {
				validationResults.AddToModelState(ModelState, null);
			}
			if (!ModelState.IsValid) {
				return JavaScriptAlert(
					Kesco.Persons.Web.Localization.Resources.Validation_ErrorDlg_Title,
					Kesco.Persons.Web.Localization.Resources.Validation_Person_ErrorDlg_Message
						+"<br clear='all'/><ul>"+
						String.Join("\n", 
								GetModelErrorMessages().Select(e => String.Format("<li>{0}</li>", e))
							)
						+"</ul>");
			}

			// если указан идентификатор лица, то операция - создать/редактировать карточку
			if (model.PersonID.HasValue && model.PersonID.Value != 0) {
				Person person = Repository.Persons.GetInstance(model.PersonID.Value);

				if (person == null)
					throw new ApplicationException(
						String.Format(Kesco.Persons.Web.Localization.Resources
							.ViewModel_Exception_JuridicalPerson_Requisites_PersonNotFound,
						model.PersonID
					));

				try {
					var card = new JuridicalCardForSave {
						NewID = 0,
						WhatDo = (model.ID != 0) ? SaveAction.РедактироватьКарточку : SaveAction.СоздатьКарточку,
						//Check = control == "confirmed", // TODO: Переделать
                        Check = (model.OperationTypeId != 0),
						КодЛица = person.ID,
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
						Проверено = person.Verified,

						КодКарточки = model.ID,
						От = model.From ?? new DateTime(1980, 1, 1),
						До = (model.To ?? new DateTime(2049, 12, 31)).AddDays(1),

						КраткоеНазваниеЛат = model.ShortNameLat ?? String.Empty,
						КраткоеНазваниеРус = model.ShortNameRus ?? String.Empty,
						КраткоеНазваниеРусРП = model.ShortNameRusGenitive ?? String.Empty,
						КодЖД = model.RwID ?? String.Empty,
						АдресЮридический = model.AddressLegal ?? String.Empty,
						АдресЮридическийЛат = model.AddressLegalLat ?? String.Empty,

						ПолноеНазвание = String.Empty,
						ОКОНХ = String.Empty,
						ОКВЭД = String.Empty,
						КПП = String.Empty
					};

					if (person.TerritoryID == JuridicalCardForSave.Russia) {
						card.КодОргПравФормы = model.IncorporationFormID;
						card.ПолноеНазвание = model.FullName ?? String.Empty;
						card.ОКОНХ = model.OKONH ?? String.Empty;
						card.ОКВЭД = model.OKVED ?? String.Empty;
						card.КПП = model.KPP ?? String.Empty;
					}

					Repository.Persons.TryToSaveJuridical(card);
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
							ViewModel.Confirmed(false);
							if ($.isArray(result)) {{
								var person = result[0];
								if (person.value == {2}) {{ // если value == PersonID, создаём/сохраняем лицо
									ViewModel.Confirmed(true);
									ViewModel.save();
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
			}

			// иначе вернуть карточку
			return ReturnDialogValue(model);
		}

		public ActionResult Delete(Requisites model)
		{
			if (model.ID == 0) return null;

			var card = new Kesco.Persons.ObjectModel.PersonCardJuridical { ID = model.ID };
			Repository.JuridicalPersonCards.Delete(card);

			return ReturnDialogValue(model.ID);
		}

	}
}
