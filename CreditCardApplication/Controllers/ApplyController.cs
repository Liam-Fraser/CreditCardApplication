using System;
using CreditCardApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardApplication.Controllers
{
    public class ApplyController : Controller
    {
        private readonly ApplicationService applicationService;

        public ApplyController(ApplicationService applicationService, CardService cardService)
        {
            this.applicationService = applicationService;
            CardService = cardService;
        }

        public CardService CardService { get; }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendApplication(string name, DateTime dob, int salary)
        {
            var card = applicationService.MakeApplication(name, dob, salary);
            return RedirectToAction("ViewCard", "Apply", new { cardId = card.Id });
        }

        public IActionResult ViewCard(int cardId)
        {
            ViewData["cardId"] = cardId;
            var card = CardService.FindCard(cardId);
            ViewData["card"] = card;
            return View();
        }
    }
}
