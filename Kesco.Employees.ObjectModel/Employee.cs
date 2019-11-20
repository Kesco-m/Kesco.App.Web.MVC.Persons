using System;
using System.Threading;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using Kesco.ObjectModel;
using System.Text;

namespace Kesco.Employees.ObjectModel
{
	/// <summary>
	/// Сотрудники
	/// </summary>
	[TableName("Сотрудники")]
	[System.ComponentModel.DataAnnotations.MetadataType(typeof(Metadata.Employee))]
	public class Employee : TrackableEntity<Employee, int>
	{
		/// <summary>
		/// КодСотрудника
		/// </summary>
		[MapField("КодСотрудника")]
		[PrimaryKey, NonUpdatable]
		public override int ID { get; set; }

		//[Фамилия]
		/// <summary>
		/// Gets or sets the last name.
		/// </summary>
		/// <value>
		/// The last name.
		/// </value>
		[MapField("Фамилия"), BLToolkit.Validation.Required("Фамилия должна быть указана")]
		public string LastName { get; set; }

		//,[Имя]
		/// <summary>
		/// Gets or sets the first name.
		/// </summary>
		/// <value>
		/// The first name.
		/// </value>
		[MapField("Имя"), BLToolkit.Validation.Required("Имя должно быть указано")]
		public string FirstName { get; set; }

		//,[Отчество]
		/// <summary>
		/// Gets or sets the name of the middle.
		/// </summary>
		/// <value>
		/// The name of the middle.
		/// </value>
		[MapField("Отчество"), BLToolkit.Validation.Required("Отчество должно быть указано")]
		public string MiddleName { get; set; }

		//,[LastName]
		/// <summary>
		/// Gets or sets the last name en.
		/// </summary>
		/// <value>
		/// The last name en.
		/// </value>
		[MapField("LastName"), BLToolkit.Validation.Required("Фамилия должна быть указана")]
		public string LastNameEn { get; set; }

		//,[FirstName]
		/// <summary>
		/// Gets or sets the first name en.
		/// </summary>
		/// <value>
		/// The first name en.
		/// </value>
		[MapField("FirstName"), BLToolkit.Validation.Required("Имя должно быть указано")]
		public string FirstNameEn { get; set; }

		//,[MiddleName]
		/// <summary>
		/// Gets or sets the middle name en.
		/// </summary>
		/// <value>
		/// The middle name en.
		/// </value>
		[MapField("MiddleName"), BLToolkit.Validation.Required("Отчество должно быть указано")]
		public string MiddleNameEn { get; set; }

		//,[Сотрудник]
		/// <summary>
		/// Gets or sets the full name.
		/// </summary>
		/// <value>
		/// The full name.
		/// </value>
		[MapField("Сотрудник"), NonUpdatable]
		public string FullName { get; set; }

		//,[ФИО]
		/// <summary>
		/// Gets or sets the last name with initials.
		/// </summary>
		/// <value>
		/// The last name with initials.
		/// </value>
		[MapField("ФИО"), NonUpdatable]
		public string LastNameWithInitials { get; set; }

		//,[ИОФ]
		/// <summary>
		/// Gets or sets the last name of the initials with.
		/// </summary>
		/// <value>
		/// The last name of the initials with.
		/// </value>
		[MapField("ИОФ"), NonUpdatable]
		public string InitialsWithLastName { get; set; }

		//,[Employee]
		/// <summary>
		/// Gets or sets the full name en.
		/// </summary>
		/// <value>
		/// The full name en.
		/// </value>
		[MapField("Employee"), NonUpdatable]
		public string FullNameEn { get; set; }

		//,[FIO]
		/// <summary>
		/// Gets or sets the last name with initials en.
		/// </summary>
		/// <value>
		/// The last name with initials en.
		/// </value>
		[MapField("FIO"), NonUpdatable]
		public string LastNameWithInitialsEn { get; set; }

		//,[IOF]
		/// <summary>
		/// Gets or sets the initials with last name en.
		/// </summary>
		/// <value>
		/// The initials with last name en.
		/// </value>
		[MapField("IOF"), NonUpdatable]
		public string InitialsWithLastNameEn { get; set; }

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

		//,[GUID]
		/// <summary>
		/// Gets or sets the GUID.
		/// </summary>
		/// <value>
		/// The GUID.
		/// </value>
		[MapField("GUID")]
		public Guid Guid { get; set; }

