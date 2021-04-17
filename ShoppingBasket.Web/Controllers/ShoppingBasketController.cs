using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingBasket.Domain.DTO;

namespace ShoppingBasket.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingBasketController : ControllerBase
    {
        private readonly IShoppingBasketService _shoppingBasketService;

        public ShoppingBasketController(IShoppingBasketService shoppingBasketService)
        {
            _shoppingBasketService = shoppingBasketService;
        }

        [HttpGet]
        [Route("getProductList")]
        public List<ProductDTO> GetProductList([FromBody] List<int> productIdList)
        {
            var productDTOList = _shoppingBasketService.LoadProductsByIdList(productIdList);
            return productDTOList;
        }

        [HttpPost]
        [Route("addProduct")]
        public void AddProduct([FromBody] ProductInsertDTO productInsertDTO)
        {
            _shoppingBasketService.AddProduct(productInsertDTO);
        }
    }
}
