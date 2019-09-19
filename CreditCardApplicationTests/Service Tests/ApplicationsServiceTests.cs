using CreditCardApplication.Models;
using CreditCardApplication.Services;
using Newtonsoft.Json;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CreditCardApplicationTests.Service_Tests
{
    public class ApplicationsServiceTests
    {
        private readonly IDatabaseAccessService DatabaseAccessService;
        private readonly ApplicationsService ApplicationsService;

        public ApplicationsServiceTests()
        {
            DatabaseAccessService = Substitute.For<IDatabaseAccessService>();
            ApplicationsService = new ApplicationsService(DatabaseAccessService);
        }

        [Fact]
        public void GetApplicationsQueriesDatabaseForAllApplications()
        {
            ApplicationsService.GetApplications();
            DatabaseAccessService.Received(1).ReadMultipleAsJSON(
                "SELECT t.Id, UserName, Date, CardName FROM TransactionLog t LEFT JOIN CreditCards c ON t.CardId = c.Id"
            );
        }
    }
}
