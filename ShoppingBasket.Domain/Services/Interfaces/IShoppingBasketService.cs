﻿using ShoppingBasket.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasket
{
    public interface IShoppingBasketService
    {
        BasketDTO AddProduct(ProductInsertDTO productInsertDTO);
        List<ProductDTO> GetProductsByIdList(List<int> basketProductsIdList);
        ProductDTO GetProductById(int productId);
    }
}
