using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NUnit.Framework;
using ShoppingBasket.DAL;
using ShoppingBasket.DAL.Settings;
using ShoppingBasket.Domain.DTO;
using ShoppingBasket.Enums;
using System.Collections.Generic;

namespace ShoppingBasket.Test
{
    public class ShoppingBasketTest
    {
        private readonly IShoppingBasketService _shoppingBasketService;
        private readonly IDiscountService _discountService;

        public ShoppingBasketTest(/*IShoppingBasketService shoppingBasketService,
                                    IDiscountService discountService*/)
        {
            //services mocked for test
            IOptions<WebAppSettings> webAppSettings = Options.Create<WebAppSettings>(new WebAppSettings());
            webAppSettings.Value.DataSourcePath = "../../../../data_source.txt";
            webAppSettings.Value.ProductDiscountsPath = "../../../../products_discount.txt";
            IProductRepository productRepository = new ProductRepository(webAppSettings);
            IDiscountService discountService = new DiscountService(productRepository);
            ILogService logService = new LogService();

            _shoppingBasketService = new ShoppingBasketService(productRepository, discountService, logService);
            _discountService = new DiscountService(productRepository);
        }

        ProductDTO butter = new ProductDTO { Id = 1, Name = "Butter", Price = 0.8M };
        ProductDTO milk = new ProductDTO { Id = 2, Name = "Milk", Price = 1.15M };
        ProductDTO bread = new ProductDTO { Id = 3, Name = "Bread", Price = 1M };

        [Test]
        public void Test_GetProduct_Scenario1()
        {
            var expected = JsonConvert.SerializeObject(milk);
            var product = JsonConvert.SerializeObject(_shoppingBasketService.GetProductById(milk.Id));

            Assert.AreEqual(expected, product);
        }

        [Test]
        public void Test_GetProduct_Scenario2()
        {
            var productIds = new List<int> { butter.Id, milk.Id, bread.Id };
            var productList = JsonConvert.SerializeObject(_shoppingBasketService.GetProductsByIds(productIds));
            var expected = JsonConvert.SerializeObject(new List<ProductDTO> { butter, milk, bread });

            Assert.AreEqual(expected, productList);
        }

        [Test]
        public void Test_AddProduct_Scenario1()
        {
            var insertData = new ProductInsertDTO
            {
                CurrentBasketProducts = new List<ProductDTO>() { butter, milk, bread },
                ProductId = 0,
                Amount = 0
            };

            decimal expected = 2.95M;
            var basket = _shoppingBasketService.AddProduct(insertData);

            Assert.AreEqual(expected, basket.Cost);
        }

        [Test]
        public void Test_AddProduct_Scenario2()
        {
            var insertData = new ProductInsertDTO
            {
                CurrentBasketProducts = new List<ProductDTO>() { butter, butter, bread, bread },
                ProductId = 0,
                Amount = 0
            };

            decimal expected = 3.1M;
            var basket = _shoppingBasketService.AddProduct(insertData);

            Assert.AreEqual(expected, basket.Cost);
        }

        [Test]
        public void Test_AddProduct_Scenario3()
        {
            var insertData = new ProductInsertDTO
            {
                CurrentBasketProducts = new List<ProductDTO>() { milk, milk, milk },
                ProductId = milk.Id,
                Amount = 1
            };

            decimal expected = 3.45M;
            var basket = _shoppingBasketService.AddProduct(insertData);

            Assert.AreEqual(expected, basket.Cost);
        }

        [Test]
        public void Test_AddProduct_Scenario4()
        {
            var insertData = new ProductInsertDTO
            {
                CurrentBasketProducts = new List<ProductDTO>() { butter, butter, bread,
                                                                 milk, milk, milk, milk,
                                                                 milk, milk, milk, milk },
                ProductId = 0,
                Amount = 0
            };

            decimal expected = 9M;
            var basket = _shoppingBasketService.AddProduct(insertData);

            Assert.AreEqual(expected, basket.Cost);
        }

        [Test]
        public void Test_AddProduct_Scenario5()
        {
            var insertData = new ProductInsertDTO
            {
                CurrentBasketProducts = new List<ProductDTO>(),
                ProductId = 0,
                Amount = 0
            };

            decimal expected = 0M;
            var basket = _shoppingBasketService.AddProduct(insertData);

            Assert.AreEqual(expected, basket.Cost);
        }

        [Test]
        public void Test_AddProduct_Scenario6()
        {
            var insertData = new ProductInsertDTO
            {
                CurrentBasketProducts = new List<ProductDTO>(),
                ProductId = milk.Id,
                Amount = 1
            };

            decimal expected = 1.15M;
            var basket = _shoppingBasketService.AddProduct(insertData);

            Assert.AreEqual(expected, basket.Cost);
        }

        [Test]
        public void Test_CalculateDiscount_Scenario1()
        {
            var currentBasketProducts = new List<int>()
            {
                butter.Id, butter.Id, butter.Id,
                milk.Id,
                bread.Id, bread.Id
            };

            decimal expected = 0.5M;
            var discount = _discountService.CalculateDiscount(currentBasketProducts);

            Assert.AreEqual(expected, discount.Total);
        }

        [Test]
        public void Test_CalculateDiscount_Scenario2()
        {
            var currentBasketProducts = new List<int>()
            {
                butter.Id, butter.Id, butter.Id,
                milk.Id, milk.Id, milk.Id, milk.Id,
                bread.Id
            };

            decimal expected = 1.65M;
            var discount = _discountService.CalculateDiscount(currentBasketProducts);

            Assert.AreEqual(expected, discount.Total);
        }
    }
}
