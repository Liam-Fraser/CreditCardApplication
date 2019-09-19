using System.Collections.Generic;
using System.Data.SqlClient;

namespace CreditCardApplication.Services
{
    public interface IDatabaseAccessService
    {
        string ReadAsJSON(string command, SqlParameter[] parameters);
        IEnumerable<string> ReadMultipleAsJSON(string command);
        void WriteRecord(string command, SqlParameter[] parameters);
    }
}