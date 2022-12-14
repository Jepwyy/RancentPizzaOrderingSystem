using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RancentPizzaOrderingSystem.Data;
using RancentPizzaOrderingSystem.Repositories;
using RancentPizzaOrderingSystem.ViewModels;

namespace RancentPizzaOrderingSystem.Controllers
{


    
    public class HomeController : Controller
    {
        private readonly IPizzaRepository _pizzaRepo;
        
        public HomeController( IPizzaRepository pizzaRepo)
        {
           
            _pizzaRepo = pizzaRepo;
           
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
        [Authorize]
        public IActionResult Dashboard()
        {
            return View();
        }
        
        public async Task<IActionResult> LandingPage()
        {
            var model = new SearchPizzasViewModel()
            {
                PizzaList = await _pizzaRepo.GetAllIncludedAsync(),
                SearchText = null
            };

            return View(model);
        }
        
    }
}
