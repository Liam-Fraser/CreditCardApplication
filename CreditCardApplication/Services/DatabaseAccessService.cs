using CreditCardApplication.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CreditCardApplication.Services
{
    public class DatabaseAccessService
    {
        private static readonly string connection = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=CreditCardApplicationData;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public string ReadAsJSON(string command, SqlParameter[] parameters)
        {
            var db = new SqlConnection(connection);
            db.Open();
            var query = new SqlCommand(command, db);
            query.Parameters.AddRange(parameters);
            var jsonResult = ExecuteReadOneCommand(query);
            db.Close();
            return jsonResult;
        }
        public IEnumerable<string> ReadMultipleAsJSON(string command)
        {
            var db = new SqlConnection(connection);
            db.Open();
            var query = new SqlCommand(command, db);
            var results = ExecuteReadMultipleCommand(query);
            db.Close();
            return results;
        }

        public void WriteRecord(string command, SqlParameter[] parameters)
        {
            var db = new SqlConnection(connection);
            db.Open();
            var sql = new SqlCommand(command, db);
            sql.Parameters.AddRange(parameters);
            sql.ExecuteNonQuery();
            db.Close();
        }

        private static string ExecuteReadOneCommand(SqlCommand query)
        {
            var row = new Dictionary<string, object>();
            var dataReader = query.ExecuteReader();
            while (dataReader.Read())
            {
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    row.Add(dataReader.GetName(i), dataReader.GetValue(i));
                }
            }
            return JsonConvert.SerializeObject(row);
        }

        private static List<string> ExecuteReadMultipleCommand(SqlCommand query)
        {
            var jsonRows = new List<string>();
            var dataReader = query.ExecuteReader();
            while (dataReader.Read())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    row.Add(dataReader.GetName(i), dataReader.GetValue(i));
                }
                jsonRows.Add(JsonConvert.SerializeObject(row));
            }
            return jsonRows;
        }
    }
}
