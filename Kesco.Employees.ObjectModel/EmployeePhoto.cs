using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Mapping;

namespace Kesco.Employees.ObjectModel
{
	/// <summary>
	/// Класс содержит информацию о фотографии сотрудника
	/// </summary>
	public class EmployeePhoto
	{
		/// <summary>
		/// Фотография сотрудника сотрудника
		/// </summary>
		/// <value>
		/// Фотография сотрудника сотрудника
		/// </value>
		[MapField("Фотография")]
		public byte[] Photo { get; set; }

		/// <summary>
		/// Указывает когда последний раз была обновлена фотография
		/// </summary>
		/// <value>
		/// Когда последний раз была обновлена 
		/// </value>
		[MapField("Изменено")]
		public DateTime? ChangedDate { get; set; }
	}
}
