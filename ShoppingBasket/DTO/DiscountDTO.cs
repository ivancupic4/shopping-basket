using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket.Domain.DTO
{
    public class DiscountDTO
    {
        public DiscountDTO()
        {
            this.DiscountItemDTOList = new List<DiscountItemDTO>();
        }

        public List<DiscountItemDTO> DiscountItemDTOList { get; set; }

        public decimal TotalDiscount
        {
            get
            {
                return DiscountItemDTOList.Select(x => x.Discount).Sum();
            }
        }
    }
}
