﻿using ShoppingBasket.DTO;
using ShoppingBasket.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ShoppingBasket.Helpers
{
    public static class DiscountHelper
    {
        private static List<ProductDTO> allBasketProducts = new List<ProductDTO>();

        // product discounts defined at one place, which can also be loaded from a database
        private static decimal breadDiscount = 0.5M;
        private static decimal milkDiscount = 1M;

        public static DiscountDTO CalculateDiscount(List<int> basketProductsIdList)
        {
            // DiscountDTO to be filled with data and returned
            DiscountDTO discountDTO = new DiscountDTO();

            // passing an Id list so that only products in the basket will be fetched
            allBasketProducts = ShoppingBasketService.LoadProductsByIdList(basketProductsIdList);

            CalculateBreadDiscount(basketProductsIdList, discountDTO);
            CalculateMilkDiscount(basketProductsIdList, discountDTO);

            return discountDTO;
        }

        private static void CalculateBreadDiscount(List<int> basketProductsIdList, DiscountDTO discountDTO)
        {
            int butterCount = basketProductsIdList.Count(x => x == (int)ProductEnum.Butter);
            int breadCount = basketProductsIdList.Count(x => x == (int)ProductEnum.Bread);
            ProductDTO bread = allBasketProducts.Where(x => x.Id == (int)ProductEnum.Bread).FirstOrDefault();

            // for every 2 butters, do something
            for (int i = 2; i <= butterCount; i += 2)
            {
                if (breadCount > 0)
                {
                    decimal amountToBeDiscounted = bread.Price * breadDiscount;

                    // fill DiscountItemDTOList for logging
                    discountDTO.DiscountItemDTOList.Add(new DiscountItemDTO(bread.Name, amountToBeDiscounted));

                    breadCount--;
                }
            }
        }

        private static void CalculateMilkDiscount(List<int> basketProductsIdList, DiscountDTO discountDTO)
        {
            int milkCount = basketProductsIdList.Count(x => x == (int)ProductEnum.Milk);
            ProductDTO milk = allBasketProducts.Where(x => x.Id == (int)ProductEnum.Milk).FirstOrDefault();

            // for every 4 milks, do something
            for (int i = 4; i <= milkCount; i += 4)
            {
                decimal amountToBeDiscounted = milk.Price * milkDiscount;

                // fill DiscountItemDTOList for logging
                discountDTO.DiscountItemDTOList.Add(new DiscountItemDTO(milk.Name, amountToBeDiscounted));
            }
        }
    }
}
