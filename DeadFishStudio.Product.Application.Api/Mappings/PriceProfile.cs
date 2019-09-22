using AutoMapper;
using DeadFishStudio.InfnetDevOps.Shared.ViewModels.ProductViewModels;

namespace DeadFishStudio.Product.Application.Api.Mappings
{
    public class PriceProfile : Profile
    {
        public PriceProfile()
        {
            CreateMap<Domain.Model.ObjectOfValue.Price, PriceViewModel>()
                .ReverseMap();
        }
    }
}