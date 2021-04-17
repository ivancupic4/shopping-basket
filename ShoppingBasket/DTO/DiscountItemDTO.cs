using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasket.Domain.DTO
{
    public class DiscountItemDTO
    {
        public DiscountItemDTO(string name, decimal discount)
        {
            this.Name = name;
            this.Discount = discount;
        }

        public string Name { get; set; }
        public decimal Discount { get; set; }
    }
}
