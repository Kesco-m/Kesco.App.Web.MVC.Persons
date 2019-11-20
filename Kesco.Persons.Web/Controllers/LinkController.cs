using System;
using System.Linq;
using System.Web.Mvc;
using FluentValidation.Mvc;
using FluentValidation.Results;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.BusinessLogic.DataAccess;
using Kesco.Persons.Web.Models.PersonLink;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.SharedViews;
using Kesco.Web.Mvc.SharedViews.Models;

namespace Kesco.Persons.Web.Controllers
{
	public partial class LinkController : SharedModelController<PersonLink>
	{

		public LinkController()
			: base()
		{
			UseCompressHtml = true;
		}

		public virtual ActionResult Index(int? id, int? PersonLinkParent, int? PersonLinkChild, int? PersonLinkType)
		{
			var vm = new ViewModel();

			if(id.HasValue && id.Value != 0)
			{
				try {
					vm.InitFromLink(id.Value);
				} catch (ApplicationException ex) {
					var model = new ErrorObjectNotFound(Kesco.Localization.Resources.Errors_PageTitle, ex.Message);
					return View("ErrorObjectNotFound", model);
				}
			}
			else
			{
				if (PersonLinkParent.HasValue) vm.Model.ParentPersonID = PersonLinkParent.Value;
				if (PersonLinkChild.HasValue) vm.Model.ChildPersonID = PersonLinkChild.Value;
				if (PersonLinkType.HasValue) vm.Model.PersonLinkTypeID = PersonLinkType.Value;
			}

			// То лицо, для которого выводим связь будет нередактируемым - сохраняем признак
			vm.ParentLinkType = PersonLinkParent.HasValue;

			return View(vm);
		}

		public ActionResult Save(string control, PersonLink model)
		{
			// Валидация только Fluent валидатором
			ModelState.Clear();

			PersonLinkValidator validator = new PersonLinkValidator(model.PersonLinkTypeID);

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

			model.ChangedBy = UserContext.EmployeeInfo.ID;
			model.ChangedDate = DateTime.UtcNow;

			PersonLinkAccessor.PersonLinkForSave sqlParams = new PersonLinkAccessor.PersonLinkForSave();
			sqlParams.WhatDo = model.ID == 0 ? 0 : 1;
			if (model.ID != 0) sqlParams.КодСвязиЛиц = model.ID;
			sqlParams.КодЛицаРодителя = model.ParentPersonID.Value;
			sqlParams.КодЛицаПотомка = model.ChildPersonID.Value;
			sqlParams.КодТипаСвязиЛиц = model.PersonLinkTypeID;
			if (model.From.HasValue) sqlParams.От = model.From.Value;
			if (model.To.HasValue) sqlParams.До = model.To.Value.AddDays(1);
			sqlParams.Описание = model.Description ?? String.Empty;
			sqlParams.Параметр = model.Parameter;

			Repository.Links.MergePersonLink(sqlParams);

			return ReturnDialogValue(model);
		}

		public ActionResult Delete(PersonLink model)
		{
			if(model.ID==0) return null;

			PersonLinkAccessor.PersonLinkForSave sqlParams = new PersonLinkAccessor.PersonLinkForSave();
			sqlParams.WhatDo = 2;
			sqlParams.КодСвязиЛиц = model.ID;
			sqlParams.КодЛицаРодителя = model.ParentPersonID.Value;
			sqlParams.КодЛицаПотомка = model.ChildPersonID.Value;

			Repository.Links.MergePersonLink(sqlParams);

			// Возврат значения для обновления родительского окна после удаления
			return ReturnDialogValue(model.ID);
		}

		public ActionResult GetWordCases(string phrase)
		{
			return JsonModel(
					Repository.Cases.Search(new CaseAccessor.SearchParameters { Search = phrase })
						.ToDictionary(c => c.Nominative, c => c),
					JsonRequestBehavior.AllowGet
				);
		}
	}
}
