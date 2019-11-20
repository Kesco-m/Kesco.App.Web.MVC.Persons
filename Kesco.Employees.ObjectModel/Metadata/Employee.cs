using System;
using System.ComponentModel.DataAnnotations;
using Kesco.Employees.ObjectModel.Localization;

namespace Kesco.Employees.ObjectModel.Metadata
{
	/// <summary>
	/// Сотрудники
	/// </summary>
	internal class Employee
	{
		/// <summary>
		/// КодСотрудника
		/// </summary>
		[UIHint("UniqueID")]
		[Display(ResourceType = typeof(Resources),
				Name = "Kesco_Employees_MDL_101",
				ShortName = "Kesco_Employees_MDL_102",
				Description = "Kesco_Employees_MDL_103",
				Prompt = "Kesco_Employees_MDL_104"
			)]
		public int ID { get; set; }

		/// <summary>
		/// ФИО
		/// </summary>
		[UIHint("TextBox")]
		[Display(ResourceType = typeof(Resources),
				Name = "Kesco_Employees_MDL_105",
				ShortName = "Kesco_Employees_MDL_106",
				Description = "Kesco_Employees_MDL_107",
				Prompt = "Kesco_Employees_MDL_108"
			)]
		public string LastNameWithInitials { get; set; }

		public string FullName { get; set; }

        ///// <summary>
        ///// Gets or sets the birthday.
        ///// </summary>
        ///// <value>
        ///// The birthday.
        ///// </value>
        //public DateTime? Birthday { get; set; }

        ///// <summary>
        ///// Gets or sets the birth place.
        ///// </summary>
        ///// <value>
        ///// The birth place.
        ///// </value>
        //[StringLength(300)]
        //public string BirthPlace { get; set; }

        ///// <summary>
        ///// Gets or sets the INN.
        ///// </summary>
        ///// <value>
        ///// The INN.
        ///// </value>
        //[StringLength(50)]
        //public string INN { get; set; }

	}
}