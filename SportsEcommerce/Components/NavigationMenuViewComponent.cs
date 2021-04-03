using Microsoft.AspNetCore.Mvc;
using SportsEcommerce.Models;
using SportsEcommerce.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsEcommerce.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IStoreRepository repository;
        public NavigationMenuViewComponent(IStoreRepository repository)
        {
            this.repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];

            return View(repository.Products
                                   .Select(x => x.Category)
                                   .Distinct()
                                   .OrderBy(x => x));
        }
    }
}
