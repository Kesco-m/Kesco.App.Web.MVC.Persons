using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using FluentValidation.Mvc;
using FluentValidation.Results;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.Web.Models.Contact;
using Kesco.Web.Mvc.SharedViews;
using Kesco.Web.Mvc.SharedViews.Models;

namespace Kesco.Persons.Web.Controllers
{
    public partial class ContactController : SharedModelController<DataModel>
    {

        public ContactController()
            : base()
        {
            UseCompressHtml = true;
        }

        public virtual ActionResult Index(int? id, int? idclient)
        {
            var vm = new ViewModel();

            if (id.HasValue && id.Value != 0)
            {
                try
                {
                    vm.InitFromContact(id.Value);
                }
                catch (ApplicationException ex)
                {
                    var model = new ErrorObjectNotFound(Kesco.Localization.Resources.Errors_PageTitle, ex.Message);
                    return View("ErrorObjectNotFound", model);
                }
            }
            else if (idclient.HasValue)
                vm.InitFromPerson(idclient.Value);

            return View(vm);
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
        protected override bool DoDispatch(string command, string control, DataModel model, out ActionResult result)
        {
            result = null;
            switch (command.ToLower())
            {
                case "choosecountryphonecode":
                    result = ChooseCountryPhoneCode(control, model);
                    return true;
                case "choosecityphonecode":
                    result = ChooseCityPhoneCode(control, model);
                    return true;
                case "adjustphone":
                    result = AdjustPhone(control, model);
                    return true;
                case "updatecontacttext":
                    result = UpdateContactText(control, model);
                    return true;
                case "save":
                    result = Save(control, null, model);
                    return true;
                default:
                    return false;
            }
        }

        public ActionResult GetPersonAccessLevel(int? personID)
        {
            var level = 0;
            if (personID.HasValue)
            {
                level = (int)Repository.Persons.GetPersonAccessLevel(personID ?? 0);
            }

            string script = String.Format(@"
					(function() {{
						ViewModel.AccessLevel({0});
					}})();"
                    , level
                );

            return JavaScript(script);

        }

        /// <summary>
        /// Chooses the country phone code.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ActionResult ChooseCountryPhoneCode(string control, DataModel model)
        {

            string script = String.Format(@"
					(function() {{
						var _dlgRez=0;
						var url = '{0}&clid=71&return=1{1}&areatype=2';
						$.removeCookie('DlgRez');
						window.showModalDialog(url, '', 'dialogHeight:800px;dialogWidth:600px;resizable:yes;scroll:yes;');
						_dlgRez = $.cookie('DlgRez');
						if (_dlgRez == 1) {{
							var r2 = $.cookie('RetVal', {{ raw: true }});
							var val = $.ReadIdsFromCookie(r2);
							if (val.length) {{
								ViewModel.Model.AreaID(val[0]);
								ViewModel.dispatchModelCommand('AdjustPhone', {2});
							}}
						}}
					}})();"
                    , Configuration.AppSettings.URI_area_search
                    , String.IsNullOrEmpty(model.Contact.CountryPhoneCode) ? "" : "&_TELCODECOUNTRY=2" + model.Contact.CountryPhoneCode
                    , Kesco.Web.Mvc.Json.Serialize(control)
                );

            return JavaScript(script);
        }

        /// <summary>
        /// Chooses the city phone code.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ActionResult ChooseCityPhoneCode(string control, DataModel model)
        {

            string script = String.Format(@"
					(function() {{
						var _dlgRez=0;
						var url = '{0}&clid=72&return=1{1}&areatype=4{2}';
						$.removeCookie('DlgRez');
						window.showModalDialog(url, '', 'dialogHeight:800px;dialogWidth:600px;resizable:yes;scroll:yes;');
						_dlgRez = $.cookie('DlgRez');
						if (_dlgRez == 1) {{
							var r2 = $.cookie('RetVal', {{ raw: true }});
							var val = $.ReadIdsFromCookie(r2);
							if (val.length) {{
								ViewModel.Model.AreaID(val[0]);
								ViewModel.dispatchModelCommand('AdjustPhone', {3});
							}}
						}}
					}})();"
                    , Configuration.AppSettings.URI_area_search
                    , String.IsNullOrEmpty(model.Contact.CountryPhoneCode) ? "" : "&TELCODECOUNTRY=2" + model.Contact.CountryPhoneCode
                    , String.IsNullOrEmpty(model.Contact.CityPhoneCode) ? "" : "&_TELCODEINCOUNTRY=1" + model.Contact.CityPhoneCode
                    , Kesco.Web.Mvc.Json.Serialize(control)
                );

            return JavaScript(script);
        }

        public ActionResult AdjustPhone(string control, DataModel model)
        {
            string phoneCode = null;
            control = control ?? String.Empty;
            if (model.AreaID.HasValue)
            {
                phoneCode = Territories.BusinessLogic.Repository.Territories.GetPhoneCode(
                        model.AreaID.Value,
                        String.IsNullOrEmpty(model.Contact.CountryPhoneCode)
                    ) ?? String.Empty;
                if (control.EndsWith("CountryPhoneCode")) model.Contact.CountryPhoneCode = phoneCode;
                if (control.EndsWith("CityPhoneCode")) model.Contact.CityPhoneCode = phoneCode;
            }

            Kesco.Territories.BusinessLogic.AreaPhoneInfo area = new Territories.BusinessLogic.AreaPhoneInfo()
            {
                Направление = String.Empty,
                ТелКодВСтране = model.Contact.CityPhoneCode ?? String.Empty,
                ТелКодСтраны = model.Contact.CountryPhoneCode ?? String.Empty
            };
            string phone = model.Contact.PhoneNumber ?? String.Empty;

            ViewModel.AdjustPhoneNumber(ref area, ref phone);

            string script = String.Format(@"
					//(function() {{
						var phoneInfo = {0};
						if (window.console) console.log('phoneInfo', phoneInfo);
						ViewModel.Model.Contact.CountryPhoneCode(phoneInfo.CountryPhoneCode);
						ViewModel.Model.Contact.CityPhoneCode(phoneInfo.CityPhoneCode);
						ViewModel.Model.Contact.PhoneNumber(phoneInfo.PhoneNumber);
						ViewModel.Model.Direction(phoneInfo.Direction);
						updateContactText();
					//}})();"
                    , Kesco.Web.Mvc.Json.Serialize(new
                    {
                        CountryPhoneCode = area.ТелКодСтраны,
                        CityPhoneCode = area.ТелКодВСтране,
                        Direction = area.Направление,
                        PhoneNumber = phone
                    }, true)
                );

            return JavaScript(script);
        }

        public ActionResult Save(string control, string docview, DataModel model)
        {
            // Валидация только Fluent валидатором
            ModelState.Clear();

            ContactsValidator validator = new ContactsValidator();

            ValidationResult validationResults = validator.Validate(model.Contact);
            if (!validationResults.IsValid)
            {
                validationResults.AddToModelState(ModelState, null);
            }
            if (!ModelState.IsValid)
            {
                return JavaScriptAlert(
                    Kesco.Persons.Web.Localization.Resources.Validation_Contact_ErrorDlg_Title,
                    Kesco.Persons.Web.Localization.Resources.Validation_Contact_ErrorDlg_Message
                        + "<br clear='all'/><ul>" +
                        String.Join("\n",
                                GetModelErrorMessages().Select(e => String.Format("<li>{0}</li>", e))
                            )
                        + "</ul>");
            }

            model.Contact.ChangedBy = UserContext.EmployeeInfo.ID;
            model.Contact.ChangedDate = DateTime.UtcNow;

            var contact = new Kesco.Persons.ObjectModel.Contact
            {
                ID = model.Contact.ID,
                PersonID = model.Contact.PersonLinkID.HasValue ? ((int?)null) : model.Contact.PersonID,
                PersonLinkID = model.Contact.PersonLinkID,
                ContactTypeID = model.Contact.ContactTypeID.Value,
                ContactText = model.Contact.ContactText ?? string.Empty,
                ContactTextRL = model.Contact.ContactTextRL ?? string.Empty,

                CountryID = 0,
                Zip = string.Empty,
                Region = string.Empty,
                CityName = string.Empty,
                CityNameRus = string.Empty,
                Address = string.Empty,

                CountryPhoneCode = string.Empty,
                CityPhoneCode = string.Empty,
                PhoneNumber = string.Empty,
                PhoneNumberAdd = string.Empty,
                PhoneNumberCorporative = string.Empty,

                OtherContact = String.Empty,
                Comment = model.Contact.Comment ?? string.Empty
            };

            if (model.Contact.ContactTypeID.Value < 20)
            {
                contact.CountryID = model.Contact.CountryID ?? 0;
                contact.Zip = model.Contact.Zip ?? string.Empty;
                contact.Region = model.Contact.Region ?? string.Empty;
                contact.CityName = model.Contact.CityName ?? string.Empty;
                contact.CityNameRus = model.Contact.CityNameRus ?? string.Empty;
                contact.Address = model.Contact.Address ?? string.Empty;
            }

            if (model.Contact.ContactTypeID.Value >= 20 && model.Contact.ContactTypeID.Value < 40)
            {
                contact.CountryPhoneCode = model.Contact.CountryPhoneCode ?? string.Empty;
                contact.CityPhoneCode = model.Contact.CityPhoneCode ?? string.Empty;
                contact.PhoneNumber = model.Contact.PhoneNumber ?? string.Empty;
                contact.PhoneNumberAdd = model.Contact.PhoneNumberAdd ?? string.Empty;
                contact.PhoneNumberCorporative = model.Contact.PhoneNumberCorporative ?? string.Empty;
            }

            if (model.Contact.ContactTypeID.Value >= 40 && model.Contact.ContactTypeID.Value < 54)
            {
                contact.OtherContact = model.Contact.ContactText ?? string.Empty;
            }

            if (model.Contact.ContactTypeID.Value == 54)
            {
                contact.OtherContact = model.Contact.OtherContact ?? string.Empty;
            }
            if (model.Contact.ID != 0)
            {
                Repository.Contacts.Save(contact);
            }
            else
            {
                model.Contact.ID = Convert.ToInt32(Repository.Contacts.CreateContact(contact));
            }
            if ((docview ?? String.Empty) == "yes")
            {
                return DialogPageReturn("{0}{1}{2}".FormatWith(
                        model.Contact.ID,
                        "\u001F",
                        StringExtensions.Coalesco(model.Contact.ContactText, model.Contact.OtherContact)
                    ));
            }
            return ReturnDialogValue(model.Contact);
        }

        public ActionResult UpdateContactText(string control, DataModel model)
        {
            BusinessLogic.DataAccess.ContactAccessor.ContactTextParts parameters = new BusinessLogic.DataAccess.ContactAccessor.ContactTextParts();
            parameters.ТипКонтакта = model.Contact.ContactTypeID ?? 0;
            parameters.АдресИндекс = model.Contact.Zip;
            parameters.АдресОбласть = model.Contact.Region;
            parameters.АдресГород = model.Contact.CityName;
            parameters.АдресГородRus = model.Contact.CityNameRus;
            parameters.Адрес = model.Contact.Address;
            parameters.КодСтраны = model.Contact.CountryID ?? 0;
            parameters.ТелефонСтрана = model.Contact.CountryPhoneCode;
            parameters.ТелефонГород = model.Contact.CityPhoneCode;
            parameters.ТелефонНомер = model.Contact.PhoneNumber;
            parameters.ТелефонДоп = model.Contact.PhoneNumberAdd;
            parameters.ДругойКонтакт = model.Contact.OtherContact;

            var contactText = Repository.Contacts.UpdateContactText(parameters);

            string script = String.Format(@"
					var contactText = {0};
					ViewModel.Model.Contact.ContactText(contactText);
				", Kesco.Web.Mvc.Json.Serialize(contactText, true));

            return JavaScript(script);
        }

        public ActionResult Delete(string control, DataModel model)
        {
            if (model.Contact.ID == 0) return null;

            var contact = new Kesco.Persons.ObjectModel.Contact { ID = model.Contact.ID };

            Repository.Contacts.Delete(contact);

            // Возврат значения для обновления родительского окна после удаления
            return ReturnDialogValue(model.Contact.ID);
        }
    }
}
