﻿using CreditCardApplication.Models;
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
            "SELECT TOP 1 * " +
            "FROM CreditCards " +
            "WHERE MinimumAge <= @Age " +
            "AND MinimumSalary <= @Salary " +
            "AND (MaximumSalary >= @Salary OR MaximumSalary = -1)";
        private readonly string RecordApplicationQuery = "" + 
            "INSERT INTO TransactionLog([User], [Date], [DOB], [CardId]) " +
            "VALUES (@UserName, @Date, @Dob, @CardId)";

        public ApplicationService(DatabaseAccessService database)
        {
            this.database = database;
        }

        public CreditCardModel MakeApplication(string name, DateTime dob, int salary)
        {
            int ageInYears = DateTime.Today.Year - dob.Year;
            SqlParameter[] parameters = BuildCardLocationParams(salary, ageInYears);
            var result = database.ReadRowAsJSON(FindApplicableCardQuery, 6, parameters);
            var card = JsonConvert.DeserializeObject<CreditCardModel>(result);

            SqlParameter[] applicationParams = BuildApplicationParams(name, dob, card);
            database.WriteRecord(RecordApplicationQuery, applicationParams);

            return card;
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

        private static SqlParameter[] BuildApplicationParams(string name, DateTime dob, CreditCardModel card)
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
                    Value = card.Id
                }
            };
        }

    }
}
