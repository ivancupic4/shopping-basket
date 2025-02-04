using ShoppingBasket.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasket
{
    public interface IDiscountService
    {
        DiscountDTO CalculateDiscount(List<int> basketProductsIds);
    }
}
