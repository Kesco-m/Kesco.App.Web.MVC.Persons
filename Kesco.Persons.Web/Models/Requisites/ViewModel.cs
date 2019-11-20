using System;
using System.Web;
using System.Web.Mvc;
using BLToolkit.Reflection;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.ObjectModel;
using Kesco.Territories.ObjectModel;
using Kesco.Web.Mvc;
using Kesco.Persons.BusinessLogic.DataAccess;

namespace Kesco.Persons.Web.Models.Requisites
{
	public class ViewModel : ViewModel<Requisites>
	{

		/// <summary>
		/// Настройки пользователя для формы, хранящиеся в БД
		/// </summary>
		public ClientParameters Params { get { return settings as ClientParameters; } }

		public string PageTitle { get; set; }

		public Person Person { get; set; }

		public Territory Territory { get; set; }

		public bool Confirmed { get; set; }

		/// <summary>
		/// Количество карточек лица
		/// </summary>
		public int CardsNumber { get; set; }

		protected override void CreateSettings()
		{
			settings = TypeAccessor<ClientParameters>.CreateInstanceEx();
		}

		public ViewModel() : base()
		{
			PageTitle = String.IsNullOrWhiteSpace(HttpContext.Current.Request["title"])
						? ((Model.TerritoryID == 0)
							? Kesco.Persons.Web.Localization.Resources.Views_JuridicalPerson_Requisites_PageTitle_AddNew
							: Kesco.Persons.Web.Localization.Resources.Views_JuridicalPerson_Requisites_PageTitle_Edit)
						: HttpContext.Current.Request["title"];
			HelpTopic = "Requisites";
			Model.TerritoryID = Territory.Russia;
		}

		public void InitFromClientContext(dynamic requisites)
		{
			Kesco.Guard.IsNotNull(requisites, "requisites");

			Model.ID = requisites.ID;
			if (requisites.From != null)
			{
				DateTime from = PersonCard.MinFromDate;
				if (!(requisites.From is DateTime))
				{
					string s = ((object)requisites.From).ToString();
					from = DateTime.Parse(s);
				}
				if (from == PersonCard.MinFromDate)
					Model.From = null;
				else
					Model.From = from;
			}

			if (requisites.To != null)
			{
				DateTime to = PersonCard.MaxToDate;
				if (!(requisites.To is DateTime))
				{
					string s = ((object)requisites.To).ToString();
					to = DateTime.Parse(s);
				}
				if (to == PersonCard.MinFromDate)
					Model.To = null;
				else
					Model.To = to;
			}

			Model.ShortNameLat = requisites.ShortNameLat;
			Model.ShortNameRus = requisites.ShortNameRus;
			Model.FullName = requisites.FullName;
			Model.IncorporationFormID = requisites.IncorporationFormID;
			Model.KPP = requisites.KPP;
			Model.OKONH = requisites.OKONH;
			Model.OKVED = requisites.OKVED;
			Model.PersonType = PersonCardType.Juridical;
			Model.RwID = requisites.RwID;
			Model.ShortNameRusGenitive = requisites.ShortNameRusGenitive;
			Model.AddressLegal = requisites.AddressLegal;
			Model.AddressLegalLat = requisites.AddressLegalLat;
			Model.ChangedBy = requisites.ChangedBy;
			Model.ChangedDate = requisites.ChangedDate;
			if (requisites.TerritoryID != null) {
				Model.TerritoryID = ((int)requisites.TerritoryID);
				InitFromTerritory(Model.TerritoryID.Value);
			}

			InitFromPerson((requisites.PersonID == null) ? 0 : ((int)requisites.PersonID));
		}

		public void InitFromCard(int cardID)
		{
			var card = Repository.JuridicalPersonCards.GetInstance(cardID);
			if (card == null)
				throw new Exception(String.Format(Kesco.Persons.Web.Localization.Resources
					.ViewModel_Exception_JuridicalPerson_Requisites_CardNotFound, cardID));
			Model.ID = card.ID;
			if (card.From == PersonCard.MinFromDate) Model.From = null;
			else Model.From = card.From;
			if (card.To == PersonCard.MaxToDate) Model.To = null;
			else Model.To = card.To.Value.AddDays(-1);
			Model.PersonID = card.PersonID;
			Model.ShortNameLat = card.ShortNameLat;
			Model.ShortNameRus = card.ShortNameRus;
			Model.FullName = card.FullName;
			Model.IncorporationFormID = card.IncorporationFormID;
			Model.KPP = card.KPP;
			Model.OKONH = card.OKONH;
			Model.OKVED = card.OKVED;
			Model.PersonType = PersonCardType.Juridical;
			Model.RwID = card.RwID;
			Model.ShortNameRusGenitive = card.ShortNameRusGen;
			Model.AddressLegal = card.AddressLegal;
			Model.AddressLegalLat = card.AddressLegalLat;
			Model.ChangedBy = card.ChangedBy;
			Model.ChangedDate = card.ChangedDate;
			InitFromPerson(card.PersonID);
		}

		public void InitFromPerson(int personID)
		{
			if (personID != 0) {
				Person = Repository.Persons.GetInstance(personID);
				if (Person == null)
					throw new Exception(String.Format(Kesco.Persons.Web.Localization.Resources
						.ViewModel_Exception_JuridicalPerson_Requisites_PersonNotFound, personID));
				Model.PersonID = personID;
				// получение количества карточек
				CardsNumber = PersonCardJuridicalAccessor.Accessor.GetInstancesByPersonID(personID).Count;

				if (Person.TerritoryID.HasValue) {
					Model.TerritoryID = Person.TerritoryID.Value;
					InitFromTerritory(Person.TerritoryID.Value);
				}
			}
		}

		public void InitFromTerritory(int territoryID)
		{
			Territory = Kesco.Territories.BusinessLogic.Repository.Territories.GetInstance(territoryID);
			if (Territory == null) {
				throw new Exception(String.Format("Не удалось установить страну регистрации с кодом #{0}", territoryID));
			}
		}

	}

}
