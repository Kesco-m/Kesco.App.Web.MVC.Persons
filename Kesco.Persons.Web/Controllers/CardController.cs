using System;
using System.Linq;
using System.Web.Mvc;
using FluentValidation.Mvc;
using FluentValidation.Results;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.Web.Models.Card;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.SharedViews;

namespace Kesco.Persons.Web.Controllers
{
	public class CardController : SharedModelController<DataModel>
	{

		public CardController()
			: base()
		{
			UseCompressHtml = true;
		}

		public ActionResult Index(int? idclient)
		{
			var model = new ViewModel();

			if (idclient.HasValue)
				model.InitFromPerson(idclient.Value);

			return View(model);
		}

		/// <summary>
		/// Does the dispatch.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="control">The control.</param>
		/// <param name="model">The model.</param>
		/// <param name="result">The result.</param>
		/// <returns></returns>
		protected override bool DoDispatch(string command, string control, DataModel model, out ActionResult result)
		{
			result = null;
			switch (command.ToLower()) {
				case "check":
					result = Check(control, model);
					return true;
				default:
					return false;
			}
		}

		public ActionResult Check(string control, DataModel model)
		{
			// Валидация только Fluent валидатором
			ModelState.Clear();

			Validator validator = new Validator();

			ValidationResult validationResults = validator.Validate(model);
			if (!validationResults.IsValid) {
				validationResults.AddToModelState(ModelState, null);
			}
			if (!ModelState.IsValid) {
				return JavaScriptAlert(
					"&nbsp;",
					"<br clear='all'/><ul>" +
						String.Join("\n",
								GetModelErrorMessages().Select(e => String.Format("<li>{0}</li>", e))
							)
						+ "</ul>");
			}

			var результатПроверки = Repository.Persons.CheckDateRange(model.PersonID, model.From, model.To);

			if (результатПроверки.Валидность == 0 || результатПроверки.От >= new DateTime(2050, 1, 1, 0, 0, 0, DateTimeKind.Utc)) {
				return JavaScriptAlert(
						Kesco.Persons.Web.Localization.Resources.Validation_Card_ErrorDlg_Title,
						Kesco.Persons.Web.Localization.Resources.Validation_Card_ErrorDlg_Message
					);
			}

			if (результатПроверки.От.Value == new DateTime(1980, 1, 1, 0, 0, 0, DateTimeKind.Utc)) результатПроверки.От = null;

			if (результатПроверки.До.Value == new DateTime(2050, 1, 1, 0, 0, 0, DateTimeKind.Utc)) результатПроверки.До = null;
			else результатПроверки.До = результатПроверки.До.Value.AddDays(-1); // До -> По
			
			return ReturnDialogValue(new {
				PersonID = результатПроверки.КодЛица,
				From = результатПроверки.От,
				To = результатПроверки.До 
			});
		}

	}
}
