using Microsoft.AspNetCore.Mvc;
using SportsEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsEcommerce.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository repo;
        private Cart cart;

        public OrderController(IOrderRepository repository, Cart cartService)
        {
            repo = repository;
            cart = cartService;
        }

        public IActionResult Checkout()
        {
            return View(new Order());
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry your cart is empty");
            }

            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                repo.SaveOrder(order);
                cart.Clear();
                return RedirectToPage("/completed", new { orderId = order.OrderId });
            }
            else
            {
                return View();
            }
        }

    }
}
