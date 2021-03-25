using System.Collections.Generic;

namespace FrietshopVives.Models
{
    public class ShopModel
    {
        public OrderCart OrderCart { get; set; }
        public IList<Product> Products { get; set; }
    }
}
