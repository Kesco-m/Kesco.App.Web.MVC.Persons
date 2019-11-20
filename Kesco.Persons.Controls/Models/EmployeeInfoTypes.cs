using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kesco.Persons.Controls.Models
{
	/// <summary>
	/// Определяет тип информации об сотруднике,
	/// которую необходимо вывести в контроле
	/// </summary>
	public enum EmployeeInfoTypes : int
	{
		/// <summary>
		/// FIO
		/// </summary>
		FIO = 1,

		/// <summary>
		/// Полное имя
		/// </summary>
		FullName = 2,

	}
}