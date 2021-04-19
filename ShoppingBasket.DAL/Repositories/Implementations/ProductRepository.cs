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

            if (productIdList != null)
            {
                foreach (var productId in productIdList)
                {
                    selectedProductsList.Add(allProductsList.Single(x => x.Id == productId));
                }

                return selectedProductsList;
            }

            return allProductsList;
        }

        public Product LoadProductById(int id)
        {
            var product = LoadProducts().Where(x => x.Id == id).Single();
            return product;
        }
    }
}
