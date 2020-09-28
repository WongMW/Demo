using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SitefinityWebApp.BusinessFacade.Interfaces.SQL
{
    public interface SqlInterface
    {
        Task<int> ExecuteSPSqlCommand(string spName, List<SqlParameter> parameters);
        Task<string> ExecuteSPUpdateWithoutResultSqlCommand(string spName, List<SqlParameter> parameters);
    }
}
