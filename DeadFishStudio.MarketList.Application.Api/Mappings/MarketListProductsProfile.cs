using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DeadFishStudio.InfnetDevOps.Shared.ViewModels.MarketListViewModels;
using DeadFishStudio.MarketList.Domain.Model;

namespace DeadFishStudio.MarketList.Application.Api.Mappings
{
    public class MarketListProductsProfile : Profile
    {
        public MarketListProductsProfile()
        {
            CreateMap<MarketListProduct, MarketListProductViewModel>()
                .ReverseMap();
        }
    }
}
