using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections.Generic;
using BLToolkit.Reflection;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.ObjectModel;
using Kesco.Persons.Web.Models.Requisites;
using Kesco.Web.Mvc;
using Kesco.Persons.BusinessLogic.DataAccess;

namespace Kesco.Persons.Web.Models.Naturals
{
	/// <summary>
	/// Модель данных для формы создания/редактирования лица
	/// </summary>
	public class PersonModel
	{
		/// <summary>
		/// Возвращает карточку физического лица
		/// </summary>
		public PersonCard Card { get; internal set; }

        public List<SimplePersonModelClass> ResponsibleEmployees { get; internal set; }

		/// <summary>
		/// Типы лиц
		/// </summary>
        [UIHint("PersonTypesList")]
        public PersonTypesList PersonTypes { get; set; }

		/// <summary>
		/// Темы лиц
		/// </summary>
		public string PersonThemes { get; set; }

		public List<dynamic> PersonThemesAndCatalogs { get; internal set; }

		public int CurrentEmployeeID { get; internal set; }

		public bool Confirmed { get; set; }

		public bool EditCard { get; set; }

		/// <summary>
		/// Количество карточек лица
		/// </summary>
		public int CardsNumber { get; set; }

		/// <summary>
		/// Id компании-работодателя (актуально для физ лиц)
		/// Наличие значения порождает создание связи после сохранения физ. лица
		/// </summary>
		public int? EmployerId { get; set; }


        public class SimplePersonModelClass
        {
            public string ID { get; set; }
            public string FullName { get; set; }
        }

		public PersonModel()
		{
			EditCard = false;
			Card = TypeAccessor<PersonCard>.CreateInstanceEx();
			//Card.TerritoryID = Territories.ObjectModel.Territory.Russia;
            ResponsibleEmployees = new List<SimplePersonModelClass>();
			CurrentEmployeeID = UserContext.Current.EmployeeInfo.ID;
			PersonThemesAndCatalogs = new List<dynamic>();
            this.PersonTypes = new PersonTypesList{};
		}

		/// <summary>
		/// Инициализирует карточку из данных лица.
		/// </summary>
		/// <param name="personID">Идентификатор лица.</param>
		public void InitFromPerson(int personID)
		{
			var personIncName = Repository.Persons.GetIncorporationFormID(personID);
			var person = Repository.Persons.GetInstance(personID);
			if (person != null) {
				Card.IncorporationFormID = personIncName;

                
				Card.PersonID = person.ID;
				Card.Birthday = person.Birthday;
				Card.Nickname = person.Nickname;
				Card.TerritoryID = person.TerritoryID;

				Card.OGRN = person.OGRN;
				Card.INN = person.INN;
				Card.OKPO = person.OKPO;
				Card.BusinessProjectID = person.BusinessProjectID;

				Card.Comment = person.Comment;

				Card.Verified = person.Verified;

				Card.ChangedBy = person.ChangedBy;
				Card.ChangedDate = person.ChangedDate;

				// получение количества карточек
				CardsNumber = PersonCardNaturalAccessor.Accessor.GetInstancesByPersonID(person.ID).Count;
			}
		}

		public void InitFromCard(int cardID)
		{
			var card = Repository.NaturalPersonCards.GetInstance(cardID);
			if (card != null) {

				EditCard = true;

				Card.ID = card.ID;
				Card.AddressLegal = card.AddressLegal;
				Card.AddressLegalLat = card.AddressLegalLat;
				Card.FirstNameLat = card.FirstNameLat;
				Card.FirstNameRus = card.FirstNameRus;
				if (card.From == PersonCard.MinFromDate) Card.From = null;
				else Card.From = card.From;
				if (card.To == PersonCard.MaxToDate) Card.To = null;
				else Card.To = card.To.Value.AddDays(-1);
				Card.IncorporationFormID = card.IncorporationFormID;
				Card.KPP = card.KPP;
				Card.LastNameLat = card.LastNameLat;
				Card.LastNameRus = card.LastNameRus;
				Card.MiddleNameLat = card.MiddleNameLat;
				Card.MiddleNameRus = card.MiddleNameRus;
				Card.OKONH = card.OKONH;
				Card.OKVED = card.OKVED;
				Card.RwID = card.RwID;
				Card.Sex = card.Sex;

				InitFromPerson(card.PersonID);
			}
		}

		public void InitPersonThemesAndCatalogs()
		{
            List<Kesco.Persons.BusinessLogic.DataAccess.PersonTypeAccessor.PersonTypeInfo> types = Repository.PersonTypes.GetPersonTypesByTypeIDs(PersonTypes.PersonTypeIDs);

			PersonThemesAndCatalogs = types
					.GroupBy(t => t.КодТемыЛица)
					.Select(gr => new {
						ID = gr.Key,
						Name = gr.First().ТемаЛица,
						Catalogs = gr.Select(c => c.Каталог)
					}).ToList<dynamic>();
		}

	}

}