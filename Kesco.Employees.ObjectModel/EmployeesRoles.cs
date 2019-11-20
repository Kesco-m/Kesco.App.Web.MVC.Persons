using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using Kesco.ObjectModel;

namespace Kesco.Employees.ObjectModel
{
	/// <summary>
	/// Объектное описание роли сотрудника
	/// </summary>
	[TableName("РолиСотрудников")]
	public class EmployeesRoles : TrackableEntity<EmployeesRoles, int>
	{
		/// <summary>
		/// КодРоли
		/// </summary>
		[MapField("КодРоли")]
		[PrimaryKey]
		public override int ID { get; set; }

		/// <summary>
		/// КодСотрудника
		/// </summary>
		[MapField("КодСотрудника")]
		[PrimaryKey]
		public int EmployeeID { get; set; }

		/// <summary>
		/// КодЛица
		/// </summary>
		[MapField("КодЛица")]
		[PrimaryKey]
		public int PersonID { get; set; }

        /// <summary>
        /// Кличка
        /// </summary>
        [MapField("Кличка")]
        [PrimaryKey]
        public string PersonName { get; set; }

        /// <summary>
        /// Роль лица
        /// </summary>
        [MapField("Роль")]
        [PrimaryKey]
        public string RoleName { get; set; }

		public override string GetInstanceFriendlyName()
		{
			return String.Format("#{0}", ID);
		}

		
	}
}
