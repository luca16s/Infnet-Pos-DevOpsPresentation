using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeadFishStudio.Product.Domain.Model.Interfaces.Repositories;
using DeadFishStudio.Product.Infrastructure.Data.Context;

namespace DeadFishStudio.Product.Infrastructure.Data.Repositories
{
    public class ProductRepositoryAsync : BaseRepositoryAsync<Domain.Model.Entity.Product>, IProductRepositoryAsync
    {
        public ProductRepositoryAsync(ProductContext productContext) : base(productContext)
        {
        }
    }
}
