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

        public BasketDTO AddProduct(ProductInsertDTO productInsertDTO)
        {
            if (productInsertDTO.ProductId > 0)
            {
                var newProductDTO = GetProductById(productInsertDTO.ProductId);
                for (int i = 0; i < productInsertDTO.Amount; i++)
                {
                    productInsertDTO.CurrentBasketProducts.Add(newProductDTO);
                }
            }

            decimal totalSum = productInsertDTO.CurrentBasketProducts.Select(x => x.Price).Sum();
            var productIdList = productInsertDTO.CurrentBasketProducts.Select(x => x.Id).ToList();
            var discountDTO = _discountService.CalculateDiscount(productIdList);

            var basketDTO = new BasketDTO()
            {
                CurrentBasketProducts = productInsertDTO.CurrentBasketProducts,
                DiscountDTO = discountDTO,
                TotalCost = totalSum - discountDTO.TotalDiscount
            };
            _logService.LogBasketDetails(basketDTO);

            return basketDTO;
        }

        public ProductDTO GetProductById(int productId)
        {
            var product = _productRepository.LoadProductById(productId);
            return ProductDTOBuilder.MapProductToDTO(product);
        }

        public List<ProductDTO> GetProductsByIdList(List<int> productIdList)
        {
            var allProductsList = _productRepository.LoadProducts(productIdList);
            return ProductDTOBuilder.MapProductsToDTOList(allProductsList);
        }
    }
}
