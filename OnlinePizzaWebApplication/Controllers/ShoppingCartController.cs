using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RancentPizzaOrderingSystem.Repositories;
using RancentPizzaOrderingSystem.Models;
using RancentPizzaOrderingSystem.ViewModels;
using RancentPizzaOrderingSystem.Data;
using Microsoft.AspNetCore.Authorization;

namespace RancentPizzaOrderingSystem.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IPizzaRepository _pizzaRepository;
        private readonly AppDbContext _context;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IPizzaRepository pizzaRepository,
            ShoppingCart shoppingCart, AppDbContext context)
        {
            _pizzaRepository = pizzaRepository;
            _shoppingCart = shoppingCart;
            _context = context;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var items = await _shoppingCart.GetShoppingCartItemsAsync();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(shoppingCartViewModel);
        }

        public async Task<IActionResult> AddToShoppingCart(int pizzaId)
        {
            var selectedPizza = await _pizzaRepository.GetByIdAsync(pizzaId);

            if (selectedPizza != null)
            {
                await _shoppingCart.AddToCartAsync(selectedPizza, 1);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveFromShoppingCart(int pizzaId)
        {
            var selectedPizza = await _pizzaRepository.GetByIdAsync(pizzaId);

            if (selectedPizza != null)
            {
                await _shoppingCart.RemoveFromCartAsync(selectedPizza);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ClearCart()
        {
            await _shoppingCart.ClearCartAsync();

            return RedirectToAction("Index");
        }

    }
}