using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GianLuca.Domain.Core.Interfaces.Repository;

namespace DeadFishStudio.Product.Domain.Model.Interfaces.Repositories
{
    public interface IProductRepositoryAsync : IBaseRepositoryAsync<Entity.Product>
    {
    }
}
