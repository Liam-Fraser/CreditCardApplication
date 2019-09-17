using System.Collections.Generic;
using System.Data.SqlClient;

namespace CreditCardApplication.Services
{
    public class DatabaseAccessService
    {
        private static readonly string connection = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=CreditCardApplicationData;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public IEnumerable<object> ReadOneResult(string command, int columnCount)
        {
            var db = new SqlConnection(connection);
            db.Open();
            var query = new SqlCommand(command, db);
            return ExecuteReadOneCommand(columnCount, query);
        }

        private static List<object> ExecuteReadOneCommand(int columnCount, SqlCommand query)
        {
            var results = new List<object>();
            var dataReader = query.ExecuteReader();
            while (dataReader.Read())
            {
                for (int i = 0; i < columnCount; i++)
                {
                    results.Add(dataReader.GetValue(i));
                }
            }

            return results;
        }
    }
}
