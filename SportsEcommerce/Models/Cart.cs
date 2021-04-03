using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsEcommerce.Models
{
    public class Cart
    {
        public List<Cartline> Lines { get; set; } = new List<Cartline>();

        public virtual void AddItem(Product product, int quantity)
        {
            Cartline line = Lines.Where(x => x.Product.ProductID == product.ProductID).FirstOrDefault();

            if (line == null)
            {
                Lines.Add(new Cartline
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Product product)
        {
            Lines.RemoveAll(x => x.Product.ProductID == product.ProductID);
        }

        public decimal ComputeTotalValue()
        {
            return Lines.Sum(x => x.Product.Price * x.Quantity);
        }

        public virtual void Clear()
        {
            Lines.Clear();
        }

    }

    public class Cartline
    {
        public int CartLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

}
