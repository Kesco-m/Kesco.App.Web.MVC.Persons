using System.Collections.Generic;
using BLToolkit.DataAccess;
using Kesco.DataAccess;
using Kesco.Employees.ObjectModel;
using Kesco.Persons.ObjectModel;

namespace Kesco.Persons.BusinessLogic.DataAccess
{
	public abstract class ResponsibleEmployeeAccessor : EntityAccessor<ResponsibleEmployeeAccessor, DB, ResponsibleEmployee,
		ResponsibleEmployeeAccessor.SearchParameters, int>
	{
		public class SearchParameters : Kesco.DataAccess.SearchParameters { }

		[SqlQuery(@"
				SELECT * 
				FROM Инвентаризация.dbo.Сотрудники
				WHERE КодСотрудника IN ( SELECT КодСотрудника FROM dbo.vwЛица_Сотрудники WHERE КодЛица = @personID)
				ORDER BY Сотрудник
			")]
		public abstract List<Employee> GetResponsibleEmployeesByPersonId(int personID);

		[SqlQuery(@"
				UPDATE dbo.vwЛица_Сотрудники
				SET Личное = @isPersonal
				WHERE КодЛица = @personID and КодСотрудника = @employeeID

				SELECT * FROM dbo.vwЛица_Сотрудники WHERE КодЛица = @personID and КодСотрудника = @employeeID
			")]
		public abstract ResponsibleEmployee SetResponsiblePersonality(int personID, int employeeID, int isPersonal);

	}
}
