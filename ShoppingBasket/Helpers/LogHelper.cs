using ShoppingBasket.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingBasket.Helpers
{
    public static class LogHelper
    {
        /// <summary>
        /// Basket details logged to debug output window in Visual Studio
        /// Run selected tests as "debug" or with Ctrl+R, Ctrl+T
        /// </summary>
        /// <param name="currentBasketProducts">Products currently in the basket</param>
        /// <param name="discountDTO">An object containing information about applied discounts</param>
        /// <param name="totalCost">Calculated total cost of all products with applied discounts</param>
        public static void LogBasketDetails(List<ProductDTO> currentBasketProducts, DiscountDTO discountDTO, decimal totalCost)
        {
            System.Diagnostics.Debug.WriteLine("\n\nShopping basket \n");

            System.Diagnostics.Debug.WriteLine("Products purchased:");
            System.Diagnostics.Debug.WriteLine("Id" + "\t" + "Product" + "\t" + "Price");
            foreach (var productDTO in currentBasketProducts)
            {
                System.Diagnostics.Debug.WriteLine(productDTO.Id + "\t" + productDTO.Name + "\t" + productDTO.Price.ToString("F"));
            }

            decimal fullPrice = currentBasketProducts.Select(x => x.Price).Sum();
            System.Diagnostics.Debug.WriteLine("Full price: " + fullPrice.ToString("F") + "\n");

            System.Diagnostics.Debug.WriteLine("Discounts:");
            System.Diagnostics.Debug.WriteLine("Product" + "\t" + "Discount");
            foreach (var discountItemDTO in discountDTO.DiscountItemDTOList)
            {
                System.Diagnostics.Debug.WriteLine(discountItemDTO.Name + "\t" + discountItemDTO.Discount.ToString("F"));
            }

            System.Diagnostics.Debug.WriteLine("Total discount: " + discountDTO.TotalDiscount.ToString("F") + "\n");

            System.Diagnostics.Debug.WriteLine("Total cost: " + totalCost.ToString("F"));

            System.Diagnostics.Debug.WriteLine("\n\n");
        }
    }
}
