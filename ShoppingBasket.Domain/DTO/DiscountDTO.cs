using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket.Domain.DTO
{
    public class DiscountDTO
    {
        public DiscountDTO()
        {
            this.Items = new List<DiscountItemDTO>();
        }

        public List<DiscountItemDTO> Items { get; set; }

        public decimal Total
        {
            get => Items.Select(x => x.Discount).Sum();
        }
    }
}
