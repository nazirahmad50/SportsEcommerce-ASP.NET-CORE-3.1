using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsEcommerce.Models
{
    public class EFStoreRepository : IStoreRepository
    {
        private readonly ApplicationDbContext context;
        public EFStoreRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Product> Products => context.Products;

        public void CreateProduct(Product p)
        {
            context.Add(p);
            context.SaveChanges();
        }

        public void DeleteProduct(Product p)
        {
            context.Remove(p);
            context.SaveChanges();
        }

        public void SaveProduct(Product p)
        {
            context.SaveChanges();
        }
    }
}
