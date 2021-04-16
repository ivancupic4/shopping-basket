using Newtonsoft.Json;
using ShoppingBasket.DAL.Models;
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
        public List<Product> LoadProducts()
        {
            List<Product> allProductList = new List<Product>();
            string dataSourceLocation = "../../../../data_source.txt";

            using (StreamReader r = new StreamReader(dataSourceLocation))
            {
                string json = r.ReadToEnd();
                allProductList = JsonConvert.DeserializeObject<List<Product>>(json);
            }

            return allProductList;
        }

        public Product LoadProductById(int id)
        {
            return LoadProducts().Where(x => x.Id == id).Single();
        }
    }
}
