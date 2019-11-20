using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kesco.Persons.Controls.ComponentModel
{
	/// <summary>
	/// Описывает доступные действия с выбранным значением контакта
	/// </summary>
	public enum PersonContactAction : int
	{
		/// <summary>
		/// Ничего не делать
		/// </summary>
		None = 0,

		/// <summary>
		/// Сделать звонок
		/// </summary>
		MakeCall = 1,

		/// <summary>
		/// Написать электронное письмо
		/// </summary>
		SendEmail = 2,

		/// <summary>
		/// Открыть MSN Messenger
		/// </summary>
		MSN = 3
	}

}