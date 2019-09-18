using CreditCardApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardApplication.Services
{
    public class ApplicationService
    {
        private readonly DatabaseAccessService database;
        private readonly string FindApplicableCardQuery = "" +
                  "SELECT TOP 1 Id, CardName, MinimumAge, MinimumSalary, MaximumSalary " +
                  "FROM CreditCards " +
                  "WHERE MinimumAge <= @Age " +
                  "AND MinimumSalary <= @Salary " +
                  "AND (MaximumSalary >= @Salary OR MaximumSalary = -1)";

        public ApplicationService(DatabaseAccessService database)
        {
            this.database = database;
        }

        public CreditCardModel MakeApplication(string name, DateTime dob, int salary)
        {
            int ageInYears = DateTime.Today.Year - dob.Year;
            var parameters = new SqlParameter[] {
                new SqlParameter {
                    ParameterName = "@Salary",
                    Value = salary
                },
                new SqlParameter
                {
                    ParameterName = "@Age",
                    Value = ageInYears
                }
            };

            var result = database.ReadRowAsJSON(FindApplicableCardQuery, 5, parameters);
            // todo: Log Application (write)
            return JsonConvert.DeserializeObject<CreditCardModel>(result);


        }

        
    }
}
