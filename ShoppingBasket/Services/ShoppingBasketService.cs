using Newtonsoft.Json;
using ShoppingBasket.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

namespace ShoppingBasket
{
    public static class ShoppingBasketService
    {
        public static BasketDTO AddProduct(List<ProductDTO> currentBasketProducts, int newProductId)
        {
            ProductDTO newProduct = LoadProductById(newProductId);
            if (newProduct != null) currentBasketProducts.Add(newProduct);

            return new BasketDTO(currentBasketProducts);
        }

        private static ProductDTO LoadProductById(int newProductId)
        {
            List<ProductDTO> allProductDTOList = LoadProducts();
            return allProductDTOList.Where(x => x.Id == newProductId).SingleOrDefault();
        }

        public static List<ProductDTO> LoadProductsByIdList(List<int> basketProductsIdList)
        {
            List<ProductDTO> allProductDTOList = LoadProducts();
            return allProductDTOList.Where(x => basketProductsIdList.Contains(x.Id)).ToList();
        }

        private static List<ProductDTO> LoadProducts()
        {
            List<ProductDTO> allProductDTOList = new List<ProductDTO>();

            using (StreamReader r = new StreamReader("../../../../data_source.txt"))
            {
                string json = r.ReadToEnd();
                allProductDTOList = JsonConvert.DeserializeObject<List<ProductDTO>>(json);
            }

            return allProductDTOList;
        }
    }
}
