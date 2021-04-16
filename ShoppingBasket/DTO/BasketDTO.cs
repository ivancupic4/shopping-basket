using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ShoppingBasket.DTO
{
    public class BasketDTO
    {
        public BasketDTO()
        {
            this.CurrentBasketProducts = new List<ProductDTO>();
        }

        public decimal TotalCost { get; set; }
        public DiscountDTO DiscountDTO { get; set; }
        public List<ProductDTO> CurrentBasketProducts { get; set; }
    }
}
