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
    public class ShoppingBasketService
    {
        ProductRepository productRepository = new ProductRepository();

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

            BasketDTO basketDTO = new BasketDTO(currentBasketProducts);

            // TODO: here calculate Discount and Log, and NOT in BasketDTO

            return basketDTO;
        }

        private ProductDTO LoadProductById(int newProductId)
        {
            ProductDTOBuilder productDTOBuilder = new ProductDTOBuilder();

            List<Product> allProductList = productRepository.LoadProducts();
            List<ProductDTO> allProductDTOList = productDTOBuilder.MapProductsToDTOList(allProductList);

            return allProductDTOList.Where(x => x.Id == newProductId).SingleOrDefault();
        }

        public List<ProductDTO> LoadProductsByIdList(List<int> basketProductsIdList)
        {
            ProductDTOBuilder productDTOBuilder = new ProductDTOBuilder();

            List<Product> allProductList = productRepository.LoadProducts();
            List<ProductDTO> allProductDTOList = productDTOBuilder.MapProductsToDTOList(allProductList);

            return allProductDTOList.Where(x => basketProductsIdList.Contains(x.Id)).ToList();
        }
    }
}
