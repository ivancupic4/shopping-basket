using ShoppingBasket.DAL;
using ShoppingBasket.DAL.Models;
using ShoppingBasket.Domain.Builders;
using ShoppingBasket.Domain.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

namespace ShoppingBasket
{
    public class ShoppingBasketService : IShoppingBasketService
    {
        private readonly IProductRepository _productRepository;
        private readonly IDiscountService _discountService;
        private readonly ILogService _logService;

        public ShoppingBasketService(IProductRepository productRepository,
                                        IDiscountService discountService,
                                        ILogService logService)
        {
            _productRepository = productRepository;
            _discountService = discountService;
            _logService = logService;
        }

        public BasketDTO AddProduct(ProductInsertDTO insertData)
        {
            if (insertData.ProductId > 0)
            {
                var newProduct = GetProductById(insertData.ProductId);
                for (int i = 0; i < insertData  .Amount; i++)
                {
                    insertData.CurrentBasketProducts.Add(newProduct);
                }
            }

            decimal totalSum = insertData.CurrentBasketProducts.Select(x => x.Price).Sum();
            var basketProductIds = insertData.CurrentBasketProducts.Select(x => x.Id).ToList();
            var discount = _discountService.CalculateDiscount(basketProductIds);

            var basket = new BasketDTO()
            {
                Products = insertData.CurrentBasketProducts,
                Discount = discount,
                Cost = totalSum - discount.Total
            };
            _logService.LogBasketDetails(basket);

            return basket;
        }

        public ProductDTO GetProductById(int productId)
        {
            var product = _productRepository.LoadProductById(productId);
            return ProductDTOBuilder.MapProductToDTO(product);
        }

        public List<ProductDTO> GetProductsByIds(List<int> productIds)
        {
            var allProducts = _productRepository.LoadProducts(productIds);
            return ProductDTOBuilder.MapProductsToDTOList(allProducts);
        }
    }
}
