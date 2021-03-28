using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsEcommerce.Models
{
    public interface IStoreRepository
    {
        /// <summary>
        /// allow a caller to obtain a sequence of Product objects
        /// without the 'IQueryable' i would have to retrieve all of the Product Objects from the database and then discard the ones that i dont want,
        /// which is an expensive operation
        /// thats why IQueryable interface is used over IEnumerable in database repository interfaces and classes
        /// However, each time the collection of objects is enemurated, teh query will be evaluated again, which means a new query will be sent to teh database,
        /// in such situations, the IQueryable interface should be converted to a more predictable form using the ToList<> or ToArray extension methods
        /// </summary>
        IQueryable<Product> Products { get;}
    }
}
