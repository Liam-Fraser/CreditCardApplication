using System;
using CreditCardApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardApplication.Controllers
{
    public class ApplyController : Controller
    {
        private readonly ApplicationService applicationService;

        public ApplyController(ApplicationService applicationService)
        {
            this.applicationService = applicationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendApplication(string name, DateTime dob, int salary)
        {
            applicationService.MakeApplication(name, dob, salary);
            return View();
        }
    }
}
