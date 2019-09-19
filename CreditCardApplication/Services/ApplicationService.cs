using CreditCardApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardApplication.Services
{
    public class ApplicationResponse
    {
        public bool IsValidApplication;
        public int CardId;
    }

    public class ApplicationService : IApplicationService
    {
        private readonly IDatabaseAccessService database;
        private readonly string FindApplicableCardQuery = "" +
            "SELECT TOP 1 * " +
            "FROM CreditCards " +
            "WHERE MinimumAge <= @Age " +
            "AND MinimumSalary <= @Salary " +
            "AND (MaximumSalary >= @Salary OR MaximumSalary = -1)";

        private readonly string RecordApplicationQuery = "" +
            "INSERT INTO TransactionLog([UserName], [Date], [DOB], [CardId], [QualifiedForCard]) " +
            "VALUES (@UserName, @Date, @Dob, @CardId, @QualifiedForCard)";

        public ApplicationService(IDatabaseAccessService database)
        {
            this.database = database;
        }

        public ApplicationResponse MakeApplication(string name, DateTime dob, int salary)
        {
            int ageInYears = DateTime.Today.Year - dob.Year;
            var parameters = BuildCardLocationParams(salary, ageInYears);
            var result = database.ReadAsJSON(FindApplicableCardQuery, parameters);
            if (result == "{}")
            {
                return UnsuccessfulApplication(name, dob);
            }
            return SuccessfulApplication(name, dob, result);
        }

        private ApplicationResponse UnsuccessfulApplication(string name, DateTime dob)
        {
            database.WriteRecord(RecordApplicationQuery, BuildApplicationParams(name, dob, false));
            return new ApplicationResponse { IsValidApplication = false };
        }

        private ApplicationResponse SuccessfulApplication(string name, DateTime dob, string result)
        {
            var card = JsonConvert.DeserializeObject<CreditCardModel>(result);
            var applicationParams = BuildApplicationParams(name, dob, true, card.Id);
            database.WriteRecord(RecordApplicationQuery, applicationParams);
            return new ApplicationResponse { IsValidApplication = true, CardId = card.Id };
        }

        private static SqlParameter[] BuildCardLocationParams(int salary, int ageInYears)
        {
            return new SqlParameter[] {
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
        }

        private static SqlParameter[] BuildApplicationParams(string name, DateTime dob, bool allowedCard, int creditCardId = -1)
        {
            return new SqlParameter[] {
                new SqlParameter {
                    ParameterName = "@Username",
                    Value = name
                },
                new SqlParameter
                {
                    ParameterName = "@Date",
                    Value = DateTime.Now
                },
                new SqlParameter
                {
                    ParameterName = "@Dob",
                    Value = dob
                },
                new SqlParameter
                {
                    ParameterName = "@CardId",
                    Value = creditCardId
                },
                new SqlParameter
                {
                    ParameterName = "@QualifiedForCard",
                    Value = allowedCard ? 1 : 0
                }
            };
        }

    }
}
