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
    public class DiscountService : IDiscountService
    {
        private readonly IProductRepository _productRepository;

        public DiscountService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public DiscountDTO CalculateDiscount(List<int> basketProductsIdList)
        {
            var discountDTO = new DiscountDTO();
            var productDiscountConditions = _productRepository.LoadProductDiscountConditions();
            var discountedProductList = _productRepository.LoadProducts(productDiscountConditions.Select(x => x.DiscountedProductId).ToList());

            foreach (var productDiscountCondition in productDiscountConditions)
            {
                int conditionProductCount = basketProductsIdList.Count(x => x == productDiscountCondition.ConditionProductId);
                int discountedProductCount = basketProductsIdList.Count(x => x == productDiscountCondition.DiscountedProductId);
                var discountedProduct = discountedProductList.First(x => x.Id == productDiscountCondition.DiscountedProductId);

                int amountOfDiscounts = conditionProductCount / productDiscountCondition.ConditionProductsRequired;

                for (int i = 0; i < amountOfDiscounts; i++) 
                {
                    if (discountedProductCount > 0)
                    {
                        decimal amountToBeDiscounted = discountedProduct.Price * productDiscountCondition.DiscountAmount;
                        discountDTO.DiscountItemDTOList.Add(new DiscountItemDTO(discountedProduct.Name, amountToBeDiscounted));

                        discountedProductCount--;
                    }
                }
            };

            return discountDTO;
        }
    }
}
