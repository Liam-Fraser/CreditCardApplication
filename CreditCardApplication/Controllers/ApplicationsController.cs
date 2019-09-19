using CreditCardApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CreditCardApplication.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly IApplicationsService applicationsService;

        public ApplicationsController(IApplicationsService applicationsService) {
            this.applicationsService = applicationsService;
        }

        public IActionResult Index()
        {
            var records = applicationsService.GetApplications();
            ViewData["records"] = records.ToList();
            return View();
        }
    }
}
