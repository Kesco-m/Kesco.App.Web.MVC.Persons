using System.Collections.Generic;
using BLToolkit.DataAccess;
using Kesco.Persons.ObjectModel;
using Kesco.DataAccess;

namespace Kesco.Persons.BusinessLogic.DataAccess
{
    public abstract class FormatRegistrationAccessor : EntityAccessor<FormatRegistrationAccessor, DB, FormatRegistration, FormatRegistrationAccessor.SearchParameters, int>
    {
        public class SearchParameters : Kesco.DataAccess.SearchParameters { }

        [SqlQuery(@"SELECT * FROM ФорматНомеровРегистрацииЛиц")]
        public abstract List<FormatRegistration> GetAllFormats();

    }
}
