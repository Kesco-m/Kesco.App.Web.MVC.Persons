using System;
using System.Text;
using System.Threading;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using Kesco.ObjectModel;

namespace Kesco.Employees.ObjectModel
{
	/// <summary>
	/// Сотрудники
	/// </summary>
	[TableName("Сотрудники")]
	public class EmployeePartial : Entity<Employee, int>
	{
		/// <summary>
		/// КодСотрудника
		/// </summary>
		[MapField("КодСотрудника")]
		[PrimaryKey, NonUpdatable]
		public override int ID { get; set; }

		//,[Сотрудник]
		/// <summary>
		/// Gets or sets the full name.
		/// </summary>
		/// <value>
		/// The full name.
		/// </value>
		[MapField("Сотрудник"), NonUpdatable]
		public string FullName { get; set; }

		//,[Employee]
		/// <summary>
		/// Gets or sets the full name en.
		/// </summary>
		/// <value>
		/// The full name en.
		/// </value>
		[MapField("Employee"), NonUpdatable]
		public string FullNameEn { get; set; }

		//,[КодЛица]
		/// <summary>
		/// Gets or sets the person ID.
		/// </summary>
		/// <value>
		/// The person ID.
		/// </value>
		[MapField("КодЛица"), Nullable]
		public int? PersonID { get; set; }

		//,[КодЛицаЗаказчика]
		/// <summary>
		/// Gets or sets the employer ID.
		/// </summary>
		/// <value>
		/// The employer ID.
		/// </value>
		[MapField("КодЛицаЗаказчика"), Nullable]
		public int? EmployerID { get; set; }

		//,[Состояние]
		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>
		/// The status.
		/// </value>
		[MapField("Состояние")]
		public int Status { get; set; }

		//,[Email]
		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		/// <value>
		/// The status.
		/// </value>
		[MapField("Email")]
		public string Email { get; set; }

		//,[DisplayName]
		/// <summary>
		/// Gets or sets the display name.
		/// </summary>
		/// <value>
		/// The display name.
		/// </value>
		[MapField("DisplayName"), BLToolkit.Validation.Required("Отображаемое имя пользователя должно быть указано")]
		public string DisplayName { get; set; }


		/// <summary>
		/// Возвращает отображаемое имя для сотрудника.
		/// </summary>
		/// <returns>Отображаемое имя для сотрудника</returns>
		public override string GetInstanceFriendlyName()
		{
			// TODO: Реализовать транслитерацию для языков с латинскими буквами.
 			string friendlyName = String.Empty;

			if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "ru")
				friendlyName = Kesco.StringExtensions.Coalesco(FullName, FullNameEn);
			else
				friendlyName = Kesco.StringExtensions.Coalesco(FullNameEn, FullName);

			return friendlyName ?? ("#" + GetUniqueID());

		}

	}
}