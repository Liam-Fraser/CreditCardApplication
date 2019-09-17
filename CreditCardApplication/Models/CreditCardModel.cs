using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardApplication.Models
{
    public class CreditCardModel
    {
        public int Id;
        public string CardName;
        public int MinimumAge;
        public int MinimumSalary;
        public int MaximumSalary;

        public CreditCardModel(
            int id,
            string cardName,
            int minimumAge,
            int minimumSalary,
            int maximumSalary)
        {
            Id = id;
            CardName = cardName;
            MinimumAge = minimumAge;
            MinimumSalary = minimumSalary;
            MaximumSalary = maximumSalary;
        }
    }
}
