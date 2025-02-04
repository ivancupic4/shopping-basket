using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ShoppingBasket.Domain.DTO
{
    public class BasketDTO
    {
        public BasketDTO()
        {
            this.Products = new List<ProductDTO>();
        }

        public decimal Cost { get; set; }
        public DiscountDTO Discount { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}
