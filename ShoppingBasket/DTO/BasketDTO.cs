using Newtonsoft.Json;
using ShoppingBasket.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ShoppingBasket.DTO
{
    public class BasketDTO
    {
        private List<ProductDTO> _currentBasketProducts;

        public BasketDTO(List<ProductDTO> currentBasketProducts)
        {
            this._currentBasketProducts = currentBasketProducts;
        }

        public decimal TotalCost
        {
            get
            {
                decimal totalSum = _currentBasketProducts.Select(x => x.Price).Sum();

                List<int> productIdList = _currentBasketProducts.Select(x => x.Id).ToList();
                DiscountDTO discountDTO = DiscountHelper.CalculateDiscount(productIdList);

                decimal totalCost = totalSum - discountDTO.TotalDiscount;

                LogHelper.LogBasketDetails(_currentBasketProducts, discountDTO, totalCost);

                return totalCost;
            }
        }
    }
}
