using CreditCardApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardApplication.Services
{
    public class ApplicationService
    {
        private readonly DatabaseAccessService database;

        public ApplicationService(DatabaseAccessService database)
        {
            this.database = database;
        }

        public CreditCardModel MakeApplication(string name, DateTime dob, int salary)
        {
            int ageInYears = DateTime.Today.Year - dob.Year;
            var query = buildCardQuery(salary, ageInYears);
            var results = database.ReadOneResult(query, 5).ToList();
            // todo: Log Application (write)
            // todo: Replace with castless creation
            // todo: NULL checks
            return new CreditCardModel(
                (int)results[0],
                (string)results[1],
                (int)results[2],
                (int)results[3],
                (int)results[4]);
        }

        private static string buildCardQuery(int salary, int ageInYears)
        {
            return $"" +
                $"SELECT TOP 1 Id, CardName, MinimumAge, MinimumSalary, MaximumSalary " +
                $"FROM CreditCards " +
                $"WHERE MinimumAge <= {ageInYears} " +
                $"AND MinimumSalary <= {salary} " +
                $"AND (MaximumSalary >= {salary} OR MaximumSalary IS NULL)";
        }
    }
}
