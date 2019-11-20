using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Kesco.Persons.Controls.ComponentModel;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.SharedViews;

namespace Kesco.Persons.Controls.Controllers
{
	public partial class PersonSelectUsageController : SharedModelController<PersonSelectUsageController.DataModel>
    {
		public class DataModel
		{

			[Display(Name="Поставщик")]
			[PersonSelect(CLID = 62)]
			public int? ShipperID { get; set; }

			[Display(Name = "Клиент")]
			[PersonSelect(CLID = 62)]
			public int? ClientID { get; set; }

			//[Display(Name = "Адрес поставщика", GroupName="Поставщик")]
			//[PersonContactSelect]
			//[PersonContactSearchParameters(ContactTypeIDList="1")]
			//public int? ShipperAddressID { get; set; }

			//[Display(Name = "Телефон поставщика", GroupName="Поставщик")]
			//[PersonContactSelect(Action=PersonContactAction.MakeCall)]
			//[PersonContactSearchParameters(
			//    PersonID=506,
			//    ContactTypeIDList = "20,21,22,30,31,32,33,34"
			//    )]
			//public int? ShipperPhoneID { get; set; }

			//[Display(Name = "Ответственный")]
			//[PersonSimpleSelect(CLID = 62)]
			//public int ResponsibleID { get; set; }

			//[Display(Name = "Банк")]
			//[PersonSimpleSelect(CLID = 62)]
			//public int? BankID { get; set; }

		}

		public class ViewModel : ViewModel<DataModel>
		{
			protected override void CreateSettings() { }
		}

		//
        // GET: /PersonSelectUsage/

		public virtual ActionResult Index()
        {
			var viewModel = new ViewModel();
			viewModel.Model.ShipperID = 506;
			//viewModel.Model.ResponsibleID = 1603;
			return View(viewModel);
		}

		protected virtual bool DoDispatch(string command, string control, PersonSelectUsageController.DataModel model, out ActionResult result)
		{
			result = null;
			return false;
		}
    }
}
