using System;

namespace DeadFishStudio.MarketList.Domain.Model
{
    public class MarketListProduct
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
