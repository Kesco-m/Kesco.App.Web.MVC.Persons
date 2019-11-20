using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kesco.Persons.Web.Models.Synchronize
{
	public class Difference
	{
		public string Field		{ get; set; }
		public object Source	{ get; set; }
		public object Target	{ get; set; }
	}

	public class WorkPlaceDifference
	{
		public int PersonID { get; set; }
		public string PersonNickname { get; set; }
		public string Position { get; set; }
		public int? PersonLinkID { get; set; }
	}

	public class Differences
	{
		public List<object> Data { get; protected set; }
		public List<object> WorkPlaces { get; protected set; }

		public bool HasDiffences { get { return Data.Count > 0 || WorkPlaces.Count > 0; } }

		public Differences()
		{
			Data = new List<object>();
			WorkPlaces = new List<object>();
		}

		public void Clear()
		{
			Data.Clear();
			WorkPlaces.Clear();
		}

		public void Compare(DataModel model)
		{
			Clear();

			if (model.Person == null || model.Employee == null) return;

			var person = model.Person;
			var employee = model.Employee;
			var card = model.PersonCard;

			if (!(person.Nickname ?? String.Empty).Equals(employee.FullName)) {
				Data.Add(new Difference { Field = "Псевдоним", Source = employee.FullName, Target = person.Nickname });
			}

			if (card != null) {

				if (employee.LastName != card.LastNameRus) {
					Data.Add(new Difference { Field = "Фамилия", Source = employee.LastName, Target = card.LastNameRus });
				}

				if (employee.FirstName != card.FirstNameRus) {
					Data.Add(new Difference { Field = "Имя", Source = employee.FirstName, Target = card.FirstNameRus });
				}

				if (employee.MiddleName != card.MiddleNameRus) {
					Data.Add(new Difference { Field = "Отчество", Source = employee.MiddleName, Target = card.MiddleNameRus });
				}

				if (employee.Gender != card.Sex) {
					Data.Add(new Difference { Field = "Пол", Source = employee.Gender, Target = card.Sex });
				}

			}

			if (model.EmployeePositions != null && model.EmployeePositions.Count > 0) {
			    model.EmployeePositions.ForEach((position) => { 
			        var filtered = model.PersonPositions.Where(
			            pos => pos.PersonID == position.PersonID && pos.Position.Equals(position.Position, StringComparison.InvariantCultureIgnoreCase)
			        ).ToList();
					if (filtered == null || filtered.Count() == 0) {
						WorkPlaces.Add(new WorkPlaceDifference { 
							PersonID = position.PersonID,
							PersonNickname = position.Organization,
							PersonLinkID = position.PersonLinkID,
							Position = position.Position
						});
					} else {
						filtered.ForEach(personPos => { 
							if (personPos.Position.ToLower() != position.Position.ToLower()) {
								WorkPlaces.Add(new WorkPlaceDifference {
									PersonID = position.PersonID,
									PersonNickname = position.Organization,
									PersonLinkID = position.PersonLinkID,
									Position = position.Position
								});
							}
						});
					}
			    });
			}
		}


	}

}