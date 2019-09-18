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
        public int Id;
        public string Username;
        public DateTime Dob;
        public DateTime Date;
        public int Salary;
    }
}
