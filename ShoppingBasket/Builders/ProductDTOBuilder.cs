using ShoppingBasket.DAL.Models;
using ShoppingBasket.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasket.Domain.Builders
{
    public class ProductDTOBuilder
    {
        public List<ProductDTO> MapProductsToDTOList(List<Product> productList)
        {
            List<ProductDTO> productDTOList = new List<ProductDTO>();

            foreach (var product in productList)
            {
                ProductDTO productDTO = MapProductToDTO(product);
                productDTOList.Add(productDTO);
            }

            return productDTOList;
        }

        public ProductDTO MapProductToDTO(Product product)
        {
            ProductDTO productDTO = new ProductDTO();
            productDTO.Id = product.Id;
            productDTO.Name = product.Name;
            productDTO.Price = product.Price;

            return productDTO;
        }
    }
}
