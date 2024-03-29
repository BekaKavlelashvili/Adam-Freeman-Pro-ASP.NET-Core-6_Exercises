﻿using System.Reflection.Metadata.Ecma335;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
             var products = new[] {
                 new { Name = "Kayak", Price = 275M },
                 new { Name = "Lifejacket", Price = 48.95M },
                 new { Name = "Soccer ball", Price = 19.50M },
                 new { Name = "Corner flag", Price = 34.95M }
             };

            return View(products.Select(x=> $"{nameof(x.Name)}: {x.Name}, {nameof(x.Price)}: {x.Price}"));
        }
    }
}
