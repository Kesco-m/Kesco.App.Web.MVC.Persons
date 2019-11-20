using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kesco.Persons.Web.Models.NaturalDuplicates;
using Kesco.Web.Mvc.SharedViews;

namespace Kesco.Persons.Web.Controllers
{
	public class NaturalDuplicatesController : SharedModelController<DuplicatesModel>
    {
        //
        // GET: /NaturalDuplicates/

		public NaturalDuplicatesController()
			: base()
		{
			UseCompressHtml = true;
		}

        public ActionResult Index(int? id)
        {
			ViewModel model = new ViewModel();
			model.Model.PersonID = id ?? 0;
			if (ClientContext != null && ClientContext.Duplicates != null) {
				model.InitFromClientContext(ClientContext.Duplicates);
				//model.HidePersonTypesSection = true;
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
		protected override bool DoDispatch(string command, string control, DuplicatesModel model, out ActionResult result)
		{
			result = null;
			switch (command) {
				case "itlookslike":
					result = ItLooksLike(control, model);
					return true;
				default:
					return false;
			}
		}

		public ActionResult ItLooksLike(string control, DuplicatesModel model)
		{

			string script = String.Format(@"(function() {{
					openPopupWindow('{0}?id={1}', {{
							type: 'GET'
						}}, null, 'wnd_ChooseThemes', 800, 600, {{ close: false }});
				}})()",
				Kesco.Persons.Web.Configuration.AppSettings.URI_person_form,
				model.PersonID
			);

			return JavaScript(script);
		}
	}
}
