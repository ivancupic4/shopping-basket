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

        public DiscountDTO CalculateDiscount(List<int> productsIds)
        {
            var discount = new DiscountDTO();
            var discountConditions = _productRepository.LoadProductDiscountConditions();
            var discountedProducts = _productRepository.LoadProducts(discountConditions.Select(x => x.DiscountedProductId).ToList());

            foreach (var discountCondition in discountConditions)
            {
                int conditionProductCount = productsIds.Count(x => x == discountCondition.ConditionProductId);
                int discountedProductCount = productsIds.Count(x => x == discountCondition.DiscountedProductId);
                var discountedProduct = discountedProducts.First(x => x.Id == discountCondition.DiscountedProductId);

                int amountOfDiscounts = conditionProductCount / discountCondition.NumberOfProductsRequired;
                for (int i = 0; i < amountOfDiscounts; i++) 
                {
                    if (discountedProductCount > 0)
                    {
                        decimal amountToBeDiscounted = discountedProduct.Price * discountCondition.AmountToDiscount;
                        discount.Items.Add(new DiscountItemDTO(discountedProduct.Name, amountToBeDiscounted));
                        discountedProductCount--;
                    }
                }
            };

            return discount;
        }
    }
}
