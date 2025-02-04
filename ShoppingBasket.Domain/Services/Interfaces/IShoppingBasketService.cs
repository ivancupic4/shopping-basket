using ShoppingBasket.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasket
{
    public interface IShoppingBasketService
    {
        BasketDTO AddProduct(ProductInsertDTO productInsert);
        List<ProductDTO> GetProductsByIds(List<int> basketProductsIds);
        ProductDTO GetProductById(int productId);
    }
}
