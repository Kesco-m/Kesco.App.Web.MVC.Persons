using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kesco.Persons.Controls.Models
{
	/// <summary>
	/// Определяет тип информации об лице,
	/// которую необходимо вывести
	/// </summary>
	public enum PersonInfoTypes : int
	{
		/// <summary>
		/// Кличка
		/// </summary>
		Nickname = 1,

		/// <summary>
		/// Краткое название
		/// </summary>
		ShortName = 2,

		/// <summary>
		/// Полное название
		/// </summary>
		FullName = 3
	}
}