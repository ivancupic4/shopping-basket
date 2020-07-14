using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasket.DTO
{
    public class DiscountItemDTO
    {
        public DiscountItemDTO(string name, decimal discount)
        {
            Name = name;
            Discount = discount;
        }

        public string Name { get; set; }
        public decimal Discount { get; set; }
    }
}
