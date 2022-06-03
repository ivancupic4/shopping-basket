using ShoppingBasket.DAL.Models;
using ShoppingBasket.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasket.Domain.Builders
{
    public static class ProductDTOBuilder
    {
        public static List<ProductDTO> MapProductsToDTOList(List<Product> productList)
        {
            var productDTOList = new List<ProductDTO>();
            foreach (var product in productList)
            {
                var productDTO = MapProductToDTO(product);
                productDTOList.Add(productDTO);
            }
            return productDTOList;
        }

        public static ProductDTO MapProductToDTO(Product product)
        {
            return new ProductDTO()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}
