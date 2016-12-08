using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.Initialize(a =>
            {
                a.AddProfile<TripViewModelProfile>();
                a.AddProfile<StopViewModelProfile>();
            });

        }
    }

    internal class TripViewModelProfile : Profile
    {
        public TripViewModelProfile()
        {
            base.CreateMap<TripViewModel, Trip>()
                .ForMember(dest => dest.DateCreated,
                    opts => opts.MapFrom(src => src.Created)).ReverseMap();
        }

    }

    internal class StopViewModelProfile : Profile
    {
        public StopViewModelProfile()
        {
            base.CreateMap<Stop, StopViewModel>().ReverseMap();
        }

    }
}
