using Kesco.Employees.BusinessLogic.DataAccess;

namespace Kesco.Employees.BusinessLogic
{
	public static class Repository
	{
		public static EmployeeAccessor Employees { get { return EmployeeAccessor.Accessor; } }
		public static EmployeePartialAccessor EmployeePartials { get { return EmployeePartialAccessor.Accessor; } }
		public static EmployeesRolesAccessor EmployeesRoles { get { return EmployeesRolesAccessor.Accessor; } }
	}
}
