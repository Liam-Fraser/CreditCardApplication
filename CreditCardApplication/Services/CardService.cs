using CreditCardApplication.Models;
using System.Data.SqlClient;

namespace CreditCardApplication.Services
{
    public class CardService
    {
        private readonly IDatabaseAccessService databaseAccess;

        public CardService(IDatabaseAccessService databaseAccess) {
            this.databaseAccess = databaseAccess;
        }

        public CreditCardModel FindCard(int cardId)
        {
            var statement = $"SELECT * FROM CreditCards WHERE Id = @CardId";
            var sqlParams = new SqlParameter[]
            {
                new SqlParameter("@CardId", cardId)
            };
            var json = databaseAccess.ReadAsJSON(statement, sqlParams);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<CreditCardModel>(json);
        }
    }
}
