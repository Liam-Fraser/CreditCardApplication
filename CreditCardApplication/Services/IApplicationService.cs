using System;

namespace CreditCardApplication.Services
{
    public interface IApplicationService
    {
        ApplicationResponse MakeApplication(string name, DateTime dob, int salary);
    }
}