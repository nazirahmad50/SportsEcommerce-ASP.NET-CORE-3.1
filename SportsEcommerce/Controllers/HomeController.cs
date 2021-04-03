using Microsoft.AspNetCore.Mvc;
using SportsEcommerce.Models;
using SportsEcommerce.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsEcommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoreRepository repo;

        public int PageSize = 4;

        public HomeController(IStoreRepository repository)
        {
            repo = repository;
        }


        public IActionResult Index(string category, int productPage = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repo.Products
                            .Where(x => category == null || x.Category == category)
                            .OrderBy(x => x.ProductID)
                            .Skip((productPage - 1) * PageSize)
                            .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? repo.Products.Count() : repo.Products.Where(x => x.Category == category).Count()
                    
                },
                CurrentCategory = category
            };

            return View(model);
        }
    }
}
