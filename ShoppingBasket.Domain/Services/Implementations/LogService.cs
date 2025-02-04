using ShoppingBasket.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingBasket
{
    public class LogService : ILogService
    {
        public LogService()
        {

        }

        /// <summary> 
        /// Basket details logged to debug output window in Visual Studio
        /// Run selected tests in debug mode (right click on a test -> Debug) to print to console window
        /// </summary>
        /// <param name="basket">Basket containing current basket products and discount information</param>
        public void LogBasketDetails(BasketDTO basket)
        {
            System.Diagnostics.Debug.WriteLine("\n\nShopping basket \n");

            System.Diagnostics.Debug.WriteLine("Products purchased:");
            System.Diagnostics.Debug.WriteLine("Id" + "\t" + "Product" + "\t" + "Price");
            foreach (var product in basket.Products)
            {
                System.Diagnostics.Debug.WriteLine(product.Id + "\t" + product.Name + "\t" + product.Price.ToString("F"));
            }
            if (basket.Products.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("-\t-\t\t-");
            }

            decimal fullPrice = basket.Products.Select(x => x.Price).Sum();
            System.Diagnostics.Debug.WriteLine("Full price: " + fullPrice.ToString("F") + "\n");

            System.Diagnostics.Debug.WriteLine("Discounts:");
            System.Diagnostics.Debug.WriteLine("Product" + "\t" + "Discount");
            foreach (var discountedItem in basket.Discount.Items)
            {
                System.Diagnostics.Debug.WriteLine(discountedItem.Name + "\t" + discountedItem.Discount.ToString("F"));
            }
            if (basket.Discount.Items.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("-\t\t-");
            }

            System.Diagnostics.Debug.WriteLine("Total discount: " + basket.Discount.Total.ToString("F") + "\n");
            System.Diagnostics.Debug.WriteLine("Total cost: " + basket.Cost.ToString("F"));
            System.Diagnostics.Debug.WriteLine("\n\n");
        }
    }
}
