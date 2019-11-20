using System.Data;
using Kesco.DataAccess;

namespace Kesco.Persons.BusinessLogic.DataAccess
{
    public class AbacusDB : Database
    {

		public AbacusDB() : base("DS_Abacus") { }

        protected override IDbCommand OnInitCommand(IDbCommand command)
        {
            IDbCommand dbCommand = base.OnInitCommand(command);
            dbCommand.CommandTimeout = 30 * 3;
            return dbCommand;
        }

    }

}
