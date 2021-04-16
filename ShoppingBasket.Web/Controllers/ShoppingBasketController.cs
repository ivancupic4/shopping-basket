using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingBasket.DTO;

namespace ShoppingBasket.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingBasketController : ControllerBase
    {
        private readonly IShoppingBasketService _shoppingBasketService;

        public ShoppingBasketController(IShoppingBasketService shoppingBasketService)
        {
            _shoppingBasketService = shoppingBasketService;
        }

        [HttpGet]
        public List<ProductDTO> Get()
        {
            List<int> productIdList = new List<int> { 1, 2, 3 };

            List<ProductDTO> productDTOList = _shoppingBasketService.LoadProductsByIdList(productIdList);
            return productDTOList;
        }
    }
}
