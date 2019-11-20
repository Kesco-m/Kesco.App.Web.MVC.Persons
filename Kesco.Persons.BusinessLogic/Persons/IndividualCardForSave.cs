using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Persons.BusinessLogic.Persons
{
	/// <summary>
	/// Карточка физлица для сохранения в бд
	/// </summary>
	public class IndividualCardForSave
	{
		public int NewID { get; set; }
		public SaveAction WhatDo { get; set; }
		public bool Check { get; set; }
		public int КодЛица { get; set; }
		public int КодКарточки { get; set; }
		public string Кличка { get; set; }
		public int? КодБизнесПроекта { get; set; }
		public int? КодТерритории { get; set; }
		public string ИНН { get; set; }
		public string ОГРН { get; set; }
		public string ОКПО { get; set; }
		public DateTime? ДатаРождения { get; set; }
		public string Примечание { get; set; }
		public bool Проверено { get; set; }

		public DateTime? От { get; set; }
		public DateTime? До { get; set; }
		public int? КодОргПравФормы { get; set; }
		public string ФамилияРус { get; set; }
		public string ИмяРус { get; set; }
		public string ОтчествоРус { get; set; }
		public string ФамилияЛат { get; set; }
		public string ИмяЛат { get; set; }
		public string ОтчествоЛат { get; set; }
		public char Пол { get; set; }
		public string ОКОНХ { get; set; }
		public string ОКВЭД { get; set; }
		public string КПП { get; set; }
		public string КодЖД { get; set; }
		public string АдресЮридический { get; set; }
		public string АдресЮридическийЛат { get; set; }
	}
}
