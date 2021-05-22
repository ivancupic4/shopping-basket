using ShoppingBasket.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasket.DAL
{
    public interface IProductRepository
    {
        List<Product> LoadProducts(List<int> productIdList = null);
        Product LoadProductById(int bread);
        List<ProductDiscountConditions> LoadProductDiscountConditions();
    }
}