		//,[Login]
		/// <summary>
		/// Gets or sets the login.
		/// </summary>
		/// <value>
		/// The login.
		/// </value>
		[MapField("Login"), BLToolkit.Validation.Required("Имя пользователя должно быть указано")]
		public string Login { get; set; }


		//,[AccountDisabled]
		/// <summary>
		/// Gets or sets the account disabled.
		/// </summary>
		/// <value>
		/// The account disabled.
		/// </value>
		[MapField("AccountDisabled")]
		public bool? AccountDisabled { get; set; }

		//,[DisplayName]
		/// <summary>
		/// Gets or sets the display name.
		/// </summary>
		/// <value>
		/// The display name.
		/// </value>
		[MapField("DisplayName"), BLToolkit.Validation.Required("Отображаемое имя пользователя должно быть указано")]
		public string DisplayName { get; set; }

		//,[Email]
		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		/// <value>
		/// The email.
		/// </value>
		[MapField("Email"), BLToolkit.Validation.Required("Электронный адрес сотрудника должен быть указан.")]
		public string Email { get; set; }

		//,[ЛичнаяПапка]
		/// <summary>
		/// Gets or sets the personal folder.
		/// </summary>
		/// <value>
		/// The personal folder.
		/// </value>
		[MapField("ЛичнаяПапка")]
		public string PersonalFolder { get; set; }

		//,[Язык]
		/// <summary>
		/// Gets or sets the language iso code.
		/// </summary>
		/// <value>
		/// The language iso code.
		/// </value>
		[MapField("Язык")]
		public string LanguageIsoCode { get; set; }


		//,[Пол]
		/// <summary>
		/// Gets or sets the gender.
		/// </summary>
		/// <value>
		/// The gender.
		/// </value>
		[MapField("Пол")]
		public char Gender { get; set; }


        /// <summary>
        /// Gets or sets the common employee.
        /// </summary>
        /// <value>
        /// The common employee
        /// </value>
        [MapField("КодОбщегоСотрудника")]
        public int? CommonEmployeeID { get; set; }

		[MapIgnore]
		public bool NationalAndInternationalAreTheSame
		{
			get {
				return
					((LastName ?? String.Empty).Trim() == (LastNameEn ?? String.Empty).Trim())
					|| ((FirstName ?? String.Empty).Trim() == (FirstNameEn ?? String.Empty).Trim())
					|| ((MiddleName ?? String.Empty).Trim() == (MiddleNameEn ?? String.Empty).Trim());
			}
		}

		[MapIgnore]
		public string FullNameCorrected
		{
			get
			{

				StringBuilder sb = new StringBuilder();

				if (!String.IsNullOrEmpty(LastName)) {
					sb.Append(LastName);
				}


				if (!String.IsNullOrEmpty(FirstName)) {
					sb.AppendFormat((sb.Length > 0) ? " {0}" : "{0}", FirstName);
				}

				if (!String.IsNullOrEmpty(MiddleName)) {
					sb.AppendFormat((sb.Length > 0) ? " {0}" : "{0}", MiddleName);
				}

				return sb.ToString();

			}
		}

		[MapIgnore]
		public string FullNameEnCorrected
		{
			get {
				
				StringBuilder sb = new StringBuilder();

				if (!String.IsNullOrEmpty(LastNameEn)) {
					sb.Append(LastNameEn);
				}

				
				if (!String.IsNullOrEmpty(FirstNameEn)) {
					sb.AppendFormat((sb.Length > 0)?" {0}":"{0}", FirstNameEn);
				}

				if (!String.IsNullOrEmpty(MiddleNameEn)) {
					sb.AppendFormat((sb.Length > 0) ? " {0}" : "{0}", MiddleNameEn);
				}

				return sb.ToString();

			}
		}

		public static string GetEmployeeStatus(int status)
		{
			switch (status) {
				case 1: return Localization.Resources.EmployeeStatus_MaternityLeave;
				case 2: return Localization.Resources.EmployeeStatus_PersonalGuest;
				case 3: return Localization.Resources.EmployeeStatus_Fired;
				case 4: return Localization.Resources.EmployeeStatus_Guest;
				case 5: return Localization.Resources.EmployeeStatus_Outlier;

				default:
					return String.Empty;
			}
		}

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