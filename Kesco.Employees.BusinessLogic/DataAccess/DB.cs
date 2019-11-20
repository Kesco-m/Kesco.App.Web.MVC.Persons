using System.Data;
using Kesco.DataAccess;

namespace Kesco.Employees.BusinessLogic.DataAccess
{
    public class DB : Database
    {

        public DB() : base("DS_User") { }

        protected override IDbCommand OnInitCommand(IDbCommand command)
        {
            var dbCommand = base.OnInitCommand(command);
            dbCommand.CommandTimeout = 30 * 3;
            return dbCommand;
        }

    }

}
