using AutoMapper;
using DeadFishStudio.InfnetDevOps.Shared.ViewModels.ProductViewModels;

namespace DeadFishStudio.Product.Application.Api.Mappings
{
    public class MarketListProfile : Profile
    {
        public MarketListProfile()
        {
            CreateMap<Domain.Model.Entity.Product, ProductViewModel>()
                .ForMember(
                    price => price.Prices,
                    s => s.MapFrom(src => src.Prices))
                .ReverseMap();
        }
    }
}