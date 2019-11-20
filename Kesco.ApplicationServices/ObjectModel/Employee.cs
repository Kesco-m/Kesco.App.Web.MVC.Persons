using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using Kesco.Localization;
using Kesco.ObjectModel;


namespace Kesco.ApplicationServices
{
	
	[TableName("Сотрудники")]
	public abstract partial class Employee : TrackableEntity<Employee, int>
	{
		[MapField("КодСотрудника"), PrimaryKey, NonUpdatable]
		public override int ID { get; set; }

		//[Фамилия]
		[MapField("Фамилия"), BLToolkit.Validation.Required("Фамилия должна быть указана")]
		public string LastName { get; set; }

		//,[Имя]
		[MapField("Имя"), BLToolkit.Validation.Required("Имя должно быть указано")]
		public string FirstName { get; set; }

		//,[Отчество]
		[MapField("Отчество"), BLToolkit.Validation.Required("Отчество должно быть указано")]
		public string MiddleName { get; set; }

		//,[LastName]
		[MapField("Фамилия"), BLToolkit.Validation.Required("Фамилия должна быть указана")]
		public string LastNameEn { get; set; }

		//,[FirstName]
		[MapField("Имя"), BLToolkit.Validation.Required("Имя должно быть указано")]
		public string FirstNameEn { get; set; }

		//,[MiddleName]
		[MapField("Отчество"), BLToolkit.Validation.Required("Отчество должно быть указано")]
		public string MiddleNameEn { get; set; }

		//,[Сотрудник]
		[MapField("Сотрудник"), NonUpdatable]
		public string FullName { get; set; }
		
		//,[ФИО]
		[MapField("ФИО"), NonUpdatable]
		public string LastNameWithInitials { get; set; }
		
		//,[ИОФ]
		[MapField("ИОФ"), NonUpdatable]
		public string InitialsWithLastName { get; set; }
		
		//,[Employee]
		[MapField("Employee"), NonUpdatable]
		public string FullNameEn { get; set; }
		
		//,[FIO]
		[MapField("FIO"), NonUpdatable]
		public string LastNameWithInitialsEn { get; set; }
		
		//,[IOF]
		[MapField("IOF"), NonUpdatable]
		public string InitialsWithLastNameEn { get; set; }
		
		//,[КодЛица]
		[MapField("КодЛица"), Nullable]
		public int? PersonID { get; set; }
		
		//,[КодЛицаЗаказчика]
		[MapField("КодЛицаЗаказчика"), Nullable]
		public int? EmployerID { get; set; }
		
		//,[Состояние]
		[MapField("Состояние")]
		public int Status { get; set; }
		
		//,[GUID]
		[MapField("GUID")]
		public Guid Guid { get; set; }
		
		//,[Login]
		[MapField("Login"), BLToolkit.Validation.Required("Имя пользователя должно быть указано")]
		public string Login { get; set; }
		
		//,[AccountDisabled]
		[MapField("AccountDisabled")]
		public bool? AccountDisabled { get; set; }
		
		//,[DisplayName]
		[MapField("DisplayName"), BLToolkit.Validation.Required("Отображаемое имя пользователя должно быть указано")]
		public string DisplayName { get; set; }
		
		//,[Email]
		[MapField("Email"), BLToolkit.Validation.Required("Электронный адрес сотрудника должен быть указан.")]
		public string Email { get; set; }
		
		//,[ЛичнаяПапка]
		[MapField("ЛичнаяПапка")]
		public string PersonalFolder { get; set; }
		
		//,[Язык]
		[MapField("Язык")]
		public string LanguageIsoCode { get; set; }
		
		//,[Пол]
		[MapField("Пол")]
		public string Gender { get; set; }

		
	}

	[DataContract(Name = "EmployeeList", Namespace = "http://www.kesco.com/")]
	public class EmployeeList : List<EmployeeInfo> { }

	[DataContract(Name="EmployeeInfo", Namespace="http://www.kesco.com/")]
	public abstract class EmployeeInfo 
	{
		[MapField("КодСотрудника")]
		[Display(ResourceType=typeof(Resources), Name="Employee_ID_Display_Name")]
		[UIHint("UniqueID")]
		[Key, DataMember]
		public int ID { get; set; }

		//,[ФИО]
		[MapField("ФИО")]
		[Display(ResourceType = typeof(Resources), Name = "Employee_LastNameWithInitials_Display_Name")]
		[UIHint("ReadOnlyText")]
		[ReadOnly(true)]
		[DataMember]
		public string LastNameWithInitials { get; set; }

		[MapField("КодЛицаЗаказчика"), Nullable]
		[ReadOnly(true)]
		[DataMember]
		public int? EmployerID { get; set; }
		

	}
}
