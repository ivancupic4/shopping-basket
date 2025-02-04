using Microsoft.Extensions.Options;
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

        public List<Product> LoadProducts(List<int> productIds = null)
        {
            var allProducts = new List<Product>();
            var selectedProducts = new List<Product>();

            string dataSourcePath = _webAppSettings.Value.DataSourcePath;
            using (StreamReader r = new StreamReader(dataSourcePath))
            {
                string json = r.ReadToEnd();
                allProducts = JsonConvert.DeserializeObject<List<Product>>(json);
            }

            if (productIds == null)
                return allProducts;

            foreach (var productId in productIds)
            {
                selectedProducts.Add(allProducts.Single(x => x.Id == productId));
            }
            return selectedProducts;
        }

        public Product LoadProductById(int id)
            => LoadProducts().Where(x => x.Id == id).Single();

        public List<ProductDiscountConditions> LoadProductDiscountConditions()
        {
            var discountConditions = new List<ProductDiscountConditions>();
            string productDiscountsPath = _webAppSettings.Value.ProductDiscountsPath;
            using (StreamReader r = new StreamReader(productDiscountsPath))
            {
                string json = r.ReadToEnd();
                discountConditions = JsonConvert.DeserializeObject<List<ProductDiscountConditions>>(json);
            }
            return discountConditions;
        }
    }
}
