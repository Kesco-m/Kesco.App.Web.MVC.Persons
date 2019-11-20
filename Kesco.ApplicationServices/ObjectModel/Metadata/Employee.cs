using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Kesco.Localization;


namespace Kesco.ApplicationServices
{

	[MetadataType(typeof(Employee_Metadata))]
	//[Display(ResourceType = typeof(Resources), Name="Employee_Display_Name")]
	public abstract partial class Employee
	{
		public class Employee_Metadata
		{
			[Display(ResourceType = typeof(Resources), Name = "Employee_ID_Display_Name")]
			[UIHint("UniqueID")]
			public int ID { get; set; }

			//[Фамилия]
			[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Employee_LastName_Validation_Required")]
			[Display(ResourceType = typeof(Resources), Name = "Employee_LastName_Display_Name")]
			[UIHint("TextBox")]
			[StringLength(50)]
			public string LastName { get; set; }

			//,[Имя]
			[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Employee_FirstName_Validation_Required")]
			[Display(ResourceType = typeof(Resources), Name = "Employee_FirstName_Display_Name")]
			[UIHint("TextBox")]
			[StringLength(50)]
			public string FirstName { get; set; }

			//,[Отчество]
			[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Employee_MiddleName_Validation_Required")]
			[Display(ResourceType = typeof(Resources), Name = "Employee_MiddleName_Display_Name")]
			[UIHint("TextBox")]
			[StringLength(50)]
			public string MiddleName { get; set; }

			//,[LastName]
			[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Employee_LastNameEn_Validation_Required")]
			[Display(ResourceType = typeof(Resources), Name = "Employee_LastNameEn_Display_Name")]
			[UIHint("TextBox")]
			[StringLength(50)]
			public string LastNameEn { get; set; }

			//,[FirstName]
			[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Employee_FirstNameEn_Validation_Required")]
			[Display(ResourceType = typeof(Resources), Name = "Employee_FirstNameEn_Display_Name")]
			[UIHint("TextBox")]
			[StringLength(50)]
			public string FirstNameEn { get; set; }

			//,[MiddleName]
			[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Employee_MiddleNameEn_Validation_Required")]
			[Display(ResourceType = typeof(Resources), Name = "Employee_MiddleNameEn_Display_Name")]
			[UIHint("TextBox")]
			[StringLength(50)]
			public string MiddleNameEn { get; set; }

			//,[Сотрудник]
			[Display(ResourceType = typeof(Resources), Name = "Employee_FullName_Display_Name")]
			[UIHint("ReadOnlyText")]
			[StringLength(152)]
			[ReadOnly(true)]
			public string FullName { get; set; }

			//,[ФИО]
			[Display(ResourceType = typeof(Resources), Name = "Employee_LastNameWithInitials_Display_Name")]
			[UIHint("ReadOnlyText")]
			[StringLength(55)]
			[ReadOnly(true)]
			public string LastNameWithInitials { get; set; }

			//,[ИОФ]
			[Display(ResourceType = typeof(Resources), Name = "Employee_InitialsWithLastName_Display_Name")]
			[UIHint("ReadOnlyText")]
			[StringLength(55)]
			[ReadOnly(true)]
			public string InitialsWithLastName { get; set; }

			//,[Employee]
			[Display(ResourceType = typeof(Resources), Name = "Employee_Employee_Display_Name")]
			[UIHint("ReadOnlyText")]
			[StringLength(55)]
			[ReadOnly(true)]
			public string FullNameEn { get; set; }

			//,[FIO]
			[Display(ResourceType = typeof(Resources), Name = "Employee_LastNameWithInitialsEn_Display_Name")]
			[UIHint("ReadOnlyText")]
			[StringLength(55)]
			[ReadOnly(true)]
			public string LastNameWithInitialsEn { get; set; }

			//,[IOF]
			[Display(ResourceType = typeof(Resources), Name = "Employee_InitialsWithLastNameEn_Display_Name")]
			[UIHint("ReadOnlyText")]
			[StringLength(55)]
			[ReadOnly(true)]
			public string InitialsWithLastNameEn { get; set; }

			//,[КодЛица]
			[Display(ResourceType = typeof(Resources), Name = "Employee_PersonID_Display_Name")]
			[UIHint("PersonID")]
			public int? PersonID { get; set; }

			//,[КодЛицаЗаказчика]
			[Display(ResourceType = typeof(Resources), Name = "Employee_CustomerPersonID_Display_Name")]
			[UIHint("CustomerPersonID")]
			public int? CustomerPersonID { get; set; }

			//,[Состояние]
			[Display(ResourceType = typeof(Resources), Name = "Employee_Status_Display_Name")]
			[UIHint("EmployeeStatus")]
			public int Status { get; set; }

			//,[GUID]
			[Display(ResourceType = typeof(Resources), Name = "Employee_GUID_Display_Name")]
			[UIHint("GUID")]
			public Guid Guid { get; set; }

			//,[Login]
			[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Employee_Login_Validation_Required")]
			[Display(ResourceType = typeof(Resources), Name = "Employee_Login_Display_Name")]
			[UIHint("TextBox")]
			[StringLength(32)]
			public string Login { get; set; }

			//,[AccountDisabled]
			[Display(ResourceType = typeof(Resources), Name = "Employee_AccountDisabled_Display_Name")]
			[UIHint("CheckBox")]
			public bool? AccountDisabled { get; set; }

			//,[DisplayName]
			[System.ComponentModel.DataAnnotations.Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Employee_DisplayName_Validation_Required")]
			[Display(ResourceType = typeof(Resources), Name = "Employee_DisplayName_Display_Name")]
			[UIHint("TextBox")]
			[StringLength(50)]
			public string DisplayName { get; set; }

			//,[Email]
			[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Employee_Email_Validation_Required")]
			[Display(ResourceType = typeof(Resources), Name = "Employee_Email_Display_Name")]
			[UIHint("TextBox")]
			[StringLength(50)]
			public string Email { get; set; }

			//,[ЛичнаяПапка]
			[Display(ResourceType = typeof(Resources), Name = "Employee_PersonalFolder_Display_Name")]
			[UIHint("TextBox")]
			[StringLength(80)]
			public string PersonalFolder { get; set; }

			//,[Язык]
			[Display(ResourceType = typeof(Resources), Name = "Employee_LanguageIsoCode_Display_Name")]
			[UIHint("TextBox")]
			[StringLength(2)]
			public string LanguageIsoCode { get; set; }

			//,[Пол]
			[Display(ResourceType = typeof(Resources), Name = "Employee_Gender_Display_Name")]
			[UIHint("Gender")]
			[StringLength(1)]
			public string Gender { get; set; }

            
		}
	}

}
