using ShoppingBasket.DTO;
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
        /// <param name="currentBasketProducts">Products currently in the basket</param>
        /// <param name="discountDTO">An object containing information about applied discounts</param>
        /// <param name="totalCost">Calculated total cost of all products with applied discounts</param>
        public void LogBasketDetails(BasketDTO basketDTO)
        {
            System.Diagnostics.Debug.WriteLine("\n\nShopping basket \n");

            System.Diagnostics.Debug.WriteLine("Products purchased:");
            System.Diagnostics.Debug.WriteLine("Id" + "\t" + "Product" + "\t" + "Price");
            foreach (var productDTO in basketDTO.CurrentBasketProducts)
            {
                System.Diagnostics.Debug.WriteLine(productDTO.Id + "\t" + productDTO.Name + "\t" + productDTO.Price.ToString("F"));
            }
            if (basketDTO.CurrentBasketProducts.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("-\t-\t\t-");
            }

            decimal fullPrice = basketDTO.CurrentBasketProducts.Select(x => x.Price).Sum();
            System.Diagnostics.Debug.WriteLine("Full price: " + fullPrice.ToString("F") + "\n");

            System.Diagnostics.Debug.WriteLine("Discounts:");
            System.Diagnostics.Debug.WriteLine("Product" + "\t" + "Discount");
            foreach (var discountItemDTO in basketDTO.DiscountDTO.DiscountItemDTOList)
            {
                System.Diagnostics.Debug.WriteLine(discountItemDTO.Name + "\t" + discountItemDTO.Discount.ToString("F"));
            }
            if (basketDTO.DiscountDTO.DiscountItemDTOList.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("-\t\t-");
            }

            System.Diagnostics.Debug.WriteLine("Total discount: " + basketDTO.DiscountDTO.TotalDiscount.ToString("F") + "\n");

            System.Diagnostics.Debug.WriteLine("Total cost: " + basketDTO.TotalCost.ToString("F"));

            System.Diagnostics.Debug.WriteLine("\n\n");
        }
    }
}
