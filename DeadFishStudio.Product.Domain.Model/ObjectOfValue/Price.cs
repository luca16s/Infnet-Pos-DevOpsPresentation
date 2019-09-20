﻿using System;

namespace DeadFishStudio.Product.Domain.Model.ObjectOfValue
{
    public class Price
    {
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
    }
}