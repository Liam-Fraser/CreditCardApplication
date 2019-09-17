using CreditCardApplication.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardApplication.Models
{
    public class ApplicationModel
    {
        public int id;
        public string name;
        public DateTime dob;
        public DateTime date;
        public int salary;
    }
}
