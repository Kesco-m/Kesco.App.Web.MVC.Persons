using System.Data;
using Kesco.DataAccess;

namespace Kesco.Persons.BusinessLogic.DataAccess
{
    public class DB : Database
    {

        public DB() : base("DS_Person") { }

        protected override IDbCommand OnInitCommand(IDbCommand command)
        {
            IDbCommand dbCommand = base.OnInitCommand(command);
            dbCommand.CommandTimeout = 30 * 3;
            return dbCommand;
        }

    }

}
