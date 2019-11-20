using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using Kesco.ObjectModel;
using System;

namespace Kesco.Persons.ObjectModel
{
	/// <summary>
	/// vwЛица_Сотрудники
	/// </summary>
	[TableName("vwЛица_Сотрудники")]
	[System.ComponentModel.DataAnnotations.MetadataType(typeof(Metadata.ResponsibleEmployee))]
	[MapValue(true, 1)]
	[MapValue(false, 0)]
	public class ResponsibleEmployee : TrackableEntity<ResponsibleEmployee, int>
	{
		/// <summary>
		/// КодЛица_Сотрудники
		/// </summary>
		[MapField("КодЛица_Сотрудники")]
		[PrimaryKey, NonUpdatable]
		public override int ID { get; set; }

		/// <summary>
		/// КодЛица
		/// </summary>
		[MapField("КодЛица")]
		public int PersonID { get; set; }

		/// <summary>
		/// КодСотрудника
		/// </summary>
		[MapField("КодСотрудника")]
		public int EmployeeID { get; set; }

		/// <summary>
		/// ЛичныйОтветственный
		/// </summary>
		[MapField("Личное")]
		public bool IsPersonal { get; set; }

		/// <summary>
		/// Возвращает отображаемое имя для связи ответсвенный сотрудник.
		/// </summary>
		/// <returns>Отображаемое имя для связи ответсвенный сотрудник</returns>
		public override string GetInstanceFriendlyName()
		{
			return String.Format("#{0}: Person #{1} <-> Employee # {2}", GetUniqueID(), PersonID, EmployeeID);
		}
	}
}
