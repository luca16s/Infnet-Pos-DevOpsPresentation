using System;
using System.Collections.Generic;
using DeadFishStudio.Product.Domain.Model.ObjectOfValue;
using GianLuca.Domain.Core.Entity;

namespace DeadFishStudio.Product.Domain.Model.Entity
{
    public class Product : BaseEntity
    {
        //Constructor for EFCore
        public Product()
        {
        }

        public Product(Guid idGuid)
            : base(idGuid)
        {

        }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public virtual List<Price> Prices { get; set; }
    }
}