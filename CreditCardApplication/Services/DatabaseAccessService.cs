using CreditCardApplication.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CreditCardApplication.Services
{
    public class DatabaseAccessService
    {
        private static readonly string connection = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=CreditCardApplicationData;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public string ReadRowAsJSON(string command, int columnCount, SqlParameter[] parameters)
        {
            var db = new SqlConnection(connection);
            db.Open();
            var query = new SqlCommand(command, db);
            query.Parameters.AddRange(parameters);
            return ExecuteReadOneCommand(columnCount, query);
        }

        private static string ExecuteReadOneCommand(int columnCount, SqlCommand query)
        {
            var row = new Dictionary<string, object>();
            var dataReader = query.ExecuteReader();
            while (dataReader.Read())
            {
                for (int i = 0; i < columnCount; i++)
                {
                    row.Add(dataReader.GetName(i), dataReader.GetValue(i));
                }
            }
            return JsonConvert.SerializeObject(row);
        }
    }
}
