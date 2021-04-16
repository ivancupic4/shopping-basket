using ShoppingBasket.DAL;
using ShoppingBasket.DAL.Models;
using ShoppingBasket.Domain.Builders;
using ShoppingBasket.DTO;
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

        public ShoppingBasketService()
        {

        }

        public ShoppingBasketService(IProductRepository productRepository,
                                        IDiscountService discountService,
                                        ILogService logService)
        {
            _productRepository = productRepository;
            _discountService = discountService;
            _logService = logService;
        }

        public BasketDTO AddProduct(List<ProductDTO> currentBasketProducts, int newProductId, int amount = 1)
        {
            ProductDTO newProduct = LoadProductById(newProductId);
            if (newProduct != null)
            {
                for (int i = 0; i < amount; i++)
                {
                    currentBasketProducts.Add(newProduct);
                }
            }

            decimal totalSum = currentBasketProducts.Select(x => x.Price).Sum();
            List<int> productIdList = currentBasketProducts.Select(x => x.Id).ToList();

            BasketDTO basketDTO = new BasketDTO();
            basketDTO.CurrentBasketProducts = currentBasketProducts;
            basketDTO.DiscountDTO = _discountService.CalculateDiscount(productIdList);
            basketDTO.TotalCost = totalSum - basketDTO.DiscountDTO.TotalDiscount;

            _logService.LogBasketDetails(basketDTO);

            return basketDTO;
        }

        private ProductDTO LoadProductById(int newProductId)
        {
            ProductDTOBuilder productDTOBuilder = new ProductDTOBuilder();

            List<Product> allProductList = _productRepository.LoadProducts();
            List<ProductDTO> allProductDTOList = productDTOBuilder.MapProductsToDTOList(allProductList);

            return allProductDTOList.Where(x => x.Id == newProductId).SingleOrDefault();
        }

        public List<ProductDTO> LoadProductsByIdList(List<int> basketProductsIdList)
        {
            ProductDTOBuilder productDTOBuilder = new ProductDTOBuilder();

            List<Product> allProductList = _productRepository.LoadProducts();
            List<ProductDTO> allProductDTOList = productDTOBuilder.MapProductsToDTOList(allProductList);

            return allProductDTOList.Where(x => basketProductsIdList.Contains(x.Id)).ToList();
        }
    }
}
