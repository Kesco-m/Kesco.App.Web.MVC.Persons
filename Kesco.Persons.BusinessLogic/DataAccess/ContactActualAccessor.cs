using Kesco.Persons.ObjectModel;
using Kesco.DataAccess;
using BLToolkit.DataAccess;
using System.Collections.Generic;

namespace Kesco.Persons.BusinessLogic.DataAccess
{
	public abstract class ContactActualAccessor : EntityAccessor<ContactActualAccessor, DB, ContactActual, ContactActualAccessor.SearchParameters, int>
	{
		public class SearchParameters : Kesco.DataAccess.SearchParameters { }

        /// <summary>
        /// Устанавливает актуальность контакта
        /// </summary>
        /// <value>
        /// Идентификатор контакта
        /// </value>
        [SqlQuery("INSERT КонтактыАктуальность SELECT @ID,0,GETUTCDATE()")]
        public abstract void SaveActualContactInfo(int ID);
	}
}
