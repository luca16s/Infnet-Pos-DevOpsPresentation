using System;

namespace DeadFishStudio.InfnetDevOps.Shared.ViewModels.MarketListViewModels
{
    public class MarketListProductViewModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
