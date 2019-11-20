using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kesco.Web.Mvc;
using BLToolkit.Reflection;
using Kesco.Persons.BusinessLogic;

namespace Kesco.Persons.Web.Models.Synchronize
{

	/// <summary>
	/// Модель представления синхронизации сотрудника и лица
	/// </summary>
	public class ViewModel : ViewModel<DataModel>
	{
		/// <summary>
		/// Есть права на редактирование или нет?.
		/// </summary>
		public int AccessLevel { get; internal set; }

		/// <summary>
		/// Представляет отличия в данных сотрудника и лица
		/// </summary>
		/// <value>
		/// Отличия в данных.
		/// </value>
		public Differences Differences { get; internal set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ViewModel" /> class.
		/// </summary>
		public ViewModel() : base()
		{
			Differences = new Differences();
		}

		/// <summary>
		/// Создаёт объект с настройками пользователя.
		/// </summary>
		protected override void CreateSettings()
		{
			settings = TypeAccessor<ClientParameters>.CreateInstanceEx();
		}

		/// <summary>
		/// Инициализирует модель представления с помощью переданных
		/// идентификаторов лица и сотрудника
		/// </summary>
		/// <param name="personID">Идентификатор лица.</param>
		/// <param name="employeeID">Идентификаторов сотрудника.</param>
		public void Init(int personID, int employeeID)
		{

			var person = Model.Person = Repository.Persons.GetInstance(personID);

			if (person != null) {
				
				AccessLevel = (int) Repository.Persons.GetPersonAccessLevel(personID);

				Model.PersonCard = Repository.NaturalPersonCards.GetCurrentPersonCard(personID);

				Model.PersonPositions = Repository.Persons.GetPersonCurrentPositions(personID);

				Model.Employee = Kesco.Employees.BusinessLogic.Repository.Employees.GetInstance(employeeID);

				Model.EmployeePositions = Repository.Persons.GetPersonEmployeePositions(employeeID);

			}

		}

		public void Compare()
		{
			Differences.Compare(Model);
		}

	}
}