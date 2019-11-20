using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Persons.BusinessLogic.Persons
{
	/// <summary>
	/// Карточка юрлица для сохранения в бд
	/// </summary>
	public class JuridicalCardForSave
	{
		public const int Russia = 188;

		public int NewID { get; set; }
		public SaveAction WhatDo { get; set; }
		public bool Check { get; set; }
		public int КодЛица { get; set; }
		public int КодКарточки { get; set; }
		public string Кличка { get; set; }
		public int? КодБизнесПроекта { get; set; }
		public int? КодТерритории { get; set; }
		public int ГосОрганизация { get; set; }
		public string БИК { get; set; }
		public string ИНН { get; set; }
		public string ОГРН { get; set; }
		public string ОКПО { get; set; }
		public string КорСчет { get; set; }
		public string БИКРКЦ { get; set; }
		public string SWIFT { get; set; }
		public string Примечание { get; set; }
		public bool Проверено { get; set; }

		public DateTime? От { get; set; }
		public DateTime? До { get; set; }
		public int? КодОргПравФормы { get; set; }
		public string КраткоеНазваниеРус { get; set; }
		public string КраткоеНазваниеРусРП { get; set; }
		public string КраткоеНазваниеЛат { get; set; }
		public string ПолноеНазвание { get; set; }
		public string ОКОНХ { get; set; }
		public string ОКВЭД { get; set; }
		public string КПП { get; set; }
		public string КодЖД { get; set; }
		public string АдресЮридический { get; set; }
		public string АдресЮридическийЛат { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="JuridicalCardForSave" /> class.
		/// </summary>
		public JuridicalCardForSave()
		{
			Кличка = String.Empty;
			БИК  = String.Empty;
			ИНН  = String.Empty;
			ОГРН  = String.Empty;
			ОКПО  = String.Empty;
			КорСчет  = String.Empty;
			БИКРКЦ  = String.Empty;
			SWIFT  = String.Empty;
			Примечание = String.Empty;
			КраткоеНазваниеРус  = String.Empty;
			КраткоеНазваниеРусРП = String.Empty;
			КраткоеНазваниеЛат = String.Empty;
			ОКОНХ = String.Empty;
			ОКВЭД= String.Empty;
			КПП = String.Empty;
			КодЖД = String.Empty;
			АдресЮридический = String.Empty;
			АдресЮридическийЛат = String.Empty;

			От = new DateTime(1980, 1, 1);
			До = new DateTime(2050, 1, 1);
		}
	}
}
