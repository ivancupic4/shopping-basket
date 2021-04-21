using ShoppingBasket.DAL;
using ShoppingBasket.DAL.Models;
using ShoppingBasket.Domain.DTO;
using ShoppingBasket.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ShoppingBasket
{
    // TODO: needs refactoring to make it generic
    public class DiscountService : IDiscountService
    {
        private readonly IProductRepository _productRepository;

        // product discounts defined at one place, which can also be loaded from a database
        private static decimal breadDiscount = 0.5M;
        private static decimal milkDiscount = 1M;

        public DiscountService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public DiscountDTO CalculateDiscount(List<int> basketProductsIdList)
        {
            // DiscountDTO to be filled with data and returned
            var discountDTO = new DiscountDTO();

            CalculateBreadDiscount(basketProductsIdList, discountDTO);
            CalculateMilkDiscount(basketProductsIdList, discountDTO);

            return discountDTO;
        }

        private void CalculateBreadDiscount(List<int> basketProductsIdList, DiscountDTO discountDTO)
        {
            int butterCount = basketProductsIdList.Count(x => x == (int)ProductEnum.Butter);
            int breadCount = basketProductsIdList.Count(x => x == (int)ProductEnum.Bread);
            Product bread = _productRepository.LoadProductById((int)ProductEnum.Bread);

            // for every 2 butters, do something
            for (int i = 2; i <= butterCount; i += 2)
            {
                if (breadCount > 0)
                {
                    decimal amountToBeDiscounted = bread.Price * breadDiscount;

                    // fill DiscountItemDTOList for logging
                    DiscountItemDTO discountItemDTO = new DiscountItemDTO(bread.Name, amountToBeDiscounted);
                    discountDTO.DiscountItemDTOList.Add(discountItemDTO);

                    breadCount--;
                }
            }
        }

        private void CalculateMilkDiscount(List<int> basketProductsIdList, DiscountDTO discountDTO)
        {
            int milkCount = basketProductsIdList.Count(x => x == (int)ProductEnum.Milk);
            Product milk = _productRepository.LoadProductById((int)ProductEnum.Milk);

            // for every 4 milks, do something
            for (int i = 4; i <= milkCount; i += 4)
            {
                decimal amountToBeDiscounted = milk.Price * milkDiscount;

                // fill DiscountItemDTOList for logging
                DiscountItemDTO discountItemDTO = new DiscountItemDTO(milk.Name, amountToBeDiscounted);
                discountDTO.DiscountItemDTOList.Add(discountItemDTO);
            }
        }
    }
}
