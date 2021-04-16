using NUnit.Framework;
using ShoppingBasket.DTO;
using ShoppingBasket.Enums;
using ShoppingBasket.Helpers;
using System;
using System.Collections.Generic;

namespace ShoppingBasket.Test
{
    public class ShoppingBasketTest
    {
        ProductDTO butter = new ProductDTO { Id = 1, Name = "Butter", Price = 0.8M };
        ProductDTO milk = new ProductDTO { Id = 2, Name = "Milk", Price = 1.15M };
        ProductDTO bread = new ProductDTO { Id = 3, Name = "Bread", Price = 1M };

        [Test]
        public void Test_AddProduct_Scenario1()
        {
            List<ProductDTO> currentBasketProducts = new List<ProductDTO>();
            currentBasketProducts.Add(butter);
            currentBasketProducts.Add(milk);
            currentBasketProducts.Add(bread);

            int newProductId = 0;
            decimal expected = 2.95M;

            ShoppingBasketService shoppingBasketService = new ShoppingBasketService();
            BasketDTO basketDTO = shoppingBasketService.AddProduct(currentBasketProducts, newProductId);

            Assert.AreEqual(expected, basketDTO.TotalCost);
        }

        [Test]
        public void Test_AddProduct_Scenario2()
        {
            List<ProductDTO> currentBasketProducts = new List<ProductDTO>();
            currentBasketProducts.Add(butter);
            currentBasketProducts.Add(butter);
            currentBasketProducts.Add(bread);
            currentBasketProducts.Add(bread);

            int newProductId = 0;
            decimal expected = 3.1M;

            ShoppingBasketService shoppingBasketService = new ShoppingBasketService();
            BasketDTO basketDTO = shoppingBasketService.AddProduct(currentBasketProducts, newProductId);

            Assert.AreEqual(expected, basketDTO.TotalCost);
        }

        [Test]
        public void Test_AddProduct_Scenario3()
        {
            List<ProductDTO> currentBasketProducts = new List<ProductDTO>();
            currentBasketProducts.Add(milk);
            currentBasketProducts.Add(milk);
            currentBasketProducts.Add(milk);
            // this line is commented out to try if adding milk as newProductId works
            //currentBasketProducts.Add(milk);

            int newProductId = 2;
            decimal expected = 3.45M;

            ShoppingBasketService shoppingBasketService = new ShoppingBasketService();
            BasketDTO basketDTO = shoppingBasketService.AddProduct(currentBasketProducts, newProductId);

            Assert.AreEqual(expected, basketDTO.TotalCost);
        }

        [Test]
        public void Test_AddProduct_Scenario4()
        {
            List<ProductDTO> currentBasketProducts = new List<ProductDTO>();
            currentBasketProducts.Add(butter);
            currentBasketProducts.Add(butter);
            currentBasketProducts.Add(bread);

            currentBasketProducts.Add(milk);
            currentBasketProducts.Add(milk);
            currentBasketProducts.Add(milk);
            currentBasketProducts.Add(milk);

            currentBasketProducts.Add(milk);
            currentBasketProducts.Add(milk);
            currentBasketProducts.Add(milk);
            currentBasketProducts.Add(milk);

            int newProductId = 0;
            decimal expected = 9M;

            ShoppingBasketService shoppingBasketService = new ShoppingBasketService();
            BasketDTO basketDTO = shoppingBasketService.AddProduct(currentBasketProducts, newProductId);

            Assert.AreEqual(expected, basketDTO.TotalCost);
        }

        [Test]
        public void Test_AddProduct_Scenario5()
        {
            List<ProductDTO> currentBasketProducts = new List<ProductDTO>();

            int newProductId = 0;
            decimal expected = 0M;

            ShoppingBasketService shoppingBasketService = new ShoppingBasketService();
            BasketDTO basketDTO = shoppingBasketService.AddProduct(currentBasketProducts, newProductId);

            Assert.AreEqual(expected, basketDTO.TotalCost);
        }

        [Test]
        public void Test_AddProduct_Scenario6()
        {
            List<ProductDTO> currentBasketProducts = new List<ProductDTO>();

            int newProductId = 2;
            decimal expected = 1.15M;

            ShoppingBasketService shoppingBasketService = new ShoppingBasketService();
            BasketDTO basketDTO = shoppingBasketService.AddProduct(currentBasketProducts, newProductId);

            Assert.AreEqual(expected, basketDTO.TotalCost);
        }

        [Test]
        public void Test_CalculateDiscount_Scenario1()
        {
            List<int> currentBasketProducts = new List<int>();
            currentBasketProducts.Add((int)ProductEnum.Butter);
            currentBasketProducts.Add((int)ProductEnum.Butter);
            currentBasketProducts.Add((int)ProductEnum.Butter);
            currentBasketProducts.Add((int)ProductEnum.Milk);
            currentBasketProducts.Add((int)ProductEnum.Bread);
            currentBasketProducts.Add((int)ProductEnum.Bread);

            decimal expected = 0.5M;

            DiscountHelper discountHelper = new DiscountHelper();
            DiscountDTO discountDTO = discountHelper.CalculateDiscount(currentBasketProducts);

            Assert.AreEqual(expected, discountDTO.TotalDiscount);
        }

        [Test]
        public void Test_CalculateDiscount_Scenario2()
        {
            List<int> currentBasketProducts = new List<int>();
            currentBasketProducts.Add((int)ProductEnum.Butter);
            currentBasketProducts.Add((int)ProductEnum.Butter);
            currentBasketProducts.Add((int)ProductEnum.Butter);
            currentBasketProducts.Add((int)ProductEnum.Butter);
            currentBasketProducts.Add((int)ProductEnum.Milk);
            currentBasketProducts.Add((int)ProductEnum.Milk);
            currentBasketProducts.Add((int)ProductEnum.Milk);
            currentBasketProducts.Add((int)ProductEnum.Milk);
            currentBasketProducts.Add((int)ProductEnum.Bread);

            decimal expected = 1.65M;

            DiscountHelper discountHelper = new DiscountHelper();
            DiscountDTO discountDTO = discountHelper.CalculateDiscount(currentBasketProducts);

            Assert.AreEqual(expected, discountDTO.TotalDiscount);
        }
    }
}
