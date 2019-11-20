using Kesco.DataAccess;
using System.Data;

namespace Kesco.BusinessProjects.BusinessLogic
{
    public class DB : Database
    {

        public DB() : base("DS_Person") { }

        protected override IDbCommand OnInitCommand(IDbCommand command)
        {
            var dbCommand = base.OnInitCommand(command);
            dbCommand.CommandTimeout = 30 * 3;
            return dbCommand;
        }

    }

}
