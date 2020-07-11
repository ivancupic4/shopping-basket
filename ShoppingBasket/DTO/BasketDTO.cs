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
        public List<ProductDTO> currentBasketProducts;

        public BasketDTO(List<ProductDTO> currentBasketProducts)
        {
            this.currentBasketProducts = currentBasketProducts;
        }

        public decimal TotalCost
        {
            get
            {
                decimal totalSum = currentBasketProducts.Select(x => x.Price).Sum();

                List<int> productIdList = currentBasketProducts.Select(x => x.Id).ToList();
                DiscountDTO discountDTO = DiscountHelper.CalculateDiscount(productIdList);

                decimal totalCost = totalSum - discountDTO.TotalDiscount;

                LogHelper.LogBasketDetails(currentBasketProducts, discountDTO, totalCost);

                return totalCost;
            }
        }
    }
}
