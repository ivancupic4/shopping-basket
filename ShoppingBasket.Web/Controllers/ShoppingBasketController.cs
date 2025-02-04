using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        [Route("getProduct/{productId:int}")]
        public ProductDTO GetProduct(int productId)
        {
            return _shoppingBasketService.GetProductById(productId);
        }

        [HttpGet]
        [Route("getProductList")]
        public List<ProductDTO> GetProductList([FromBody] List<int> productIdList)
        {
            return _shoppingBasketService.GetProductsByIds(productIdList);
        }

        [HttpPost]
        [Route("addProduct")]
        public void AddProduct([FromBody] ProductInsertDTO insertData)
        {
            _shoppingBasketService.AddProduct(insertData);
        }
    }
}
