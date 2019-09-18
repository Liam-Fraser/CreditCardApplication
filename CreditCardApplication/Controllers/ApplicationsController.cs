using Microsoft.AspNetCore.Mvc;

namespace CreditCardApplication.Controllers
{
    public class ApplicationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
