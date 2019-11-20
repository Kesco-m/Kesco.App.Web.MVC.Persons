using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Mapping;

namespace Kesco.Employees.ObjectModel
{
	/// <summary>
	/// Класс содержит информацию о последнем проходе сотрудника
	/// </summary>
	public class EmployeePassage
	{
		/// <summary>
		/// Указывает местоположения последнего прохода сотрудника
		/// </summary>
		/// <value>
		/// Местоположение последнего прохода сотрудника
		/// </value>
		[MapField("Считыватель")]
		public string Point { get; set; }

		/// <summary>
		/// Указывает время(UTC) прохода сотрудника через пост
		/// </summary>
		/// <value>
		/// Время(UTC) прохода сотрудника через пост
		/// </value>
		[MapField("ПоследнийПроход")]
		public DateTime TimeAt { get; set; }
	}
}
