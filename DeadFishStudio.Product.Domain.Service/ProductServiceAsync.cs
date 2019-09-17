using DeadFishStudio.Product.Domain.Model.Interfaces.Repositories;
using DeadFishStudio.Product.Domain.Model.Interfaces.Services;
using GianLuca.Domain.Core.Interfaces.Repository;

namespace DeadFishStudio.Product.Domain.Service
{
    public class ProductServiceAsync : BaseServiceAsync<Model.Entity.Product>, IProductServiceAsync
    {
        public ProductServiceAsync(IProductRepositoryAsync productRepositoryAsync) : base(productRepositoryAsync)
        {
        }
    }
}
