using CreditCardApplication.Models;
using CreditCardApplication.Services;
using FluentAssertions;
using Newtonsoft.Json;
using NSubstitute;
using System;
using System.Data.SqlClient;
using Xunit;

namespace CreditCardApplicationTests.Service_Tests
{
    public class ApplicationServiceTests
    {
        private readonly IDatabaseAccessService DatabaseAccessService;
        private readonly ApplicationService ApplicationService;
        private readonly string FindApplicableCardQuery = "" +
           "SELECT TOP 1 * " +
           "FROM CreditCards " +
           "WHERE MinimumAge <= @Age " +
           "AND MinimumSalary <= @Salary " +
           "AND (MaximumSalary >= @Salary OR MaximumSalary = -1)";
        private readonly DateTime DOB = DateTime.Now;


        public ApplicationServiceTests()
        {
            DatabaseAccessService = Substitute.For<IDatabaseAccessService>();
            ApplicationService = new ApplicationService(DatabaseAccessService);
        }

        [Fact]
        public void MakeApplicationQueriesTheDatabaseForCards()
        {
            DatabaseAccessService.ReadAsJSON(Arg.Any<string>(), Arg.Any<SqlParameter[]>())
                .Returns("{}");

            ApplicationService.MakeApplication("Jim", DOB, 100);

            DatabaseAccessService.Received(1).ReadAsJSON(FindApplicableCardQuery, Arg.Any<SqlParameter[]>());
        }

        [Fact]
        public void MakeApplicationReturnsUnsuccessfulResponseIfNoCardsFound()
        {
            DatabaseAccessService.ReadAsJSON(Arg.Any<string>(), Arg.Any<SqlParameter[]>())
                .Returns("{}");

            var response = ApplicationService.MakeApplication("Jim", DOB, 100);

            response.IsValidApplication.Should().BeFalse();
        }

        [Fact]
        public void MakeApplicationReturnsSuccesfulResponseIfCardsFound()
        {
            var card = new CreditCardModel
            {
                Id = 42,
                CardName = "Oyster"
            };
            DatabaseAccessService.ReadAsJSON(Arg.Any<string>(), Arg.Any<SqlParameter[]>())
                .Returns(JsonConvert.SerializeObject(card));

            var response = ApplicationService.MakeApplication("Jim", DOB, 100);

            response.IsValidApplication.Should().BeTrue();
            response.CardId.Should().Be(card.Id);
        }
    }
}
