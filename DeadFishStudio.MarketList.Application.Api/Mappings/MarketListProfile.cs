using AutoMapper;
using DeadFishStudio.InfnetDevOps.Shared.ViewModels.MarketListViewModels;

namespace DeadFishStudio.MarketList.Application.Api.Mappings
{
    public class MarketListProfile : Profile
    {
        public MarketListProfile()
        {
            CreateMap<Domain.Model.Entities.MarketList, MarketListViewModel>()
                .ForMember(
                    items => items.ItemViewModels,
                    s => s.MapFrom(src => src.Items))
                .ReverseMap();
        }
    }
}