using CreditCardApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardApplication.Services
{
    public class CardService
    {
        private readonly DatabaseAccessService databaseAccess;

        public CardService(DatabaseAccessService databaseAccess) {
            this.databaseAccess = databaseAccess;
        }

        public CreditCardModel FindCard(int cardId)
        {
            var statement = $"SELECT * FROM CreditCards WHERE Id = @CardId";
            var sqlParams = new SqlParameter[]
            {
                new SqlParameter("@CardId", cardId)
            };
            var json = databaseAccess.ReadRowAsJSON(statement, 6, sqlParams);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<CreditCardModel>(json);
        }
    }
}
