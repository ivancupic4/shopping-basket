using System.Collections.Generic;

namespace ShoppingBasket.DTO
{
    public class DiscountDTO
    {
        public DiscountDTO()
        {
            this.DiscountItemDTOList = new List<DiscountItemDTO>();
        }

        public List<DiscountItemDTO> DiscountItemDTOList { get; set; }
        public decimal TotalDiscount { get; set; }
    }
}
