using ShoppingBasket.DAL.Models;
using ShoppingBasket.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasket.Domain.Builders
{
    public class ProductDTOBuilder
    {
        public List<ProductDTO> MapProductsToDTOList(List<Product> productList)
        {
            var productDTOList = new List<ProductDTO>();

            foreach (var product in productList)
            {
                var productDTO = MapProductToDTO(product);
                productDTOList.Add(productDTO);
            }

            return productDTOList;
        }

        public ProductDTO MapProductToDTO(Product product)
        {
            var productDTO = new ProductDTO()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };

            return productDTO;
        }
    }
}
