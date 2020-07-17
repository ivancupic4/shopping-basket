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
        public BasketDTO(List<ProductDTO> currentBasketProducts)
        {
            this.CurrentBasketProducts = currentBasketProducts;
        }

        public List<ProductDTO> CurrentBasketProducts;

        public DiscountDTO DiscountDTO { get; set; }

        public decimal TotalCost
        {
            get
            {
                decimal totalSum = CurrentBasketProducts.Select(x => x.Price).Sum();

                List<int> productIdList = CurrentBasketProducts.Select(x => x.Id).ToList();
                this.DiscountDTO = DiscountHelper.CalculateDiscount(productIdList);

                decimal totalCost = totalSum - this.DiscountDTO.TotalDiscount;

                LogHelper.LogBasketDetails(CurrentBasketProducts, this.DiscountDTO, totalCost);

                return totalCost;
            }
        }
    }
}
