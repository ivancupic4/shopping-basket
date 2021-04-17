using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasket.Domain.DTO
{
    public class ProductInsertDTO
    {
        public List<ProductDTO> CurrentBasketProducts { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
