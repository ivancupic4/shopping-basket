using Microsoft.Extensions.Options;
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
                                    IProductRepository productRepository,
                                    IDiscountService discountService,
                                    ILogService logService*/)
        {
            //TODO: implement dependency injection in this test class
            //services mocked for test
            IOptions<WebAppSettings> webAppSettings = Options.Create<WebAppSettings>(new WebAppSettings());
            webAppSettings.Value.DataSourceLocation = "../../../../data_source.txt";
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
        public void Test_AddProduct_Scenario1()
        {
            ProductInsertDTO productInsertDTO = new ProductInsertDTO
            {
                CurrentBasketProducts = new List<ProductDTO>() { butter, milk, bread },
                ProductId = 0,
                Amount = 0
            };

            decimal expected = 2.95M;

            BasketDTO basketDTO = _shoppingBasketService.AddProduct(productInsertDTO);

            Assert.AreEqual(expected, basketDTO.TotalCost);
        }

        [Test]
        public void Test_AddProduct_Scenario2()
        {
            ProductInsertDTO productInsertDTO = new ProductInsertDTO
            {
                CurrentBasketProducts = new List<ProductDTO>() { butter, butter, bread, bread },
                ProductId = 0,
                Amount = 0
            };

            decimal expected = 3.1M;

            BasketDTO basketDTO = _shoppingBasketService.AddProduct(productInsertDTO);

            Assert.AreEqual(expected, basketDTO.TotalCost);
        }

        [Test]
        public void Test_AddProduct_Scenario3()
        {
            ProductInsertDTO productInsertDTO = new ProductInsertDTO
            {
                CurrentBasketProducts = new List<ProductDTO>() { milk, milk, milk },
                ProductId = 2,
                Amount = 1
            };

            decimal expected = 3.45M;

            BasketDTO basketDTO = _shoppingBasketService.AddProduct(productInsertDTO);

            Assert.AreEqual(expected, basketDTO.TotalCost);
        }

        [Test]
        public void Test_AddProduct_Scenario4()
        {
            ProductInsertDTO productInsertDTO = new ProductInsertDTO
            {
                CurrentBasketProducts = new List<ProductDTO>() { butter, butter, bread,
                                                                 milk, milk, milk, milk,
                                                                 milk, milk, milk, milk },
                ProductId = 0,
                Amount = 0
            };

            decimal expected = 9M;

            BasketDTO basketDTO = _shoppingBasketService.AddProduct(productInsertDTO);

            Assert.AreEqual(expected, basketDTO.TotalCost);
        }

        [Test]
        public void Test_AddProduct_Scenario5()
        {
            ProductInsertDTO productInsertDTO = new ProductInsertDTO
            {
                CurrentBasketProducts = new List<ProductDTO>(),
                ProductId = 0,
                Amount = 0
            };

            decimal expected = 0M;

            BasketDTO basketDTO = _shoppingBasketService.AddProduct(productInsertDTO);

            Assert.AreEqual(expected, basketDTO.TotalCost);
        }

        [Test]
        public void Test_AddProduct_Scenario6()
        {
            ProductInsertDTO productInsertDTO = new ProductInsertDTO
            {
                CurrentBasketProducts = new List<ProductDTO>(),
                ProductId = 2,
                Amount = 1
            };

            decimal expected = 1.15M;

            BasketDTO basketDTO = _shoppingBasketService.AddProduct(productInsertDTO);

            Assert.AreEqual(expected, basketDTO.TotalCost);
        }

        [Test]
        public void Test_CalculateDiscount_Scenario1()
        {
            List<int> currentBasketProducts = new List<int>() 
            {
                butter.Id, butter.Id, butter.Id,
                milk.Id, 
                bread.Id, bread.Id
            };

            decimal expected = 0.5M;

            DiscountDTO discountDTO = _discountService.CalculateDiscount(currentBasketProducts);

            Assert.AreEqual(expected, discountDTO.TotalDiscount);
        }

        [Test]
        public void Test_CalculateDiscount_Scenario2()
        {
            List<int> currentBasketProducts = new List<int>() 
            {
                butter.Id, butter.Id, butter.Id,
                milk.Id, milk.Id, milk.Id, milk.Id,
                bread.Id
            };

            decimal expected = 1.65M;

            DiscountDTO discountDTO = _discountService.CalculateDiscount(currentBasketProducts);

            Assert.AreEqual(expected, discountDTO.TotalDiscount);
        }
    }
}
