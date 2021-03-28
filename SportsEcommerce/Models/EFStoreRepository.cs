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
    }
}
