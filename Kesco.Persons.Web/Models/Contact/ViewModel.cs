using System;
using System.Collections.Generic;
using BLToolkit.Reflection;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.ObjectModel;
using Kesco.Web.Mvc;
using System.Text.RegularExpressions;
using System.Web;

namespace Kesco.Persons.Web.Models.Contact
{
	public class ViewModel : ViewModel<DataModel>
	{
		/// <summary>
		/// Количество связей - при количестве == 0 скрывается контрол выбора связи
		/// </summary>
		public int LinksCount { get; internal set; }

		/// <summary>
		/// Есть права на редактирвоание или нет?.
		/// </summary>
		public int AccessLevel { get; internal set; }

		/// <summary>
		/// Типы контактов - используется при рисовании поля выбора
		/// </summary>
		public List<ContactType> ContactTypes { get; internal set; }

		public ViewModel()
		{
			AccessLevel = 0;

			var request = HttpContext.Current.Request;
			if (request != null) {
				if (request["personContactText"] != null) {
					var phone = request["personContactText"];
					Model.Contact.PhoneNumber = request["personContactText"];
					Model.Contact.ContactText = request["personContactText"];
					Model.Contact.OtherContact = request["personContactText"];

					Kesco.Territories.BusinessLogic.AreaPhoneInfo area = new Territories.BusinessLogic.AreaPhoneInfo();
					AdjustPhoneNumber(ref area, ref phone);
					Model.Direction = area.Направление;

					Model.Contact.CountryPhoneCode = area.ТелКодСтраны;
					Model.Contact.CityPhoneCode = area.ТелКодВСтране;
					Model.Contact.PhoneNumber = phone;

				}
			}

			InitContactTypes();
		}

		protected override void CreateSettings()
		{
			settings = TypeAccessor<ClientParameters>.CreateInstanceEx();
		}

		public void InitContactTypes()
		{
			ContactTypes = Repository.ContactTypes.GetListByIds(
					HttpContext.Current.Request["personContactType"],
					HttpContext.Current.Request["personContactCategor"]
				);
			if (ContactTypes.Count == 1) {
				Model.Contact.ContactTypeID = ContactTypes[0].ID;
			}
		}

		public void InitFromContact(int contactId)
		{
			var contact = Repository.Contacts.GetInstance(contactId);
			if (contact == null)
				throw new ApplicationException(String.Format(Resources.Resources.Persons_Contact_NotFound, contactId));

			Model.Contact.ID = contact.ID;
			Model.Contact.PersonLinkID = contact.PersonLinkID;
			Model.Contact.ContactTypeID = contact.ContactTypeID;
			Model.Contact.ContactText = contact.ContactText;
			Model.Contact.ContactTextRL = contact.ContactTextRL;
			Model.Contact.CountryID = contact.CountryID;
			Model.Contact.Zip = contact.Zip;
			Model.Contact.Region = contact.Region;
			Model.Contact.CityName = contact.CityName;
			Model.Contact.CityNameRus = contact.CityNameRus;
			Model.Contact.Address = contact.Address;
			Model.Contact.CountryPhoneCode = contact.CountryPhoneCode;
			Model.Contact.CityPhoneCode = contact.CityPhoneCode;
			Model.Contact.PhoneNumber = contact.PhoneNumber;
			Model.Contact.PhoneNumberAdd = contact.PhoneNumberAdd;
			Model.Contact.PhoneNumberCorporative = contact.PhoneNumberCorporative;
			Model.Contact.OtherContact = contact.OtherContact;
			Model.Contact.Comment = contact.Comment;
			Model.Contact.ChangedBy = contact.ChangedBy;
			Model.Contact.ChangedDate = contact.ChangedDate;

			Kesco.Territories.BusinessLogic.AreaPhoneInfo area = new Territories.BusinessLogic.AreaPhoneInfo(){
				Направление = String.Empty,
				ТелКодВСтране = contact.CityPhoneCode ?? String.Empty,
				ТелКодСтраны = contact.CountryPhoneCode ?? String.Empty
			};
			string phone = contact.PhoneNumber ?? String.Empty;
			AdjustPhoneNumber(ref area, ref phone);

			Model.Direction = area.Направление;
			if (contact.PersonID.HasValue)
				InitFromPerson(contact.PersonID.Value);
			if (contact.PersonLinkID.HasValue)
				InitFromPersonLink(contact.PersonLinkID.Value);
		}

		public static void AdjustPhoneNumber(ref Kesco.Territories.BusinessLogic.AreaPhoneInfo area, ref string phone)
		{
			string number = Regex.Replace(area.ТелКодСтраны + area.ТелКодВСтране + phone, @"\D", "");
			phone = String.Empty;

			//сделаем замены в номере
			if (area.ТелКодСтраны.Trim().Length == 0 && area.ТелКодВСтране.Trim().Length == 0)
			{
				if (number.StartsWith("810"))
					number = "+" + number.Remove(0, 3);
				else if (number.StartsWith("00"))
					number = "+" + number.Remove(0, 2);

				if (number.StartsWith("+"))
					number = number.Remove(0, 1);
			}

			if (number.Length == 0) return;

			if (area.ТелКодСтраны.Trim().Length == 0 && area.ТелКодВСтране.Trim().Length > 0 ) return;

			var areaInfo2 = Territories.BusinessLogic.Repository.Territories.GetAreaPhoneInfo(number);

			if(areaInfo2 != null)
			{
				area = areaInfo2;

				string countryPhoneCode = area.ТелКодСтраны;
				int CodeLength = area.ДлинаКодаВСтране ?? 0;

				string PhoneWithoutCountry = number;
				if (PhoneWithoutCountry.StartsWith(countryPhoneCode)) {
					PhoneWithoutCountry = number.Remove(0, countryPhoneCode.Length);
					area.ТелКодВСтране = PhoneWithoutCountry.Substring(0, Math.Min(CodeLength, PhoneWithoutCountry.Length));
				}

				string phoneCode = string.Concat(countryPhoneCode, area.ТелКодВСтране);
				string PhoneNum = number;
				if (number.StartsWith(phoneCode)) {
					PhoneNum = number.Remove(0, phoneCode.Length);
				}
				if (PhoneNum.Length > 0) {
					string Phone2 = "";
					//возмьем из исходного номера столько цифр с конца пока не получится номер Phone2
					for (int i = 0; i < number.Length; i++) {
						phone = number[number.Length - 1 - i] + phone;

						if (char.IsDigit(number[number.Length - 1 - i]))
							Phone2 = number[number.Length - 1 - i] + Phone2;

						if (PhoneNum == Phone2)
							break;
					}
				}

				int length = Math.Min(CodeLength, area.ТелКодВСтране.Length);
				phone = area.ТелКодВСтране.Substring(length, area.ТелКодВСтране.Length - length) + phone;
				area.ТелКодВСтране = area.ТелКодВСтране.Substring(0, length);
			}
		}

		public void InitFromPersonLink(int personLinkID)
		{
			ObjectModel.PersonLink link = Repository.Links.GetInstance(personLinkID);
			if (link != null) {
				InitFromPerson(link.ParentPersonID);
			}
		}

		public void InitFromPerson(int personID)
		{
			Model.Person = Repository.Persons.GetInstance(personID);
			if (Model.Person != null) {
				AccessLevel = (int) Repository.Persons.GetPersonAccessLevel(personID);
				Model.Contact.PersonID = personID;
				LinksCount = GetLinksCount(personID);
			}
		}

		private int GetLinksCount(int personID)
		{
			return Repository.Links.GetPersonLinksByID(personID).Count;
		}
	}
}