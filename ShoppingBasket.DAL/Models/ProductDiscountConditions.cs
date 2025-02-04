using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasket.DAL.Models
{
    public class ProductDiscountConditions
    {
        public int ConditionProductId { get; set; }
        public int NumberOfProductsRequired { get; set; }
        public int DiscountedProductId { get; set; }
        public decimal AmountToDiscount { get; set; }
    }
}
