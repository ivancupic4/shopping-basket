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
            ProductDTO newProduct = LoadProductById(productInsertDTO.ProductId);
            if (newProduct != null)
            {
                for (int i = 0; i < productInsertDTO.Amount; i++)
                {
                    productInsertDTO.CurrentBasketProducts.Add(newProduct);
                }
            }

            decimal totalSum = productInsertDTO.CurrentBasketProducts.Select(x => x.Price).Sum();
            var productIdList = productInsertDTO.CurrentBasketProducts.Select(x => x.Id).ToList();
            var discount = _discountService.CalculateDiscount(productIdList);

            BasketDTO basketDTO = new BasketDTO()
            {
                CurrentBasketProducts = productInsertDTO.CurrentBasketProducts,
                DiscountDTO = discount,
                TotalCost = totalSum - discount.TotalDiscount
            };

            _logService.LogBasketDetails(basketDTO);

            return basketDTO;
        }

        private ProductDTO LoadProductById(int newProductId)
        {
            ProductDTOBuilder productDTOBuilder = new ProductDTOBuilder();

            var allProductList = _productRepository.LoadProducts();
            var allProductDTOList = productDTOBuilder.MapProductsToDTOList(allProductList);

            return allProductDTOList.Where(x => x.Id == newProductId).SingleOrDefault();
        }

        public List<ProductDTO> LoadProductsByIdList(List<int> basketProductsIdList)
        {
            ProductDTOBuilder productDTOBuilder = new ProductDTOBuilder();

            var allProductList = _productRepository.LoadProducts();
            var allProductDTOList = productDTOBuilder.MapProductsToDTOList(allProductList);

            return allProductDTOList.Where(x => basketProductsIdList.Contains(x.Id)).ToList();
        }
    }
}
