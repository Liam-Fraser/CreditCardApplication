using CreditCardApplication.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CreditCardApplication.Services
{
    public class ApplicationsService
    {
        private readonly IDatabaseAccessService databaseAccess;

        public ApplicationsService(IDatabaseAccessService databaseAccess) {
            this.databaseAccess = databaseAccess;
        }

        public IEnumerable<ApplicationRecordModel> GetApplications()
        {
            var statement = "SELECT t.Id, UserName, Date, CardName FROM TransactionLog t LEFT JOIN CreditCards c ON t.CardId = c.Id";
            var json = databaseAccess.ReadMultipleAsJSON(statement);
            var records = json.Select(j => JsonConvert.DeserializeObject<ApplicationRecordModel>(j));
            return records;
        }
    }
}
