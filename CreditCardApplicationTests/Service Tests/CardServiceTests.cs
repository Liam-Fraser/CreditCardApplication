using CreditCardApplication.Models;
using CreditCardApplication.Services;
using FluentAssertions;
using Newtonsoft.Json;
using NSubstitute;
using System.Data.SqlClient;
using Xunit;

namespace CreditCardApplicationTests
{
    public class CardServiceTests
    {
        private readonly CardService CardService;
        private IDatabaseAccessService DatabaseAccessService;
        public CardServiceTests()
        {
            DatabaseAccessService = Substitute.For<IDatabaseAccessService>();
            CardService = new CardService(DatabaseAccessService);
        }

        [Fact]
        public void FindCardQueriesTheDatabase()
        {
            CardService.FindCard(1);

            DatabaseAccessService.Received(1).ReadAsJSON(
                "SELECT * FROM CreditCards WHERE Id = @CardId",
                Arg.Any<SqlParameter[]>()
            );
        }

        [Fact]
        public void ItReturnsTheRetrievedCard()
        {
            var card = new CreditCardModel
            {
                Id = 1, CardName ="Test Card", Apr = 1.5F, ImageUrl="image.com", MaximumSalary = 100, MinimumAge = 19, MinimumSalary = 10, SalesText = ""
            };
            DatabaseAccessService.ReadAsJSON(Arg.Any<string>(), Arg.Any<SqlParameter[]>()).Returns(JsonConvert.SerializeObject(card));

            var result = CardService.FindCard(1);

            result.Should().BeEquivalentTo(card);
        }
    }
}
