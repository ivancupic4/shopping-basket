using ShoppingBasket.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasket
{
    public interface IShoppingBasketService
    {
        BasketDTO AddProduct(List<ProductDTO> currentBasketProducts, int newProductId, int amount = 1);
        List<ProductDTO> LoadProductsByIdList(List<int> basketProductsIdList);
    }
}
