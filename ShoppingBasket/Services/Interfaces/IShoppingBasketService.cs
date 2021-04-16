using ShoppingBasket.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasket
{
    public interface IShoppingBasketService
    {
        public BasketDTO AddProduct(List<ProductDTO> currentBasketProducts, int newProductId, int amount = 1);
        public List<ProductDTO> LoadProductsByIdList(List<int> basketProductsIdList);
    }
}
