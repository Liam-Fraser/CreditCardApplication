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
            if (ApplicationDataIsInvalid(name, dob))
            {
                return RedirectToAction("Error");
            }
            var response = applicationService.MakeApplication(name, dob, salary);
            if (!response.IsValidApplication)
            {
                return RedirectToAction("Error");
            }
            return RedirectToAction("ViewCard", "Apply", new { cardId = response.CardId });
        }

        private static bool ApplicationDataIsInvalid(string name, DateTime dob)
        {
            return dob == DateTime.MinValue || name == "";
        }

        public IActionResult Error()
        {
            return View();
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
