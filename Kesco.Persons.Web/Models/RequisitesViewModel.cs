using System.Collections.Generic;
using BLToolkit.Reflection;
using Kesco.Persons.ObjectModel;
using Kesco.Web.Mvc;

namespace Kesco.Persons.Web.Models
{
    public class RequisitesViewModel : DialogViewModel
    {

        /// <summary>
        /// Настройки пользователя для формы поиска, хранящиеся в БД
        /// </summary>
        public ClientParameters Params { get; internal set; }

        public RequisitesViewModel() : base()
        {
        }

		protected override void CreateSettings()
		{
			settings = new object();
		}

		public class ClientParameters
        {
        }
    }
}