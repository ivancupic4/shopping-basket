﻿using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ShoppingBasket.DAL.Models;
using ShoppingBasket.DAL.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasket.DAL
{
    public class ProductRepository : IProductRepository
    {
        private readonly IOptions<WebAppSettings> _webAppSettings;

        public ProductRepository(IOptions<WebAppSettings> webAppSettings)
        {
            _webAppSettings = webAppSettings;
        }

        public List<Product> LoadProducts(List<int> productIdList = null)
        {
            var allProductsList = new List<Product>();
            var selectedProductsList = new List<Product>();

            string dataSourceLocation = _webAppSettings.Value.DataSourceLocation;
            using (StreamReader r = new StreamReader(dataSourceLocation))
            {
                string json = r.ReadToEnd();
                allProductsList = JsonConvert.DeserializeObject<List<Product>>(json);
            }

            if (productIdList == null)
                return allProductsList;

            foreach (var productId in productIdList)
            {
                selectedProductsList.Add(allProductsList.Single(x => x.Id == productId));
            }
            return selectedProductsList;
        }

        public Product LoadProductById(int id)
            => LoadProducts().Where(x => x.Id == id).Single();

        public List<ProductDiscountConditions> LoadProductDiscountConditions()
        {
            var productDiscountConditions = new List<ProductDiscountConditions>();
            string productsDiscountLocation = _webAppSettings.Value.ProductsDiscountLocation;
            using (StreamReader r = new StreamReader(productsDiscountLocation))
            {
                string json = r.ReadToEnd();
                productDiscountConditions = JsonConvert.DeserializeObject<List<ProductDiscountConditions>>(json);
            }
            return productDiscountConditions;
        }
    }
}
