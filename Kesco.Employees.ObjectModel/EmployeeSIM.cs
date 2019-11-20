using System;
using BLToolkit.Mapping;

namespace Kesco.Employees.ObjectModel
{
	/// <summary>
	/// Класс содержит информацию о сим карте, прикрепленной к сотруднику
	/// </summary>
	public class EmployeeSIM
	{
		/// <summary>
		/// Номер телефона
		/// </summary>
		[MapField("НомерТелефона")]
		public string PhoneNumber { get; set; }

		/// <summary>
		/// Указывает когда последний раз была обновлена информация
		/// </summary>
		/// <value>
		/// Когда последний раз была обновлена информация
		/// </value>
		[MapField("Изменено")]
		public DateTime? ChangedDate { get; set; }
	}
}
