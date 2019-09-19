using System.Collections.Generic;
using CreditCardApplication.Models;

namespace CreditCardApplication.Services
{
    public interface IApplicationsService
    {
        IEnumerable<ApplicationRecordModel> GetApplications();
    }
}