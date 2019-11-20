using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections.Generic;
using BLToolkit.Reflection;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.ObjectModel;
using Kesco.Persons.Web.Models.Requisites;
using Kesco.Web.Mvc;

namespace Kesco.Persons.Web.Models.Juridicals
{
	/// <summary>
	/// Модель данных для формы создания/редактирования лица
	/// </summary>
	public class PersonModel
	{
		/// <summary>
		/// Возвращает карточку юридического лица
		/// </summary>
		public PersonCard Card { get; internal set; }

        public List<Naturals.PersonModel.SimplePersonModelClass> ResponsibleEmployees { get; internal set; }

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

		public PersonModel()
		{
			Card = TypeAccessor<PersonCard>.CreateInstanceEx();
			Card.TerritoryID = Card.Requisites.TerritoryID = null;
			ResponsibleEmployees = new List<Naturals.PersonModel.SimplePersonModelClass>();
			CurrentEmployeeID = UserContext.Current.EmployeeInfo.ID;
			PersonThemesAndCatalogs = new List<dynamic>();
            this.PersonTypes = new PersonTypesList { };
		}

		/// <summary>
		/// Инициализирует карточку из данных лица.
		/// </summary>
		/// <param name="personID">Идентификатор лица.</param>
		public void InitFromPerson(int personID)
		{
			Person person = Repository.Persons.GetInstance(personID);
			if (person != null) {
				Card.PersonID = person.ID;
				Card.Nickname = person.Nickname;

				Card.TerritoryID = person.TerritoryID;
				Card.IsStateOrganization = person.IsStateOrganization;
	
				Card.OGRN = person.OGRN;
				Card.INN = person.INN;
				Card.OKPO = person.OKPO;
				Card.BusinessProjectID = person.BusinessProjectID;

				Card.IsBank = !String.IsNullOrEmpty(person.BIK) || !String.IsNullOrEmpty(person.SWIFT);
				Card.Comment = person.Comment;

				Card.BIK = person.BIK;
				Card.SWIFT = person.SWIFT;
				Card.LoroConto = person.LoroConto;
				Card.BIKRKC = person.BIKRKC;

				Card.Verified = person.Verified;
				Card.ChangedBy = person.ChangedBy;
				Card.ChangedDate = person.ChangedDate;
			}
		}

		/// <summary>
		/// Инициализирует список ответственных сотрудников.
		/// </summary>
		public void InitResponsibleEmployees() 
		{
			ResponsibleEmployees.Clear();
			if (Card.ID.HasValue) {

			} else {
				ResponsibleEmployees.Add(
                    new Naturals.PersonModel.SimplePersonModelClass
                    { 
						ID = UserContext.Current.EmployeeInfo.ID.ToString(), 
						FullName = UserContext.Current.EmployeeInfo.LastNameWithInitials 
					}
				);
			}
		}

		/// <summary>
		/// Инициализирует структуру для отображения типов сотрудников.
		/// </summary>
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