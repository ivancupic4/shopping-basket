using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasket.DTO
{
    public class DiscountItemDTO
    {
        public DiscountItemDTO(string name, decimal price, decimal discount)
        {
            Name = name;
            Price = price;
            Discount = discount;
        }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
    }
}
