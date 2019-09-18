﻿using System;
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
        public string ImageUrl;
        public float Apr;
        public string SalesText;
    }
}
