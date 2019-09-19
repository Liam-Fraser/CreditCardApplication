using CreditCardApplication.Models;

namespace CreditCardApplication.Services
{
    public interface ICardService
    {
        CreditCardModel FindCard(int cardId);
    }
}