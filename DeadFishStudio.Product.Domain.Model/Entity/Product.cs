using System.Collections.Generic;
using DeadFishStudio.Product.Domain.Model.ObjectOfValue;
using GianLuca.Domain.Core.Entity;

namespace DeadFishStudio.Product.Domain.Model.Entity
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public virtual List<Price> Prices { get; set; }
    }
}
